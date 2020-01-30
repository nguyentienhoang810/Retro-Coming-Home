using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    private Animator animator;
    private Rigidbody2D playerBody;
    
    // get reference from IDE
    public float jumpVelocity;
    private float fall = 2.5f;

    //max = 2 (double jump)
    private int jumpCount = 0;

    private void Awake() {
        Application.targetFrameRate = 60;
        playerBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Start() {

    }

    private void FixedUpdate() {
        if (playerBody.velocity.y < 0) {
            falling();
        }
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
        if (objectInfo.gameObject.tag == "enemy") {
            activeAnimation(PlayerState.isHurt);
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

    private void activeAnimation(PlayerState state) {
        switch (state) {
            case PlayerState.isFalling:
            animator.SetBool(getState(PlayerState.isJumping), false);
            animator.SetBool(getState(PlayerState.isRunning), false);
            animator.SetBool(getState(PlayerState.isFalling), true);
            break;
            case PlayerState.isJumping:
            animator.SetBool(getState(PlayerState.isHurt), false);
            animator.SetBool(getState(PlayerState.isFalling), false);
            animator.SetBool(getState(PlayerState.isRunning), false);
            animator.SetBool(getState(PlayerState.isJumping), true);
            break;
            case PlayerState.isRunning:
            animator.SetBool(getState(PlayerState.isHurt), false);
            animator.SetBool(getState(PlayerState.isFalling), false);
            animator.SetBool(getState(PlayerState.isJumping), false);
            animator.SetBool(getState(PlayerState.isRunning), true);
            break;
            case PlayerState.isHurt:
            // animator.SetBool(getState(PlayerState.isJumping), false);
            // animator.SetBool(getState(PlayerState.isFalling), false);
            animator.SetBool(getState(PlayerState.isRunning), false);
            animator.SetBool(getState(PlayerState.isHurt), true);
            break;
        }
    }

    //Controller player animation state
    private enum PlayerState {
        isFalling,
        isJumping,
        isRunning,
        isHurt
    }
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
