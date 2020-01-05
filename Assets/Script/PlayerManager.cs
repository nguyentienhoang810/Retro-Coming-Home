using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    private Animator animator;
    private Rigidbody2D playerBody;
    private float jumpPower = 600;
    private bool isJumping = false;

    public float jumpVelocity;
    public float fall = 2.5f;

    private void Awake() {
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
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", true);
            playerBody.velocity += Vector2.up * Physics2D.gravity.y * (fall - 1) * Time.deltaTime;
        }
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D info) {
        if (info.gameObject.name == "Tilemap") {
            animator.SetBool("isRunning", true);
            animator.SetBool("isFalling", false);
            animator.SetBool("isJumping", false);
        }
    }

    public void Jump() {
        animator.SetBool("isJumping", true);
        animator.SetBool("isFalling", false);
        animator.SetBool("isRunning", false);
        isJumping = true;
    }

    public void Attack() {
        Debug.Log("Attack");
    }
}
