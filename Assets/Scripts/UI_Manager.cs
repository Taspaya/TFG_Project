using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    [SerializeField]
    GameObject dialogueCanvas;

    [SerializeField]
    Text currentDialogueText;

    [System.NonSerialized]
    public SimpleDialogue currentSimpleDialogue;
    public void ShowUIDialogue()
    {
        dialogueCanvas.SetActive(true);
    }
    public void HideUIDialogue()
    {
        dialogueCanvas.SetActive(false);
    }
    public void WriteDialogue(string text)
    {
        currentDialogueText.text = text;
    }

    private void Update()
    {
        if (GameManager.Instance.GetCurrentGameState() == GameManager.GameState.Dialogue)
            if (Input.GetButtonDown("Attack")) currentSimpleDialogue.NextDialogue();
    }

}
