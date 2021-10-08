using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : PhysicsBase
{
    //[SerializeField] float moveSpeed = 1f;
    //Rigidbody2D enemyRB;

    public List<Transform> points;
    public int nextID = 0;
    int idChangeValue = 1;
    public float speed = 2;
    

    void Start()
    {
        //desiredx = 3;
    }

    // Update is called once per frame
    void Update()
    {
        MoveToNextPoint();
    }

    public override void CollideHorizontal(Collider2D other)
    {
        desiredx = -desiredx;
    }

    void MoveToNextPoint()
    {
        Transform goalPoint = points[nextID];
        // Flip enemy direction 
        if(goalPoint.transform.position.x > transform.position.x)
            transform.localScale = new Vector3(-1, 0.5f, 1);
        else
            transform.localScale = new Vector3(1, 0.5f, 1);
        // Move enemy to target
        transform.position = Vector2.MoveTowards(transform.position, goalPoint.position, speed*Time.deltaTime);
        // Check the distance betwen enemy and goal point to trigger next point
        if(Vector2.Distance(transform.position, goalPoint.position) < 1f)
        {
            // check if reached the target 
            if(nextID == points.Count - 1)
                idChangeValue = -1;
            if(nextID == 0)
            {
                idChangeValue = 1;
            }
            nextID += idChangeValue;
        }
    }
}
