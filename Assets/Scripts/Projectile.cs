using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 20f;

    [HideInInspector]
    public int damage;

    private Vector3 targetPosition;
    public Vector3 p_targetPosition
    {
        set
        {
            targetPosition = value;
        }
    }

    private void Update()
    {
        MoveTowardPlayerTroop();
        
        // destroy when distance between projectile and troop is small
        float destroySelfDistance = 1f;
        if (Vector3.Distance(transform.position, targetPosition) < destroySelfDistance)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // remember to cycle through all the troops
            PlayerTroop troop = other.GetComponent<PlayerTroop>();
            if (troop != null)
            {
                troop.TakeDamage(damage);
            }
        }
    }

    // move toward player troop
    private void MoveTowardPlayerTroop()
    {
        Vector3 moveDir = (targetPosition - transform.position).normalized;

        transform.position += moveDir * moveSpeed * Time.deltaTime;

        float angle = GetAngleFromVectorFloat(moveDir);
        transform.eulerAngles = new Vector3(0, angle + 90, 90);
    }

    // rotate the projectile toward the player troop
    private float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        return n;
    }
}
