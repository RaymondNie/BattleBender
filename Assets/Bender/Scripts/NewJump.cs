using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewJump : MonoBehaviour {
	public AudioClip jumpSound;
private AudioSource source;
public bool playSound;

    Rigidbody2D playerBody;
    public float startForce;
    //public float maxForce = 500;
    public float addForce;
    public float jumpForce;
    Vector2 force;
    bool canJump;
    public bool onGround;
	public int playerNumber;

	private Animator anim;

	// Use this for initialization
	void Start () {
        addForce = 250;
        startForce = 110;
        jumpForce = startForce;
        playerBody = GetComponent<Rigidbody2D>();
        canJump = true;
        onGround = true;
		anim = GetComponent<Animator> ();
		source = GetComponent<AudioSource> ();
		playSound = true;
    }

    // Update is called once per frame
    void Update()
    {
		if(playerBody.velocity.y < -0.1f)
		{
			anim.SetBool("IsFalling",true);
			anim.SetBool("IsRunning",false);
			anim.SetBool("IsJumping",false);
		} else
		{
	        if (Input.GetButton("Jump" + playerNumber) && canJump)
	        { 
	            JumpForce -= Time.deltaTime * addForce;
	           	 playerJump();
	        }

			if (Input.GetButtonUp("Jump" + playerNumber) && !onGround)
			{
	            canJump = false;
	        }
	    }
    }

    void playerJump()
    {
		if (playSound) {
			source.PlayOneShot (jumpSound, 1);
			playSound = false;
		}
    	anim.SetBool("IsJumping", true);
		anim.SetBool("IsRunning", false);
        force = new Vector2(0, jumpForce);
        playerBody.AddForce(force);        
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
		if (collider.gameObject.tag == "Ground" || collider.gameObject.tag == "Platform")
        {
			playSound = true;
            JumpForce = startForce;
            canJump = true;
            onGround = true;
			anim.SetBool("IsJumping", false);
			anim.SetBool("IsFalling", false);
        }
    }

    void OnCollisionExit2D(Collision2D collider)
    {
        if(collider.gameObject.tag == "Ground")
            onGround = false;
    }


    // Accessors
    float JumpForce
    {
        get { return jumpForce; }
        set
        {
            if (value <= 0)
                jumpForce = 0;
            else
                jumpForce = value;
        }
    }
}
