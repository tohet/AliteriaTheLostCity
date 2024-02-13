using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hero : MonoBehaviour
{
    [SerializeField] private Transform groundCheckTransform = null;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private float tutnSmooth = 0.1f;
    [SerializeField] private float turnSmoothVelocity;
    [SerializeField] private Transform cameraTransform;

    float input_movement_x;
    float input_movement_z;
	Vector3 direction;
    Vector3 moveDir;

    public int speed_multiplier = 4;

	public bool jumping_key_is_pressed;
    public bool forward_is_down;
    public bool is_in_air = false;
    bool activate_is_down;
    Rigidbody rigidbodyComponent;

    public GameObject spear;

    public pause_menu pause_Menu;
    public inventoryObj player_inventory;
    public inventoryObj player_equipment;
    public inventoryObj mental_inventory;

	void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();
        player_inventory = GameObject.FindGameObjectWithTag("inventory_holder").GetComponent<InventoryHolder>().player_inventory;
        player_equipment = GameObject.FindGameObjectWithTag("inventory_holder").GetComponent<InventoryHolder>().player_equipment;
        mental_inventory = GameObject.FindGameObjectWithTag("inventory_holder").GetComponent<InventoryHolder>().mental_inventory;
    }


    void Update()
    {
        if (!StatHolder.dialogue_window_called)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                jumping_key_is_pressed = true;
                StartCoroutine(WaitJump());
                is_in_air = true;
            }

            input_movement_x = Input.GetAxis("Horizontal");
            input_movement_z = Input.GetAxis("Vertical");
            direction = new Vector3(input_movement_x * 3, rigidbodyComponent.velocity.y, input_movement_z * 3);

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                forward_is_down = true;
                if (direction.magnitude >= 0.1f)
                {
                    float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
                    float smoothedAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, tutnSmooth);
                    transform.rotation = Quaternion.Euler(0f, smoothedAngle, 0f);
                    moveDir = (Quaternion.Euler(input_movement_x, targetAngle, input_movement_z) * Vector3.forward) * speed_multiplier;
                }
            }

            if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
                forward_is_down = false;

        }
        else
            forward_is_down = false;

        if (gameObject.transform.position.y <= -50)
        {
            gameObject.transform.position = new Vector3(0f, 4.5f, 0f);
        }

    }
	private void FixedUpdate()

	{
        if (forward_is_down)
        {
            rigidbodyComponent.velocity = new Vector3(moveDir.x, rigidbodyComponent.velocity.y, moveDir.z);

        }
        else
            rigidbodyComponent.velocity = new Vector3(0f, rigidbodyComponent.velocity.y, 0f);

        //Мы обращаемся к Empty, создаём вокруг него проверяющую сферу радиусом 0,1, обращаемся к длине массива объектов, к которым 
        //прикасается эта сфера. Если объектов нет (==0), то это значит, что капсула наоходится в воздухе.
        //Поэтому мы выходим из метода физики через return.
        //ПРИМЕЧАНИЕ: Empty прикасается к капсуле, потэтому нужно ставить значение 1

        is_in_air = false;
        if (Physics.OverlapSphere(groundCheckTransform.position, 0.2f, playerMask).Length == 0)
        {
            is_in_air = true;
            return;
        }

        if (jumping_key_is_pressed)
        {
            is_in_air = true;
            rigidbodyComponent.AddForce(Vector3.up * 5, ForceMode.VelocityChange);
            jumping_key_is_pressed = false;

        }


    }

	private void OnTriggerEnter(Collider other)
	{
        var item = other.GetComponent<ItemHolder>();

        if (item)
        {
            Item _item = new Item(item.held_item);

            if (_item.object_mental_act != null)
            {
                if (mental_inventory.AddItem(_item, 1))
                {
                    GameObject.Destroy(other.gameObject);
                }
            }

            else
            {
                if (player_inventory.AddItem(_item, 1))
                {
                    GameObject.Destroy(other.gameObject);
                }
            }


        }
	}
	private void OnTriggerStay(Collider other)
	{
        if (other.gameObject.layer == 9 && Input.GetMouseButtonDown(0))
        {
            other.gameObject.GetComponent<Renderer>().material.color = Color.yellow;
            string switch_name = other.gameObject.name;
			switch (switch_name)
			{
                case "Switch":
                    SceneManager.LoadScene("1.57 - Boss 1");
                    break;
                default:
                    Debug.Log("Switch not found");
                    break;
			}
		}
	}

    IEnumerator WaitJump()
    {
        yield return new WaitForSeconds(0.5f);

    }

    public void UpdateEquipment()
    {
        foreach (InventorySlot slot in player_equipment.GetSlots)
        {
            if (slot.allowed_items[0] == ItemType.Spear)
            {
                if (slot.item.id >= 0)
                    spear.SetActive(true);
                else
                    spear.SetActive(false);
            }
        }
    }
}
