using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkeletonMovement : MonoBehaviour
{
    private Vector3 originPos;
    private Vector3 destinationPos;
    [SerializeField]
    private int speed;
    private bool DoRearchGoal;
    private bool DoDiscoverPlayer;
    public GameObject player;
    int damage;
    private Vector3 playerpos;
    int hp;

    private enum SkeletonState
    {
        Stroll, MoveTowardsPlayer, Attack, Flee
    }
    [SerializeField]
    private SkeletonState curState;
    private SkeletonState prevState;



    private void Start()
    {
        hp = 100;
        playerpos = player.GetComponent<PlayerMovement>().GetPlayerPos();
        originPos = new Vector3(10.0f, 0.5f, 0.0f);
        speed = 5;
        DoRearchGoal = true;
        curState = SkeletonState.Stroll;
        damage = 10;
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.H))
        {
            heal(20);
        }
        if(Input.GetKey(KeyCode.T))
        {
            getDamage(20);
        }

        //if (Vector3.Distance(gameObject.transform.position, playerpos) < 10)
        //{
        //    UpdateState(SkeletonState.MoveTowardsPlayer);
        //}
        //Debug.Log(Vector3.Distance(gameObject.transform.position, playerpos));

        switch (curState)
        {
            case SkeletonState.Stroll:

                if (!DoRearchGoal)
                {
                    Move(gameObject.transform.position, destinationPos);
                }
                else
                {
                    SetRandomDirection(10);
                }
                break;
            case SkeletonState.MoveTowardsPlayer:
                Move(gameObject.transform.position, player.GetComponent<PlayerMovement>().GetPlayerPos());
                prevState = SkeletonState.MoveTowardsPlayer;
                if (Vector3.Distance(gameObject.transform.position, playerpos) < 5)
                {
                    UpdateState(SkeletonState.Attack);
                }
                if (Vector3.Distance(gameObject.transform.position, playerpos) >= 15)
                {
                    UpdateState(SkeletonState.Stroll);
                }
                break;
            case SkeletonState.Attack:
                Attack(player);
                prevState = SkeletonState.Attack;
                if (hp<=20)
                {
                    UpdateState(SkeletonState.Flee);
                }
                if (Vector3.Distance(gameObject.transform.position, playerpos) > 6)
                {
                    UpdateState(SkeletonState.MoveTowardsPlayer);
                }
                break;
            case SkeletonState.Flee:
                Flee();
                if (hp >= 60)
                {
                    UpdateState(SkeletonState.Stroll);
                }
                break;
        }
    }

    
    private void Flee()
    {
        Vector3 fleepos = new Vector3 ( playerpos.x + 10, playerpos.y, playerpos.z + 10 );
        Move(gameObject.transform.position, fleepos);
    }

    private void Attack(GameObject player)
    {
        player.GetComponent<PlayerMovement>().getDamage(damage);
    }

    private void SetRandomDirection(int distance)
    {
        float x = gameObject.transform.position.x;
        float y = gameObject.transform.position.y;
        float z = gameObject.transform.position.z;

        int randomnum = UnityEngine.Random.Range(0,4);

        switch (randomnum)
        {
            case 0:
                SetDirectionPos(x + distance, 0.5f, z);
                break;              
            case 1:
                SetDirectionPos(x, 0.5f, z + distance);
                break;
            case 2:
                SetDirectionPos(x - distance, 0.5f, z);
                break;
            case 3:
                SetDirectionPos(x, 0.5f, z - distance);
                break;
        }

    }

    private void SetDirectionPos(float x, float y, float z)
    {
        destinationPos = new Vector3(x, 0.5f, z);
        CheckReachGoal();
    }

    private void Move(Vector3 curPos, Vector3 TowardPos)
    {
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, destinationPos, Time.deltaTime * speed);
        CheckReachGoal();
    }

    private void CheckReachGoal()
    {
        if (gameObject.transform.position == destinationPos)
        {
            DoRearchGoal = true;
        }
        else
        {
            DoRearchGoal = false;
        }
    }

    void UpdateState(SkeletonState newstate)
    {
        switch (newstate)
        {
            case SkeletonState.Stroll:
                curState = SkeletonState.Stroll;
                break;
            case SkeletonState.MoveTowardsPlayer:
                curState = SkeletonState.MoveTowardsPlayer;
                break;
            case SkeletonState.Attack:
                curState = SkeletonState.Attack;
                break;
            case SkeletonState.Flee:
                curState = SkeletonState.Flee;
                break;
        }
    }

    public void SetHp(int changedhp)
    {
        hp = changedhp;
    }

    void getDamage(int damage)
    {
        hp -= damage;
        Debug.Log(hp);
    }

    void heal(int heal)
    {
        hp += heal;
        Debug.Log(hp);
    }
}
