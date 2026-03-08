using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Transform _visionOrigin;
    [SerializeField] private LineRenderer _visionLine;
    [SerializeField] private LayerMask _obstacleMask;
    [SerializeField] private float _coneUpdate = 0.05f;
    [SerializeField] private float _viewDistance = 3f;
    [SerializeField] private float _viewAngle = 45f;
    [SerializeField] private int _segments = 15;

    private float _coneTimer;

    private void Update()
    {
        DrawConeTime();
    }

    private void DrawConeTime() // Ottimizzazione
    {
        _coneTimer -= Time.deltaTime;

        if (_coneTimer <= 0f)
        {
            DrawVisionCone();
            _coneTimer = _coneUpdate;
        }
    }


    public bool CanSeePlayer(Transform player)
    {
        Vector3 directionPlayer = (player.position - _visionOrigin.position).normalized;
        float distance = Vector3.Distance(_visionOrigin.position, player.position);

        float angle = Vector3.Angle(transform.forward, directionPlayer);

        if (distance > _viewDistance || angle > _viewAngle) return false;

        if (Physics.Raycast(_visionOrigin.position, directionPlayer, out RaycastHit hit, _viewDistance, _obstacleMask))
        {
            if (!hit.collider.CompareTag("Player")) return false;
        }

        return true;

    }

    private void DrawVisionCone()
    {
        _visionLine.positionCount = _segments + 1;

        float startAngle = -_viewAngle;

        Vector3 originLine = _visionOrigin.position;
        Vector3 rayCastOrigin = _visionOrigin.position;
        Vector3 forward = transform.forward;

        _visionLine.SetPosition(0, originLine);

        float deltaAngle = (2 * _viewAngle) / _segments;

        for (int i = 0; i < _segments; i++)
        {
            float currentAngle = startAngle + deltaAngle * i;

            Vector3 dir = Quaternion.Euler(0, currentAngle, 0) * forward;
            Vector3 point = originLine + dir * _viewDistance;

            if (Physics.Raycast(rayCastOrigin, dir, out var hitInfo, _viewDistance))
            {
                point = hitInfo.point - (rayCastOrigin - originLine);
            }
            _visionLine.SetPosition(i + 1, point);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (_visionOrigin == null) return;

        Gizmos.color = Color.yellow;

        Vector3 origin = _visionOrigin.position;
        float step = (_viewAngle * 2f) / _segments;

        for (int i = 0; i <= _segments; i++)
        {
            float angle = -_viewAngle + step * i;
            Vector3 dir = Quaternion.Euler(0, angle, 0) * transform.forward;

            Gizmos.DrawRay(origin, dir * _viewDistance);
        }
    }
}
