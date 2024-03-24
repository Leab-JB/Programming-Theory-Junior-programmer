using UnityEngine;

public class Enemy : MonoBehaviour
{

    public Type.EType enemyType;

    [SerializeField]
    protected int health;

    [SerializeField]
    protected int damage;

    protected Vector3 projectilePosition;

    // player troop for enemy search
    protected GameObject troop;

    // to target one player troop at a time
    private bool isTarget;

    [SerializeField]
    protected float range;

    // shoot timer
    protected float shootTimer;

    [SerializeField]
    protected float shootTimerMax;

    [SerializeField]
    protected GameObject projectile;

    private void Start()
    {
        projectilePosition = transform.Find("Projectile").GetComponentInChildren<Transform>().position;
        range = 40f;
        shootTimerMax = 0.15f;
    }
    private void Update()
    {

        CheckHealth();

        EnemyShoot();

    }

    ///<summary>
    ///This function looks for all the nearest player troop to the tower enemy.
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

    protected void EnemyShoot()
    {
        shootTimer -= Time.deltaTime;

        if (shootTimer <= 0f)
        {
            shootTimer = shootTimerMax;

            // check if the tower has target a player troop
            if (!isTarget)
            {
                troop = LookForEnemy();
            }

            // if there is a player troop and is in range, then shoot
            if (troop != null)
            {
                isTarget = true;
                Create(projectilePosition, troop.transform.position);
            }
            // player troop is empty, then look for another one nearer to the tower
            else
            {
                isTarget = false;
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
