using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MoveToCommand : Command
{
    Transform _transform;
    float step = 5f;

    //int xPos;
    //int yPos;

    //public MoveToCommand(Transform newtransform, int xPos, int yPos)
    //{
    //    _transform = newtransform;
    //    this.xPos = xPos;
    //    this.yPos = yPos;
    //}

    public MoveToCommand(Transform newtransform)
    {
        _transform = newtransform;
    }


    public override void Execute()
    {
        moveTo();
    }

    private void moveTo()
    {
        Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
        point.y = 1f;
        if (Input.GetMouseButtonDown(0))
        {
            
            Vector3 pos = Input.mousePosition;
            pos.z = Camera.main.farClipPlane;
            Vector3 targetlocation = Camera.main.ScreenToWorldPoint(pos) + new Vector3(0f, 2f, 0f);
            //StartCoroutine(moveTowardMousePos(targetlocation));
        }
    }

    //IEnumerator moveTowardMousePos(Vector3 targetlocation)
    //{
    //    yield return new WaitForSeconds(2.0f);

    //    _transform.position = Vector3.MoveTowards(_transform.position, targetlocation, step * Time.deltaTime);
    //}
}
