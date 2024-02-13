using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class button_index : MonoBehaviour
{
    public string level_name;
    public bool level_name_to_be_picked = true;

    public bool pushable = false;
    public bool current = false;
    public bool chosen = false;

    public bool is_one_way;
    public bool is_two_way;
    public bool is_three_way;
    public bool is_path_end;

    bool icon_spawned = false;

    public GameObject this_button;

    public Sprite_Keeper sprite_keeper;

    public bool child_buttons_defined;

    public List<GameObject> button_children = new List<GameObject>(); //делать этот лист публичным, если что-то будет не так с наследованием

    public int children_ammount = 0;
    public int button_ind;
    void Start()
    {
        if (!child_buttons_defined)
        button_ind = pause_menu.IncreaceButtonIndex(button_ind);

        if (level_name_to_be_picked)
        {
            level_name = PickRandomLevel();
        }
        

        //gameObject.GetComponentInChildren<Text>().text = level_name;

        if (!icon_spawned)
        {
            if (level_name[0] == 'R')
                Instantiate(sprite_keeper.path_sprites[0], sprite_keeper.transform);
            else if (button_ind != 1)
                Instantiate(sprite_keeper.path_sprites[1], sprite_keeper.transform);
            //sprite_keeper.sprite_spawned = true;
            icon_spawned = true;
        }

        Invoke("FindChildButtons", 0.4f);
    }

    public void FindChildButtons()
    {
        if (button_ind == 1)
        {
            this.chosen = true;
            this.current = true;
            Path_mngr.AttachNewRoot(this_button);
        }

        if (this.current)
        {
            //делает цвет выбранной кнопки зелёным
            ChangeColorButton(new Color(0, 255, 0), this_button);

            Transform[] transform_button_children = this_button.transform.GetComponentsInChildren<Transform>();

            int child_number = 0;
            foreach (Transform child in transform_button_children)
            {
                if (child.gameObject.tag == "path_button" && child.gameObject != this_button && child.transform.parent == this_button.transform/* && !child_buttons_defined*/)
                {
                    if (child_number < children_ammount)
                    {
                        button_children.Add(child.gameObject);
                        child_number++;
                    }
                }
            }

            foreach (GameObject child_button in button_children)
            {
                ChangeColorButton(Color.yellow, child_button);
                child_button.GetComponent<button_index>().pushable = true;
            }

            /*
            if (child_number > 0)
            {
                int children_accessed = 0;
                do
                {
                    ChangeColorButton(Color.yellow, button_children[children_accessed]);
                    button_children[children_accessed].GetComponent<button_index>().pushable = true;
                    children_accessed++;
                } while (children_accessed < child_number);
            }
             */
        }
    }

    public void ChangeColorButton(Color new_color, GameObject button)
    {
        ColorBlock cb = button.GetComponent<Button>().colors;
        cb.normalColor = new_color;
        cb.highlightedColor = new_color;
        cb.pressedColor = new_color;
        cb.selectedColor = new_color;
        button.GetComponent<Button>().colors = cb;
    }

    public void PathChoice()
    {
        if (this.pushable)
        {
            //MentalCaller.CallMentals(MentalCaller.CallMentalType.PathPointPassed);
            if (is_path_end)
            {
                pause_menu.day_finished = true;
            }
            // сначала код находит все кнопки на поле и делает их белыми, если они не были выбрани ранее
            GameObject[] all_buttons = GameObject.FindGameObjectsWithTag("path_button");

            foreach (GameObject button in all_buttons)
            {
                button.GetComponent<button_index>().current = false;
                button.GetComponent<button_index>().pushable = false;
            }

            this.current = true;
            this.chosen = true;


            foreach (GameObject button in all_buttons)
            {
                button_index this_but_ID_script = button.GetComponent<button_index>();
                Button this_but_button_script = button.GetComponent<Button>();

                if (!this_but_ID_script.chosen)
                {
                    ChangeColorButton(Color.gray, button);
                }
            }

            FindChildButtons();
            //journey_mngr.AttachNewDayPath(journey_mngr.root);
            GameObject pause_UI = GameObject.FindGameObjectWithTag("pause_menu");
            pause_UI.GetComponent<pause_menu>().CloseAllUI();
            MentalCaller.CallMentals(MentalCaller.CallMentalType.PathPointPassed);
            pause_menu.path_point_passed = false;
            SceneManager.LoadScene(level_name);
        }

        //этот код активируется при нажатии доступного узла пути
        //выбирает новый узел пути
        //делает его нынешним

    }

    string PickRandomLevel()
    {
        if (is_path_end && StoryProgression.day_count == 4) //cracker first appearance
        {
            return "1.56 - First Cracker";
        }

        else if (is_path_end && StoryProgression.day_count == 6) //first bossfight
        {
            return "1.57 - Boss 1";
        }

            List<string> levels = new List<string>() {"1.5 - Sand Pillars", "1.6 - Sky Whales", "1.7 - Broken Tower", "1.39 - Horner Chest", "1.36 - Rake", "1.37 - Killer Rake", "1.44 - Oasis",
            "1.50 - Sniper", "1.41 - Knee Buster", "1.8 - Ballcrab Nest", "1.47 - Mystery Fruit", "1.52 - Chest 1", "1.53 - Chest 2", "1.54 - Chest 3", "1.38 - Friendly Ballcrab", "1.55 - Gartek Statue",
            "1.9 - Whalers", "1.15 -Village",  "1.31 - Altar Sandstorm", "1.24 - Stranger (Thief)", "1.25 - Stranger (Killer)", "1.26 - Stranger (Healer)", "1.32 - Harest", "RPG2", "R1.1 Cavern",
            "R1.3 Pillars", "R1.7 Crabball Hut", "R1.10 Bridges","R1.11 Lungs","R1.17 Separation", "R1.24 - Ring", "R1.31 - Tight Cave", "R1.29 - Three Ballcrabs", "R1.21 - Wait",
            "R1.4 - Hanged Man", "R1.23 - Crossed Tunnels"/*"1.16 - Ballcrab Pit", "1.51 - Training Poligon",*/ };

        if (StoryProgression.day_count >= 4)
        {
            levels.Add("R1.8 Hut Ambush");
            levels.Add("R1.13 Ambush");
            levels.Add("R1.18 Stuck");
            levels.Add("R1.19 Cacoon");
        }


        if (StoryProgression.day_count >= 5) //crackers added
        {
            levels.Add("R1.12 - Backup");
            levels.Add("R1.6 - Patrol");
            levels.Add("R1.26 - Animal Keeper");
            levels.Add("R1.32 - Pet");
            levels.Add("R1.30 - Bad Situation");
            levels.Add("R1.28 - Tight Place");
            levels.Add("R1.2 - Cracker Cave");
            levels.Add("R1.14 - Fault");
        }


        /*string[] level_names = new string[] { "RPG2", "R1.1 Cavern", "R1.3 Pillars", "R1.7 Crabball Hut", "R1.8 Hut Ambush",
            "R1.10 Bridges", "R1.11 Lungs", "R1.13 Ambush", "R1.17 Separation", "R1.18 Stuck", "R1.19 Cacoon", "1.14  -  Sniper Nest", "1.5 - Sand Pillars", "1.6 - Sky Whales",
            "1.9 - Whalers", "1.15 -Village", "1.31 - Altar Sandstorm", "1.24 - Stranger (Thief)", "1.25 - Stranger (Killer)", "1.26 - Stranger (Healer)"};*/
        return levels[Random.Range(0, levels.Count)];
    }

}
