using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerArcher : Enemy
{
    private void Start()
    {
        projectilePosition = transform.Find("Projectile").GetComponentInChildren<Transform>().position;
        range = 40f;
        shootTimerMax = 0.15f;
    }
    private void Update()
    {

        CheckHealth();

        shootTimer -= Time.deltaTime;

        if (shootTimer <= 0f)
        {
            shootTimer = shootTimerMax;
            // remember to cycle troops
            GameObject troop = LookForEnemy();
            if (troop != null)
            {
                Create(projectilePosition, troop.transform.position);
            }
        }
    }
    // spawning projectiles function
    private void Create(Vector3 spawnPosition, Vector3 targetPosition)
    {
        GameObject obj = Instantiate(projectile, spawnPosition, Quaternion.identity);
        Projectile p = obj.GetComponent<Projectile>();
        p.damage = damage;
        p.p_targetPosition = targetPosition;
    }

    
}
