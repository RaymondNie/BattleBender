using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour {

	public AudioClip deathSound;
	private AudioSource source;
	public GameObject explosion;
	public float delay = 0f;
	private PlayerController playerController;
    GameController gameController;


	// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource> ();
		playerController = GetComponent<PlayerController>();
        gameController = (GameController)FindObjectOfType(typeof(GameController));
	}
	
	// Update is called once per frame
	void Update () {

		if (-5 > this.transform.position.y && playerController.isAlive)
		{ 
			source.PlayOneShot (deathSound, 1);
			playerController.isAlive = false;
			GameObject DeathExplosion = (GameObject) Instantiate(explosion,
				(Vector2) transform.position + (new Vector2(0.25f,0)), 
				transform.rotation);

            if (playerController.gameObject.tag == "Player 1")
                gameController.ScorePlayer2++;
            else
                gameController.ScorePlayer1++;

		}
	}


}
