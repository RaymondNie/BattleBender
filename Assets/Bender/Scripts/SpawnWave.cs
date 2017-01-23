using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWave : MonoBehaviour {

	public int playerNumber;
	public KeyCode boundKeyCode;

	public AudioClip shootSound;
	private AudioSource source;

	public GameObject projectile_level_1;
	public Vector2 spawnDist_level_1;
	public Vector2 spawnVelocity_level_1;
	public Vector2 spawnScale_level_1;
	public Color spawnColor_level_1;

	public GameObject projectile_level_2;
	public Vector2 spawnDist_level_2;
	public Vector2 spawnVelocity_level_2;
	public Vector2 spawnScale_level_2;
	public Color spawnColor_level_2;

	public GameObject projectile_level_3;
	public Vector2 spawnDist_level_3;
	public Vector2 spawnVelocity_level_3;
	public Vector2 spawnScale_level_3;
	public Color spawnColor_level_3;
	
	private float spawnTimer;
	private float cooldownTimer;
	public float durationCooldown;

	private charging charge;
	private NewJump jump;

	private bool stateAttack;
	private bool stateCooldown;
	private bool isKeyPressed;

	// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource> ();
		spawnDist_level_1 = new Vector2(2f, -0.815f);
		spawnVelocity_level_1 = new Vector2(500f, 0f);
		spawnScale_level_1 =  new Vector2(3f, 3f);
		spawnColor_level_1 = new Color(255f, 255f, 255f, 255f);

		spawnDist_level_2 = new Vector2(2f, -0.815f);
		spawnVelocity_level_2 = new Vector2(500f, 0f);
		spawnScale_level_2 =  new Vector2(3.2f, 3.2f);
		spawnColor_level_2 = new Color(255f, 255f, 255f, 255f);

		spawnDist_level_3 = new Vector2(2f, -0.815f);
		spawnVelocity_level_3 = new Vector2(500f, 0f);
		spawnScale_level_3 =  new Vector2(3.2f, 3.2f);
		spawnColor_level_3 = new Color(255f, 255f, 255f, 255f);

		durationCooldown = 0.4f;

		charge = GetComponent<charging>();
		jump = GetComponent<NewJump>();
		
		stateAttack = false;
		stateCooldown = false;
		isKeyPressed = false;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetButton("Fire"+playerNumber)) {
			charge.CalculateCharge();
		} else {
			if (charge.chargeLevel > 1 && jump.onGround && 
				gameObject.transform.GetChild(1).GetComponent<GroundDetect>().interactCount != 0) {
				stateAttack = true;
			} else {
				charge.ChargeReset();
			}
		}

		if (Input.GetButtonDown("Fire"+playerNumber)) {
			isKeyPressed = true;
		} else if (Input.GetButtonUp("Fire"+playerNumber) && isKeyPressed) {
			if (!stateCooldown && jump.onGround && 
				gameObject.transform.GetChild(1).GetComponent<GroundDetect>().interactCount != 0) {
				stateAttack = true;
			}
			isKeyPressed = false;
		}

		if (stateAttack) {
			source.PlayOneShot (shootSound, 1);
			if (charge.chargeLevel == 1) {
				GameObject wave = (GameObject) Instantiate(projectile_level_1,
									transform.position, transform.rotation);
				if (transform.localScale.x > 0) {
					wave.transform.position += new Vector3(spawnDist_level_1.x, 
									spawnDist_level_1.y, 0f);
					wave.transform.localScale = new Vector3(spawnScale_level_1.x, 
										spawnScale_level_1.y, 0f);
					wave.GetComponent<Rigidbody2D>().AddForce(new Vector2(spawnVelocity_level_1.x,
										spawnVelocity_level_1.y), ForceMode2D.Force);
				} else if (transform.localScale.x < 0) {
					wave.transform.position -= new Vector3(spawnDist_level_1.x, 
									-spawnDist_level_1.y, 0f);
					wave.transform.localScale = new Vector3(-spawnScale_level_1.x, 
										spawnScale_level_1.y, 0f);
					wave.GetComponent<Rigidbody2D>().AddForce(new Vector2(-spawnVelocity_level_1.x,
										spawnVelocity_level_1.y), ForceMode2D.Force);
				}
				wave.tag = "Wave";
				wave.name = "Wave";
				wave.layer = LayerMask.NameToLayer("Wave" + playerNumber);
			}
			if (charge.chargeLevel == 2) {
				GameObject wave = (GameObject) Instantiate(projectile_level_2,
									transform.position, transform.rotation);
				if (transform.localScale.x > 0) {
					wave.transform.position += new Vector3(spawnDist_level_2.x, 
									spawnDist_level_2.y, 0f);
					wave.transform.localScale = new Vector3(spawnScale_level_2.x, 
										spawnScale_level_2.y, 0f);
					wave.GetComponent<Rigidbody2D>().AddForce(new Vector2(spawnVelocity_level_2.x,
										spawnVelocity_level_2.y), ForceMode2D.Force);
				} else if (transform.localScale.x < 0) {
					wave.transform.position -= new Vector3(spawnDist_level_2.x, 
									-spawnDist_level_2.y, 0f);
					wave.transform.localScale = new Vector3(-spawnScale_level_2.x, 
										spawnScale_level_2.y, 0f);
					wave.GetComponent<Rigidbody2D>().AddForce(new Vector2(-spawnVelocity_level_2.x,
										spawnVelocity_level_2.y), ForceMode2D.Force);
				}
				wave.tag = "Wave";
				wave.name = "Wave";
				wave.layer = LayerMask.NameToLayer("Wave" + playerNumber);
			}
			if (charge.chargeLevel == 3) {
				GameObject wave = (GameObject) Instantiate(projectile_level_3,
									transform.position, transform.rotation);
				if (transform.localScale.x > 0) {
					wave.transform.position += new Vector3(spawnDist_level_3.x, 
									spawnDist_level_3.y, 0f);
					wave.transform.localScale = new Vector3(spawnScale_level_3.x, 
										spawnScale_level_3.y, 0f);
					wave.GetComponent<Rigidbody2D>().AddForce(new Vector2(spawnVelocity_level_3.x,
										spawnVelocity_level_3.y), ForceMode2D.Force);
				} else if (transform.localScale.x < 0) {
					wave.transform.position -= new Vector3(spawnDist_level_3.x, 
									-spawnDist_level_3.y, 0f);
					wave.transform.localScale = new Vector3(-spawnScale_level_3.x, 
										spawnScale_level_3.y, 0f);
					wave.GetComponent<Rigidbody2D>().AddForce(new Vector2(-spawnVelocity_level_3.x,
										spawnVelocity_level_3.y), ForceMode2D.Force);
				}
				wave.tag = "Wave";
				wave.name = "Wave";
				wave.layer = LayerMask.NameToLayer("Wave" + playerNumber);
			}
			
			cooldownTimer = Time.time + durationCooldown;
			stateAttack = false;
			stateCooldown = true;

			charge.ChargeReset();
		}

		if (stateCooldown && cooldownTimer < Time.time) {
			stateCooldown = false;
		}
	}
}
