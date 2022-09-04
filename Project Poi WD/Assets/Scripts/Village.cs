using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Village : MonoBehaviour
{
    public int food;
    public int wood;
    public int gold;
    public int villager;

    public GameObject villagerPrefab;
    public Transform villagerSpawn;
    public TextMeshProUGUI foodDisplay;
    public TextMeshProUGUI woodDisplay;
    public TextMeshProUGUI goldDisplay;
    public TextMeshProUGUI villagerDisplay;
    private Vector3 v_villagerSpawn;

    public float villagerGatherRate;
    public int villagerCarryCapacity;

    void Start()
    {
        villagerCarryCapacity = 10;
        villagerGatherRate = 1;

        v_villagerSpawn = villagerSpawn.position;
    }

    void Update()
    {
        foodDisplay.text = food.ToString();
        woodDisplay.text = wood.ToString();
        goldDisplay.text = gold.ToString();

        villager = GameObject.FindGameObjectsWithTag("Villager").Length;
        villagerDisplay.text = villager.ToString();
    }

    public void SpawnWoodCutter()
    {
        GameObject[] resourceGameObjects = GameObject.FindGameObjectsWithTag("Woodline");
        GameObject[] collectionPointGameObjects = GameObject.FindGameObjectsWithTag("Lumber camp");
        if (food >= 100 && collectionPointGameObjects.Length > 0)
        {
            Villager villager = villagerPrefab.GetComponent<Villager>();
            villager.villagerType = "Wood Cutter";
            Instantiate(villagerPrefab, v_villagerSpawn, Quaternion.identity);
            food -= 100;
        }
        else
            Debug.Log("Woodcutter: Appropriate buildings not found");
    }

    public void SpawnWoodCutterFromSave()
    {
        Villager villager = villagerPrefab.GetComponent<Villager>();
        villager.villagerType = "Wood Cutter";
        Instantiate(villagerPrefab, v_villagerSpawn, Quaternion.identity);
    }

    public void SpawnFarmer()
    {
        GameObject[] resourceGameObjects = GameObject.FindGameObjectsWithTag("Farm");
        GameObject[] collectionPointGameObjects = GameObject.FindGameObjectsWithTag("Mill");
        if (food >= 100 && collectionPointGameObjects.Length > 0 && resourceGameObjects.Length > 0)
        {

            Villager villager = villagerPrefab.GetComponent<Villager>();
            villager.villagerType = "Farmer";
            Instantiate(villagerPrefab, v_villagerSpawn, Quaternion.identity);
            food -= 100;
        }
        else
            Debug.Log("Farmer: Appropriate buildings not found");
    }

    public void SpawnFarmerFromSave()
    {
        Villager villager = villagerPrefab.GetComponent<Villager>();
        villager.villagerType = "Farmer";
        Instantiate(villagerPrefab, v_villagerSpawn, Quaternion.identity);
    }

    public void SpawnMiner()
    {
        GameObject[] resourceGameObjects = GameObject.FindGameObjectsWithTag("Gold Mine");
        GameObject[] collectionPointGameObjects = GameObject.FindGameObjectsWithTag("Mining Camp");
        if (food >= 100 && collectionPointGameObjects.Length > 0 && resourceGameObjects.Length > 0)
        {
            Villager villager = villagerPrefab.GetComponent<Villager>();
            villager.villagerType = "Miner";
            Instantiate(villagerPrefab, v_villagerSpawn, Quaternion.identity);
            food -= 100;
        }
        else
            Debug.Log("Miner: Appropriate buildings not found");
    }

    public void SpawnMinerFromSave()
    {
        Villager villager = villagerPrefab.GetComponent<Villager>();
        villager.villagerType = "Miner";
        Instantiate(villagerPrefab, v_villagerSpawn, Quaternion.identity);
    }
}
