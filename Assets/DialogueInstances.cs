using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInstances : MonoBehaviour
{
    public string[] characterName;
    [TextArea(5,10)]
    public string[] sentences;

    [SerializeField]
    Dialogue dialogueManager;


    [SerializeField]
    public GameObject dialogueBox;

    [SerializeField]
    public GameObject continueButton;


    [SerializeField]
    bool activated;

    private void Start()
    {
        dialogueManager = GameObject.FindWithTag("Dialogue").GetComponent<Dialogue>();
        //dialogueBox = GameObject.FindGameObjectWithTag("DialogueBox");
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && !activated)
        {
            activated = true;
            dialogueBox.SetActive(true);
            //SettingPlayerAndEnemy(false);
            dialogueManager.index = 0;
            dialogueManager.dialogueObject = gameObject;
            dialogueManager.characterName = characterName;
            dialogueManager.sentences = sentences;
            dialogueManager.startTyping();
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("exit area");
        activated = false;
        dialogueManager.clearDialogue();
        dialogueBox.SetActive(false);
        continueButton.SetActive(true);
        //SettingPlayerAndEnemy(true);
    }

    public void SettingPlayerAndEnemy( bool con = true)
    {
        // need to fix movement bug
        // disable or enable player movement
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        player.GetComponent<PlayerManager>().enabled = con;

        foreach(GameObject enemy in enemies)
        {
            enemy.GetComponent<Enemy>().enabled = con;
        }

        // disable or enable player weapon
        GameObject hand = GameObject.FindGameObjectWithTag("Hand");
        hand.GetComponent<Rotate>().enabled = con;
        hand.GetComponent<WeaponSwitch>().enabled = con;

        foreach(Transform child in hand.transform)
        {
            if (child.gameObject.GetComponent<RangedWeapon>() != null)
                child.gameObject.GetComponent<RangedWeapon>().enabled = con;
        }

    }

   
}
