using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private Vector3 tempPos;
    private float leftDistance = 20;
    private float rightDistance;
    private bool isMoveLeft = true;
    private void Awake() {
        rightDistance = leftDistance;
        tempPos = transform.position;
    }
    void Start()
    {
        
    }
    void Update()
    {

    }
    private void FixedUpdate() {
        tempPos.x -= 5 * Time.deltaTime;
        transform.position = tempPos;
        if (isMoveLeft == true) {
            moveLeft();
        } else {
            moveRight();
        }
    }

    private void moveLeft() {
        if (leftDistance > 0) {
            //on moving to left
            leftDistance -= 0.2f;
            tempPos.x -= 5 * Time.deltaTime;
            transform.position = tempPos;
        } else {
            isMoveLeft = false;
            rightDistance = 20;
        }
    }

    private void moveRight() {
        if(rightDistance > 0) {
            //on moving right
            rightDistance -= 0.2f;
            tempPos.x += 5 * Time.deltaTime;
            transform.position = tempPos;
        } else {
            isMoveLeft = true;
            leftDistance = 20;
        }
    }
}
