using UnityEngine;
using System;

public class Scrolling : MonoBehaviour
{
    public float speed = -1.8f;
    private float startX = 23.5f;
    private Camera cam;
    private GameObject player;

    //get from Player when hit ground. get from PlayerManager
    public static bool start = false;

     //distance X between player and main camera
    private float correctDistance = 3;

    public static bool gameover = false; //controlled in PlayerManager

    private void Awake() {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        player = GameObject.Find("Player");
    }

    private void FixedUpdate() {
        Application.targetFrameRate = 60;
        if (start == true && gameover == false) {
            Vector3 playerTemp = player.transform.position;
            playerTemp.x -= speed * Time.deltaTime;
            player.transform.position = playerTemp;

            Vector3 camTemp = cam.transform.position;
            camTemp.x = playerTemp.x + correctDistance;
            cam.transform.position = camTemp;
        }
    }
}
