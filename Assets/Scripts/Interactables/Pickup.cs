using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Pickup : Interactable
{
    public Camera targetCamera = null;

    public Item item;

    void Awake()
    {
        if (targetCamera != null)
        {
            this.transform.LookAt(targetCamera.transform, Vector3.up);
        }
    }

    public override void Interact()
    {
        base.Interact();
        bool pickedUp = Inventory.instance.Add(item);
        if(pickedUp)
            Object.Destroy(gameObject);
    }
}
