using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Cs_CombatUnit
{
    [SerializeField]
    Transform handPos;
    [SerializeField]
    LayerMask EnemyMask;
    float checkRadius = 1f;



    protected static PlayerController _instance;

    public static PlayerController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PlayerController>();

                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    _instance = obj.AddComponent<PlayerController>();
                }
            }
            return _instance;
        }
    }
    // Start is called before the first frame update
    
    protected void Awake()
    {
        if (_instance == null)
        {
            _instance = this as PlayerController;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            if (this != _instance)
            {
                Destroy(this.gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        ManageAnimations();
        //if (Input.GetButtonDown("Attack")) Attack();
    } 
    
    public override void Attack()
    {
        Debug.Log("ATTACKING");
        Collider[] enemiesArray = Physics.OverlapSphere(handPos.position, checkRadius, EnemyMask);

        if (enemiesArray.Length > 0)
            foreach (var item in enemiesArray) Debug.Log(item.gameObject.name);
        else Debug.Log("empty");

    }
    void ManageAnimations()
    {
        if(Input.GetButtonDown("Attack")) GetComponentInChildren<Animator>().SetTrigger("Attack");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(handPos.position, checkRadius);
    }
}
