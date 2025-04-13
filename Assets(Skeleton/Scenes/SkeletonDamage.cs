using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonDamage : MonoBehaviour
{
    public int damage = 10;
    public float hitCooldown = 1.0f; 
    private float lastHitTime = -Mathf.Infinity;

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Time.time - lastHitTime >= hitCooldown)
            {
                PlayerHealth health = other.GetComponent<PlayerHealth>();
                if (health != null)
                {
                    health.TakeDamage(damage);
                    lastHitTime = Time.time;
                }
            }
        }
    }
}


