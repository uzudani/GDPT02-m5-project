using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFSM : MonoBehaviour
{
    public enum EnemyState
    {
        PATROLLING,
        CHASING,
        STUNNED
    }

    [Header("Settings")]
    [SerializeField] protected EnemyState _state;
    [SerializeField] protected Transform _player;

    protected NavMeshAgent _agent;
    protected EnemyState _currentState;
    protected float _currentSpeed;

    protected virtual void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _currentSpeed = _agent.speed;
        _currentState = _state;
    }

    private void OnEnable()
    {
        InteractableTotem.OnStunTriggered += GetStunned;
        InteractableTotem.OnStunEnded += EndStun;
    }

    private void OnDisable()
    {
        InteractableTotem.OnStunTriggered -= GetStunned;
        InteractableTotem.OnStunEnded -= EndStun;
    }

    protected virtual void GetStunned(float duration)
    {
        _state = EnemyState.STUNNED;
    }

    protected virtual void EndStun()
    {
        _state = EnemyState.PATROLLING;
    }

    private void Update()
    {
        SwitchLogic();
    }

    private void SwitchLogic()
    {
        switch (_state)
        {
            case EnemyState.PATROLLING:
                PatrolLogic();
                break;
            case EnemyState.CHASING:
                ChaseLogic();
                break;
            case EnemyState.STUNNED:
                StunnedLogic();
                break;
        }
    }

    public void ChangeState(EnemyState newState)
    {
        _state = newState;
    }

    protected virtual void PatrolLogic() { }
    protected virtual void ChaseLogic() { }
    protected virtual void StunnedLogic() { }
}
