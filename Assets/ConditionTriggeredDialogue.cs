using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionTriggeredDialogue : MonoBehaviour
{
    public List<Dialogue> dialogues;

    private void OnDestroy()
    {
        FindObjectOfType<DialogueManager>().startDialogue(dialogues);
    }
}
