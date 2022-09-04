using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGameManager : MonoBehaviour
{
    Village village;
    UpgradeManager upgradeManager;

    public GameObject lumberCampPrefab;
    public GameObject farmPrefab;
    public GameObject millPrefab;
    public GameObject miningCampPrefab;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        village = GetComponent<Village>();
        upgradeManager = GetComponent<UpgradeManager>();
        LoadGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadGame()
    {
        village.wood = PlayerPrefs.GetInt("Wood");
        village.food = PlayerPrefs.GetInt("Food");
        village.gold = PlayerPrefs.GetInt("Gold");

        for (int i = 0; i < PlayerPrefs.GetInt("Wood Cutter"); i++)
        {
            village.SpawnWoodCutter();
        }
        for (int i = 0; i < PlayerPrefs.GetInt("Farmer"); i++)
        {
            village.SpawnFarmer();
        }
        for (int i = 0; i < PlayerPrefs.GetInt("Miner"); i++)
        {
            village.SpawnMiner();
        }

        for (int i = 0; i <= PlayerPrefs.GetInt("n_lumberCamps"); i++)
        {

            Instantiate(lumberCampPrefab, new Vector3(PlayerPrefs.GetFloat("lumberCamp" + i + ".x"), 
                PlayerPrefs.GetFloat("lumberCamp" + i + ".y"), 
                PlayerPrefs.GetFloat("lumberCamp" + i + ".z")), Quaternion.identity);
        }
        for (int i = 0; i <= PlayerPrefs.GetInt("n_farms"); i++)
        {

            Instantiate(lumberCampPrefab, new Vector3(PlayerPrefs.GetFloat("farms" + i + ".x"),
                PlayerPrefs.GetFloat("farms" + i + ".y"),
                PlayerPrefs.GetFloat("farms" + i + ".z")), Quaternion.identity);
        }
        for (int i = 0; i <= PlayerPrefs.GetInt("n_mills"); i++)
        {

            Instantiate(lumberCampPrefab, new Vector3(PlayerPrefs.GetFloat("mills" + i + ".x"),
                PlayerPrefs.GetFloat("mills" + i + ".y"),
                PlayerPrefs.GetFloat("mills" + i + ".z")), Quaternion.identity);
        }
        for (int i = 0; i <= PlayerPrefs.GetInt("n_miningCamps"); i++)
        {

            Instantiate(lumberCampPrefab, new Vector3(PlayerPrefs.GetFloat("miningCamps" + i + ".x"),
                PlayerPrefs.GetFloat("miningCamps" + i + ".y"),
                PlayerPrefs.GetFloat("miningCamps" + i + ".z")), Quaternion.identity);
        }

        Debug.Log("Game Loaded");
    }

    public void Save()
    {
        PlayerPrefs.SetInt("Wood", village.wood);
        PlayerPrefs.SetInt("Food", village.food);
        PlayerPrefs.SetInt("Gold", village.gold);

        GameObject[] villagers = GameObject.FindGameObjectsWithTag("Villager");
        int n_woodCutter = 0;
        int n_farmer = 0;
        int n_miner = 0;
        foreach (GameObject villager in villagers)
        {
            if (villager.GetComponent<Villager>().villagerType == "Wood Cutter")
                n_woodCutter ++;
            if (villager.GetComponent<Villager>().villagerType == "Famer")
                n_farmer++;
            if (villager.GetComponent<Villager>().villagerType == "Miner")
                n_miner++;
        }

        PlayerPrefs.SetInt("Wood Cutter", n_woodCutter);
        PlayerPrefs.SetInt("Farmer", n_farmer);
        PlayerPrefs.SetInt("Miner", n_miner);

        PlayerPrefs.SetInt("Stockpiling Level", upgradeManager.level_stockpilling);
        PlayerPrefs.SetInt("Log felling Level", upgradeManager.level_logFelling);
        PlayerPrefs.SetInt("Bountiful harvest Level", upgradeManager.level_bountifulharvest);
        PlayerPrefs.SetInt("Crop rotation Level", upgradeManager.level_cropRotation);
        PlayerPrefs.SetInt("Fertilised pastures Level", upgradeManager.level_fertilisedPastures);
        PlayerPrefs.SetInt("Mining shaft Level", upgradeManager.level_miningShaft);
        PlayerPrefs.SetInt("Wheelbarrow Level", upgradeManager.level_wheelBarrow);

        GameObject[] lumberCamps = GameObject.FindGameObjectsWithTag("Lumber camp");
        GameObject[] farms = GameObject.FindGameObjectsWithTag("Farm");
        GameObject[] mills = GameObject.FindGameObjectsWithTag("Mill");
        GameObject[] miningCamps = GameObject.FindGameObjectsWithTag("Mining Camp");

        PlayerPrefs.SetInt("n_lumberCamps", lumberCamps.Length);
        PlayerPrefs.SetInt("n_farms", farms.Length);
        PlayerPrefs.SetInt("n_mills", mills.Length);
        PlayerPrefs.SetInt("n_miningCamps", miningCamps.Length);

        for (int i = 0; i < lumberCamps.Length; i++)
        {
            float x = lumberCamps[i].transform.position.x;
            float y = lumberCamps[i].transform.position.y;
            float z = lumberCamps[i].transform.position.z;
            PlayerPrefs.SetFloat("lumberCamp" + i + ".x", x);
            PlayerPrefs.SetFloat("lumberCamp" + i + ".y", y);
            PlayerPrefs.SetFloat("lumberCamp" + i + ".z", z);
        }
        for (int i = 0; i < farms.Length; i++)
        {
            float x = farms[i].transform.position.x;
            float y = farms[i].transform.position.y;
            float z = farms[i].transform.position.z;
            PlayerPrefs.SetFloat("farms" + i + ".x", x);
            PlayerPrefs.SetFloat("farms" + i + ".y", y);
            PlayerPrefs.SetFloat("farms" + i + ".z", z);
        }
        for (int i = 0; i < mills.Length; i++)
        {
            float x = mills[i].transform.position.x;
            float y = mills[i].transform.position.y;
            float z = mills[i].transform.position.z;
            PlayerPrefs.SetFloat("mills" + i + ".x", x);
            PlayerPrefs.SetFloat("mills" + i + ".y", y);
            PlayerPrefs.SetFloat("mills" + i + ".z", z);
        }
        for (int i = 0; i < miningCamps.Length; i++)
        {
            float x = miningCamps[i].transform.position.x;
            float y = miningCamps[i].transform.position.y;
            float z = miningCamps[i].transform.position.z;
            PlayerPrefs.SetFloat("miningCamps" + i + ".x", x);
            PlayerPrefs.SetFloat("miningCamps" + i + ".y", y);
            PlayerPrefs.SetFloat("miningCamps" + i + ".z", z);
        }

        Debug.Log("Game saved");
    }    

    public void ResetGame()
    {
        PlayerPrefs.DeleteAll();
        Application.Quit();
    }
}
