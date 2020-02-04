using UnityEngine;

public class BGScaler : MonoBehaviour
{
    GameObject mainCamObj;
    Camera cam;
    float bgWidth;
    SpriteRenderer bgRender;

    private void Awake() {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        bgRender = GetComponent<SpriteRenderer>();
        Vector3 initScale = new Vector3(1, 1, 0);
        Vector3 tempScale = transform.localScale;
        transform.localScale = initScale;
    }

    void Start()
    {
        bgWidth = bgRender.bounds.size.x;
        float bgHeight = bgRender.bounds.size.y;
        float worldHeight = cam.orthographicSize * 2f;
        float worldWidth = worldHeight * Screen.width / Screen.height;

        Vector3 scrScale = new Vector3(1, worldHeight/bgHeight, 0);
        transform.localScale = scrScale;

        // Debug.Log("cam render " + worldWidth);
        // Debug.Log("bg after render" + bgRender.bounds.size);
    }
}
