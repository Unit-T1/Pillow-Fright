using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //[SerializeField] float moveSpeed = 1f;
    //Rigidbody2D enemyRB;

    public int health = 100;

    [SerializeField] List<Vector3> points;
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

        if(health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    /*
    public override void CollideHorizontal(Collider2D other)
    {
        desiredx = -desiredx;
    }
    */

    void MoveToNextPoint()
    {
        Vector3 goalPoint = points[nextID];
        // Flip enemy direction 
        if (goalPoint.x > transform.position.x)
            GetComponent<SpriteRenderer>().flipX = false;
        else
            GetComponent<SpriteRenderer>().flipX = true;
        // Move enemy to target
        transform.position = Vector2.MoveTowards(transform.position, goalPoint, speed*Time.deltaTime);
        // Check the distance betwen enemy and goal point to trigger next point
        if(Vector2.Distance(transform.position, goalPoint) < 0.01f)
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

    public void addCurrentPosition()
    {
        Vector3 position = new Vector3();
        position = GetComponent<Transform>().position;
        position.x = Mathf.Round(position.x * 2f) * 0.5f;
        position.y = Mathf.Round(position.y * 2f) * 0.5f;
        position.z = Mathf.Round(position.z * 2f) * 0.5f;
        points.Add(position);
        Debug.Log("Added Position: " + position);
    }

    public void damage(int damage)
    {
        health -= damage;
    }

}





[CustomEditor(typeof(EnemyController))]
public class EnemyControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EnemyController EC = (EnemyController)target;
        if (GUILayout.Button("Add Current Position"))
        {
            EC.addCurrentPosition();
        }
    }
}