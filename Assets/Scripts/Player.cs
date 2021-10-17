using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterController controller;

    public enum States {Default, Pause, Attack, Damage}
    public States state = 0;


    public float speed = 12.0f;
    public float turnSpeed = 120.0f;

    public Interactable currentInteract = null;

    public GameObject canvas;
    InventoryUI inventoryUI;

    void Start()
    {
        inventoryUI = InventoryUI.instance;
        canvas.SetActive(false);
    }

    void Update()
    {
        HandleMovement();
        HandleInput();
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

    void HandleInput()
    {
        if (Input.GetButton("Interact") && currentInteract != null)
            currentInteract.Interact();
        if (Input.GetButtonDown("Inventory"))
        {
            if (!canvas.activeSelf)
            {
                state = States.Pause;
                canvas.SetActive(true);
                canvas.transform.GetChild(0).gameObject.SetActive(true);
                inventoryUI.UpdateUI();
            }
            else
            {
                canvas.SetActive(false);
                state = States.Default;
            }

        }
    }

    void OnTriggerEnter(Collider other)
    {
        currentInteract = other.gameObject.GetComponent<Interactable>();
    }

    void OnTriggerExit(Collider other)
    {
        currentInteract = null;
    }

    public void SetPlayerState(string playerState)
    {
        switch(playerState)
        {
            case "Default":
                state = States.Default;
                break;
            case "Pause":
                state = States.Pause;
            break;
            case "Attack":
                state = States.Attack;
            break;
            case "Damage":
                state = States.Damage;
            break;
        }
    }

    public void SetPlayerPos(Vector3 pos)
    {
        transform.position = pos;
    }

    public void StartDialogue()
    {
        canvas.SetActive(true);
        canvas.transform.GetChild(1).gameObject.SetActive(true);
        canvas.transform.GetChild(1).gameObject.GetComponent<Dialogue>().InitDialogue();


    }
}