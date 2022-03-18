using System.Collections;
using System.Collections.Generic;
using Cinemachine;
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
    float hitCheckRadius = 0f;

    float wallCheckRadius = 0.1f;
    [Header(" ======= Left & Right Limits =========")]
    bool isLeftLimited = false;
    bool isRightLimited = false;

    bool canWalk = true;

    [SerializeField]
    [Tooltip("Mask for the ground and walls")]
    LayerMask wallMask;

    [SerializeField]
    [Tooltip("Position of the left limit")]
    Transform leftLimitPos;
    [SerializeField]
    [Tooltip("Position of the right limit")]
    Transform rightLimitPos;
    [SerializeField]
    LayerMask destructibleMask;

    [Header(" ======= Particles =========")]

    [SerializeField]
    public ParticleSystem[] particles;

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

    private void Update()
    {
        CheckSideLimits();
        if (GameManager.Instance.GetCurrentGameState() == GameManager.GameState.Playing) ManageAnimations();
    } 
    
    public override void Attack()
    {
        CheckForEnemies();
        CheckForDestructibles();

    }

    void CheckForEnemies()
    {
        Collider[] enemiesArray = Physics.OverlapSphere(handPos.position, hitCheckRadius, EnemyMask);

        if (enemiesArray.Length > 0)
            foreach (var item in enemiesArray)
            {
                Cs_CombatUnit enemy = item.GetComponent<Cs_CombatUnit>();
                if (enemy) DealDamage(enemy);
            }
    }
    
    void CheckForDestructibles() {

        Collider[] destructiblesArray = Physics.OverlapSphere(handPos.position, hitCheckRadius, destructibleMask);

        if (destructiblesArray.Length > 0)
            foreach (var item in destructiblesArray)
            {
                Cs_Destructible destructible = item.GetComponent<Cs_Destructible>();
                if (destructible) destructible.Hit();
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
    public bool GetCanWalk() { return canWalk; }
    public void SetCanWalk(bool value) { canWalk = value;  }
}
