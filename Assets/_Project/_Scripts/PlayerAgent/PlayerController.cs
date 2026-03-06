using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [Header ("Settings")]
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private LayerMask _walkableMask;

    private Camera _cam;
    private NavMeshPath _path;

    private void Awake()
    {
        _cam = Camera.main;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _path = new NavMeshPath();
    }

    private void Update()
    {
        MovementLogic();
    }

    private void MovementLogic()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast( ray, out RaycastHit hit, _walkableMask))
            {
                _navMeshAgent.CalculatePath(hit.point, _path);
                _navMeshAgent.SetDestination(hit.point);
            }
        }
    }
}
