using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    public Vector3 destination;

    public bool isLocal;

    public Player player;

    public override void Interact()
    {
        base.Interact();
        Debug.Log("Transporting player to: " + destination);
        player.SetPlayerState("Pause");
        if (isLocal)
            player.SetPlayerPos(destination);
        else
            Debug.Log("Changing room");
        player.SetPlayerState("Default");

    }
}
