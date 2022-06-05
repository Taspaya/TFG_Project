using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] Transform[] checkpoints;


    public int checkpoint = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) SceneManager.LoadScene("LoadScene");
        if (Input.GetKeyDown(KeyCode.P)) Respawn();

        if (Input.GetKeyDown(KeyCode.Alpha1)) PlayerController.Instance.transform.position = checkpoints[0].position;
        if (Input.GetKeyDown(KeyCode.Alpha2)) PlayerController.Instance.transform.position = checkpoints[1].position;
        if (Input.GetKeyDown(KeyCode.Alpha3)) PlayerController.Instance.transform.position = checkpoints[2].position;
        if (Input.GetKeyDown(KeyCode.Alpha4)) PlayerController.Instance.transform.position = checkpoints[3].position;
        if (Input.GetKeyDown(KeyCode.Alpha5)) PlayerController.Instance.transform.position = checkpoints[4].position;
    }


    public void Respawn()
    {
        PlayerController.Instance.currentLife = PlayerController.Instance.maxLife;
        PlayerController.Instance.enabled = true;
        PlayerController.Instance.transform.position = checkpoints[checkpoint].position;
        PlayerController.Instance.GetComponentInChildren<Collider>().enabled = true;
        GameManager.Instance.UI_Manager.FillLife();
    }


    public void LoadCustomCheckpoint(int i)
    {
        PlayerController.Instance.transform.position = checkpoints[i].position;
    }
}
