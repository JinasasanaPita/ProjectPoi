using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningCampBuilder : MonoBehaviour
{
    GameObject gameManager;
    BuildingManager buildingManager;
    public GameObject miningCampGameObject;
    private bool hasBeenBuilt;

    RaycastHit hit;
    Vector3 movePoint;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Game Manager");
        buildingManager = gameManager.GetComponent<BuildingManager>();

        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        if (buildingManager.isInBuildMode && buildingManager.building == "Mining Camp" && Physics.Raycast(ray, out hit)
            && !hasBeenBuilt)
            transform.position = hit.point;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        if (buildingManager.isInBuildMode && buildingManager.building == "Mining Camp" && Physics.Raycast(ray, out hit)
            && !hasBeenBuilt)
        {
            movePoint = hit.point;
            movePoint.y = 0.0f;
            transform.position = movePoint;
        }

        if (Input.GetMouseButton(0))
        {
            Instantiate(miningCampGameObject, transform.position, transform.rotation);
            Destroy(gameObject);
            buildingManager.isInBuildMode = false;
            hasBeenBuilt = true;
        }
    }
}
