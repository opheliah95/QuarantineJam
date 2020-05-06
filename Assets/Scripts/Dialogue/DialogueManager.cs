using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DialogueManager : MonoBehaviour {

    public GameObject dialogueBox;
	public TextMeshProUGUI nameText;
	public TextMeshProUGUI dialogueText;
    public GameObject characterImage;

	public Animator animator;
    public GameObject instructions;

    public static bool dialogueEnd = false;
    public static bool sentenceEnd = false;
	private Queue<string> sentences = new Queue<string>();

    [SerializeField]
    Queue<Dialogue> dialogueChunks = new Queue<Dialogue>();

    [SerializeField]
    Dialogue dequedDialogue;

  
    public void setinstruction(string action)
    {
        instructions.SetActive(true);
        instructions.GetComponent<TextMeshProUGUI>().text = action;

    }

    public void startDialogue(List<Dialogue> dialogues)
    {
       
        if (dialogueBox == null) return;

        // if player is talking then  he cannot move
        PlayerManager.isTalking = true;

        dialogueBox.SetActive(true);
        // clear current dialogue
        if(dialogueChunks.Count != 0)
            dialogueChunks.Clear();

        // add dialgoues again to the queue
        foreach (Dialogue chunks in dialogues)
        {
            dialogueChunks.Enqueue(chunks);
        }

        nextChunk(); // read next chunk of dialogue

    }

    public void nextChunk()
    {
       
        if (dialogueChunks.Count > 0)
        {
            // drop the first dialogue chunk
            Dialogue chunk = dialogueChunks.Dequeue();
            dequedDialogue = chunk; // assign it to dialogue to be removed

            // code to handle animation
            if (chunk.animationAssociated.Length != 0)
            {
                StartCoroutine(playActionAttachedWithDialogue(chunk.objectItself, chunk.animationAssociated));
            }


            nameText.GetComponent<TextMeshProUGUI>().text = chunk.name;
            characterImage.GetComponent<Image>().sprite = chunk.characterImage;

            StartIndividualDialogue(chunk);

            if (chunk.instructionsFollowed.Length != 0)
                setinstruction(chunk.instructionsFollowed);

        }
        else
        {
            EndDialogue();

        }

    }

    public void StartIndividualDialogue (Dialogue dialogue)
	{
		//animator.SetBool("IsOpen", true);

		nameText.text = dialogue.name;

		sentences.Clear();

		foreach (string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}

		DisplayNextSentence();
	}

	public void DisplayNextSentence ()
	{
		if (sentences.Count == 0)
		{
            nextChunk(); // move on to the next chunk
			return;
		}

		string sentence = sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
	}

	IEnumerator TypeSentence (string sentence)
	{
		dialogueText.text = "";
        sentenceEnd = false;
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return null;
		}
        sentenceEnd = true;
	}

	public void EndDialogue()
	{
        dialogueBox.SetActive(false);
        dialogueEnd = true;
        sentenceEnd = true;
        PlayerManager.isTalking = false;
		//animator.SetBool("IsOpen", false);
	}

    public void branchedDialogueOption(List<Dialogue> dialogues, int index, string newSentences)
    {
        Debug.Log(newSentences);
        dialogues[index].sentences[0] = newSentences;
    }

    public void CountDownForDialogue(List<Dialogue> dialogues, float seconds = 0.5f)
    {
        StartCoroutine(countDownFunc(seconds, dialogues));
    }

    IEnumerator countDownFunc(float seconds, List<Dialogue> dialogues)
    {
        yield return new WaitForSeconds(seconds);

        startDialogue(dialogues);
    }

    IEnumerator playActionAttachedWithDialogue(GameObject obj, string action)
    {
        obj.GetComponent<Animator>().SetTrigger(action);
        yield return new WaitForSeconds(0.8f);
    }

}
