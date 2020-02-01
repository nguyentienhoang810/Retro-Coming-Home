using UnityEngine;

public class EnemyMng : MonoBehaviour
{

    private Vector3 tempPos;
    public float leftDistance = 20;
    public int speed = 2;
    private float rightDistance;
    private bool isMoveLeft = true;

    private enum DIRECTION_TYPE {
        LEFT,
        RIGHT
    }
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
        Application.targetFrameRate = 60;
        if (isMoveLeft == true) {
            moveLeft();
        } else {
            moveRight();
        }
    }

    private void moveLeft() {
        if (leftDistance > 0) {
            //on moving to left
            transformDirection(DIRECTION_TYPE.LEFT);
            leftDistance -= 0.2f;
            tempPos.x -= speed * Time.deltaTime;
            transform.position = tempPos;
        } else {
            isMoveLeft = false;
            rightDistance = 20;
        }
    }

    public void Destroy() {
        Destroy(this.gameObject);
    }

    private void moveRight() {
        if(rightDistance > 0) {
            //on moving right
            transformDirection(DIRECTION_TYPE.RIGHT);
            rightDistance -= 0.2f;
            tempPos.x += speed * Time.deltaTime;
            transform.position = tempPos;
        } else {
            isMoveLeft = true;
            leftDistance = 20;
        }
    }

    private void transformDirection(DIRECTION_TYPE direction) {
        switch(direction) {
            case DIRECTION_TYPE.LEFT:
            transform.localScale = new Vector3(1,1,1);
            break;
            case DIRECTION_TYPE.RIGHT:
            transform.localScale = new Vector3(-1,1,1);
            break;
        }
    }
}
