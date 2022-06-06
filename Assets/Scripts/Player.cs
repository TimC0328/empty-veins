using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;
    private float speed = 2.0f;
    private float turnSpeed = 120.0f;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDir;

        transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0);
        moveDir = transform.forward * Input.GetAxis("Vertical") * speed;
        // moves the character in horizontal direction
        controller.Move(moveDir * Time.deltaTime - Vector3.up * 0.1f);
        HandleAnimation();

    }

    void HandleAnimation()
    {
        if (Input.GetKeyDown(KeyCode.W))
            StartCoroutine(WalkAnim());
        else if (Input.GetKeyUp(KeyCode.W))
            animator.SetInteger("state", 0);
    }

    IEnumerator WalkAnim()
    {
        yield return new WaitForSeconds(0.1f);
        if(Input.GetKey(KeyCode.W))
            animator.SetInteger("state", 1);
    }
}
