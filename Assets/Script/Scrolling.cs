using UnityEngine;

public class Scrolling : MonoBehaviour
{
    public float speed;
    private float startX = 23.5f;
    private Camera cam;
    private void Awake() {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    private void FixedUpdate() {
        Application.targetFrameRate = 60;
        Vector3 temp = transform.position;
        temp.x -= speed * Time.deltaTime;
        transform.position = temp;
    }

    public void stopMoving() {
        
    }
}
