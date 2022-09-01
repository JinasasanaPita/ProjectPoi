using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Node : IHeapItem<Node>
{
    public bool isWalkable;
    public Vector3 worldPosition;

    public int gCost;
    public int hCost;

    public int gridPosX;
    public int gridPosY;
    public Node parent;
    int _heapIndex;

    public Node(bool _isWalkable, Vector3 _worldPos, int _gridPosX, int _gridPosY)
    {
        isWalkable = _isWalkable;
        worldPosition = _worldPos;
        gridPosX = _gridPosX;
        gridPosY = _gridPosY;
    }

    public int fCost
    {
        get { return gCost + hCost; }
    }

    public int heapIndex { 
        get { return _heapIndex; }  
        set { _heapIndex = value; } 
    }

    public int CompareTo(Node nodeToCompare)
    {
        int compare = fCost.CompareTo(nodeToCompare.fCost);
        if (compare == 0)
            compare = hCost.CompareTo(nodeToCompare.hCost);
        return -compare;
    }
}
