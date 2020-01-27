using UnityEngine;

public class Scrolling : MonoBehaviour
{
    public float speed;
    
    private void FixedUpdate() {
        Application.targetFrameRate = 60;
        Vector3 temp = transform.position;
        temp.x -= speed * Time.deltaTime;
        transform.position = temp;
    }

    //infinity loop for background

    public void stopMoving() {
        
    }
}
