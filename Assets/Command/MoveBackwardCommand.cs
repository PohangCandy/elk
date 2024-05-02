using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class MoveBackwardCommand : Command
{
    Transform _transform;
    float step = 5f;

    //public MoveBackwardCommand(Transform newtransform) : base(newtransform){}
    public MoveBackwardCommand(Transform newtransform) { _transform = newtransform; }
    public override void Execute()
    {
        moveBackward();
    }

    private void moveBackward()
    {
        Vector3 targetLocation = _transform.position + new Vector3(0f, 0f, -1f);
        _transform.position = Vector3.MoveTowards(_transform.position, targetLocation, step * Time.deltaTime);
    }
}
