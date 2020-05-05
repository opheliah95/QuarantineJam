using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DialogueUIController : MonoBehaviour
{
    public bool isEnabled = false;

    // press any key to continue

    private void Update()
    {
        if(isEnabled && Input.anyKeyDown)
        {
            FindObjectOfType<DialogueManager>().DisplayNextSentence();
        }
    }

    // enable or disable canvas
    private void OnEnable()
    {
        isEnabled = true;
    }

    private void OnDisable()
    {
        isEnabled = false;
    }
}
