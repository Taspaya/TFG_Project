
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
class Point
{
    [SerializeField]
    public Transform point;
    [SerializeField]
    public float radius = 1;

    public Vector3 position { get; set; }

    public void InitPosition(){ position = point.position; }
}

public class Base_Patrol : Cs_CombatUnit
{
    [SerializeField]
    GameObject EnemyObject;

    [SerializeField]
    Point Point_A = new Point();
    [SerializeField]
    Point Point_B = new Point();

    Point GoingTowards;

    [SerializeField]
    public int actualLife ;
    [SerializeField]
    public int actualDamage;
    [SerializeField]
    bool isStopped = false;
    [SerializeField]
    int speed = 1;

    private void Awake()
    {
        Init_BasePatrol();
        maxLife = actualLife;
        currentLife = actualLife;
        currentDamage = actualDamage;
    }

    private void Start()
    {
        Point_A.InitPosition();
        Point_B.InitPosition();
        GoingTowards = Point_A;
    }
    void Update()
    {
        if(!isStopped) Patrol();
    }

    void Patrol()
    {
        
        if(Vector3.Distance(EnemyObject.transform.position , GoingTowards.position) < Point_A.radius)
            GoingTowards = GoingTowards == Point_A ? Point_B : Point_A;

        direction = GoingTowards.point.transform.position - EnemyObject.transform.position;
        EnemyObject.transform.position += direction.normalized * speed * 0.01f;


        if (GoingTowards != Point_A) EnemyObject.transform.rotation = Quaternion.Euler(-90,180,0);
        else EnemyObject.transform.rotation = Quaternion.Euler(-90,0,0);
                // = new Vector3(EnemyObject.transform.localScale.x * -1, EnemyObject.transform.localScale.y, EnemyObject.transform.localScale.z);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(Point_A.point.position, Point_A.radius);
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(Point_B.point.position, Point_B.radius);
    }

    public override void Attack()
    {
        throw new System.NotImplementedException();
    }

    public void SetIsStopped(bool val)
    {
        isStopped = val;
    }

    public override void RecieveDamage(int n)
    {
        base.RecieveDamage(n);
    }
}
