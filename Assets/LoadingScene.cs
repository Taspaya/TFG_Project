using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadingScene : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadScene("AlwaysScene", LoadSceneMode.Additive);   
    }



    private void Update()
    {
        Debug.Log(SceneManager.GetSceneByName("AlwaysScene").isLoaded + " , " + SceneManager.GetSceneByName("Lvl 0").isLoaded);
        if (SceneManager.GetSceneByName("AlwaysScene").isLoaded && SceneManager.GetSceneByName("Lvl 0").isLoaded)
        {
            SceneManager.UnloadSceneAsync("LoadScene");
        }
    }

}
