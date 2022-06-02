using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    Animator otherAnimator;

    private void OnMouseEnter()
    {
        GetComponent<Animator>().SetBool("over", true);
        otherAnimator.SetBool("over", true);
    }

    private void OnMouseExit()
    {
        GetComponent<Animator>().SetBool("over", false);
        otherAnimator.SetBool("over", false);

    }

    private void OnMouseDown()
    {
        GetComponent<Animator>().SetTrigger("click");
        StartCoroutine(LoadSceneLoader());

    }

    IEnumerator LoadSceneLoader()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("LoadingScene");
    }
    }
