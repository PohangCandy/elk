using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibleTrigger : MonoBehaviour
{
    GameObject parentObj;
    BoxCollider boxCollider;
    Enemy enemy;

    float visibleRange;

    void Start()
    {
        parentObj = gameObject.transform.parent.gameObject;
        if(parentObj != null)
        {
            enemy = parentObj.GetComponent<Enemy>();
        }

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
        //Debug.Log(parentObj);
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
            enemy.VisibleTriggerEnter();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enemy.VisibleTriggerStay();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SetVisibleRange(visibleRange - 10.0f);
            enemy.VisibleTriggerExit();
        }
    }
}
