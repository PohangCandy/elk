using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrollState : State
{
    Vector3 targetLocation;
    float step;
    float radius; 

    public StrollState(Enemy enemy, Transform player) : base(enemy, player)
    {
        step = 1.0f;
        radius = 3.0f;
    }

    

    // implement
    public override State HandleInput()
    {
        if (Vector3.Distance(player.position, enemy.transform.position) <= enemy.VisibleRange) 
        {
            return new MoveTowardsPlayerState(enemy, player);
        }
        return null;
    }
    
    // implement
    public override void DoAction()
    {
        if (Vector3.Distance(enemy.transform.position, targetLocation) < Mathf.Epsilon)
        {
            targetLocation = getRandomLocation(3.0f);
        }
        enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, targetLocation, step * Time.deltaTime);

    }

    public override void Enter()
    {
        enemy.VisibleRange = 10f;
        targetLocation = getRandomLocation(3.0f);
        enemy.ChangeColor(Color.white);
    }

    public override void Exit()
    {
    }

}
