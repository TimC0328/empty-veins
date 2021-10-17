using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Typewriter : MonoBehaviour
{
    public float delay = 0;


    public void TypewriterEffect(Text textBox, string text, string[] textArray)
    {
        textBox.text = "";

        if (text != null)
        {
            StartCoroutine(writeDelay(textBox, text));
            return;
        }

        for (int i = 0; i < textArray.Length; i++)
        {
            StartCoroutine(writeDelay(textBox, textArray[i]));
        }

    }

    IEnumerator writeDelay(Text textBox, string text)
    {
        foreach (char character in text)
        {
            textBox.text += character;
            yield return new WaitForSeconds(0.01f + delay);
        }
        textBox.text += "\n";

    }

}