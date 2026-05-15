using UnityEngine;

public class EnemyArcher : Enemy
{
    protected override void Attack()
    {
        Debug.Log(EnemyName + " shoots an arrow!");
    }
}
