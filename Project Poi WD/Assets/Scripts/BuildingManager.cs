using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public GameObject lumberCampPrefab;
    public GameObject farmPrefab;
    public GameObject miningCampPrefab;

    public string building;
    public bool isInBuildMode;

    // Start is called before the first frame update
    void Start()
    {
        isInBuildMode = false;
    }

    // Update is called once per frame
    void Update()
    {

    }    

    public void CreateLumberCamp()
    {
        isInBuildMode = true;
        building = "Lumber Camp";
        Instantiate(lumberCampPrefab);
    }

    public void CreateFarm()
    {
        isInBuildMode = true;
        building = "Farm";
        Instantiate (farmPrefab);
    }

    public void CreateMiningCamp()
    {
        isInBuildMode = true;
        building = "Mining Camp";
        Instantiate(miningCampPrefab);
    }
}
