using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    private Animator animator;
    private Rigidbody2D playerBody;
    private bool isJumping = false;

    public float jumpVelocity;
    public float fall = 2.5f;

    private void Awake() {
        Application.targetFrameRate = 60;
        playerBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Start() {
        
    }

    private void FixedUpdate() {
        if (isJumping) {
            playerBody.velocity = Vector2.up * jumpVelocity;
            isJumping = false;
        }
        if (playerBody.velocity.y < 0) {
            activeAnimation(PlayerState.isFalling);
            playerBody.velocity += Vector2.up * Physics2D.gravity.y * (fall - 1) * Time.deltaTime;
        }
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D objectInfo) {
        if(objectInfo.gameObject.tag == "gem") {
            objectInfo.gameObject.GetComponent<ItemManager>().getItem();
        }
    }

    private void OnCollisionEnter2D(Collision2D info) {
        if (info.gameObject.name == "Tilemap") {
            activeAnimation(PlayerState.isRunning);
        }
    }

    public void Jump() {
        activeAnimation(PlayerState.isJumping);
        isJumping = true;
    }

    public void Attack() {
        Debug.Log("Attack");
    }


    //Controller player animation state

    private enum PlayerState {
        isFalling,
        isJumping,
        isRunning
    }

    private void activeAnimation(PlayerState state) {
        switch (state) {
            case PlayerState.isFalling:
            animator.SetBool(getState(PlayerState.isFalling), true);
            animator.SetBool(getState(PlayerState.isJumping), false);
            animator.SetBool(getState(PlayerState.isRunning), false);
            break;
            case PlayerState.isJumping:
            animator.SetBool(getState(PlayerState.isFalling), false);
            animator.SetBool(getState(PlayerState.isJumping), true);
            animator.SetBool(getState(PlayerState.isRunning), false);
            break;
            case PlayerState.isRunning:
            animator.SetBool(getState(PlayerState.isFalling), false);
            animator.SetBool(getState(PlayerState.isJumping), false);
            animator.SetBool(getState(PlayerState.isRunning), true);
            break;
        }
    }
    private string getState(PlayerState state) {
        switch (state) {
            case PlayerState.isFalling:
            return "isFalling";
            case PlayerState.isJumping:
            return "isJumping";
            case PlayerState.isRunning:
            return "isRunning";
        }
        return "";
    }
}
