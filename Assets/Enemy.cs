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

    public Transform GetPlayerTransform()
    {
        return player;
    }

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
        Debug.Log(this);
        return hp;
    }
    protected virtual void Start()
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
    private void Update()
    {
        //Debug.Log(this);
        //Debug.Log(player);

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
                changeColor(Color.white);
                SetTargetLocation(getRandomLocation(3f));
                transform.position = Vector3.MoveTowards(transform.position, targetLocation, step * Time.deltaTime);
                break;

            case EnemyFSM.MoveTowardsPlayer:
                //player = GameObject.Find("Player").transform;
                changeColor(Color.yellow);
                transform.position = Vector3.MoveTowards(transform.position, GetPlayerTransform().position, step * Time.deltaTime);
                break;

            case EnemyFSM.Attack:
                if (hp < 20f)
                {
                    //targetLocation = transform.position + (transform.position - GetPlayerTransform().position).normalized * 5f;
                    state = EnemyFSM.Flee;
                    break;
                }
                changeColor(Color.red);
                break;

            case EnemyFSM.Flee:
                if (hp > 60f)
                {
                    //targetLocation = transform.position + (transform.position - GetPlayerTransform().position).normalized * 5f;
                    state = EnemyFSM.Stroll;
                    break;
                }
                changeColor(Color.green);
                targetLocation = transform.position + (transform.position - GetPlayerTransform().position).normalized * 5f;
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

    public void VisibleTriggerEnter()
    {
        if (state == EnemyFSM.Stroll)
        {
            SetEnemyState(EnemyFSM.MoveTowardsPlayer);
        }
    }
    public void VisibleTriggerStay()
    {
        if (state == EnemyFSM.MoveTowardsPlayer || state == EnemyFSM.Stroll)
            SetEnemyState(EnemyFSM.MoveTowardsPlayer);
    }
    public void VisibleTriggerExit()
    {
        if (state == EnemyFSM.MoveTowardsPlayer)
        {
            SetEnemyState(EnemyFSM.Stroll);
            SetTargetLocation(getRandomLocation(3f));
        }
    }

    public void AttackTriggerEnter()
    {
        if (state == EnemyFSM.MoveTowardsPlayer)
        {
            SetEnemyState(EnemyFSM.Attack);
        }
    }
    public void AttackTriggerStay()
    {
        if (state == EnemyFSM.Attack || state == EnemyFSM.MoveTowardsPlayer)
            SetEnemyState(EnemyFSM.Attack);
    }
    public void AttackTriggerExit()
    {
        if (state == EnemyFSM.Attack)
        {
            SetEnemyState(EnemyFSM.MoveTowardsPlayer);
        }
    }

    public void ExistTriggerEnter()
    {
        if (state == EnemyFSM.Stroll)
        {
            SetEnemyState(EnemyFSM.Stroll);
        }
        else if (state == EnemyFSM.Flee)
        { SetEnemyState(EnemyFSM.Flee);}
    }
    public void ExistTriggerStay()
    {
        if (state == EnemyFSM.Stroll)
            SetEnemyState(EnemyFSM.Stroll);
        else if (state == EnemyFSM.Flee)
        { SetEnemyState(EnemyFSM.Flee); }
    }
    public void ExistTriggerExit()
    {
            SetEnemyState(EnemyFSM.End);
    }


}

