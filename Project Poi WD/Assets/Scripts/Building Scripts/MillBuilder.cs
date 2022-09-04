using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MillBuilder : MonoBehaviour
{
    GameObject gameManager;
    BuildingManager buildingManager;
    public GameObject millGameObject;
    private bool hasBeenBuilt;

    RaycastHit hit;
    Vector3 movePoint;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Game Manager");
        buildingManager = gameManager.GetComponent<BuildingManager>();

        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        if (buildingManager.isInBuildMode && buildingManager.building == "Mill" && Physics.Raycast(ray, out hit)
            && !hasBeenBuilt)
            transform.position = hit.point;
    }

    void Update()
    {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        if (buildingManager.isInBuildMode && buildingManager.building == "Mill" && Physics.Raycast(ray, out hit)
            && !hasBeenBuilt)
        {
            movePoint = hit.point;
            movePoint.y = 0.0f;
            transform.position = movePoint;
        }

        if (Input.GetMouseButton(0))
        {
            Instantiate(millGameObject, transform.position, transform.rotation);
            Destroy(gameObject);
            buildingManager.isInBuildMode = false;
            hasBeenBuilt = true;
        }
    }
}
