using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibleTrigger : Enemy
{
    BoxCollider boxCollider;

    float visibleRange;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();

        if (boxCollider == null)
        {
            Debug.LogError("Box Collider가 없습니다!");
        }

        visibleRange = 20f;

        SetBoxSize();
    }

    private void Update()
    {
    }

    public void SetBoxSize()
    {
        boxCollider.size = new Vector3(visibleRange, visibleRange, visibleRange);
    }

    public float GetVisibleRange()
    {
        return visibleRange;
    }

    public void SetVisibleRange(float newvisibleRange)
    {
        this.visibleRange = newvisibleRange;
        SetBoxSize();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SetVisibleRange(visibleRange + 10.0f);
            changeColor(Color.yellow); // 추격 시 노란색으로 1회 변화
            SetEnemyState(EnemyFSM.MoveTowardsPlayer);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SetEnemyState(EnemyFSM.MoveTowardsPlayer);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SetVisibleRange(visibleRange - 10.0f);
            changeColor(Color.white);
            SetTargetLocation(getRandomLocation(3f));
            SetEnemyState(EnemyFSM.Stroll);
        }
    }
}
