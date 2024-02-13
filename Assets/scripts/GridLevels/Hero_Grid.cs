using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Hero_Grid : Grid_Move
{
    public TextMeshProUGUI ap_display; 
    bool game_paused = pause_menu.game_paused;
    void Start()
    {
        Initialize();
    }

    void Update()
    {
        /*
        game_paused = pause_menu.game_paused;
        Debug.DrawRay(transform.position, transform.forward);

        if (!turn)
        {
            return;
        }

        if (!isMoving)
        {
            FindSelectableTiles();

            if (!game_paused)
                CheckMouse();
        }
        else
        {
            Move();
        }
        */
    }

    protected override void Move()
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
                move--;
                //ACTION POINTS DECREASE 
            }
        }

        else if (!turnManager.round_over) //удалить, как получится убрать моргание path map!!!!!
        {
            //we stop the movement
            RemoveSelectableTiles();
            isMoving = false;
            move++;
            //StartCoroutine(WaitAfterAttack());

            //помечено для изменения - должно проверять, есть ли AP

            if (move <= 0)
            {
                turnManager.EndTurn();
            }

            else if (!isMoving)
            {
                FindSelectableTiles();

                if (!game_paused)
                    CheckMouse();
            }
            //!!!
            //IT ENDS WHEN THE PLAYER STOPS MOVING
            //NEED TO IMPLY ACTION SYSTEM
            //TO BE CHANGED
            //!!!
        }

    }
    void CheckMouse()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Tiles")
                {
                    TileScript t = hit.collider.GetComponent<TileScript>();
                    if (t.selectableTile)
                    {
                        MoveToTile(t);
                    }
                }
            }
        }
    }
}
