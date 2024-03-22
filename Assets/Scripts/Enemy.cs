using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    protected int health;

    [SerializeField]
    protected int damage;

    protected Vector3 projectilePosition;
    protected Vector3 screenPosition;

    [SerializeField]
    protected float range;

    // shoot timer
    protected float shootTimer;

    [SerializeField]
    protected float shootTimerMax;

    [SerializeField]
    protected GameObject projectile;

    ///<summary>
    ///This funcion looks for all the nearest player troop to the tower enemy.
    ///Note that it returns a GameObject.
    ///</summary>
    protected GameObject LookForEnemy()
    {
        GameObject result = null;
        GameObject[] objTroop = GameObject.FindGameObjectsWithTag("Player");
        switch (objTroop.Length)
        {
            case 0:
                return result;
            case 1:
                if (Vector3.Distance(transform.position, objTroop[0].transform.position) <= range)
                {
                    result = objTroop[0];
                }
                return result;
        }
        for (int i = 0; i < objTroop.Length; i++)
        {
            for (int j = 0; j < objTroop.Length; j++)
            {
                if (i == j) continue;
                float a = Vector3.Distance(transform.position, objTroop[i].transform.position);
                float b = Vector3.Distance(transform.position, objTroop[j].transform.position);
                if (a < b)
                {
                    if (a <= range)
                    {
                        result = objTroop[i];
                    }
                }
            }
        }
        return result;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    protected void CheckHealth()
    {
        if(health < 0)
        {
            Destroy(gameObject);
        }
    }
}
