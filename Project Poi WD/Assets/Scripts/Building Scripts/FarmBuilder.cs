using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmBuilder : MonoBehaviour
{
    GameObject gameManager;
    BuildingManager buildingManager;
    public GameObject farmGameObject;
    public bool hasBeenBuilt;

    RaycastHit hit;
    Vector3 movePoint;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Game Manager");
        buildingManager = gameManager.GetComponent<BuildingManager>();

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (buildingManager.isInBuildMode && buildingManager.building == "Farm" && Physics.Raycast(ray, out hit)
            && !hasBeenBuilt)
            transform.position = hit.point;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (buildingManager.isInBuildMode && buildingManager.building == "Farm" && Physics.Raycast(ray, out hit)
            && !hasBeenBuilt)
            transform.position = hit.point;

        if (Input.GetMouseButton(0))
        {
            Destroy(gameObject);
            Instantiate(farmGameObject, transform.position, transform.rotation);
            farmGameObject.GetComponent<FarmBuilder>().hasBeenBuilt = true;
            buildingManager.isInBuildMode = false;
        }
    }
}
