
public class Robber : PlayerTroop
{
    protected override void Update()
    {
        CheckHealth();

        TroopAttackEnemy(Type.EType.Resource);
    }
}
