using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeftCommand : Command
{
    Transform _transform;
    float step = 5f;


    //public MoveBackwardCommand(Transform newtransform) : base(newtransform){}
    public MoveLeftCommand(Transform newtransform) { _transform = newtransform; }
    public override void Execute()
    {
        moveLeft();
    }

    private void moveLeft()
    {
        Vector3 targetLocation = _transform.position + new Vector3(-1f, 0f, 0f);
        _transform.position = Vector3.MoveTowards(_transform.position, targetLocation, step * Time.deltaTime);
    }
}
