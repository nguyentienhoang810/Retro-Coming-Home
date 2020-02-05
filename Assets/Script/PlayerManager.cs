using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class PlayerManager : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    private Renderer render;
    private Animator animator;
    private Rigidbody2D playerBody;
    private BoxCollider2D playerCollider;
    
    // get reference from IDE
    public float jumpVelocity;
    private float fall = 2.5f; //fall speed
    //max = 2 (double jump)
    private int jumpCount = 0;
    private float blinkTime = 0.1f;

    public Image[] hearts;
    public Sprite heart;
    public Sprite emptyHeart;
    private int fullHeath = 3;

    private bool isBlinking;

    private enum PlayerState {
        isFalling,
        isJumping,
        isRunning,
        isHurt
    }
    private PlayerState playerState; //when blinking. player dont get hurt

    private void Awake() {
        Application.targetFrameRate = 60;
        playerBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        render = GetComponent<Renderer>();
        playerCollider = GetComponent<BoxCollider2D>();
    }

    void Start() {
        StartCoroutine(blink());
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
        if (objectInfo.gameObject.tag == "gem") {
            gameManager.playGemSE();
            ItemManager gemObj = objectInfo.gameObject.GetComponent<ItemManager>();
            int score = gemObj.gemScore;
            gameManager.updateScore(score);
            gemObj.destroy();
        }
        if (objectInfo.gameObject.tag == "trap") {
            if (!isBlinking) {
                downHP();
                activeAnimation(PlayerState.isHurt);
            }
        }

        if (objectInfo.gameObject.tag == "reset") { //after get out of trap
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
                EnemyMng opossum = objectInfo.gameObject.GetComponent<EnemyMng>();
                if (hitPos.normal.y > 0) { //hit enemy from top
                    gameManager.playDestroyEnemySE();
                    opossum.Destroy();
                    // Debug.Log(enemy.transform.position);
                    Jump();
                    jumpCount = 1; //make player able to jump again
                } else {
                    if (!isBlinking) {
                        downHP();
                        gameManager.playHurtSE();
                        activeAnimation(PlayerState.isHurt);
                    }
                }
            }
        }
    }

    //onClick
    public void Jump() {
        if (Scrolling.gameover == false) {
            if (jumpCount < 2) {
                jumping();
                jumpCount += 1;
            }
        }
    }

    //jump animation and physic
    private void jumping() {
        activeAnimation(PlayerState.isJumping);
        playerBody.velocity = Vector2.up * jumpVelocity;
    }

    //fall animation and physic
    private void falling() {
        activeAnimation(PlayerState.isFalling);
        playerBody.velocity += Vector2.up * Physics2D.gravity.y * (fall - 1) * Time.deltaTime;
    }

    public void Attack() {
        Debug.Log("Attack");
    }

    private void downHP() {
        //down HP
        fullHeath -= 1;
        Debug.Log(fullHeath);
        // hearts[fullHeath].sprite = emptyHeart;
        hearts[fullHeath].enabled = false;
        if (fullHeath <= 0) {
            activeAnimation(PlayerState.isHurt);
            Scrolling.gameover = true;
            Scrolling.start = false;
        }
    }

    private IEnumerator blink() {
        isBlinking = true;
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
        yield return new WaitForSeconds(blinkTime);
        render.enabled = true;
        yield return new WaitForSeconds(blinkTime);
        render.enabled = false;
        yield return new WaitForSeconds(blinkTime);
        render.enabled = true;
        isBlinking = false;
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
            gameManager.playJumpSE();
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
            gameManager.playHurtSE();
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
