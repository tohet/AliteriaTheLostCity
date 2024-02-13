using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid_Move : MonoBehaviour
{
	public TurnManager turnManager;
	public bool turn = false;
	public bool modeIsAttack = false;
	public bool unit_is_dead = false;

	public bool moved_last_turn = false;
	public bool was_attacked_before_turn = false;

	List<TileScript> selectableTiles = new List<TileScript>();
	GameObject[] tiles;

	protected Stack<TileScript> path = new Stack<TileScript>();
	public TileScript currentTile;

	public bool isMoving = false;
	public int max_move = 0;
	public int move = 0;
	public float jumpHeight = 1;
	public float moveSpeed = 2;
	public float jumpVelocity = 4.5f;

	protected Vector3 velocity = new Vector3();
	protected Vector3 heading = new Vector3();

	protected float halfHeight = 0;

	public bool fallingDown = false;
	public bool jumpingUp = false;
	bool movingToEdge = false;

	Vector3 jumpTarget;

	public TileScript actualTargetTile;
	protected void Initialize()
	{
		tiles = GameObject.FindGameObjectsWithTag("Tiles");

		halfHeight = GetComponent<Collider>().bounds.extents.y;

		turnManager.AddUnit(this);
	}

	public void GetCurrentTile()
	{
		currentTile = GetTargetTile(gameObject);
		currentTile.entityOnCurrentTile = true;
	}

	public TileScript GetTargetTile(GameObject target)
	{
		RaycastHit hit;
		TileScript tile = null;
		if (Physics.Raycast(target.transform.position, -Vector3.up, out hit, 1))
		{
			tile = hit.collider.GetComponent<TileScript>();
		}
		return tile;
	}

	
	public void ComputeAdjacencyList(float jumpHeight, TileScript target) //This method gives a path of tiles
	{
		//tiles = GameObject.FindGameObjectsWithTag("Tiles");
		foreach (GameObject tile in tiles)
		{
			TileScript t = tile.GetComponent<TileScript>();
			t.FindNeighbours(jumpHeight, target);
		}
	}

	public void FindSelectableTiles()
	{
		ComputeAdjacencyList(jumpHeight, null);
		GetCurrentTile();

		Queue<TileScript> process = new Queue<TileScript>();
		process.Enqueue(currentTile);
		currentTile.tileIsVisited = true;

		while (process.Count > 0)
		{
			TileScript t = process.Dequeue();
			selectableTiles.Add(t);
			t.selectableTile = true;

			if (t.distance < move)
			{
				foreach (TileScript tile in t.adjacencyList)
				{
					if (!tile.tileIsVisited)
					{
						tile.parentTile = t;
						tile.tileIsVisited = true;
						tile.distance = 1 + t.distance;
						process.Enqueue(tile);
					}
				}
			}

		}
	}

	protected TileScript FindLowestF(List<TileScript> list)
	{
		TileScript lowest = list[0];

		foreach (TileScript t in list)
		{
			if (t.f_cost < lowest.f_cost)
			{
				lowest = t;
			}
		}

		list.Remove(lowest);
		return lowest;
	}

	protected TileScript FindEndTile(TileScript t)
	{
		// Finds Path
		Stack<TileScript> tempPath = new Stack<TileScript>();

		TileScript next = t.parentTile;

		while (next != null)
		{
			tempPath.Push(next);
			next = next.parentTile;
		}

		if (tempPath.Count <= move)
		{
			return t.parentTile;
		}

		TileScript endTile = null;

		for (int i = 0; i <= move; i++)
		{
			endTile = tempPath.Pop();
		}

		return endTile;
	}

	public void FindPath(TileScript target) //Finding path to the target
	{
		ComputeAdjacencyList(jumpHeight, target);
		GetCurrentTile();

		List<TileScript> openList = new List<TileScript>();
		List<TileScript> closedList = new List<TileScript>();
		openList.Add(currentTile);
		currentTile.h_costProcessTileToDestiny = Vector3.Distance(currentTile.transform.position, target.transform.position);
		currentTile.f_cost = currentTile.h_costProcessTileToDestiny;

		while (openList.Count > 0)
		{
			TileScript t = FindLowestF(openList);
			closedList.Add(t);

			if (t == target)
			{
				actualTargetTile = FindEndTile(t);
				MoveToTile(actualTargetTile);
				return;
			}

			foreach (TileScript tile in t.adjacencyList)
			{
				if (closedList.Contains(tile))
				{
					//Do nothing, already processed
				}

				else if (openList.Contains(tile))
				{
					float tempG = t.g_costParentToTile + Vector3.Distance(tile.transform.position, t.transform.position);

					// See it it's the faster way 
					if (tempG < t.g_costParentToTile)
					{
						tile.parentTile = t;

						tile.g_costParentToTile = tempG;
						tile.f_cost = tile.g_costParentToTile + tile.h_costProcessTileToDestiny;
					}
				}

				else
				{
					// Process the tile if it's never been processed before
					tile.parentTile = t;

					tile.g_costParentToTile = t.g_costParentToTile + Vector3.Distance(transform.position, t.transform.position);
					tile.h_costProcessTileToDestiny = Vector3.Distance(tile.transform.position, target.transform.position);
					tile.f_cost = tile.g_costParentToTile + tile.h_costProcessTileToDestiny;

					openList.Add(tile);
				}
			}
		}

		Debug.Log("Path not found");
		EndTurn();
		turnManager.EndTurn();

		//Watch from 17:00 

	}
	public void MoveToTile(TileScript tile)
	{
		path.Clear();
		tile.targetOfMovement = true;
		isMoving = true;
		moved_last_turn = true;
		TileScript nextTile = tile;
		while (nextTile != null)
		{
			path.Push(nextTile);
			nextTile = nextTile.parentTile;
		}
	}

	protected virtual void Move()
	{
		if (path.Count > 0 && !turnManager.round_over)
		{
			//there are tiles to move to
			TileScript t = path.Peek();
			Vector3 target = t.transform.position;

			//we measure the tile, on which the player is standing
			target.y += halfHeight + t.GetComponent<Collider>().bounds.extents.y;

			if (Vector3.Distance(transform.position, target) >= 0.05f)
			{
				bool jump_is_needed = transform.position.y != target.y;
				if (jump_is_needed)
				{
					Jump(target);
				}
				else
				{
					CalculateHeading(target);
					SetHorizontalVelocity();
				}

				//Locomotion
				transform.forward = heading;
				transform.position += velocity * Time.deltaTime;
			}
			else
			{
				//Tile senter reached
				transform.position = target;
				path.Pop();
				//ACTION POINTS DECREASE 
			}
		}
		
		else if (!turnManager.round_over) //удалить, как получится убрать моргание path map!!!!!
		{
			//we stop the movement
			RemoveSelectableTiles();
			isMoving = false;
			StartCoroutine(WaitAfterAttack());

			//помечено для изменения - должно проверять, есть ли AP

			turnManager.EndTurn();

			//!!!
			//IT ENDS WHEN THE PLAYER STOPS MOVING
			//NEED TO IMPLY ACTION SYSTEM
			//TO BE CHANGED
			//!!!
		}
		
	}

	protected void CalculateHeading(Vector3 target)
	{
		heading = target - transform.position;
		heading.Normalize();
	}

	protected void RemoveSelectableTiles()
	{
		if (currentTile != null)
		{
			currentTile.entityOnCurrentTile = false;
			currentTile = null;
		}
		foreach (TileScript tile in selectableTiles)
		{
			tile.TilesReset();
		}
		selectableTiles.Clear();
	}

	protected void SetHorizontalVelocity()
	{
		velocity = heading * moveSpeed;
	}

	protected void Jump(Vector3 target)
	{
		if (fallingDown)
		{
			FallDown(target);
		}
		else if (jumpingUp)
		{
			JumpUp(target);
		}
		else if (movingToEdge)
		{
			MoveToEdge();
		}
		else
		{
			PrepareJump(target);
		}
	}

	void PrepareJump(Vector3 target)
	{
		float targetY = target.y;
		target.y = transform.position.y;
		CalculateHeading(target);

		if (transform.position.y > targetY)
		{
			fallingDown = false;
			jumpingUp = false;
			movingToEdge = true;

			jumpTarget = transform.position + (target - transform.position) / 2f;
		}

		else
		{
			fallingDown = false;
			jumpingUp = true;
			movingToEdge = false;

			velocity = heading * moveSpeed / 3f;
			float difference = targetY - transform.position.y;
			velocity.y = jumpVelocity * (0.5f + difference / 2f);
		}
	}

	void FallDown(Vector3 target)
	{
		velocity += Physics.gravity * Time.deltaTime;

		if (transform.position.y <= target.y)
		{
			fallingDown = false;
			jumpingUp = false;
			movingToEdge = false;

			Vector3 position = transform.position;
			position.y = target.y;
			transform.position = position;

			velocity = new Vector3();
		}
	}

	void JumpUp(Vector3 target)
	{
		velocity += Physics.gravity * Time.deltaTime;

		if (transform.position.y > target.y)
		{
			jumpingUp = false;
			fallingDown = true;

		}
	}

	void MoveToEdge()
	{
		if (Vector3.Distance(transform.position, jumpTarget) >= 0.05f)
		{
			SetHorizontalVelocity();
		}

		else
		{
			movingToEdge = false;
			fallingDown = true;

			velocity /= 5f;
			velocity.y = 1.5f;
		}
	}

	public void BeginTurn()
	{
		StartCoroutine(WaitAfterAttack());
		turn = true;
		MentalCaller.CallMentals(MentalCaller.CallMentalType.TurnStart);
	}

	public void EndTurn()
	{
		was_attacked_before_turn = false;
		StartCoroutine(WaitAfterAttack());

	}

	protected IEnumerator WaitAfterAttack()
	{
		modeIsAttack = false;
		//помечено для изменения - должно проверять, есть ли AP

		turn = false;

		yield return new WaitForSeconds(10);
	}

}
