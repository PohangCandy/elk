using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    int hp;
    [SerializeField]
    int speed;

    private Vector3 direction = Vector3.zero;

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");  
        float z = Input.GetAxisRaw("Vertical");     

        direction = new Vector3(x, 0, z);
        transform.position += direction * Time.deltaTime * speed;
    }


    private void Start()
    {
        speed = 10;
        hp = 100;
    }
    public Vector3 GetPlayerPos()
    {
        return gameObject.transform.position;
    }

    public void SetHp(int changedhp)
    {
        hp = changedhp;
    }

    public void getDamage(int damage)
    {
        hp -= damage;
    }
}
