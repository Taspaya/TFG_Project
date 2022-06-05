using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerController : Cs_CombatUnit
{
    [Header(" ======= Run Settings =========")]
    [Tooltip("Player Run Speed")]
    [SerializeField]
    public float speed = 2;

    [Header(" ======= COMBAT =========")]
    [SerializeField]
    int actualLife = 4;
    [Tooltip("Position of the hand")]
    [SerializeField]
    Transform handPos;
    [Tooltip("Mask of the enemies ")]
    [SerializeField]
    LayerMask EnemyMask;
    [SerializeField]
    float hitCheckRadius = 0f;

    [SerializeField]
    GameObject BigSword;
    float wallCheckRadius = 0.1f;
    //[Header(" ======= Left & Right Limits =========")]
    //bool isLeftLimited = false;
    //bool isRightLimited = false;

    bool canWalk = true;

    [SerializeField]
    [Tooltip("Mask for the ground and walls")]
    LayerMask wallMask;

    //[SerializeField]
    //[Tooltip("Position of the left limit")]
    //Transform leftLimitPos;
    //[SerializeField]
    //[Tooltip("Position of the right limit")]
    //Transform rightLimitPos;
    [SerializeField]
    LayerMask destructibleMask;
    [Header(" ======= Jump Settings =========")]
    [Tooltip("How long the player can keep going upwards when jumping")]
    public float jumpTime = 0.5f;
    [SerializeField]
    [Tooltip("Radius of the isGrouded collider checker")]
    float checkRadius = 1;

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

    private void Start()
    {
        currentLife = actualLife;
        maxLife = actualLife;
    }
     
    private void Update()
    {
        //CheckSideLimits();
        if (GameManager.Instance.GetCurrentGameState() == GameManager.GameState.Playing) ManageAnimations();
    } 
    
    public override void Attack()
    {
        CheckForEnemies();
        CheckForDestructibles();
    }

    public void ShowBigSword()
    {
        BigSword.SetActive(true);
        GetComponentInChildren<Animator>().SetBool("HasSword", true);
        handPos = BigSword.transform;
        currentDamage = 3;
    }
    void CheckForEnemies()
    {
        Collider[] enemiesArray = Physics.OverlapSphere(handPos.position, hitCheckRadius, EnemyMask);

        if (enemiesArray.Length > 0)
            foreach (var item in enemiesArray)
            {
                Cs_CombatUnit enemy = item.GetComponentInParent<Base_Patrol>();
                
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

    //void CheckSideLimits()
    //{
    //    isLeftLimited = Physics.OverlapSphere(leftLimitPos.position, wallCheckRadius, wallMask).Length > 0;
    //    isRightLimited = Physics.OverlapSphere(rightLimitPos.position, wallCheckRadius, wallMask).Length > 0;
    //}
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(handPos.position, hitCheckRadius);
        Gizmos.color = Color.green;
        //Gizmos.DrawWireSphere(rightLimitPos.position, wallCheckRadius);
        //Gizmos.DrawWireSphere(leftLimitPos.position, wallCheckRadius);
    }


    //public bool GetIsLeftLimited() { return isLeftLimited; }
    //public bool GetIsRightLimited() { return isRightLimited; }
    public bool GetCanWalk() { return canWalk; }
    public void SetCanWalk(bool value) { canWalk = value;  }

    public override void DealDamage(Cs_CombatUnit other)
    {
        base.DealDamage(other);
    }

    public override void RecieveDamage(int n)
    {
        base.RecieveDamage(n); 
        for(int i = currentLife; i < maxLife; i++)
         if(currentLife > 0) GameManager.Instance.UI_Manager.HitHeart(i);

        PlayerController.Instance.GetComponentInChildren<Animator>().SetTrigger("Hit");
    }

}
