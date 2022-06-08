using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private enum State {DEFAULT, READY}
    private State state = State.DEFAULT;
    private CharacterController controller;
    private PlayerCombat combatSystem;
    private Animator animator;
    private float speed = 2.0f;
    private float turnSpeed = 120.0f;


    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        combatSystem = GetComponent<PlayerCombat>();
    }

    // Update is called once per frame
    void Update()
    {

        HandleMovement();
        HandleCombat();
    }

    void HandleMovement()
    {
        Vector3 moveDir;

        if(state == State.DEFAULT || state == State.READY)
            transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0);

        if (state != State.DEFAULT)
            return;

        if (Input.GetKey(KeyCode.LeftShift))
            speed = 5f;
        else
            speed = 2f;

        moveDir = transform.forward * Input.GetAxis("Vertical") * speed;
        // moves the character in horizontal direction

        controller.Move(moveDir * Time.deltaTime - Vector3.up * 0.1f);

        if (Input.GetKeyDown(KeyCode.W))
            StartCoroutine(WalkAnim());
        else if (Input.GetKeyUp(KeyCode.W))
            animator.SetInteger("state", 0);

        if(Input.GetKey(KeyCode.W))
        {
            if(Input.GetKey(KeyCode.LeftShift))
                animator.SetInteger("state", 2);
            if (Input.GetKeyUp(KeyCode.LeftShift))
                animator.SetInteger("state", 1);
        }


    }

    void HandleCombat()
    {
        if (state != State.DEFAULT && state != State.READY)
            return;

        if (Input.GetKey(KeyCode.Space))
        {
            state = State.READY;
            animator.SetInteger("state", 3);

            if (Input.GetMouseButtonDown(0))
                combatSystem.MakeAttack();
        }
        else if(Input.GetKeyUp(KeyCode.Space))
        {
            state = State.DEFAULT;
            if (Input.GetKey(KeyCode.W))
                StartCoroutine(WalkAnim());
            else
                animator.SetInteger("state", 0);
            combatSystem.DropCombo();
        }
    }


    private IEnumerator WalkAnim()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetInteger("state", 2);
            yield return null;
        }
        yield return new WaitForSeconds(0.1f);
        if(Input.GetKey(KeyCode.W))
            animator.SetInteger("state", 1);

    }
}
