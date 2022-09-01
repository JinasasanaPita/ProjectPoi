using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public bool         displayGridGizmos;
    public bool         onlyDisplayPathGizmos; 

    public LayerMask    unwalkableMask;
    public Vector2      worldGridSize;
    public float        nodeRadius;
    Node[,] worldGrid;

    float   nodeDiameter;
    int     gridSizeX;
    int     gridSizeY;


    void Awake()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(worldGridSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(worldGridSize.y / nodeDiameter);
        CreateGrid();
    }

    public int MaxSize
    {
        get { return gridSizeX * gridSizeY; }
    }

    void CreateGrid()
    {
        worldGrid = new Node[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = transform.position - (Vector3.right * worldGridSize.x/2) - (Vector3.forward * worldGridSize.y/2);

        for (int x = 0; x < gridSizeX; x++)
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) +
                    Vector3.forward * (y * nodeDiameter + nodeRadius);
                bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask));
                worldGrid[x, y] = new Node(walkable, worldPoint, x, y);
            }
    }

    public Node NodeCoordFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + worldGridSize.x / 2) / worldGridSize.x;
        float percentY = (worldPosition.z + worldGridSize.y / 2) / worldGridSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        return worldGrid[x, y];
    }

    public List<Node> FindNeighbouringNodes(Node node)
    {
        List<Node> neighbours = new List<Node>();
        for (int x = -1; x <= 1; x++)
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;
                int checkX = node.gridPosX + x;
                int checkY = node.gridPosY + y;

                // Checking if the neighbouring node is within the bounds of the global grid
                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                    neighbours.Add(worldGrid[checkX, checkY]);
            }
                
        return neighbours;
    }

    public List<Node> path;
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(worldGridSize.x, 1, worldGridSize.y));
        if (onlyDisplayPathGizmos)
            if (path != null)
                foreach (Node n in path)
                {
                    Gizmos.color = Color.cyan;
                    Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - 0.1f));
                }
            else if (worldGrid != null && displayGridGizmos)
            {
                foreach (Node n in worldGrid)
                {
                    Gizmos.color = (n.isWalkable) ? Color.white : Color.red;
                    if (path != null)
                        if (path.Contains(n))
                            Gizmos.color = Color.cyan;
                    Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - 0.1f));
                }
            }
        //Gizmos.DrawWireCube(transform.position, new Vector3(worldGridSize.x, 1, worldGridSize.y));        
        //if (worldGrid != null && displayGridGizmos)
        //{
        //    foreach (Node n in worldGrid)
        //    {
        //        Gizmos.color = (n.isWalkable) ? Color.white : Color.red;
        //        Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - 0.1f));
        //    }
        //}           
    }
}
