using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Villager : MonoBehaviour
{
    GameObject gameManager;
    UpgradeManager upgradeManager;
    Village village;

    public float f_currentCarryResources;
    public float f_carryCapacity;

    public string villagerType;
    public float gatherRate;
    public string villagerState;

    public Transform currentDestination;
    float speed = 2;
    Vector3[] path;
    int targetIndex;

    Transform resourcePoint;
    Transform collectionPoint;

    bool isAtCollectionPoint;
    bool isAtResourcePoint;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Game Manager");
        upgradeManager = gameManager.GetComponent<UpgradeManager>();
        village = gameManager.GetComponent<Village>();
        f_carryCapacity = village.villagerCarryCapacity;
        gatherRate = village.villagerGatherRate;
        villagerState = "goingToResourcePoint";

        SetDestinations();
        SetVillagerGoal();
        PathRequestManager.RequestPath(transform.position, currentDestination.position, OnPathFound);
    }

    public void OnPathFound(Vector3[] newPath, bool isPathFound)
    {
        Debug.Log("Path found");
        if (isPathFound)
        {
            path = newPath;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator FollowPath()
    {
        Vector3 currentWaypoint = path[0];
        while (true)
        {
            if (transform.position == currentWaypoint)
            {
                targetIndex++;
                if (targetIndex >= path.Length)
                {
                    targetIndex = 0;
                    path = new Vector3[0];
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }

            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed);
            yield return null;
        }
    }

    void Update()
    {
        SetDestinations();
        SetVillagerStats();

        if (villagerState == "goingToResourcePoint")
        {            
            if (currentDestination != resourcePoint)
            {
                SetVillagerGoal();
                PathRequestManager.RequestPath(transform.position, currentDestination.position, OnPathFound);
            }
            if (isAtResourcePoint)
            {
                Debug.Log("Switching villager state to gatheringResources");
                villagerState = "gatheringResource";
            }
        }
        else if (villagerState == "gatheringResource")
        {
            Debug.Log("Villager is gathering resources");
            if (f_currentCarryResources < f_carryCapacity)
                GatherResources();
            else
                villagerState = "goingToCollectionPoint";
        }
        else if (villagerState == "goingToCollectionPoint")
        {
            if (currentDestination != collectionPoint)
            {
                SetVillagerGoal();
                PathRequestManager.RequestPath(transform.position, currentDestination.position, OnPathFound);
            }
            if (isAtCollectionPoint)
            {
                DropOffResources();
                villagerState = "goingToResourcePoint";
            }
        }
    }

    private void SetVillagerStats()
    {
        if (villagerType == "Wood Cutter")
        {
            f_carryCapacity = 10 + upgradeManager.CalculateStockpillingEffect(upgradeManager.level_stockpilling);
            gatherRate = 1 + upgradeManager.CalculateLogFellingEffect(upgradeManager.level_logFelling);
        }
        else if (villagerType == "Farmer")
        {
            f_carryCapacity = 10 + upgradeManager.CalculateBountifulHarvestEffect(upgradeManager.level_bountifulharvest);
            gatherRate = 1 + (upgradeManager.CalculateCropRotationEffect(upgradeManager.level_cropRotation)
                + upgradeManager.CalculateFertilisedPasturesEffect(upgradeManager.level_fertilisedPastures));
        }
        else if (villagerType == "Miner")
        {
            f_carryCapacity = 10 + upgradeManager.CalculateMiningShaftEffect(upgradeManager.level_miningShaft);
            gatherRate = 1 + upgradeManager.CalculateWheelbarrowEffect(upgradeManager.level_wheelBarrow);
        }
    }

    private void SetVillagerGoal()
    {
        if (villagerState == "goingToResourcePoint")
            currentDestination = resourcePoint;
        else if (villagerState == "goingToCollectionPoint")
            currentDestination = collectionPoint;
    }

    private void GatherResources()
    {
        f_currentCarryResources += gatherRate * Time.deltaTime;
    }

    private void SetDestinations()
    {
        if (villagerType == "Wood Cutter")
        {
            GameObject[] resourceGameObjects = GameObject.FindGameObjectsWithTag("Woodline");
            GameObject[] collectionPointGameObjects = GameObject.FindGameObjectsWithTag("Lumber camp");

            if (resourceGameObjects.Length != 0)
                resourcePoint = resourceGameObjects[0].transform;
            if (collectionPointGameObjects.Length != 0)
                collectionPoint = collectionPointGameObjects[0].transform;
        }
        else if (villagerType == "Farmer")
        {
            GameObject[] resourceGameObjects = GameObject.FindGameObjectsWithTag("Farm");
            GameObject[] collectionPointGameObjects = GameObject.FindGameObjectsWithTag("Mill");

            if (resourceGameObjects.Length != 0)
                resourcePoint = resourceGameObjects[0].transform;
            if (collectionPointGameObjects.Length != 0)
                collectionPoint = collectionPointGameObjects[0].transform;
        }
        else if (villagerType == "Miner")
        {
            GameObject[] resourceGameObjects = GameObject.FindGameObjectsWithTag("Gold Mine");
            GameObject[] collectionPointGameObjects = GameObject.FindGameObjectsWithTag("Mining Camp");

            if (resourceGameObjects.Length != 0)
                resourcePoint = resourceGameObjects[0].transform;
            if (collectionPointGameObjects.Length != 0)
                collectionPoint = collectionPointGameObjects[0].transform;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        CheckIsAtResourcePoint(collider);       
    }

    private void CheckIsAtResourcePoint(Collider collider)
    {
        if (villagerType == "Wood Cutter")
        {
            if (collider.gameObject.tag == "Woodline")
            {
                Debug.Log("Villager has reached the resource point");
                isAtResourcePoint = true;
                isAtCollectionPoint = false;
            }
            else if (collider.gameObject.tag == "Lumber camp")
            {
                isAtResourcePoint = false;
                isAtCollectionPoint = true;
            }
            else
            {
                isAtResourcePoint = false;
                isAtCollectionPoint = false;
            }
        }
        else if (villagerType == "Farmer")
        {
            if (collider.gameObject.tag == "Farm")
            {
                Debug.Log("Villager has reached the resource point");
                isAtResourcePoint = true;
                isAtCollectionPoint = false;
            }
            else if (collider.gameObject.tag == "Mill")
            {
                isAtResourcePoint = false;
                isAtCollectionPoint = true;
            }
            else
            {
                isAtResourcePoint = false;
                isAtCollectionPoint = false;
            }
        }
        else
        {
            if (collider.gameObject.tag == "Gold Mine")
            {
                Debug.Log("Villager has reached the resource point");
                isAtResourcePoint = true;
                isAtCollectionPoint = false;
            }
            else if (collider.gameObject.tag == "Mining Camp")
            {
                isAtResourcePoint = false;
                isAtCollectionPoint = true;
            }
            else
            {
                isAtResourcePoint = false;
                isAtCollectionPoint = false;
            }
        }
    }

    private void DropOffResources()
    {
        if (villagerType == "Wood Cutter")
            village.wood += (int)f_currentCarryResources;
        else if (villagerType == "Farmer")
            village.food += (int)f_currentCarryResources;
        else if (villagerType == "Miner")
            village.gold += (int)f_currentCarryResources;

        f_currentCarryResources = 0;
    }
}