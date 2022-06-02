using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    [SerializeField]
    GameObject dialogueCanvas;
    [SerializeField]
    GameObject objectiveCanvas;
    [SerializeField]
    Text currentDialogueText;
     
    [System.NonSerialized]
    public SimpleDialogue currentSimpleDialogue;


    [SerializeField]
    public GameObject[] PlayerLife;
    //public void ShowUIDialogue()
    //{
    //    dialogueCanvas.SetActive(true);
    //}
    //public void HideUIDialogue()
    //{
    //    dialogueCanvas.SetActive(false);
    //}
    public void WriteDialogue(string text)
    {
        currentDialogueText.text = text;
    }
    public void TriggerDialogue(string value)
    {
        dialogueCanvas.GetComponentInChildren<Animator>().SetTrigger(value);
    }

    public void ManageLife()
    {
        int rdm = (int)Random.Range(-1000.0f, 100.0f);

        if (rdm >= 0 && rdm < PlayerLife.Length && rdm < PlayerController.Instance.currentLife) PlayerLife[rdm].GetComponent<Animator>().SetTrigger("Jump");
    }


    private void Update()
    {
        if (GameManager.Instance.GetCurrentGameState() == GameManager.GameState.Dialogue)
            if (Input.GetButtonDown("Attack")) currentSimpleDialogue.NextDialogue();

        ManageLife();
    }

    public void HitHeart(int v)
    {
        PlayerLife[v].GetComponent<Animator>().SetTrigger("Hit");
    }

    public void FillHeart(int v) {
        PlayerLife[v].GetComponent<Animator>().SetTrigger("Heal");
    } 
}
