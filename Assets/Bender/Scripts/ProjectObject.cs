using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectObject : MonoBehaviour {

	public GameObject projectile_level_1, projectile_level_2;
	public int chargeLevel;

	private bool collision_fade;

	// Use this for initialization
	void Start () {
		collision_fade = false;
	}

	// Update is called once per frame
	void Update () {
		if (gameObject.transform.GetChild(0).GetComponent<GroundDetect>().interactCount == 0 || 
			GetComponent<Rigidbody2D>().velocity.x == 0) {
			if (collision_fade) { 
				gameObject.transform.GetComponent<SpriteRenderer>().color -= new Color(0f, 0f, 0f, 0.34f);
			} else {
				gameObject.transform.GetComponent<SpriteRenderer>().color -= new Color(0f, 0f, 0f, 0.10f);
			}
		}
		if (gameObject.transform.GetComponent<SpriteRenderer>().color.a <= 0.0f) {
			Destroy(this.gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.name == "Wave") {
			if (gameObject.GetComponent<ProjectObject>().chargeLevel > collider.GetComponent<ProjectObject>().chargeLevel) {
				int newWave = gameObject.GetComponent<ProjectObject>().chargeLevel - 
								collider.GetComponent<ProjectObject>().chargeLevel;
				Vector2 spawnDist = new Vector2(gameObject.transform.localScale.x, gameObject.transform.localScale.y);
				
				if (collider.transform.localScale.x > 0) {
					collider.GetComponent<Rigidbody2D>().AddForce(new Vector2(-500f, 0f), ForceMode2D.Force);
				} else if (collider.transform.localScale.x < 0) {
					collider.GetComponent<Rigidbody2D>().AddForce(new Vector2(500f, 0f), ForceMode2D.Force);
				}
				Destroy(collider.gameObject.transform.GetComponent<BoxCollider2D>());

				if (newWave == 2) {
					GameObject wave = (GameObject) Instantiate(projectile_level_2,
											transform.position, transform.rotation);
					wave.transform.position = new Vector3(transform.position.x,
											transform.position.y, 0f);
					
					if (spawnDist.x/Mathf.Abs(spawnDist.x) > 0) {
						wave.transform.localScale = new Vector3(3.2f, spawnDist.y, 0f);
						wave.GetComponent<Rigidbody2D>().AddForce(new Vector2(500f, 0f), ForceMode2D.Force);
					} else if (spawnDist.x/Mathf.Abs(spawnDist.x) < 0) {
						wave.transform.localScale = new Vector3(-3.2f, spawnDist.y, 0f);
						wave.GetComponent<Rigidbody2D>().AddForce(new Vector2(-500f, 0f), ForceMode2D.Force);
					}

					wave.name = gameObject.name;
					wave.layer = gameObject.layer;
				}
				if (newWave == 1) {
					GameObject wave = (GameObject) Instantiate(projectile_level_1,
									transform.position, transform.rotation);
					wave.transform.position = new Vector3(transform.position.x,
											transform.position.y, 0f);
					
					if (spawnDist.x/Mathf.Abs(spawnDist.x) > 0) {
						wave.transform.localScale = new Vector3(3f, spawnDist.y, 0f);
						wave.GetComponent<Rigidbody2D>().AddForce(new Vector2(500f, 0f), ForceMode2D.Force);
					} else if (spawnDist.x/Mathf.Abs(spawnDist.x) < 0) {
						wave.transform.localScale = new Vector3(-3f, spawnDist.y, 0f);
						wave.GetComponent<Rigidbody2D>().AddForce(new Vector2(-500f, 0f), ForceMode2D.Force);
					}

					wave.name = gameObject.name;
					wave.layer = gameObject.layer;
				}

				if (transform.localScale.x > 0) {
					GetComponent<Rigidbody2D>().AddForce(new Vector2(-500f, 0f), ForceMode2D.Force);
				} else if (transform.localScale.x < 0) {
					GetComponent<Rigidbody2D>().AddForce(new Vector2(500f, 0f), ForceMode2D.Force);
				}
				Destroy(gameObject.transform.GetComponent<BoxCollider2D>());

			} else if (gameObject.GetComponent<ProjectObject>().chargeLevel == collider.GetComponent<ProjectObject>().chargeLevel) {
				if (collider.transform.localScale.x > 0) {
					collider.GetComponent<Rigidbody2D>().AddForce(new Vector2(-250f, 0f), ForceMode2D.Force);
				} else if (collider.transform.localScale.x < 0) {
					collider.GetComponent<Rigidbody2D>().AddForce(new Vector2(250f, 0f), ForceMode2D.Force);
				}
				Destroy(collider.gameObject.transform.GetComponent<BoxCollider2D>());

				if (transform.localScale.x > 0) {
					GetComponent<Rigidbody2D>().AddForce(new Vector2(-250f, 0f), ForceMode2D.Force);
				} else if (transform.localScale.x < 0) {
					GetComponent<Rigidbody2D>().AddForce(new Vector2(250f, 0f), ForceMode2D.Force);
				}
				Destroy(gameObject.transform.GetComponent<BoxCollider2D>());
			}
		}
	}
}
