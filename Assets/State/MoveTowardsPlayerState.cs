using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsPlayerState : State
{
    float step = 1.0f;

    public MoveTowardsPlayerState(Enemy enemy, Transform player) : base(enemy, player) { }



    // implement
    public override State HandleInput()
    {
        if (Vector3.Distance(player.position, enemy.transform.position) < enemy.AttackRange)
        {
            return new AttackState(enemy, player);
        }
        if (Vector3.Distance(player.position, enemy.transform.position) > enemy.VisibleRange)
        {
            return new StrollState(enemy, player);
        }
        return null;
    }
    
    // implement
    public override void DoAction()
    {
        enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, player.position, step * Time.deltaTime);
    }

    public override void Enter()
    {
        enemy.VisibleRange = 15.0f;
        enemy.ChangeColor(Color.yellow);
    }

    public override void Exit()
    { 
    
    }

}
