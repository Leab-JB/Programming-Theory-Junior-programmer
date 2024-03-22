using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyTower : Enemy
{
    private Vector3 projectilePosition;
    private Vector3 screenPosition;

    // shoot timer
    private float shootTimer, shootTimerMax;

    private float range;

    [SerializeField]
    private GameObject projectile;

    private void Start()
    {
        projectilePosition = GetComponentInChildren<Transform>().position;
        range = 40f;
        shootTimerMax = 0.15f;
    }
    private void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    screenPosition = Input.mousePosition;
        //    Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        //    if(Physics.Raycast(ray, out RaycastHit hit))
        //    {
        //        Create(projectilePosition, hit.point);
        //    }

        //}

        shootTimer -= Time.deltaTime;

        if (shootTimer <= 0f)
        {
            shootTimer = shootTimerMax;

            PlayerTroop troop = GameObject.FindObjectOfType<PlayerTroop>();
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
        p.p_targetPosition = targetPosition;
    }

    
}
