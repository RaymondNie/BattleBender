using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetect : MonoBehaviour {

	public int interactCount;

	// Use this for initialization
	void Start () {
		interactCount = 0;
	}

	// Update is called once per frame
	void Update () {

	}

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.tag == "Ground")
        {
            ++interactCount;
        }
    }

    void OnTriggerExit2D (Collider2D collider) {
		if (collider.gameObject.tag == "Ground") {
			--interactCount;
		}
	}
}