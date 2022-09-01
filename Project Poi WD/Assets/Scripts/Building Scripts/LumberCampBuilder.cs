using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LumberCampBuilder : MonoBehaviour
{
    GameObject gameManager;
    BuildingManager buildingManager;
    public GameObject lumberCampGameObject;
    public bool hasBeenBuilt = false;

    RaycastHit hit;
    Vector3 movePoint;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Game Manager");
        buildingManager = gameManager.GetComponent<BuildingManager>();

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (buildingManager.isInBuildMode && buildingManager.building == "Lumber Camp" && Physics.Raycast(ray, out hit)
            && !hasBeenBuilt)
            transform.position = hit.point;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (buildingManager.isInBuildMode && buildingManager.building == "Lumber Camp" && Physics.Raycast(ray, out hit)
            && !hasBeenBuilt)
        {
            movePoint = hit.point;
            movePoint.y = 0.0f;
            transform.position = movePoint;
        }

        if (Input.GetMouseButton(0))
        {
            Destroy(gameObject);
            Instantiate(lumberCampGameObject, transform.position, transform.rotation);
            lumberCampGameObject.GetComponent<LumberCampBuilder>().hasBeenBuilt = true;
            buildingManager.isInBuildMode = false;
        }
    }
}
