using UnityEngine;
using System.Collections;

public class FrogMng : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D frogBody;
    private delegate void Callback();
    private enum FrogState {
        isIdle,
        isJumping
    }

    private void Awake() {
        animator = GetComponent<Animator>();
        frogBody = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        // jump();
        // StartCoroutine(startFrogJump(1));
        var callback = new Callback(move);
        jump(callback);
    }

    private void FixedUpdate() {
        Application.targetFrameRate = 60;
        // StartCoroutine(startFrogJump(1));
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.name == "Tilemap") {
            activeAnimation(FrogState.isIdle);
        }
    }

    private IEnumerator startFrogJump(float time) {
        yield return new WaitForSeconds(time);
        ;
    }

    private void jump(Callback callback) {
        activeAnimation(FrogState.isJumping);
        frogBody.velocity = Vector2.up * 12f;
        callback();
    }

    private void move() {
        Vector3 frogTemp = transform.position;
        frogTemp.x -= 2.8f * Time.deltaTime;
        transform.position = frogTemp;
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
