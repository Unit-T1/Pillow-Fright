using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControls : MonoBehaviour {

	public float maxSpeed = 3f;		    //Limits player speed (mostly in the air)
	public float sprintFactor = 2f; 	//Determines how fast sprinting is
	public float speed = 50f;		    //Determines player speed
	public float jumpPower = 200f;	    //Determines jump height

	public bool grounded;           	//Checks for contact with ground
	//bool sprinting;          			//Checks if player is sprinting
    bool hasPillow;						//Checks if player has pillow
	public bool noLife;                 //Checks if player is dead
	public bool isInvulnerable;

	private Rigidbody2D rb;
	private Animator anim;

	//float attackCD = 0.0f;
	float attackDuration = 0.0f;
	bool attacking = false;
	bool movement = true;

	public Color myColor;
	public SpriteRenderer myRender;

	void Start()
    {
		isInvulnerable = false;
		noLife = false;
        hasPillow = false;
		rb = gameObject.GetComponent<Rigidbody2D>();	//store rigidbody component
		anim = gameObject.GetComponent<Animator>();     //store animation component
		myRender = GetComponent<SpriteRenderer>();	
		myColor = myRender.material.color;
	}

	void Update()
	{
		//------------------ Movement -------------------------------
		if (movement)	//disable movement when attacking
		{
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
		}

		//Sprint
		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			maxSpeed = maxSpeed * sprintFactor;
			speed = speed * sprintFactor;
			//sprinting = true;
			anim.SetBool("sprinting", true);
		}
		if (Input.GetKeyUp(KeyCode.LeftShift))
		{
			maxSpeed = maxSpeed / sprintFactor;
			speed = speed / sprintFactor;
			//sprinting = false;
			anim.SetBool("sprinting", false);
		}

		//----------------- Fighting --------------------------------

		if (attackDuration > 0) 
		{
			attackDuration -= Time.deltaTime;	//A timer for each attack
		}
		else 
		{
			attacking = false;
			movement = true;
			anim.SetInteger("attack num", 0);	
		}

        if (hasPillow)
        {
			//Neutral 3
			if (Input.GetKeyDown(KeyCode.X) && grounded && attacking && anim.GetInteger("attack num") == 2)
			{
				StartCoroutine(Attack(attackDuration, 0.7f, 3, 70));
			}
			//Neutral 2
			if (Input.GetKeyDown(KeyCode.X) && grounded && attacking && anim.GetInteger("attack num") == 1)
			{
				StartCoroutine(Attack(attackDuration, 0.6f, 2, 40));
			}
			//Ground Melee
			if (Input.GetKeyDown(KeyCode.X) && grounded)
            {
				//Neutral 1
				if(!attacking && attackDuration <= 0)
                {
					attackDuration = 0.01f;
					StartCoroutine(Attack(0, 0.5f, 1, 20));
				}
			}
			
			//Air Melee
			if (Input.GetKeyDown(KeyCode.X) && !grounded)
            {
				if (!attacking && attackDuration <= 0)
				{
					attackDuration = 0.4f;
					attacking = true;
					anim.SetInteger("attack num", -1);
				}
            }
            //Ranged Attack?
            if (Input.GetKeyDown(KeyCode.C))
            {

            }
        }

		//--------------- Other Animations ------------------

		if (grounded)
		{
			anim.SetBool("grounded", true);
		}
		else
		{
			anim.SetBool("grounded", false);
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

		if (movement) //disabled when attacking
		{
			rb.AddForce((Vector2.right * speed) * h); //Increases speed

			rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed), rb.velocity.y); //Limits the player's speed
		}

		anim.SetFloat("yVelocity", rb.velocity.y);
		anim.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
		if (!attacking)
		{
			if (h < 0)
				transform.localScale = new Vector3(-1, 1, 1);
			else if (h > 0)
				transform.localScale = new Vector3(1, 1, 1);
		}
	}

    public void gotPillow()
    {
        //play sound
        //player animation
        hasPillow = true;
    }

	IEnumerator Attack(float lastDuration, float duration, int attackNum, float force)
    {
		rb.velocity = new Vector2(0, 0);
		attacking = true;
		movement = false;
		yield return new WaitForSeconds(lastDuration);	//wait until last attack is finished
		attackDuration = duration;
		anim.SetInteger("attack num", attackNum);       //start next attack

		if (transform.localScale.x == -1)				//move forward a bit when attacking
			rb.AddForce(new Vector2(-force, 0));
		else
			rb.AddForce(new Vector2(force, 0));
	}

	public int getAttackNum()
    {
		return anim.GetInteger("attack num");
    }

	public void isDead()
	{
		noLife = true;
		SceneManager.LoadScene("Game Over");
		noLife = false;
	}

    // Invunerable and taking damage
    void OnTriggerEnter2D(Collider2D col)
    {
		if (col.tag == "Enemy" && noLife == false)
			takeDamage();
		if (col.tag == "Exit Point" && FindObjectOfType<DreamCatcher>().isAllFound())
			SceneManager.LoadScene("Game Over");

	}

	public void takeDamage()
    {
		if (!isInvulnerable)
		{
			FindObjectOfType<HealthBar>().LoseLife();
			StartCoroutine("getInvulnerable");
		}
	}

	public IEnumerator getInvulnerable()
    {
		//yield return new WaitForSeconds(.01f);
		isInvulnerable = true;
		for (int i = 0; i < 15; i++)
        {
			Physics2D.IgnoreLayerCollision(6, 7, true);
			myColor.a = 0.5f;
			myRender.material.color = myColor;
			yield return new WaitForSeconds(.1f);
			myColor.a = 1f;
			myRender.material.color = myColor;
			yield return new WaitForSeconds(.1f);
		}
		Physics2D.IgnoreLayerCollision(6, 7, false);
		isInvulnerable = false;
	}
}
