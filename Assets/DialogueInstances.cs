using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInstances : MonoBehaviour
{
    public string characterName;
    public string[] sentences;

    [SerializeField]
    Dialogue dialogueManager;

    public bool activated;

    [SerializeField]
    public GameObject dialogueBox;

    private void Start()
    {
        dialogueManager = GameObject.FindWithTag("Dialogue").GetComponent<Dialogue>();
        //dialogueBox = GameObject.FindGameObjectWithTag("DialogueBox");
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && !activated)
        {
            dialogueBox.SetActive(true);
            dialogueManager.dialogueObject = gameObject;
            dialogueManager.characterName = characterName;
            dialogueManager.sentences = sentences;
            dialogueManager.startTyping();
            activated = true;
        }
    }


}
