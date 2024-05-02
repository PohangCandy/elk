using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command : MonoBehaviour
{
    //protected Transform _transform;

    //public Command(Transform newTransform) { this._transform = newTransform; }

    public virtual void Execute() { }
}
