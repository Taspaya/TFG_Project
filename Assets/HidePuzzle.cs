using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidePuzzle : MonoBehaviour
{
    

    public void EndPuzzle()
    {
        PlayerController.Instance.GetComponent<PlayerMovement>().playerMesh.transform.localPosition = new Vector3(0, 0, 0);
    }

}
