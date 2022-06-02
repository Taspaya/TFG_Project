using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadSceneObject : MonoBehaviour
{

    [SerializeField]
    string sceneToLoad;

    [SerializeField]
    string sceneToUnload = "NONE";

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Additive);
            if(sceneToUnload != "NONE") SceneManager.UnloadSceneAsync(sceneToUnload);
            Destroy(gameObject);
        }
    }
}
