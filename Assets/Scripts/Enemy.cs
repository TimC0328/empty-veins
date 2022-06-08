using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Weapon")
            Destroy(gameObject);
    }
}
