using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreeperMovement : MonoBehaviour
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

    private enum CreeperState
    {
        Stroll, MoveTowardsPlayer, Attack, Flee
    }

    private CreeperState curState;
    private CreeperState prevState;



    private void Start()
    {
        hp = 100;
        playerpos = player.GetComponent<PlayerMovement>().GetPlayerPos();
        originPos = new Vector3(10.0f, 0.5f, 0.0f);
        speed = 5;
        DoRearchGoal = true;
        curState = CreeperState.Flee;
        damage = 10;
    }

    private void Update()
    {
        switch (curState)
        {
            case CreeperState.Stroll:
                if (!DoRearchGoal)
                {
                    Move(gameObject.transform.position, destinationPos);
                }
                else
                {
                    SetRandomDirection(10);
                }
                if (Vector3.Distance(gameObject.transform.position, playerpos) < 10)
                {
                    UpdateState(CreeperState.MoveTowardsPlayer);
                }
                break;
            case CreeperState.MoveTowardsPlayer:
                Move(gameObject.transform.position, player.GetComponent<PlayerMovement>().GetPlayerPos());
                prevState = CreeperState.MoveTowardsPlayer;
                if (Vector3.Distance(gameObject.transform.position, playerpos) < 1)
                {
                    UpdateState(CreeperState.Attack);
                }
                if (Vector3.Distance(gameObject.transform.position, playerpos) >= 15)
                {
                    UpdateState(CreeperState.Stroll);
                }
                break;
            case CreeperState.Attack:
                Attack(player);
                prevState = CreeperState.Attack;
                if (hp <= 20)
                {
                    UpdateState(CreeperState.Flee);
                }
                if (Vector3.Distance(gameObject.transform.position, playerpos) > 2)
                {
                    UpdateState(CreeperState.MoveTowardsPlayer);
                }
                break;
            case CreeperState.Flee:
                Flee();
                if (hp >= 60)
                {
                    UpdateState(CreeperState.Stroll);
                }
                break;
        }
    }


    private void Flee()
    {
        Vector3 fleepos = new Vector3(playerpos.x + 10, playerpos.y, playerpos.z + 10);
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

        int randomnum = UnityEngine.Random.Range(0, 4);

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

    void UpdateState(CreeperState newstate)
    {
        switch (newstate)
        {
            case CreeperState.Stroll:
                curState = CreeperState.Stroll;
                break;
            case CreeperState.MoveTowardsPlayer:
                curState = CreeperState.MoveTowardsPlayer;
                break;
            case CreeperState.Attack:
                curState = CreeperState.Attack;
                break;
            case CreeperState.Flee:
                curState = CreeperState.Flee;
                break;
        }
    }
}
