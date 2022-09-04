using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject upgradePanelGameObject;
    public GameObject createBuildingsPanelGameObject;
    public GameObject helpPanelGameObject;
    public GameObject settingsPanelGameObject;

    Animator upgradePanelAnimator;
    Animator createBuildingsAnimator;
    Animator helpPanelAnimator;
    Animator settingsPanelAnimator;
    bool upgradePanelIsShowing;
    bool createBuildingsPanelIsShowing;
    bool helpPanelIsShowing;
    bool settingsPanelIsShowing;

    GameObject gameManager;
    SaveGameManager saveGameManager;

    void Start()
    {
        upgradePanelAnimator = upgradePanelGameObject.GetComponent<Animator>();
        createBuildingsAnimator = createBuildingsPanelGameObject.GetComponent<Animator>();
        helpPanelAnimator = helpPanelGameObject.GetComponent<Animator>();
        settingsPanelAnimator = settingsPanelGameObject.GetComponent<Animator>();
        upgradePanelIsShowing = upgradePanelGameObject.activeSelf;
        createBuildingsPanelIsShowing = createBuildingsPanelGameObject.activeSelf;
        helpPanelIsShowing = helpPanelGameObject.activeSelf;
        settingsPanelIsShowing = settingsPanelGameObject.activeSelf;

        gameManager = GameObject.FindGameObjectWithTag("Game Manager");
        saveGameManager = gameManager.GetComponent<SaveGameManager>();  
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ToggleUpgradePanelActive()
    {
        if (!upgradePanelIsShowing)
        {
            upgradePanelAnimator.SetBool("upgradePanelActive", true);
            upgradePanelIsShowing = true;
        }
        else
        {
            upgradePanelAnimator.SetBool("upgradePanelActive", false);
            upgradePanelIsShowing = false;
        }
    }

    public void ToggleCreateBuildingsPanelActive()
    {
        if (!createBuildingsPanelIsShowing)
        {
            createBuildingsAnimator.SetBool("createPanelActive", true);
            createBuildingsPanelIsShowing = true;
        }
        else
        {
            createBuildingsAnimator.SetBool("createPanelActive", false);
            createBuildingsPanelIsShowing = false;
        }
    }

    public void ToggleHelpPanelActive()
    {
        if (!helpPanelIsShowing)
        {
            helpPanelAnimator.SetBool("helpPanelActive", true);
            helpPanelIsShowing = true;
        }
        else
        {
            helpPanelAnimator.SetBool("helpPanelActive", false);
            helpPanelIsShowing = false;
        }
    }

    public void ToggleSettingsPanelActive()
    {
        if (!settingsPanelIsShowing)
        {
            settingsPanelAnimator.SetBool("settingsPanelActive", true);
            settingsPanelIsShowing = true;
        }
        else
        {
            settingsPanelAnimator.SetBool("settingsPanelActive", false);
            settingsPanelIsShowing = false;
        }
    }

    public void QuitGame()
    {
        saveGameManager.Save();
        Application.Quit();
    }
}
