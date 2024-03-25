using UnityEngine;

public class Golem : PlayerTroop
{
    // The golem attack first wizard tower before moving to others
    protected override void Update()
    {
        CheckHealth();
        TroopAttackEnemy(Type.EType.Wizard);
    }
}
