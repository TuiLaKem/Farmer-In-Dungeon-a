using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TManage : MonoBehaviour
{
    public Tile[] carr = new Tile[3];
    public Tile[] pota = new Tile[3];
    public Tile[] caba = new Tile[3];
    public Tilemap cropTile;


    public GameObject carrot;
    public GameObject potato;
    public GameObject cabage;
    public class Plant
    {
        public string name;
        public int growStage;

        public Vector3Int cell;



        public Plant(string n, int gS, Vector3Int pos)
        {
            name = n;
            growStage = gS;
            cell = pos;

        }
    }

    public List<Plant> currplan { get; set; } = new List<Plant>();



    public bool addGrow(Plant plant)
    {
        if (plant.growStage < 2)
        {
            ++plant.growStage;
            return true;
        }
        else 
        {
            
            cropTile.SetTile(plant.cell, null);
            if (plant.name == "Cabage_seed")
            {
                Instantiate(cabage, cropTile.GetCellCenterWorld(plant.cell), Quaternion.identity);
            }
            if (plant.name == "Carrot_seed")
            {
                Instantiate(carrot, cropTile.GetCellCenterWorld(plant.cell), Quaternion.identity);
            }
            if (plant.name == "Potato_seed")
            {
                Instantiate(potato, cropTile.GetCellCenterWorld(plant.cell), Quaternion.identity);
            }
            return false;
        }
    }

    public void updateTile(Plant plant)
    {


        if (plant.name == "Cabage_seed")
        {
            cropTile.SetTile(plant.cell, caba[plant.growStage]);
        }
        if (plant.name == "Carrot_seed")
        {
            cropTile.SetTile(plant.cell, carr[plant.growStage]);
        }
        if (plant.name == "Potato_seed")
        {
            cropTile.SetTile(plant.cell, pota[plant.growStage]);
        }
    }
    public void nextDay()
    {
        //print("============NEXT==================");
        foreach (Plant plant in currplan)
        {
            //print(plant.name + " " + plant.growStage);
            if (addGrow(plant))
            {
                updateTile(plant);
            }
            else 
            {
                //currplan.Remove(plant);
            }
            
            
        }

        //for (int i = 0; i < currplan.Count; i++) 
        //{
        //    Plant temp = currplan[i];
        //    addGrow(temp);
        //    updateTile(temp);

        //    currplan[i] = temp;
        //}
    }

    public void addCrop(Vector3Int cropPos, string crop)
    {
        if (crop == "Cabage_seed")
        {
            currplan.Add(new Plant(crop, -1, cropPos));
        }
        if (crop == "Carrot_seed")
        {
            currplan.Add(new Plant(crop, -1, cropPos));
        }
        if (crop == "Potato_seed")
        {
            currplan.Add(new Plant(crop, -1, cropPos));
        }
    }

    public void printList()
    {
        foreach (Plant plant in currplan)
        {
            //print(plant.name );

        }
    }
}
