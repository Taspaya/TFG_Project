using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{   
    public void EnableTimeLine(int n)
    {
        GameManager.Instance.timelines[n].myTimeline.SetActive(true);
    } 

    public void DisableTimeLine(int n)
    {
        GameManager.Instance.timelines[n].myTimeline.SetActive(false);
    }

    public void SetPlayerCanMove(bool value)
    {
        PlayerController.Instance.SetCanWalk(value);
    }

    public void HealPlayer()
    {
        if(PlayerController.Instance.currentLife < PlayerController.Instance.maxLife)
        {
            PlayerController.Instance.currentLife++;
            GameManager.Instance.UI_Manager.FillHeart(PlayerController.Instance.currentLife-1);
        }
    }

    public void TriggerUI(string value)
    {
        GameManager.Instance.UI_Manager.TriggerDialogue(value);
    }

    public void PlayerPickup()
    {
        PlayerController.Instance.GetComponentInChildren<Animator>().SetTrigger("PickUp");
        PlayerController.Instance.ShowBigSword();
    }
}
