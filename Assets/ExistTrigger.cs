using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExistTrigger : MonoBehaviour
{
    GameObject parentObj;
    Enemy enemy;
    BoxCollider boxCollider;

    float existRange;

    void Start()
    {
        parentObj = gameObject.transform.parent.gameObject;
        if (parentObj != null)
        {
            enemy = parentObj.GetComponent<Enemy>();
        }
        boxCollider = GetComponent<BoxCollider>();

        if (boxCollider == null)
        {
            Debug.LogError("Box Collider가 없습니다!");
        }

        existRange = 80f;

        SetBoxSize();
    }

    public void SetBoxSize()
    {
        boxCollider.size = new Vector3(existRange, existRange, existRange);
    }

    public float GetVisibleRange()
    {
        return existRange;
    }

    public void SetVisibleRange(float newvisibleRange)
    {
        this.existRange = newvisibleRange;
        SetBoxSize();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enemy.ExistTriggerEnter();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enemy.ExistTriggerStay();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enemy.ExistTriggerExit();
        }
    }
}
