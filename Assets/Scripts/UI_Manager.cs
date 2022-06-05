using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    [SerializeField]
    GameObject dialogueCanvas;
    [SerializeField]
    TextMeshProUGUI dialogueText;
    [SerializeField]
    GameObject ThanksCanvas;
    [SerializeField]
    Text currentDialogueText;

    [SerializeField] GameObject DieCanvas;
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
    public void TriggerDialogueIn(string value)
    {
        dialogueCanvas.GetComponentInChildren<Animator>().SetTrigger("In");
        dialogueText.text = value;
    }
    public void TriggerDialogueOut()
    {
        dialogueCanvas.GetComponentInChildren<Animator>().SetTrigger("Out");
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
        //FillLife();
    }

    public void HitHeart(int v)
    {
        PlayerLife[v].GetComponent<Animator>().SetTrigger("Hit");
    }

    public void FillHeart(int v) {
        PlayerLife[v].GetComponent<Animator>().SetTrigger("Heal");
        PlayerLife[v].GetComponent<Animator>().ResetTrigger("Hit");
    }
    
    public void FillLife()
    {
       for(int i = 0; i <= PlayerController.Instance.currentLife-1; i++)
        {
            FillHeart(i);
        }
    }

    public void EnableDieCanvas()
    {
        DieCanvas.SetActive(true);
    }

    public void DisableDieCanvas()
    {
        DieCanvas.SetActive(false);
    }

    public void EnableThanksCanvas()
    {
        ThanksCanvas.SetActive(true);
    }
}
