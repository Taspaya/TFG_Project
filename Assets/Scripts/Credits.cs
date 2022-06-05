using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Credits : MonoBehaviour
{

    void Update()
    {
        if (Input.anyKey) SceneManager.LoadScene("DEMO SCENE Standard");
    }
}
