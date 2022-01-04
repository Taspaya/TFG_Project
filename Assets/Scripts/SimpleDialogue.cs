using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDialogue : InteractableBase
{
    [SerializeField]
    bool canPlayerWalk = false;

    [SerializeField]
    string[] dialogues;

    int currentDialogue = 0;


    public override void ExecuteAction()
    {
        base.ExecuteAction();
        if(currentDialogue < dialogues.Length -1)
        StartDialogue();
    }

    public void NextDialogue()
    {
        if(currentDialogue < dialogues.Length -1) 
        {
            currentDialogue++;
            GameManager.Instance.UI_Manager.WriteDialogue(dialogues[currentDialogue]);
        }
        else
        {
            GameManager.Instance.ChangeGameState(GameManager.GameState.Playing);
            GameManager.Instance.UI_Manager.HideUIDialogue();
            PlayerController.Instance.SetCanWalk(true);
        }
    }

    public void StartDialogue()
    {
        GameManager.Instance.ChangeGameState(GameManager.GameState.Dialogue);
        PlayerController.Instance.SetCanWalk(canPlayerWalk);
        GameManager.Instance.UI_Manager.ShowUIDialogue();
        GameManager.Instance.UI_Manager.currentSimpleDialogue = this;
        GameManager.Instance.UI_Manager.WriteDialogue(dialogues[currentDialogue]);
    }

    

}
