using UnityEngine;
using UnityEngine.AI;

public class PlayerTroop : MonoBehaviour
{

    public Type.TroopType troopType;

    public Type.Priority priorityEnemy;

    [SerializeField]
    private int health = 100;

    [SerializeField]
    private int damage = 3;

    private bool isDeath = false;

    private GameObject enemy;

    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        CheckHealth();
        if (!isDeath)
        {
            GameObject enemy = LookForEnemy();
            if (enemy != null)
            {
                agent.SetDestination(enemy.transform.position);
            }
            else
            {
                agent.isStopped = true;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // remember to cycle through all the enemy script
            Enemy enemy = other.GetComponent<Enemy>();
            if(enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }

    ///<summary>
    ///This function looks for all the nearest tower enemy to the player troop.
    ///Note that it returns a GameObject.
    ///</summary>
    protected GameObject LookForEnemy(Type.Priority priority = Type.Priority.None)
    {
        GameObject result = null;
        GameObject[] objEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        switch (objEnemies.Length)
        {
            case 0:
                return result;
            case 1:
                result = objEnemies[0];
                return result;
        }
        float dis = Vector3.Distance(objEnemies[0].transform.position, transform.position);
        for (int i = 0; i < objEnemies.Length; i++)
        {
            for (int j = 0; j < objEnemies.Length; j++)
            {
                // skip when it check itself
                if (i == j) continue;

                float a = Vector3.Distance(transform.position, objEnemies[i].transform.position);
                float b = Vector3.Distance(transform.position, objEnemies[j].transform.position);
                if (a < b)
                {
                    if(a <= dis)
                    {
                        dis = a;
                        result = objEnemies[i];
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
        if (health < 0)
        {
            isDeath = true;
            Destroy(gameObject);
        }
    }
}
