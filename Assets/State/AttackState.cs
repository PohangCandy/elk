using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    public AttackState(Enemy enemy, Transform player) : base(enemy, player) { }


    // implement
    public override State HandleInput()
    {
        if (enemy.Hp < 20f)
        {
            return new FleeState(enemy, player);
        }
        if (Vector3.Distance(player.position, enemy.transform.position) > enemy.AttackRange)
        {
            return new MoveTowardsPlayerState(enemy, player);
        }
        return null;
    }
    
    // implement
    public override void DoAction()
    {
        // do nothing
    }

    public override void Enter()
    {
        enemy.AttackRange = 6.0f;
        enemy.ChangeColor(Color.red);
    }

    public override void Exit()
    {
        enemy.AttackRange = 5.0f;
    }

}
