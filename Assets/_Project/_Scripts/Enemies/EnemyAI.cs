using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class EnemyAI : EnemyFSM
{
    [Header("Settings")]
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _chaseSpeed = 4f;
    [SerializeField] private float _playerLostTime = 3f;
    [SerializeField] private float _waypointTime = 3f;
    [SerializeField] private float _rotationAngle = 180f;
    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private GameObject _stunCanvas;
    [SerializeField] private Image _stunFill;

    private EnemyVision _vision;
    private Coroutine _patrollingCoroutine;
    private Coroutine _playerLostCoroutine;
    private Coroutine _UIStunCoroutine;
    private bool _isPatrolling = false;
    private bool _isPlayerLost = false;
    private Vector3 _startPosition;
    private Quaternion _startRotation;
    private Quaternion _targetRotation;
    private int _currentPosition;

    protected override void Awake()
    {
        base.Awake(); // Awake padre prima

        _vision = GetComponent<EnemyVision>();
        _startPosition = transform.position;
        _startRotation = transform.rotation;
        _targetRotation = transform.rotation * Quaternion.Euler(0, _rotationAngle, 0);
    }

    #region UI STUN
    protected override void GetStunned(float duration)
    {
        base.GetStunned(duration);

        if (_UIStunCoroutine != null) StopCoroutine(_UIStunCoroutine);
        _UIStunCoroutine = StartCoroutine(UIStunRoutine(duration));
    }

    protected override void EndStun()
    {
        base.EndStun();

        if (_UIStunCoroutine != null) StopCoroutine(_UIStunCoroutine);
        if (_stunCanvas != null) _stunCanvas.SetActive(false);
    }
    #endregion

    protected override void ChaseLogic()
    {
        _agent.isStopped = false;
        _isPatrolling = false;
        _agent.speed = _chaseSpeed;

        if (_vision.CanSeePlayer(_player)) // Se vede il player
        {
            _agent.SetDestination(_player.position); // Lo insegue continuamente

            if (_isPlayerLost && _playerLostCoroutine != null) // Se lo aveva perso ma poi ritrovato
            {
                StopCoroutine(_playerLostCoroutine);
                _isPlayerLost = false;
            }
        }
        else // Se non lo vede piu'
        {
            if (!_isPlayerLost)
            {
                _playerLostCoroutine = StartCoroutine(PlayerLostRoutine());
            }
        }
    }

    protected override void PatrolLogic()
    {
        _agent.isStopped = false;
        if (_vision.CanSeePlayer(_player))
        {
            if (_patrollingCoroutine != null)
            {
                StopCoroutine(_patrollingCoroutine);
                _isPatrolling = false;
            }
            _state = EnemyState.CHASING;
            return;
        }
        if (!_isPatrolling)
        {
            _patrollingCoroutine = StartCoroutine(PatrollingRoutine());
        }
    }

    protected override void StunnedLogic()
    {
        _agent.isStopped = true;

        if (_patrollingCoroutine != null)
        {
            StopCoroutine(_patrollingCoroutine);
            _isPatrolling = false;
        }

        if (_playerLostCoroutine != null)
        {
            StopCoroutine(_playerLostCoroutine);
            _isPlayerLost = false;
        }
    }

    #region COROUTINES

    private IEnumerator UIStunRoutine(float duration)
    {
        if (_stunCanvas != null) _stunCanvas.SetActive(true);
        if (_stunFill != null) _stunFill.fillAmount = 1; // Attivo con fill al max

        float fillTime = 0f;

        while (fillTime < duration)
        {
            fillTime += Time.deltaTime;

            if (_stunFill != null)
            {
                _stunFill.fillAmount = 1f - (fillTime / duration);
            }
            yield return null;
        }

        if (_stunCanvas != null) _stunCanvas.SetActive(false); // Finito! Sparisci
    }
    private IEnumerator PatrollingRoutine()
    {
        if (_isPatrolling) yield break;
        _isPatrolling = true;

        bool rotToTarget = true;

        while (_state == EnemyState.PATROLLING)
        {
            if (_waypoints.Length == 0)
            {
                if (Vector3.Distance(transform.position, _startPosition) > 0.1f) // Enemy si e' spostato ed ha perso il player
                {
                    _agent.SetDestination(_startPosition); // Torna alla pozione di partenza
                    yield return null;
                }
                else
                {
                    Quaternion currentRotation = rotToTarget ? _targetRotation : _startRotation; // Scelta target

                    while (Quaternion.Angle(transform.rotation, currentRotation) > 0.01f) // Ciclo che va verso il target scelto
                    {
                        transform.rotation = Quaternion.RotateTowards(transform.rotation, currentRotation, _rotationSpeed * Time.deltaTime);
                        yield return null;
                    }

                    transform.rotation = currentRotation;

                    rotToTarget = !rotToTarget; // Inversione direzione prossimo giro

                    yield return new WaitForSeconds(_waypointTime);
                }
            }
            else
            {
                if (_currentPosition >= _waypoints.Length) _currentPosition = 0;
                _agent.SetDestination(_waypoints[_currentPosition].position);

                if (_agent.pathPending || _agent.remainingDistance > _agent.stoppingDistance)
                {
                    yield return null;
                }
                else
                {
                    yield return new WaitForSeconds(_waypointTime);
                    _currentPosition++;
                }
            }
        }
        _isPatrolling = false;
    }

    private IEnumerator PlayerLostRoutine()
    {
        _isPlayerLost = true;

        yield return new WaitForSeconds(_playerLostTime);

        _isPlayerLost = false;
        _state = EnemyState.PATROLLING;
    }
    #endregion COROUTINES

}