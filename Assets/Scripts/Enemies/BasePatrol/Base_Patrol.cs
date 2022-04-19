
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
    int speed = 1;

    private void Awake()
    {
        Init_BasePatrol();
    }

    private void Start()
    {
        Point_A.InitPosition();
        Point_B.InitPosition();
        GoingTowards = Point_A;
    }
    void Update()
    {
        if(canMove) Patrol();
    }


    void Patrol()
    {
        if(Vector3.Distance(EnemyObject.transform.position , GoingTowards.position) < Point_A.radius)
            GoingTowards = GoingTowards == Point_A ? Point_B : Point_A;

        direction = GoingTowards.point.transform.position - EnemyObject.transform.position;
        EnemyObject.transform.position += direction.normalized * speed * 0.01f;

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
}
