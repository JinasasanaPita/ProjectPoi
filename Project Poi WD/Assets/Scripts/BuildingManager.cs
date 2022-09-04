using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    Village village;
    public GameObject lumberCampPrefab;
    public GameObject farmPrefab;
    public GameObject millPrefab;
    public GameObject miningCampPrefab;

    public string building;
    public bool isInBuildMode;

    void Start()
    {
        isInBuildMode = false;
    }

    void Update()
    {
        village = GetComponent<Village>();
    }    

    public void CreateLumberCamp()
    {
        if (village.wood >= 500)
        {
            isInBuildMode = true;
            building = "Lumber Camp";
            Instantiate(lumberCampPrefab);
            village.wood -= 500;
        }
        else
            Debug.Log("Lumber Camp: Not enough wood");
    }

    public void CreateFarm()
    {
        if (village.wood >= 500)
        {
            isInBuildMode = true;
            building = "Farm";
            Instantiate(farmPrefab);
            village.wood -= 500;
        }
        else
            Debug.Log("Farm: Not enough wood");
    }

    public void CreateMill()
    {
        if (village.wood >= 500)
        {
            isInBuildMode = true;
            building = "Mill";
            Instantiate(millPrefab);
            village.wood -= 500;
        }
        else
            Debug.Log("Mill: Not enough wood");
    }

    public void CreateMiningCamp()
    {
        if (village.wood >= 500)
        {
            isInBuildMode = true;
            building = "Mining Camp";
            Instantiate(miningCampPrefab);
            village.wood -= 500;
        }
        else
            Debug.Log("Mining Camp: Not enough wood");
    }
}
