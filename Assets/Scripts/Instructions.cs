using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instructions : MonoBehaviour
{
    private void OnEnable()
    {
        Debug.Log("enabled");
        Invoke("Disappear", 3.0f);
    }

    private void Disappear()
    {
        gameObject.SetActive(false);
    }
}
