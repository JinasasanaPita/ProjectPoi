using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    Village village;

    public int level_stockpilling;
    public int level_logFelling;
    public int level_bountifulharvest;
    public int level_cropRotation;
    public int level_fertilisedPastures;
    public int level_miningShaft;
    public int level_wheelBarrow;

    // Start is called before the first frame update
    void Start()
    {
        level_stockpilling = 0;
        level_logFelling = 0;
        level_bountifulharvest = 0;
        level_cropRotation = 0;
        level_fertilisedPastures = 0;
        level_miningShaft = 0;
        level_wheelBarrow = 0;
    }

    // Update is called once per frame
    void Update()
    {
        village = GetComponent<Village>();
    }

    public int CalculateStockpillingEffect(int level)
    {
        return level + 2;
    }

    public int CalculateLogFellingEffect(int level)
    {
        return (int)(level + 0.01 * level);
    }

    public int CalculateBountifulHarvestEffect(int level)
    {
        return level + 2;
    }

    public int CalculateCropRotationEffect(int level)
    {
        return level + 2;
    }

    public int CalculateFertilisedPasturesEffect(int level)
    {
        return level + 2;
    }

    public int CalculateMiningShaftEffect(int level)
    {
        return (int)(level + 0.01 * level);
    }

    public int CalculateWheelbarrowEffect(int level)
    {
        return level + 2;
    }

    public void IncreaseStockPilingLevel() 
    { 
        if (village.gold >= (level_stockpilling + Mathf.Pow(level_stockpilling, 2)))
        {
            level_stockpilling += 1;
            village.gold -= (int)(level_stockpilling + Mathf.Pow(level_stockpilling, 2));
        }
        else
            Debug.Log("Not enough gold");
    }
    public void IncreaseLogFellingLevel() 
    {
        if (village.gold >= (level_logFelling + Mathf.Pow(level_logFelling, 2)))
        {
            level_logFelling += 1;
            village.gold -= (int)(level_logFelling + Mathf.Pow(level_logFelling, 2));
        }
        else
            Debug.Log("Not enough gold");
    }
    public void IncreaseBountifulHarvestLevel() 
    {
        if (village.gold >= (level_bountifulharvest + Mathf.Pow(level_bountifulharvest, 2)))
        {
            level_bountifulharvest += 1;
            village.gold -= (int)(level_bountifulharvest + Mathf.Pow(level_bountifulharvest, 2));
        }
        else
            Debug.Log("Not enough gold");
    }

    public void IncreaseCropRotationLevel()
    {
        if (village.gold >= (level_cropRotation + Mathf.Pow(level_cropRotation, 2)))
        {
            level_cropRotation += 1;
            village.gold -= (int)(level_cropRotation - Mathf.Pow(level_cropRotation, 2));
        }
        else
            Debug.Log("Not enough gold");
    }

    public void IncreaseFertilisedPasturesLevel()
    {
        if (village.gold >= (level_fertilisedPastures + Mathf.Pow(level_fertilisedPastures, 2)))
        {
            level_fertilisedPastures += 1;
            village.gold -= (int)(level_fertilisedPastures - Mathf.Pow(level_fertilisedPastures, 2));
        }
        else
            Debug.Log("Not enough gold");
    }

    public void IncreaseMiningShaftLevel()
    {
        if (village.gold >= (level_miningShaft + Mathf.Pow(level_miningShaft, 2)))
        {
            level_miningShaft += 1;
            village.gold -= (int)(level_miningShaft - Mathf.Pow(level_miningShaft, 2));
        }
        else
            Debug.Log("Not enough gold");
    }

    public void IncreaseWheelBarrowLevel()
    {
        if (village.gold >= (level_wheelBarrow + Mathf.Pow(level_wheelBarrow, 2)))
        {
            level_wheelBarrow += 1;
            village.gold -= (int)(level_wheelBarrow - Mathf.Pow(level_wheelBarrow, 2));
        }
        else
            Debug.Log("Not enough gold");
    }
}
