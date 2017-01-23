	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class charging : MonoBehaviour {
		public int playerNumber;
		public float chargeAmount = 0; 
		public float chargeSpeed = 2; 
		public int chargeLevel = 1;
		private ParticleSystem particles;

		void Start () {
			particles = GetComponentInChildren<ParticleSystem>();
			ChargeReset();
		}

	public void CalculateCharge () {
			var emission = particles.emission;
			var particleSettings = particles.main;
			Color newColor = new Color();
			newColor.r = 1f;
			newColor.g = 1f;
			newColor.b = 1f;
			newColor.a = 1f;

			// user is holding the button
			chargeAmount += Time.deltaTime * chargeSpeed;

			if (chargeAmount >= 3.0) {
				//print ("Super Charged");
				this.chargeLevel = 3;
				emission.rateOverTime = 40;
				particleSettings.startSpeed = 25f;
				newColor.b = 0.5f;
				newColor.g = 0.5f;
				GetComponent<SpriteRenderer>().color = newColor;
			} else if(chargeAmount >= 1.25){
				//print ("Charged");
				this.chargeLevel = 2;
				emission.rateOverTime = 20;
				particleSettings.startSpeed = 10f;
				newColor.b = 0.75f;
				newColor.g = 0.75f;
				GetComponent<SpriteRenderer>().color = newColor;
			} else if(chargeAmount > 0){
				emission.rateOverTime = 5;
				particleSettings.startSpeed = 5f;
			}
		}

		public void ChargeReset(){
			Color newColor = new Color();
			newColor.r = 1f;
			newColor.g = 1f;
			newColor.b = 1f;
			newColor.a = 1f;

			chargeLevel = 1;
			chargeAmount = 0;
			GetComponent<SpriteRenderer>().color = newColor;

			var emission = particles.emission;
			emission.rateOverTime = 0;
		}

	}
