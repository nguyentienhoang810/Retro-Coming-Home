using UnityEngine;
using System;

public class Scrolling : MonoBehaviour
{
    public float speed = -1.8f;
    private float startX = 23.5f;
    private Camera cam;
    private PlayerManager player;
    public static bool start = false; //get from Player when hit ground
    private float correctDistance;

    private void Awake() {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        player = GameObject.Find("Player").GetComponent<PlayerManager>();
        correctDistance = cam.transform.position.x - player.transform.position.x;
        Debug.Log("awake distance:" + correctDistance);
    }

    private void FixedUpdate() {
        Application.targetFrameRate = 60;
        if (start == true) {
            Vector3 playerTemp = player.transform.position;
            playerTemp.x -= speed * Time.deltaTime;
            player.transform.position = playerTemp;

            Vector3 camTemp = cam.transform.position;
            camTemp.x = playerTemp.x + 3;
            cam.transform.position = camTemp;
        }
    }

    public void stopMoving() {
        
    }
}
