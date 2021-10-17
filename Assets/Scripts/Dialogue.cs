using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    Typewriter typewriter;

    GameObject canvas;


    void Start()
    {
        canvas = GameObject.Find("Canvas");
        typewriter = canvas.GetComponent<Typewriter>();
    }

    public void InitDialogue()
    {
        canvas.SetActive(true);
        canvas.transform.GetChild(0).gameObject.SetActive(false);
    }
}
