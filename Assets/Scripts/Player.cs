using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterController controller;

    public enum States {Default, Pause, Attack, Damage}

    public States state = 0;


    float speed = 12.0f;
    float turnSpeed = 120.0f;


    void Update()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        //No movement if paused
        if (state == States.Pause) 
            return;


        //Player can rotate while attacking, but must stand still;
        transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0);

        if (state == States.Attack)
            return;


        Vector3 moveDir;

        moveDir = transform.forward * Input.GetAxis("Vertical") * speed;
        // moves the character in horizontal direction
        controller.Move(moveDir * Time.deltaTime - Vector3.up * 0.1f);
    }
}