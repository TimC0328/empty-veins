using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public string Name;
    public Types type;

    public bool[] conditions;

    public bool cutsceneTrigger;

    public enum Types {Pickup, Door, Button}

    public string[] interactText = new string[2];

    public virtual void Interact()
    {
        Debug.Log("Interacting with: " + Name);

        if (conditions.Length > 0)
        {
            foreach (bool condition in conditions)
            {
                if (condition == false)
                {
                    Debug.Log("failed");
                    return;
                }
                GameObject.Find("Player").GetComponent<Player>().StartDialogue();
            }
        }
    }


}
