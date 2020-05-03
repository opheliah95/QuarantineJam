using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{
    public GameObject text;
    public bool canRestart;
    public float timer = 1.0f;
    public string sceneName = "CutScene";

    private void Start()
    {
        Invoke("Restart", timer);
    }

    void Restart()
    {
        text.SetActive(true);
        canRestart = true;
    }

    private void Update()
    {
        if(canRestart)
        {
            if(Input.anyKeyDown)
            {
                SceneManager.LoadScene(sceneName);
            }
        }
    }
}
