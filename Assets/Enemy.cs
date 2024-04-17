using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{

    Vector3 targetLocation;

    Transform player;

    float step;

    //float visibleRange;

    //float attackRange;

    Renderer rend;

    [SerializeField] float hp;

    protected enum EnemyFSM
    {
        Stroll = 0,
        MoveTowardsPlayer,
        Attack,
        Flee,
        End
    }

    EnemyFSM state;

    protected EnemyFSM GetEnemyState()
    {
        return state;
    }

    public void SetTargetLocation(Vector3 newtargetLocation)
    {
        this.targetLocation = newtargetLocation;
    }

    public Vector3 getRandomLocation(float radius)
    {
        return transform.position + new Vector3(Random.Range(radius * -1f, radius * 1f), 0f, Random.Range(radius * -1f, radius * 1f));
    }

    public void changeColor(Color newColor)
    {
        rend.material.SetColor("_Color", newColor);
    }

    public float GetHp()
    {
        return hp;
    }
    void Start()
    {
        player = GameObject.Find("Player").transform;
        rend = GetComponent<Renderer>();
        changeColor(Color.white);
        state = EnemyFSM.Stroll;
        SetTargetLocation(getRandomLocation(3f));
        //attackRange = 5f;
        //visibleRange = 10f;
        step = 1f;
        hp = 100f;
    }

    //Update the enemy by giving it a new state
    void Update()
    {
        if (Input.GetKey(KeyCode.H))
        {
            heal(20);
        }
        if (Input.GetKey(KeyCode.T))
        {
            getDamage(20);
        }

        //switch (state)
        //{
        //    case EnemyFSM.Stroll:
        //        if (Vector3.Distance(player.position, transform.position) < visibleRange)
        //        {
        //            visibleRange += 5f;
        //            changeColor(Color.yellow);
        //            state = EnemyFSM.MoveTowardsPlayer;
        //            break;
        //        }
        //        if (Vector3.Distance(transform.position, targetLocation) < Mathf.Epsilon)
        //        {
        //            targetLocation = getRandomLocation(3f);
        //        }
        //        transform.position = Vector3.MoveTowards(transform.position, targetLocation, step * Time.deltaTime);
        //        break;

        //    case EnemyFSM.MoveTowardsPlayer:
        //        if (Vector3.Distance(player.position, transform.position) < attackRange)
        //        {
        //            attackRange += 1f;
        //            changeColor(Color.red);
        //            state = EnemyFSM.Attack;
        //            break;
        //        }
        //        if (Vector3.Distance(player.position, transform.position) > visibleRange)
        //        {
        //            visibleRange = 10f;
        //            targetLocation = getRandomLocation(3f);
        //            changeColor(Color.white);
        //            state = EnemyFSM.Stroll;
        //            break;
        //        }
        //        transform.position = Vector3.MoveTowards(transform.position, player.position, step * Time.deltaTime);
        //        break;

        //    case EnemyFSM.Attack:
        //        if (hp < 20f)
        //        {
        //            targetLocation = transform.position + (transform.position - player.position).normalized * 5f;
        //            changeColor(Color.green);
        //            state = EnemyFSM.Flee;
        //            break;
        //        }
        //        if (Vector3.Distance(player.position, transform.position) > attackRange)
        //        {
        //            attackRange -= 1f;
        //            changeColor(Color.yellow);
        //            state = EnemyFSM.MoveTowardsPlayer;
        //            break;
        //        }
        //        break;

        //    case EnemyFSM.Flee:
        //        if (hp > 60f)
        //        {
        //            targetLocation = transform.position + (transform.position - player.position).normalized * 5f;
        //            changeColor(Color.red);
        //            state = EnemyFSM.Attack;
        //            break;
        //        }
        //        if (Vector3.Distance(transform.position, targetLocation) < Mathf.Epsilon)
        //        {
        //            targetLocation = transform.position + (transform.position - player.position).normalized * 5f;
        //        }
        //        hp += 0.02f;
        //        //Move
        //        transform.position = Vector3.MoveTowards(transform.position, targetLocation, step * Time.deltaTime);
        //        break;
        //}
    }

    protected void SetEnemyState(EnemyFSM fsm)
    {
        state = fsm;
        switch (state)
        {
            case EnemyFSM.Stroll:
                SetTargetLocation(getRandomLocation(3f));
                transform.position = Vector3.MoveTowards(transform.position, targetLocation, step * Time.deltaTime);
                break;

            case EnemyFSM.MoveTowardsPlayer:
                transform.position = Vector3.MoveTowards(transform.position, player.position, step * Time.deltaTime);
                break;

            case EnemyFSM.Attack:
                if (hp < 20f)
                {
                    targetLocation = transform.position + (transform.position - player.position).normalized * 5f;
                    changeColor(Color.green);
                    state = EnemyFSM.Flee;
                    break;
                }
                break;

            case EnemyFSM.Flee:
                if (hp > 60f)
                {
                    targetLocation = transform.position + (transform.position - player.position).normalized * 5f;
                    changeColor(Color.red);
                    state = EnemyFSM.Attack;
                    break;
                }
                if (Vector3.Distance(transform.position, targetLocation) < Mathf.Epsilon)
                {
                    targetLocation = transform.position + (transform.position - player.position).normalized * 5f;
                }
                hp += 0.02f;
                //Move
                transform.position = Vector3.MoveTowards(transform.position, targetLocation, step * Time.deltaTime);
                break;
        }
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