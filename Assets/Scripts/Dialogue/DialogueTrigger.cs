using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

	public List<Dialogue> dialogues;

	public void TriggerDialogue ()
	{
        FindObjectOfType<DialogueManager>().startDialogue(dialogues);
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && !PlayerManager.isTalking)
        {
            TriggerDialogue();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            FindObjectOfType<DialogueManager>().EndDialogue();
            PlayerManager.isTalking = false;
        }
    }
}
