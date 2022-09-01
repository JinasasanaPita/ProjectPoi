using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Diagnostics;

public class PathFinder : MonoBehaviour
{
    PathRequestManager requestManager;
    Grid grid;

    private void Awake()
    {
        requestManager = GetComponent<PathRequestManager>();
        grid = GetComponent<Grid>();
    }

    public void StartFindPath(Vector3 startPos, Vector3 targetPos)
    {
        StartCoroutine(FindPath(startPos, targetPos));
    }

    // Since we are going to attempt to have several units find their paths
    // managed by the path request manager, we are going to have several 
    // co-routines (i.e., instruction threads) to manage these requests
    IEnumerator FindPath(Vector3 startPosition, Vector3 targetPosition)
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();

        Vector3[] waypoints = new Vector3[0];
        bool isPathFound = false;

        Node startNode = grid.NodeCoordFromWorldPoint(startPosition);
        Node targetNode = grid.NodeCoordFromWorldPoint(targetPosition);

        if (startNode.isWalkable && targetNode.isWalkable)
        {
            // List<Node> openSet = new List<Node>();
            Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
            HashSet<Node> closedSet = new HashSet<Node>();
            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                // Node currentNode = openSet[0];
                Node currentNode = openSet.RemoveFirst();
                //for (int i = 1; i < openSet.Count; i++)
                //    if (openSet[i].fCost < currentNode.fCost || 
                //        openSet[i].fCost == currentNode.fCost && 
                //        openSet[i].hCost < currentNode.hCost)
                //        currentNode = openSet[i];

                //openSet.Remove(currentNode);
                closedSet.Add(currentNode);

                if (currentNode == targetNode)
                {
                    sw.Stop();
                    print("Path found: " + sw.ElapsedMilliseconds + "ms");
                    isPathFound = true;
                    break;
                    // yield return null;
                }

                foreach (Node neighbour in grid.FindNeighbouringNodes(currentNode))
                {
                    if (!neighbour.isWalkable || closedSet.Contains(neighbour))
                        continue;

                    int newCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
                    if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                    {
                        neighbour.gCost = newCostToNeighbour;
                        neighbour.hCost = GetDistance(neighbour, targetNode);
                        neighbour.parent = currentNode;

                        if (!openSet.Contains(neighbour))
                            openSet.Add(neighbour);
                    }
                }
            }
        }
        
        yield return null;
        if (isPathFound)
            waypoints = RetracePath(startNode, targetNode);
        requestManager.FinishedProcessingPath(waypoints, isPathFound);
    }

    Vector3[] RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while(currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        Vector3[] waypoints = SimplifyPath(path);
        Array.Reverse(waypoints);
        return waypoints;
    }

    Vector3[] SimplifyPath(List<Node> path)
    {
        List<Vector3> waypoints = new List<Vector3>();
        Vector2 directionOld = Vector2.zero;
        for (int i = 1; i < path.Count; i++)
        {
            Vector2 directionNew = new Vector2(path[i - 1].gridPosX - path[i].gridPosX, path[i - 1].gridPosY - path[i].gridPosY);
            if (directionNew != directionOld)
                waypoints.Add(path[i].worldPosition);
            directionOld = directionNew;
        }
        return waypoints.ToArray();
    }

    int GetDistance(Node nodeA, Node nodeB)
    {
        int distX = Mathf.Abs(nodeA.gridPosX - nodeB.gridPosX);
        int distY = Mathf.Abs(nodeA.gridPosY - nodeB.gridPosY);

        if (distX > distY)
            return 14 * distY + 10 * (distX - distY);
        return 14 * distX + 10 * (distY - distX);
    }

}
