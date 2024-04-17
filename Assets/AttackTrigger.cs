using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : Enemy
{
    BoxCollider boxCollider;

    float attackRange;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();

        if (boxCollider == null)
        {
            Debug.LogError("Box Collider가 없습니다!");
        }

        attackRange = 10f;

        SetBoxSize();
    }

    public void SetBoxSize()
    {
        boxCollider.size = new Vector3(attackRange, attackRange, attackRange);
    }

    public float GetVisibleRange()
    {
        return attackRange;
    }

    public void SetVisibleRange(float newvisibleRange)
    {
        this.attackRange = newvisibleRange;
        SetBoxSize();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SetVisibleRange(attackRange + 2.0f);
            changeColor(Color.red);
            SetEnemyState(EnemyFSM.Attack);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        SetEnemyState(EnemyFSM.Attack);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SetVisibleRange(attackRange - 2.0f);
            changeColor(Color.yellow);
            SetEnemyState(EnemyFSM.MoveTowardsPlayer);
        }
    }
}
