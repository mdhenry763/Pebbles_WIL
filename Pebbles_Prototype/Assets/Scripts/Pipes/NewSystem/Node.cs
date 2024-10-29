using System;
using System.Collections.Generic;
using System.Linq;
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
    public MeshRenderer renderer;
    
    [Header("Pipe State")]
    public bool IsSource;
    public bool IsFlowing;
    public bool IsClosed;
    public PipeType Type;
    
    
    private List<ConnectionCheck> _connectionChecks = new List<ConnectionCheck>();

    //Events
    public static event Action OnConnectionChanged;

    private void Start()
    {
        //renderer = GetComponent<MeshRenderer>();
        
        //Setup Connections
        SetupConnections();
    }

    private void OnEnable()
    {
        InteractionArea.OnPipeToggled += HandlePipeToggled;
    }

    private void OnDisable()
    {
        InteractionArea.OnPipeToggled -= HandlePipeToggled;
    }

    private void HandlePipeToggled()
    {
        CheckConnection();
    }

    private void SetupConnections()
    {
        foreach (var connection in connectionPoints)
        {
            ConnectionCheck connectionCheck = new ConnectionCheck(connection, connectionDistance, connectionLayer);
            _connectionChecks.Add(connectionCheck);
        }
    }

    public void PipeToggle()
    {
        //Switch flowing on and off
        if(!IsSource) return;

        IsFlowing = !IsFlowing;
    }

    private bool CheckConnection()
    {
        var count = _connectionChecks.Count(connection => connection.CheckConnection());
        
        //onConnectionChanged?.Invoke(this);
        if (!IsSource) //Do not have to check connections if pipe is the source
        {
            IsFlowing = count > 0;
        }

        //Check is circuit is closed
        if (IsFlowing && Type == PipeType.End)
        {
            IsClosed = true;
        }
        else
        {
            IsClosed = false;
        }
        
        OnConnectionChanged?.Invoke();
        
        if (IsFlowing)
        {
            renderer.material = connectedMat;
            return true;
        }
       
        renderer.material = notConnectedMat;
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

    //Could add a connection check to see if node connected
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