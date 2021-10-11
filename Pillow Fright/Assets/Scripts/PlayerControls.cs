using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

	public float maxSpeed = 3f;		    //Limits player speed (mostly in the air)
	public float sprintFactor = 2f; 	//Determines how fast sprinting is
	public float speed = 50f;		    //Determines player speed
	public float jumpPower = 200f;	    //Determines jump height

	public bool grounded;           	//Checks for contact with ground
	public bool sprinting;          	//Checks if player is sprinting
    public bool hasPillow;              //Checks if player has pillow

	private Rigidbody2D rb;
	private Animator anim;

	void Start()
    {
        hasPillow = false;
		rb = gameObject.GetComponent<Rigidbody2D>();	//store rigidbody component
		anim = gameObject.GetComponent<Animator>();		//store animation component (for later)
	}

    void Update()
    {
		//------------------ Movement -------------------------------

		//Jump
		if ((Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.UpArrow)) && grounded)
		{
            rb.velocity = new Vector2(rb.velocity.x, 0f);
			rb.AddForce(Vector2.up * jumpPower);
		}

		//Control Jump Height
		if ((Input.GetKeyUp(KeyCode.Z) || Input.GetKeyUp(KeyCode.UpArrow)) && rb.velocity.y >= 0.1)
		{
			rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y / 2); //Slows down y-axis momentum
		}

        //Sprint
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
			maxSpeed = maxSpeed * sprintFactor;
			speed = speed * sprintFactor;
			sprinting = true;
        }
		if (Input.GetKeyUp(KeyCode.LeftShift))
		{
			maxSpeed = maxSpeed / sprintFactor;
			speed = speed / sprintFactor;
			sprinting = false;
		}

        //----------------- Fighting --------------------------------
        if (hasPillow)
        {
            //Ground Melee
            if (Input.GetKeyDown(KeyCode.X) && grounded)
            {

            }
            //Air Melee
            if(Input.GetKeyDown(KeyCode.X) && !grounded)
            {

            }
            //Ranged AttacK?
            if (Input.GetKeyDown(KeyCode.C))
            {

            }
        }

	}

	void FixedUpdate() //More Physics-based movement
	{
		Vector3 easeVelocity = rb.velocity;
		easeVelocity.y = rb.velocity.y;
		easeVelocity.z = 0.0f;
		easeVelocity.x *= 0.75f;

		float h = Input.GetAxis("Horizontal"); // Direction (Left/Right)

		if (grounded)
			rb.velocity = easeVelocity;

		rb.AddForce((Vector2.right * speed) * h); //Increases speed

		rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed), rb.velocity.y); //Limits the player's speed
	}

    public void gotPillow()
    {
        //play sound
        //player animation
        hasPillow = true;
    }
}
