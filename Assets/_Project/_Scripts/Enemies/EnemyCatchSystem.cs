using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCatchSystem : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Transform _respawnPoint;
    [SerializeField] private AudioClip _SFX;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            if (other.TryGetComponent(out NavMeshAgent playerAgent))
            {
                AudioManager.Instance.PlaySFX(_SFX);
                playerAgent.Warp(_respawnPoint.position);
                other.transform.rotation = _respawnPoint.rotation;
            }
        }
    }
}
