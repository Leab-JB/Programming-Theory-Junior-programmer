using UnityEngine;


public class Resource : Enemy
{
    /*
     * Damage, Range, Shoot timer max, and projectile fields are useless for this script
     * So consider to not change their values or you want to implement something that will
     * use those values
     */

    [SerializeField]
    private int resourceMax;
    public int resourceCurrent;
    protected override void Start()
    {
        
        resourceCurrent = resourceMax;
    }

    protected override void Update()
    {
        CheckHealth();
    }

    public void TakeResource(Type.TroopType tTroop, int damage)
    {
        if(tTroop == Type.TroopType.Rob)
        {
            health -= damage * 3;
            // retrieve the resource from the current resource
            // need to implement this with inventory
        }
        else
        {
            health -= damage;
            // retrieve the resource from the current resource
            // need to implement this with inventory
        }
    }
}
