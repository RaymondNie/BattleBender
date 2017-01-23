using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platforms : MonoBehaviour {

    Rigidbody2D player;
    const float resetCount = 1;
    const float decreaseCount = 5;
    float count;
    bool ignoringCollision;
    public int playerNumber;

	// Use this for initialization
	void Start () {
        player = GetComponent<Rigidbody2D>();
        ignoringCollision = false;

        if (player.tag == "Player 1")
            playerNumber = 1;
        else
            playerNumber = 2;
	}
	
	// Update is called once per frame
	void Update () {

        Physics2D.IgnoreLayerCollision(26, this.gameObject.layer, player.velocity.y > 0.01f);

        if (Input.GetAxis("Fall" + playerNumber) < -0.25f || Input.GetAxis("FallKey" + playerNumber) < -0.25f)
            ignoringCollision = true;

        print(Input.GetAxis("Fall" + playerNumber));


        if (ignoringCollision && player.velocity.y <= 0)
        {
            Physics2D.IgnoreLayerCollision(26, this.gameObject.layer, true);
            Count -= decreaseCount * Time.deltaTime;

            if (Count <= 0)
            {
                ignoringCollision = false;
                Count = resetCount;
            }
        }
    }

    void enableCollision()
    {
        Physics2D.IgnoreLayerCollision(26, this.gameObject.layer, false);
        count = resetCount;
        ignoringCollision = false;
    }

    float Count
    {
        get { return count; }
        set
        {
            if (value >= 0)
                count = value;
            else
                count = 0;
        }
    }
}
