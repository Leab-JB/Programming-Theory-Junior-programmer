using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class PlayerTroop : MonoBehaviour
{

    public Type.TroopType troopType;

    [SerializeField]
    private GameObject troopModel;
    private Animator anim;

    private bool isStopTime;

    private bool isTarget = false;

    private GameObject enemy;

    [SerializeField]
    private int health = 100;

    [SerializeField]
    private int damage = 3;

    protected bool isDeath = false;

    protected NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

    }

    IEnumerator StopTime()
    {
        // Range to vary troop stop time
        yield return new WaitForSeconds(Random.Range(0.2f,0.5f));
        isStopTime = false;
    }

    protected virtual void Update()
    {
        CheckHealth();

        TroopAttackEnemy(Type.EType.None);
        
    }

    // ---------------------- Remember to remove this function in order to make the troop attack when near the enemy and not from a
    // --------------------- collider
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if(enemy != null)
            {
                if(enemy.enemyType == Type.EType.Resource)
                {
                    Resource resource = other.GetComponent<Resource>();
                    resource.TakeResource(troopType, damage);
                }
                else
                    enemy.TakeDamage(damage);
            } 
        }
    }

    ///<summary>
    ///This function looks for all the nearest tower enemy to the player troop.
    ///Note that it returns a GameObject.
    ///</summary>

    protected void TroopAttackEnemy(Type.EType priority)
    {
        // when a tower is destroyed, wait for 1 sec before looking for another enemy tower
        if (isStopTime)
        {
            agent.velocity = Vector3.zero;
            return;
        }
        if (!isDeath)
        {
            // if there is no target, set a target
            if (!isTarget)
            {
                enemy = LookForEnemy(priority);
            }
            // move toward the target if the target is already set
            if (enemy != null)
            {
                isTarget = true;
                agent.SetDestination(enemy.transform.position);
                transform.LookAt(enemy.transform);
            }
            else
            {

                isTarget = false;
                isStopTime = true;

                StartCoroutine(StopTime());
                // if there are no enemies, then stop moving and disable the script
                if (!LookForEnemy())
                {
                    agent.isStopped = true;
                    enabled = false;
                }

            }
        }
    }

    ///<summary>
    ///This function looks for all the nearest tower enemy to the player troop.
    ///Note that it returns a GameObject.
    ///</summary>
    protected GameObject LookForEnemy(Type.EType priority)
    {
        bool isPriority = false;
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

        float dis = 0f;

        // This is where the course of ASP NET CORE Save me (^_^), if not, I would have loop the objEnemies again to check for the
        // enemyType, and it would have took some lines of code.
        // Thanks ASP NET CORE, it help me learn basic of Linq !!!!

        // checking for all priority tower
        if(priority != Type.EType.None && objEnemies.Any(i => i.GetComponent<Enemy>().enemyType == priority))
        {
            isPriority = true;
            // assign the distance of the first object that matches the priority
            GameObject temp = objEnemies.FirstOrDefault(i => i.GetComponent<Enemy>().enemyType == priority);

            dis = Vector3.Distance(temp.transform.position, transform.position);
        }
        else
        {
            dis = Vector3.Distance(objEnemies[0].transform.position, transform.position);
        }
        
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
                    if (isPriority)
                    {
                        if (a <= dis)
                        {
                            if (objEnemies[i].GetComponent<Enemy>().enemyType == priority)
                            {
                                dis = a;
                                result = objEnemies[i];
                            }
                        }
                    }else if(a <= dis)
                    {
                        dis = a;
                        result = objEnemies[i];
                    }
                }
            }
        }
        return result;
    }

    ///<summary>
    ///This function checks whether there exist an enemy.
    ///Note that it returns a bool.
    ///</summary>
    protected bool LookForEnemy()
    {
        GameObject[] objEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (objEnemies.Any())
        {
            return true;
        }
        return false;
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
