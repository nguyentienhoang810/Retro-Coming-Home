using UnityEngine;
using System.Collections;

public class FrogMng : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D frogBody;
    private enum FrogState {
        isIdle,
        isJumping
    }

    private bool onJumping = false;
    private bool leftJump = true;
    private void Awake() {
        animator = GetComponent<Animator>();
        frogBody = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        StartCoroutine(startFrogJump(2));
    }

    private void FixedUpdate() {
        Application.targetFrameRate = 60;
        Vector3 temp = transform.position;
        if (onJumping) {
            move(temp, 2.8f, leftJump);
        } else {
            move(temp, 0f, leftJump);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.name == "Tilemap") {
            onJumping = false;
            activeAnimation(FrogState.isIdle);
        }
    }

    private IEnumerator startFrogJump(float time) {
        //left
        yield return new WaitForSeconds(time);
        onJumping = true;
        leftJump = true;
        makeJump();
        yield return new WaitForSeconds(time);
        onJumping = true;
        leftJump = true;
        makeJump();
        //right
        yield return new WaitForSeconds(time);
        onJumping = true;
        leftJump = false;
        makeJump();
        yield return new WaitForSeconds(time);
        onJumping = true;
        leftJump = false;
        makeJump();
    }

    private void makeJump() {
        activeAnimation(FrogState.isJumping);
        frogBody.velocity = Vector2.up * 12f;
    }

    private void move(Vector3 temp, float amount, bool isLeft) {
        if (isLeft) {
            temp.x -= amount * Time.deltaTime;
        } else {
            temp.x += amount * Time.deltaTime;
        }
        transform.position = temp;
    }

    private void activeAnimation(FrogState state) {
        switch (state) {
            case FrogState.isIdle:
            animator.SetBool(getStateString(FrogState.isJumping), false);
            animator.SetBool(getStateString(FrogState.isIdle), true);
            break;
            case FrogState.isJumping:
            animator.SetBool(getStateString(FrogState.isIdle), false);
            animator.SetBool(getStateString(FrogState.isJumping), true);
            break;
        }
    }

    private string getStateString(FrogState state) {
        switch (state) {
            case FrogState.isIdle:
            return "idle";
            case FrogState.isJumping:
            return "jump";
        }
        return "";
    }
}
