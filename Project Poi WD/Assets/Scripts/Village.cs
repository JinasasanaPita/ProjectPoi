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

    public TextMeshProUGUI villageStats;

    public float villagerGatherRate;
    public int villagerCarryCapacity;



    // Start is called before the first frame update
    void Start()
    {
        villagerCarryCapacity = 10;
        villagerGatherRate = 1;

        villageStats.fontSize = 15;
    }

    // Update is called once per frame
    void Update()
    {
        villageStats.text = "Gold: " + gold + "\nWood: " + wood + "\nFood: " + food;
    }
}
