using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))] 
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
    }

    // Update is called once per frame
    void Update()
    {
        MoveToNextPoint();

        if(health <= 0)
        {
            //Decrease meter by a certain amount on death
            FindObjectOfType<DarkMeter>().StartCoroutine(FindObjectOfType<DarkMeter>().addMeter2(-10));
            Destroy(this.gameObject);
        }
    }

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

    private void Reset()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }


    // Damage player
    /*
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            //Debug.Log($"{name} Triggered");
            FindObjectOfType<HealthBar>().LoseLife();
        }
    }
    */

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

    public IEnumerator knockback(float duration, float power, float direction)
    {
        float lastSpeed = speed;
        speed = 0f;
        GetComponent<BoxCollider2D>().enabled = false;

        Vector3 startPos = transform.position;
        Vector3 endPos = new Vector3(transform.position.x + (power * direction), transform.position.y);
        float timeElapsed = 0f;

        while(transform.position != endPos)
        {
            if (timeElapsed < duration)
            {
                transform.position = Vector2.Lerp(startPos, endPos, (1 - (Mathf.Pow(1 - (timeElapsed / duration), 2))));
                timeElapsed += Time.deltaTime;
                yield return null;
            }
            else
                transform.position = endPos;
            //Debug.Log("Time elapsed = " + timeElapsed);
        }
        transform.position = endPos;

        GetComponent<BoxCollider2D>().enabled = true;
        speed = lastSpeed;
    }

}



/*

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
*/