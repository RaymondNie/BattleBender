using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour {

	public AudioClip MusicIntro;
	public AudioClip MusicLoop;
	private AudioSource source;

	// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource> ();
		StartCoroutine(playBackgroundMusic());

	}

	IEnumerator playBackgroundMusic()
	{
		source.loop = true;
		source.clip = MusicIntro;
		source.Play ();
		yield return new WaitForSeconds(source.clip.length-5.75f);
		source.loop = true;
		source.clip = MusicLoop;
		source.Play ();

	}
}
