using UnityEngine;

public class Scrolling : MonoBehaviour
{
    public float speed;
    private float bgWidth;
    private float scrWidth;
    
    private GameObject bg;
    private GameObject bg2;
    private Vector3 initPos;
    private SpriteRenderer bgRender;
    Camera cam;

    private void Awake() {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        bg = GameObject.FindGameObjectWithTag("background");
        bg2 = GameObject.FindGameObjectWithTag("background2");
        bgRender = bg.GetComponent<SpriteRenderer>();
        bgWidth = bgRender.bounds.size.x;
        scrWidth = calculateScrWidth();
        initPos = bg2.transform.localPosition;
    }

    private void FixedUpdate() {
        Application.targetFrameRate = 60;
        Vector3 temp = transform.position;
        temp.x -= speed * Time.deltaTime;
        transform.position = temp;
        genBackground();
    }

    //infinity loop for background
    private void genBackground() {
        float xTemp = bg.transform.position.x;
        if (xTemp < -((bgWidth / 2 + scrWidth / 2) + (bgWidth / 2 - scrWidth / 2))) {
            bg.transform.position = new Vector3(initPos.x - 1, initPos.y, initPos.z);
        }

        float x2Temp = bg2.transform.position.x;
        if (x2Temp < -((bgWidth / 2 + scrWidth / 2) + (bgWidth / 2 - scrWidth / 2))) {
            bg2.transform.position = new Vector3(initPos.x - 1, initPos.y, initPos.z);
        }
    }

    private float calculateScrWidth() {
        float bgHeight = bgRender.bounds.size.y;
        float scrHeight = cam.orthographicSize * 2f;
        float scrWidth = scrHeight * Screen.width / Screen.height;
        return scrWidth;
    }

    public void stopMoving() {
        
    }
}
