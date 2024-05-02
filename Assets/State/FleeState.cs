using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeState : State
{
    Vector3 targetLocation;
    float step = 1.0f;

    public FleeState(Enemy enemy, Transform player) : base(enemy, player) { }

    // implement
    public override State HandleInput()
    {
        if (enemy.Hp > 60f)
            return new AttackState(enemy, player);
        
        return null;
    }
    
    // implement
    public override void DoAction()
    {
        if (Vector3.Distance(enemy.transform.position, targetLocation) < Mathf.Epsilon)
            targetLocation = enemy.transform.position + (enemy.transform.position - player.position).normalized * 5f;

        Debug.Log(enemy.Hp);

        enemy.Hp += 0.02f;
        //Move
        enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, targetLocation, step * Time.deltaTime);
    }

    public override void Enter()
    {
        targetLocation = enemy.transform.position + (enemy.transform.position - player.position).normalized * 5f;
        enemy.ChangeColor(Color.green);
    }

    public override void Exit()
    { 
    }

}
