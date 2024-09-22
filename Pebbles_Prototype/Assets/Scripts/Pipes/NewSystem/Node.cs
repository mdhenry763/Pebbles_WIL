using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Node : MonoBehaviour
{
    [Header("Connection points")] 
    public float connectionDistance = 1f;
    public Transform[] connectionPoints;
    public LayerMask connectionLayer;

    [Header("Materials")] 
    public Material notConnectedMat;
    public Material connectedMat;
    
    [Header("Pipe State")]
    public bool IsSource;
    [FormerlySerializedAs("IsConnected")] public bool IsFlowing;
    public PipeType Type;
    
    private MeshRenderer _renderer;
    private List<ConnectionCheck> _connectionChecks = new List<ConnectionCheck>();

    private void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
        
        //Setup Connections
        SetupConnections();
    }

    private void SetupConnections()
    {
        foreach (var connection in connectionPoints)
        {
            ConnectionCheck connectionCheck = new ConnectionCheck(connection, connectionDistance, connectionLayer);
            _connectionChecks.Add(connectionCheck);
        }
    }

    //Events
    public static event Action<Node> onConnectionChanged;

    private void Update()
    {
        CheckConnection();
    }

    public void PipeToggle()
    {
        //Switch flowing on and off
        if(!IsSource) return;

        IsFlowing = !IsFlowing;
    }

    private bool CheckConnection()
    {
        //onConnectionChanged?.Invoke(this);
        if (!IsSource) //Do not have to check connections if pipe is the source
        {
            int count = 0;
            foreach (var connection in _connectionChecks)
            {
                if (connection.CheckConnection())
                {
                    count++;
                }
            }

            IsFlowing = count > 0;
        }
        
        
        if (IsFlowing)
        {
            _renderer.material = connectedMat;
            return true;
        }
       
        _renderer.material = notConnectedMat;
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawRay(cPointOne.position, cPointOne.right);
    }
}

public class ConnectionCheck
{
    private Transform _connectionPoint;
    private float _connectionDistance;
    private LayerMask _connectionLayer;

    public ConnectionCheck(Transform connectionPoint, float connectDistance, LayerMask connectionLayer)
    {
        _connectionPoint = connectionPoint;
        _connectionDistance = connectDistance;
        _connectionLayer = connectionLayer;
    }

    public bool CheckConnection()
    {
        RaycastHit cHit;
        bool connection = Physics.Raycast(
            _connectionPoint.position, _connectionPoint.right, out cHit, _connectionDistance, _connectionLayer);

        if (!connection)
        {
            return false;
        }

        if (cHit.transform.TryGetComponent<Node>(out var node))
        {
            return node.IsFlowing;
        }
        
        return false;
    }
    
}
