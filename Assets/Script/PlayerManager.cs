using UnityEngine;
using System.Collections;
public class PlayerManager : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    private Renderer render;
    private Animator animator;
    private Rigidbody2D playerBody;
    
    // get reference from IDE
    public float jumpVelocity;
    private float fall = 2.5f; //fall speed
    //max = 2 (double jump)
    private int jumpCount = 0;
    private float blinkTime = 0.1f;

    private enum PlayerState {
        isFalling,
        isJumping,
        isRunning,
        isHurt
    }
    private PlayerState playerState;

    private void Awake() {
        Application.targetFrameRate = 60;
        playerBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        render = GetComponent<Renderer>();
    }

    void Start() {

    }

    private void FixedUpdate() {
        if (playerBody.velocity.y < 0) {
            falling();
        }
        blink();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D objectInfo) {
        gameManager.updateScore(objectInfo);
        if (objectInfo.gameObject.tag == "trap") {
            activeAnimation(PlayerState.isHurt);
        }

        if (objectInfo.gameObject.tag == "reset") {
            activeAnimation(PlayerState.isRunning);
        }
    }

    private void OnCollisionEnter2D(Collision2D objectInfo) {
        if (objectInfo.gameObject.name == "Tilemap") {
            activeAnimation(PlayerState.isRunning);
            jumpCount = 0;
            Scrolling.start = true;
        }

        foreach ( ContactPoint2D hitPos in objectInfo.contacts) {
            if (objectInfo.gameObject.tag == "enemy") {
                EnemyMng enemy = objectInfo.gameObject.GetComponent<EnemyMng>();   
                if (hitPos.normal.y > 0) { //hit enemy from top
                    enemy.Destroy();
                    Debug.Log(enemy.transform.position);
                    Jump();
                    jumpCount = 1; //make player able to jump again
                } else {
                    activeAnimation(PlayerState.isHurt);
                }
            }
        }
    }

    //onClick
    public void Jump() {
        if (jumpCount < 2) {
            jumping();
            jumpCount += 1;
        }
    }

    //fall animation and physic
    private void falling() {
        activeAnimation(PlayerState.isFalling);
        playerBody.velocity += Vector2.up * Physics2D.gravity.y * (fall - 1) * Time.deltaTime;
    }
    //jump animation and physic
    private void jumping() {
        activeAnimation(PlayerState.isJumping);
        playerBody.velocity = Vector2.up * jumpVelocity;
    }

    public void Attack() {
        Debug.Log("Attack");
    }

    private IEnumerator blink() {
        render.enabled = false;
        yield return new WaitForSeconds(blinkTime);
        render.enabled = true;
        yield return new WaitForSeconds(blinkTime);
        render.enabled = false;
        yield return new WaitForSeconds(blinkTime);
        render.enabled = true;
        yield return new WaitForSeconds(blinkTime);
        render.enabled = false;
        yield return new WaitForSeconds(blinkTime);
        render.enabled = true;
    }

    private void activeAnimation(PlayerState state) {
        switch (state) {
            case PlayerState.isFalling:
            playerState = PlayerState.isFalling;
            animator.SetBool(getState(PlayerState.isJumping), false);
            animator.SetBool(getState(PlayerState.isRunning), false);
            animator.SetBool(getState(PlayerState.isFalling), true);
            break;
            case PlayerState.isJumping:
            playerState = PlayerState.isJumping;
            animator.SetBool(getState(PlayerState.isHurt), false);
            animator.SetBool(getState(PlayerState.isFalling), false);
            animator.SetBool(getState(PlayerState.isRunning), false);
            animator.SetBool(getState(PlayerState.isJumping), true);
            break;
            case PlayerState.isRunning:
            playerState = PlayerState.isRunning;
            animator.SetBool(getState(PlayerState.isHurt), false);
            animator.SetBool(getState(PlayerState.isFalling), false);
            animator.SetBool(getState(PlayerState.isJumping), false);
            animator.SetBool(getState(PlayerState.isRunning), true);
            break;
            case PlayerState.isHurt:
            playerState = PlayerState.isHurt;
            // InvokeRepeating("blink", 0.3f, 1f);
            StartCoroutine(blink());
            // animator.SetBool(getState(PlayerState.isJumping), false);
            // animator.SetBool(getState(PlayerState.isFalling), false);
            animator.SetBool(getState(PlayerState.isRunning), false);
            animator.SetBool(getState(PlayerState.isHurt), true);
            break;
        }
    }

    //Controller player animation state
    private string getState(PlayerState state) {
        switch (state) {
            case PlayerState.isFalling:
            return "isFalling";
            case PlayerState.isJumping:
            return "isJumping";
            case PlayerState.isRunning:
            return "isRunning";
            case PlayerState.isHurt:
            return "isHurt";
        }
        return "";
    }
}
