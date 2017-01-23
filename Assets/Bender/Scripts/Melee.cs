using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour {

	public AudioClip hitSound;
	private AudioSource source;
    Rigidbody2D player;
    GameObject opponent;
    Rigidbody2D opponentBody;
    Animator anim;
    Vector2 force;
    int playerNumber;
    public float forceX = 800;
    public float forceY = 400;
    public float forceYAir = 0;
    public float forceXAir = 0;


	// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource> ();
        player = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
        if (gameObject.tag == "Player 1")
            opponent = GameObject.FindGameObjectWithTag("Player 2");
        else
            opponent = GameObject.FindGameObjectWithTag("Player 1");

        opponentBody = opponent.GetComponent<Rigidbody2D>();

        if (player.tag == "Player 1")
            playerNumber = 1;
        else
            playerNumber = 2;

        forceX = 800;
        forceY = 400;
        forceXAir = 200;
        forceYAir = 200;
    }
	
	// Update is called once per frame
	void Update () {

        int direction;
        float distance = Vector2.Distance(player.transform.position, opponentBody.transform.position);
        float distanceX = player.transform.position.x - opponent.transform.position.x;
        Vector2 force;
        

        if (player.transform.localScale.x > 0)
            direction = 1;
        else
            direction = -1;

        if (Input.GetButtonDown("Melee" + playerNumber))
        {
			anim.speed = 2.5f;
			anim.SetTrigger("Attack");
	        if(distance < 1.5 && direction * distanceX < 0)
	        {
				source.PlayOneShot (hitSound, 1);
				GetComponent<Animator>().SetTrigger("Attack");
	            print(opponent.GetComponent<NewJump>().onGround);

	            if (opponent.GetComponent<NewJump>().onGround)
	                force = new Vector2(direction * forceX, forceY);
	            else
	                force = new Vector2(direction * forceXAir, forceYAir);

	            opponentBody.AddForce(force);
	        }
		}
	}
}
