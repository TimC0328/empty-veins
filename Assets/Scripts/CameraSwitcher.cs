using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera current;
    public Camera target;

    void OnTriggerExit(Collider other)
    {
        target.enabled = true;
        current.enabled = false;

        Camera temp = current;
        current = target;
        target = temp;
    }
        
}
