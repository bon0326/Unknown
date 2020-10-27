using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {

    public Text nameText;
    public Text dialogText;
    public GameObject dialogbox;
    public GameObject leftIllust;
    public GameObject rightIllust;
    //public Animator animator;
    public Queue<string> sentences;

	// Use this for initialization
	void Start () {
        sentences = new Queue<string>();
	}

    public void StartDialog(Dialog dialog)
    {
        dialogbox.SetActive(true);
        //animator.SetBool("IsOpen", true);
        nameText.text = dialog.name;

        sentences.Clear();

        foreach(string sentence in dialog.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialog();
            return;
        }

       string sentence = sentences.Dequeue();
        StopAllCoroutines();
       StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogText.text += letter;
            yield return null;
        }
    }

    void EndDialog()
    {
        dialogbox.SetActive(false);
        if (leftIllust != null) leftIllust.SetActive(false);
        if (rightIllust != null) rightIllust.SetActive(false);
        //animator.SetBool("IsOpen", false);
    }
	
	// Update is called once per frame

}
