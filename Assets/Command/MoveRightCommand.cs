using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRightCommand : Command
{
    Transform _transform;
    float step = 5f;


    //public MoveBackwardCommand(Transform newtransform) : base(newtransform){}
    public MoveRightCommand(Transform newtransform) { _transform = newtransform; }
    public override void Execute()
    {
        moveRight();
    }

    private void moveRight()
    {
        Vector3 targetLocation = _transform.position + new Vector3(1f, 0f, 0f);
        _transform.position = Vector3.MoveTowards(_transform.position, targetLocation, step * Time.deltaTime);
    }
}
