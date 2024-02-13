using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn_path_code : MonoBehaviour
{
    public bool second_spawnpoint;

    public int colum_index;
    public int spawn_index;
    // 1 = one-way
    // 2 = two-way
    // 3 = three-way
    private path_temlates templates;
    public bool path_spawned = false;
    public GameObject new_path;

    public bool spawns_path = true;

	private void Start()
	{
        templates = GameObject.FindGameObjectWithTag("path_template").GetComponent<path_temlates>();
        if (path_temlates.ammount_of_spawns < 10)
        {
            if (spawns_path)
            {
                Invoke("Spawn", 0.1f);
                colum_index = Path_mngr.journey_part_colum;
                Path_mngr.journey_part_colum++;
                path_temlates.ammount_of_spawns++;
            }

        }

    }

    void Spawn()
    {
        if (!path_spawned)
        {
            if (path_temlates.ammount_of_spawns > 5) //слшжность от 3 до 5 - с 6 начинает сходить с ума
            {
                spawn_index = templates.path_types.Length - 1;
            }
            /*
             0 == two-way
             1 == one-way
             3 == path-end
             */
            else if (path_temlates.ammount_of_spawns < 3)
            {
                if (path_temlates.three_ways == 1) //если появляется одна тройная развилка, то код перестаёт позволять появляться ещё тройным развилкам - а то слишком много
                {
                    spawn_index = Random.Range(0, templates.path_types.Length - 3);
                }

                else
                {
                    spawn_index = Random.Range(0, templates.path_types.Length - 2);
                }
                
                if (spawn_index == 2)
                {
                    path_temlates.three_ways++;
                }
            }

            else
            {
                    if (path_temlates.three_ways == 1) //если появляется одна тройная развилка, то код перестаёт позволять появляться ещё тройным развилкам - а то слишком много
                    {
                        spawn_index = Random.Range(0, templates.path_types.Length - 3);
                    }

                    else
                    {
                        spawn_index = Random.Range(0, templates.path_types.Length - 2);
                    }


            }
            if (spawn_index == 0)
            {
                path_temlates.two_ways++;

                if (path_temlates.two_ways > 1)
                {
                    spawn_index = 1;
                }
            }
            // Здесь создаётся новая кнопка (узел пути) под именем new_path
            new_path = Instantiate(templates.path_types[spawn_index], transform.position, templates.path_types[spawn_index].transform.rotation);

            if (new_path.GetComponent<button_index>().is_two_way)
            {
                pause_menu.forked_paths++;
            }

            // Здесь новая кнопка объявляется как потомок спавнпоинта, поэтому она становится на правильное место по отношению к нынешней кнопке - рядом с её хвостом 
            new_path.transform.SetParent(GameObject.FindGameObjectWithTag("path_layout").transform, false);

            // Эта строчка присваивает новую кнопку как потомка нынешней кнопки

            //new_path.transform.SetParent(GameObject.FindGameObjectWithTag("path_button").transform, true);

            
            //создаёт массив из всех кнопок на поле
            GameObject[] previous_buttons = GameObject.FindGameObjectsWithTag("path_button");

            foreach (GameObject button in previous_buttons)
            {
                int parent_candidate_ID = button.GetComponent<button_index>().button_ind;
                int current_button_ID = pause_menu.path_index;

                //сравнивает каждую кнопку с индексом предыдущих кнопок
                //если индекс меньше текущего высшего значения на 1, то это предок 

                /*
                if (button.GetComponent<button_index>().is_two_way && (parent_candidate_ID > current_button_ID - 3))
                {
                    new_path.transform.SetParent(button.transform, true);
                    break;
                }
                */
                if (parent_candidate_ID + 2 >= current_button_ID)
                {
                    if (button.GetComponent<button_index>().is_one_way && button.GetComponent<button_index>().children_ammount < 1)
                    {
                        new_path.transform.SetParent(button.transform, true);
                        button.GetComponent<button_index>().children_ammount++;
                        break;
                    }

                    if (button.GetComponent<button_index>().is_two_way && button.GetComponent<button_index>().children_ammount < 2)
                    {
                        new_path.transform.SetParent(button.transform, true);
                        button.GetComponent<button_index>().children_ammount++;
                        break;
                    }


                }
                
            }

            path_spawned = true;
        }

    }

    /*void ApplyParentChildRelationships() // Надо найти родителя этого спавнпоитна (кнопку) и присвоить ему кнопки, которые от него появляются
    {
        if (this.colum_index != 0)
        {
            GameObject[] buttons = GameObject.FindGameObjectsWithTag("path_button");

            foreach (GameObject button in buttons)
            {
                if (
            }
        }
        //Находим родителя
        Transform this_spawn_button = this.transform.parent;
        this_spawn_button.
    }*/



    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("path_spawnpoint"))
        {
            Destroy(this);
        }
    }
}
