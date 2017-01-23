using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
	public AudioClip hitSound;
	private AudioSource source;
	public float moveSpeed = 5f;
	public int playerNumber = 1;
	public bool isAlive;

	private bool canBeHit;
	private bool runningRight;
	private float timeSinceHit;
	private Rigidbody2D myRigidbody;

	private Animator anim;

	void Start () 
	{
		source = GetComponent<AudioSource> ();
		myRigidbody = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator> ();
		ResetPosition();
	}

	void Update () 
	{
		CharacterMovementByForce ();
		if(Input.GetKeyDown(KeyCode.Tab))
		{
			ResetPosition();
		}
		timeSinceHit += Time.time;
		CheckHitStatus();
	}

	void CheckHitStatus()
	{
		if(timeSinceHit > 200f)
		{
			canBeHit = true;
		}
	}

	public void ResetPosition()
	{
		Vector3 newPosition = new Vector3(5f, -2.8f, 0f);
		Vector3 newScale = new Vector3(-2.5f, 2.5f, 2.5f);

		if(playerNumber == 1)
		{
			newPosition.x *= -1;
			newScale.x *= -1;
			runningRight = true;
		} else
		{
			runningRight = false;
		}

		transform.position = newPosition;
		transform.localScale = newScale;
		canBeHit = true;
		timeSinceHit = 0f;
		myRigidbody.velocity = Vector3.zero;
		isAlive = true;
	}

	void CharacterMovementByForce()
	{
		Vector2 newForce = Vector2.zero;
		Vector3 newScale;
		Vector3 newVelocity = myRigidbody.velocity;
		float moveAxis = Input.GetAxis ("Horizontal" + playerNumber);
		newForce.x += moveAxis * moveSpeed;

		if(moveAxis != 0 && canBeHit)
		{
			if(!anim.GetBool("IsJumping"))
			{
				anim.SetBool("IsRunning",true);
			}

			if(moveAxis < 0 && runningRight)
			{
				newVelocity.x = 0;
				myRigidbody.velocity = newVelocity;
				runningRight = false;
				newScale = transform.localScale;
				newScale.x = -2.5f;
				transform.localScale = newScale;
			}

			if(moveAxis > 0 && !runningRight)
			{
				newVelocity.x = 0;
				myRigidbody.velocity = newVelocity;
				runningRight = true;
				newScale = transform.localScale;
				newScale.x = 2.5f;
				transform.localScale = newScale;
			}

			float maxVelocity = 7;
			if(myRigidbody.velocity.x > maxVelocity)
			{
				newForce.x = maxVelocity - myRigidbody.velocity.x;
			} else if(myRigidbody.velocity.x < -maxVelocity)
			{
				newForce.x = -maxVelocity - myRigidbody.velocity.x;
			}

			myRigidbody.AddForce(newForce);
		} else
		{
			anim.SetBool("IsRunning",false);
		}
		transform.rotation = Quaternion.identity;
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.tag == "Wave" && canBeHit)
		{
			source.PlayOneShot (hitSound, 1);
			timeSinceHit = 0f;
			canBeHit = false;

			Vector2 hitForce = new Vector2(600f, 0);

			if(collider.transform.position.x > transform.position.x)
			{
				hitForce.x *= -1;
			}

			myRigidbody.velocity = Vector3.zero;
			myRigidbody.AddForce(hitForce);
		}
	}
}
