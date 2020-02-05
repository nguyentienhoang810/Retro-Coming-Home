using UnityEngine;
using System;

public class Scrolling : MonoBehaviour
{
    public float speed = -1.8f;
    private float startX = 23.5f;
    private Camera cam;
    private PlayerManager player;

    //get from Player when hit ground. get from PlayerManager
    public static bool start = false;

     //distance X between player and main camera
    private float correctDistance = 3;

    private bool gameover = false;

    private void Awake() {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        player = GameObject.Find("Player").GetComponent<PlayerManager>();
    }

    private void FixedUpdate() {
        Application.targetFrameRate = 60;
        if (start == true) {
            Vector3 playerTemp = player.transform.position;
            playerTemp.x -= speed * Time.deltaTime;
            player.transform.position = playerTemp;

            Vector3 camTemp = cam.transform.position;
            camTemp.x = playerTemp.x + correctDistance;
            cam.transform.position = camTemp;
        }
    }

    private void moveObject() {

    }
}
