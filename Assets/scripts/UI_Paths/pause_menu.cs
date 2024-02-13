using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class pause_menu : MonoBehaviour
{
    public static int path_index = 1;
    public static int forked_paths = 0;
    public static bool game_paused = false;

    public static bool day_finished = false;

    public GameObject pause_menu_UI;
    public GameObject level_progrssion;
    public GameObject inventory;

    public GameObject chestFiller;
    public GameObject level_up_screen;
    public GameObject spawned_chest_filler;
    public List<GameObject> spawned_chest_fillers = new List<GameObject>();

    public GameObject use_buttons_screen;
    public GameObject use_buttons_UI;


    public GameObject empty_for_day_path;
    public GameObject saved_path;

    public GameObject dialogue_manager;

    public Button day_finish_button;

    public bool is_reset_day;

    public static bool path_point_passed = false;

    public bool forbid_pause_menu_call = false;
    //public bool forbid_path_gen_call = false;
    public bool forbid_inventory_call = false;

    bool saved_path_spawned = false;

	private void Awake()
	{
        if (Path_mngr.pause_menu_instantiated)
        {
            GameObject.Destroy(this.gameObject);
        }
        SceneManager.sceneUnloaded += OnSceneUnloaded;
        SceneManager.sceneLoaded += OnSceneLoaded;
        /*
        if (!journey_mngr.chhest_instantiated)
        {
            journey_mngr.chhest_instantiated = true;
        }
        */
        if (dialogue_manager == null)
        {
            dialogue_manager = GameObject.FindGameObjectWithTag("dialogue_menu");
        }

    }

	private void Start()
	{
        use_buttons_screen.SetActive(true);

        Path_mngr.pause_menu_instantiated = true;
        GameObject.DontDestroyOnLoad(this.gameObject);
        saved_path = Path_mngr.day_path;
        //inventory.SetActive(true);
        //Invoke("SetInventoryInactive", 0.01f);
    }

    void OnSceneUnloaded(Scene current)
    {
        forbid_inventory_call = false;
        forbid_pause_menu_call = false;
        game_paused = false;
        //Destroy(spawned_chest_filler.gameObject);
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        forbid_inventory_call = false;
        forbid_pause_menu_call = false;
        if (Game_Mode_Definer.Gamemode_Explore())
        {
            if (!Path_mngr.chhest_instantiated)
            {
                spawned_chest_filler = Instantiate(chestFiller);
                Path_mngr.chhest_instantiated = true;
            }
            //ChestFiller chest_fill = spawned_chest_filler.GetComponent<ChestFiller>();

            spawned_chest_filler.GetComponent<ChestFiller>().chest_items_holder.inventory.Clear();
            spawned_chest_filler.GetComponent<ChestFiller>().chest_items_holder.InitializeSlots();
            spawned_chest_filler.GetComponent<ChestFiller>().FillChest(1);

            //spawned_chest_fillers.Add(spawned_chest_filler);
        }
            
        //use_buttons_screen.SetActive(true);
    }
    void Update()
    {
        SceneManager.sceneLoaded += SetDialogeWindowToTrue;

        if (Input.GetKeyDown(KeyCode.Escape) && !forbid_pause_menu_call)
        {
            if (game_paused)
            {
                Resume();
                forbid_inventory_call = false;
            }

            else
            {
                forbid_inventory_call = true;
                
                Pause();
            }
        }

        if (Input.GetKeyDown(KeyCode.Minus) || path_point_passed) 
        {
            if (game_paused)
            {
                forbid_pause_menu_call = false;
                forbid_inventory_call = false;
                level_progrssion.SetActive(false);
                Time.timeScale = 1f;
                game_paused = false;
            }
            
            else
            {
                //pause_menu_UI.SetActive(false);
                forbid_pause_menu_call = true;
                //inventory.SetActive(false);
                forbid_inventory_call = true;
                if (!Game_Mode_Definer.Gamemode_Explore())
                {
                    GameObject.FindGameObjectWithTag("turn_manager").GetComponent<TurnManager>().UpdateHealthAfterBattle();
                    StatHolder.IncreaceEXP(GameObject.FindGameObjectWithTag("turn_manager").GetComponent<TurnManager>().level_EXP);
                }
                CallPathMap();
            }
        }

        if (Input.GetKeyDown(KeyCode.I) && !forbid_inventory_call)
        {

            if (game_paused)
            {
                forbid_pause_menu_call = false;
                inventory.SetActive(false);
                use_buttons_UI.SetActive(true);
                Time.timeScale = 1f;
                game_paused = false;
            }

            else if (Game_Mode_Definer.Gamemode_Explore())
            {
                forbid_pause_menu_call = true;
                inventory.SetActive(true);
                use_buttons_UI.SetActive(false);
                /*
                DynamicInterface player_inventory = inventory.GetComponentInChildren<DynamicInterface>();
                player_inventory.UpdateSlots();
                
                if (player_inventory.mouse_item.temp_mouse_item_description != null)
                    Destroy(player_inventory.mouse_item.temp_mouse_item_description);

                StaticInterface player_equipment = inventory.GetComponentInChildren<StaticInterface>();
                player_equipment.UpdateSlots();
                if (player_equipment.mouse_item.temp_mouse_item_description != null)
                    Destroy(player_equipment.mouse_item.temp_mouse_item_description);

                MentalInterface player_mentals = inventory.GetComponentInChildren<MentalInterface>();
                player_mentals.UpdateSlots();
                if (player_mentals.mouse_item.temp_mouse_item_description)
                    Destroy(player_mentals.mouse_item.temp_mouse_item_description);
                */
                
                inventory.GetComponentInChildren<DynamicInterface>().UpdateSlots();
                inventory.GetComponentInChildren<StaticInterface>().UpdateSlots();
                inventory.GetComponentInChildren<MentalInterface>().UpdateSlots();
                
                Time.timeScale = 0f;
                game_paused = true;
            }
        }
    }



        public void Resume()
        {
            pause_menu_UI.SetActive(false);
            Time.timeScale = 1f;
            game_paused = false;
        }

        void Pause()
        {
            pause_menu_UI.SetActive(true);
            Time.timeScale = 0f;
            game_paused = true;
        }

        void Pause_PathChoose()
        {
        //if (!is_reset_day)
            Time.timeScale = 0f;
        //is_reset_day = false;
        }

        public void LoadMenu()
        {
        Time.timeScale = 1f;
        game_paused = false;
        SceneManager.LoadScene("intro");
        }

        public void QuitGame()
        {
            Debug.Log("Quitting game");
            Application.Quit();
        }

    public void LoadLevel()
    {
        Time.timeScale = 1f;
        game_paused = false;
        SceneManager.LoadScene("RPG2");
    }


    public static int IncreaceButtonIndex(int but_ind)
    {
        but_ind = path_index;
        path_index++;
        return but_ind;
    }

    public void CallPathMap()
    {
        if (StatHolder.level_up)
        {
            Instantiate(level_up_screen, transform);
            StatHolder.level_up = false;
        }
        level_progrssion.SetActive(true);

        if (day_finished)
        {
            day_finish_button.gameObject.SetActive(true);
            day_finished = false;
        }

        if (Path_mngr.day_path != null)
        {
            empty_for_day_path.SetActive(false);

            //path_temlates.AttachNewRoot(empty_for_day_path);

            if (!saved_path_spawned)
            {
                saved_path = Instantiate(Path_mngr.day_path, level_progrssion.transform);
                saved_path_spawned = true;
            }

            saved_path.SetActive(true);
        }

        else
        {
            empty_for_day_path.SetActive(true);
            Path_mngr.AttachNewDayPath(empty_for_day_path);
            saved_path = Path_mngr.day_path;
            //GameObject.DontDestroyOnLoad(saved_path);
            /*
            saved_path = empty_for_day_path;
            path_temlates.day_path = saved_path;
            */
        }



        pause_menu_UI.SetActive(false);
        Invoke("Pause_PathChoose", 0.7f);
        game_paused = true;
    }


    public void CloseAllUI()
    {
        level_progrssion.SetActive(false);
        pause_menu_UI.SetActive(false);
        Resume();
    }

    void SetDialogeWindowToTrue(Scene scene, LoadSceneMode mode)
    {
        dialogue_manager.SetActive(true);
    }

    public void ResetDay()
    {
        is_reset_day = true;
        path_temlates.ammount_of_spawns = 0;
        path_temlates.two_ways = 0;
        path_temlates.three_ways = 0;

        Destroy(saved_path);
        Destroy(empty_for_day_path);
        path_temlates temlates = GameObject.FindGameObjectWithTag("path_template").GetComponent<path_temlates>();

        empty_for_day_path = Instantiate(temlates.path_types[1], level_progrssion.transform);
        empty_for_day_path.GetComponent<RectTransform>().position = new Vector3(500, 470);
        path_index = 1;
        //CallPathMap();
        day_finish_button.gameObject.SetActive(false);

        level_progrssion.SetActive(false);
        //game_paused = false;
        SceneManager.LoadScene("DayEnd_Desert");
        Resume();
    }

    void SetInventoryInactive()
    {
        inventory.SetActive(false);
    }

}

