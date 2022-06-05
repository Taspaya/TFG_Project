using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
public class AnimationEventHandler : MonoBehaviour
{
    // Start is called before the first frame update

    public UnityEvent customEvent;

    public void AnimationEventAttack()
    {
        //Called in the end of the "Push" animation of the Player
        PlayerController.Instance.Attack();
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("DEMO SCENE Standard");
    }

    public void Respawn()
    {
        GameManager.Instance.SaveManager.Respawn();
    }

    public void ExecuteCustomEvent()
    {
        customEvent.Invoke();
    }
}
