using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForwardCommand : Command
{
    Transform _transform;
    float step = 5f;


    //public MoveBackwardCommand(Transform newtransform) : base(newtransform){}
    public MoveForwardCommand(Transform newtransform) { _transform = newtransform; }
    public override void Execute()
    {
        moveForwardward();
    }

    private void moveForwardward()
    {
        Vector3 targetLocation = _transform.position + new Vector3(0f, 0f, 1f);
        _transform.position = Vector3.MoveTowards(_transform.position, targetLocation, step * Time.deltaTime);
    }
}
