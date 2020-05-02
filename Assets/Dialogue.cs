using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public TextMeshProUGUI characterNameDisplay;
    public GameObject continueButton;
    public GameObject dialogueObject;
    public string characterName;
    public string[] sentences;
    public float typingSpeed = 0.02f;
    private int index;


    public void startTyping()
    {
        StartCoroutine(Type());
    }
  
    IEnumerator Type()
    {
        characterNameDisplay.text = characterName;
        foreach(char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    
    private void Update()
    {
        if(textDisplay.text == sentences[index])
        {
            continueButton.SetActive(true);
        }
    }
    


   

    public void nextSentence()
    {
        continueButton.SetActive(false);
        if(index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            textDisplay.text = "...";
            dialogueObject.GetComponent<DialogueInstances>().dialogueBox.SetActive(false);
            dialogueObject.GetComponent<DialogueInstances>().activated = false;
        }
    }
}
