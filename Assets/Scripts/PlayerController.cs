using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Cs_CombatUnit
{
    [Header(" ======= COMBAT =========")]
    [Tooltip("Position of the hand")]
    [SerializeField]
    Transform handPos;
    [Tooltip("Mask of the enemies ")]
    [SerializeField]
    LayerMask EnemyMask;
    float hitCheckRadius = 1f;

    float wallCheckRadius = 0.1f;
    [Header(" ======= Left & Right Limits =========")]
    bool isLeftLimited = false;
    bool isRightLimited = false;

    [SerializeField]
    [Tooltip("Mask for the ground and walls")]
    LayerMask wallMask;

    [SerializeField]
    [Tooltip("Position of the left limit")]
    Transform leftLimitPos;
    [SerializeField]
    [Tooltip("Position of the right limit")]
    Transform rightLimitPos;

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

    private void Start()
    {
        InvokeRepeating("CheckSideLimits", 0f, 0.3f);
    }

    private void Update()
    {
        ManageAnimations();
        //if (Input.GetButtonDown("Attack")) Attack();
    } 
    
    public override void Attack()
    {
        Collider[] enemiesArray = Physics.OverlapSphere(handPos.position, hitCheckRadius, EnemyMask);

        if (enemiesArray.Length > 0)
            foreach (var item in enemiesArray)
            {
                Cs_CombatUnit enemy = item.GetComponent<Cs_CombatUnit>();
                if (enemy) DealDamage(enemy);
            }
    }
    void ManageAnimations()
    {
        if (Input.GetButtonDown("Attack")) GetComponentInChildren<Animator>().SetTrigger("Attack");
    }

    void CheckSideLimits()
    {
        isLeftLimited = Physics.OverlapSphere(leftLimitPos.position, wallCheckRadius, wallMask).Length > 0;
        isRightLimited = Physics.OverlapSphere(rightLimitPos.position, wallCheckRadius, wallMask).Length > 0;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(handPos.position, hitCheckRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(rightLimitPos.position, wallCheckRadius);
        Gizmos.DrawWireSphere(leftLimitPos.position, wallCheckRadius);
    }


    public bool GetIsLeftLimited() { return isLeftLimited; }
    public bool GetIsRightLimited() { return isRightLimited; }
}
