using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    static int scorePlayer1 = 0;
    static int scorePlayer2 = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //print(scorePlayer1);	
	}

    public int ScorePlayer1
    {
        get { return scorePlayer1; }
        set { scorePlayer1 = value; }
    }

    public int ScorePlayer2
    {
        get { return scorePlayer2; }
        set { scorePlayer1 = value; }
    }

    static public void Reset()
    {
        SceneManager.LoadScene("Battle");
    }

    void ResetScore()
    {
        scorePlayer1 = 0;
        scorePlayer2 = 0;
    }
}
