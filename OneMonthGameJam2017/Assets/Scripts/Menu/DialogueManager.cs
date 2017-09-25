using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    public Text nameText;
    public Text dialogueText;

    public Animator animator;
    public Queue <string> sentences;

	public PlayerController playerController;


	// Use this for initialization
	void Start () {

        sentences = new Queue<string> ();
	}

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Space)) {
			DisplayNextSentence ();
			Debug.Log ("spacePressed");
		}
	}
		
	
	public void StartDialogue (Dialogue dialogue)

    {
		animator.SetBool("IsOpen", true);

		playerController.canMove = false;

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
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));

        dialogueText.text = sentence;

    }

    IEnumerator TypeSentence (string sentence)

    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;

        }

    }

    void EndDialogue ()

    {

        animator.SetBool("IsOpen", false);
		playerController.canMove = true;
    }
}
