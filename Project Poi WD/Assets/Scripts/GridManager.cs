using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase highlightTile;
    [SerializeField] private Vector3Int previousTileCoordinate;
    [SerializeField] private Vector3Int tileCoordinate;
    [SerializeField] private Vector3 highlightWorldPos;



    public bool isInBuildMode = false;

    void Start()
    {
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isInBuildMode)
            HighlightTile();
    }

    private void HighlightTile()
    {
        float mousePosX = Input.mousePosition.x;
        float mousePosY = Input.mousePosition.y;
        Vector3 mousePos = new Vector3(mousePosX, mousePosY, 0);

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        if (Physics.Raycast(ray, out hit))
        {
            highlightWorldPos = hit.point;
            highlightWorldPos.y = 0.1f;
            tileCoordinate = tilemap.WorldToCell(highlightWorldPos);

            // TO-DO: clean this
            if (tileCoordinate != previousTileCoordinate)
            {
                tilemap.SetTile(previousTileCoordinate, null);
                tilemap.SetTile(tileCoordinate, highlightTile);
                previousTileCoordinate = tileCoordinate;
            }
        }
        else
        {
            tilemap.SetTile(previousTileCoordinate, null);
            tilemap.SetTile(tileCoordinate, null);
            previousTileCoordinate = tileCoordinate;
        }
    }

    private void SetTreeResourceTiles(GameObject[] treeGameObjects)
    {
        //float minX;
        //float minY;
        //float maxX;
        //float maxY;
    }
}
