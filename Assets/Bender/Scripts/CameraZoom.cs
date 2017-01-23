using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{

    /*
        
        REQUIRES PLAYER 1 TAGGED AS "Player 1" AND PLAYER 2 TAGGED AS "Player 2"

    */

    const float scaleX = 0.384f * 0.8f;
    const float scaleY = 0.682f;
    const float paddingx = 3;
    const float paddingy = 2;
    const float minSize = 5;
    const float maxSize = 11;

    GameObject player1;
    GameObject player2;
    Camera cam;

    public bool shouldScale;


    // Use this for initialization
    void Start()
    {
        player1 = GameObject.FindWithTag("Player 1");
        player2 = GameObject.FindWithTag("Player 2");
        cam = GetComponent<Camera>();
        shouldScale = true;
    }

    // Update is called once per frame
    void Update()
    {
        float camHeight = 2 * cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;
        float camPos = cam.transform.position.x;
        float player1Pos = player1.transform.position.x;
        float player2Pos = player2.transform.position.x;
        float playerDistance = Vector2.Distance(player1.transform.position, player2.transform.position);

        if(shouldScale)
            scaleCamera(player1.transform.position, player2.transform.position);

        if ((cam.orthographicSize > 5 && playerDistance > 16) ||
            (player1Pos + paddingx > camPos + camWidth/2) || (player2Pos + paddingx > camPos + camWidth / 2) ||
            (player1Pos - paddingx < camPos + camWidth / 2) || (player2Pos - paddingx < camPos + camWidth / 2)
            || (player1.transform.position.y + paddingy > cam.transform.position.y + camHeight / 2 ))
            moveCamera(playerDistance);
    }

    void moveCamera(float pos)
    {
        Vector3 newPosition;
        newPosition = cam.transform.position;
        newPosition.x = (player1.transform.position.x + player2.transform.position.x) / 2;
        newPosition.y = Mathf.Max(0, (player1.transform.position.y + player2.transform.position.y) / 2);

        cam.transform.position = newPosition;       
    }

    public void ResetCamera()
    {
    	Vector3 newPosition = new Vector3(0, 0, -10);
		cam.transform.position = newPosition;
		cam.orthographicSize = minSize;
    }

    void scaleCamera(Vector2 player1Pos, Vector2 player2Pos)
    {
        float playerDistanceX = Mathf.Abs(player1Pos.x - player2Pos.x);
        float playerDistanceY = Mathf.Abs(player1Pos.y - player2Pos.y);
        float scaleToX = playerDistanceX * scaleX;
        float scaleToY = playerDistanceY * scaleY;

        if ((scaleToX > minSize && scaleToX < maxSize) && (scaleToY > minSize && scaleToY < maxSize))
            cam.orthographicSize = Mathf.Max(scaleToX, scaleToY);
        else if (scaleToX > minSize && scaleToX < maxSize)
            cam.orthographicSize = scaleToX;
        else if (scaleToY > minSize && scaleToY < maxSize)
            cam.orthographicSize = scaleToY;
    }
}
