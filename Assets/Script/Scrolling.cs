using UnityEngine;

public class Scrolling : MonoBehaviour
{
    public float speed;
    private float startX = 23.5f;
    private Camera cam;
    private PlayerManager player;

    public static bool start = false;

    //correct distance between camera and player
    private float correctDistance;
    private void Awake() {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        player = GameObject.Find("Player").GetComponent<PlayerManager>();
        correctDistance = player.transform.position.x - cam.transform.position.x;
    }

    private void FixedUpdate() {
        if (start == true) {
            Application.targetFrameRate = 60;
            Vector3 temp = transform.position;
            temp.x -= speed * Time.deltaTime;
            transform.position = temp;
        }

        // if (player.transform.position.x - cam.transform.position.x < correctDistance) {
        //     cam.transform.position = new Vector3(player.transform.position.x + correctDistance, cam.transform.position.y, cam.transform.position.z);
        // }
    }

    public void stopMoving() {
        
    }
}
