using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PathRequestManager : MonoBehaviour
{
    Queue<PathRequest> pathRequests = new Queue<PathRequest>();
    PathRequest currentPathRequest;

    static PathRequestManager instance;
    PathFinder pathFinder;
    bool isProcessingPath;

    private void Awake()
    {
        instance = this;
        pathFinder = GetComponent<PathFinder>();    
    }

    public static void RequestPath(Vector3 pathStart, Vector3 pathEnd, Action<Vector3[], bool> callback)
    {
        PathRequest newRequest = new PathRequest(pathStart, pathEnd, callback);
        instance.pathRequests.Enqueue(newRequest);
        instance.TryProcessNext();
    }

    // Function to detect if we're already currently processing a path
    // If we are not procesing a path, it will ask the pathfinding script to
    // process the next path
    void TryProcessNext()
    {
        if (!isProcessingPath && pathRequests.Count > 0)
        {
            currentPathRequest = pathRequests.Dequeue();
            isProcessingPath = true;
            pathFinder.StartFindPath(currentPathRequest.pathStart, currentPathRequest.pathEnd);
        }
    }

    // Function to be called by the pathfinding script once it has finished
    // finding a path
    public void FinishedProcessingPath(Vector3[] path, bool success)
    {
        currentPathRequest.callback(path, success);
        isProcessingPath = false;
        TryProcessNext();
    }

    struct PathRequest
    {
        public Vector3 pathStart;
        public Vector3 pathEnd;
        public Action<Vector3[], bool> callback;    

        public PathRequest(Vector3 _start, Vector3 _end, Action<Vector3[], bool> _callback)
        {
            pathStart = _start;
            pathEnd = _end;
            callback = _callback;
        }
    }

}
