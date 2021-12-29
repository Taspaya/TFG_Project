using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cs_DestroyAfterCreated : MonoBehaviour
{

    [SerializeField]
    float timeBeforeDestroy = 1;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyAfterCreated(timeBeforeDestroy));
    }


    IEnumerator DestroyAfterCreated(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
