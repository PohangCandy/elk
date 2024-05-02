using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    // Start is called before the first frame update
    private Command button_W;
    private Command button_A;
    private Command button_S;
    private Command button_D;
    private Command mouse_left;
    Transform _transform;

    public Transform target = null;

    void handleinput() {
        if (Input.GetKey(KeyCode.W)) { this.button_W.Execute(); }
        else if (Input.GetKey(KeyCode.A)) { this.button_A.Execute(); }
        else if (Input.GetKey(KeyCode.S)) { this.button_S.Execute(); }
        else if (Input.GetKey(KeyCode.D)) { this.button_D.Execute(); }
        else if (Input.GetMouseButtonDown(0)) { this.mouse_left.Execute(); }
    }

    private void Start()
    {
        _transform = transform;
        button_W = new MoveForwardCommand(_transform);
        button_A = new MoveLeftCommand(_transform);
        button_S = new MoveBackwardCommand(_transform);
        button_D = new MoveRightCommand(_transform);
        mouse_left = new MoveToCommand(_transform);
    }


    private void Update()
    {
        handleinput();
    }
}
