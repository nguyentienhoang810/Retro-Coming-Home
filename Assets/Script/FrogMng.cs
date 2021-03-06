﻿using UnityEngine;
using System.Collections;

public class FrogMng : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D frogBody;

    [SerializeField] GameObject death;
    private enum FrogState {
        isIdle,
        isJumping
    }

    private bool onJumping = false;

    private void Awake() {
        animator = GetComponent<Animator>();
        frogBody = GetComponent<Rigidbody2D>();
        leftJumpCount = jumpCount;
        rightJumpCount = jumpCount;
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
            Invoke("makeJump", 1); //after 1s make jump looper
        }
    }

    public void Destroy() {
        //add EnemyDeath effect animation to current enemy transform.location
        Instantiate(death, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }

    public int jumpCount = 2;
    private int leftJumpCount;
    private int rightJumpCount;
    private bool leftJump = true;
    private void makeJump() {
        onJumping = true;
        if (leftJump == true) {
            leftJumpCount -= 1;
            if(leftJumpCount < 0) {
                leftJump = false;
                rightJumpCount = jumpCount;
            }
        } else {
            rightJumpCount -= 1;
            if( rightJumpCount < 1) {
                leftJump = true;
                leftJumpCount = jumpCount - 1;
            }
        }

        activeAnimation(FrogState.isJumping);
        frogBody.velocity = Vector2.up * 12f;
    }

    private void move(Vector3 temp, float amount, bool isLeft) {
        if (isLeft) {
            temp.x -= amount * Time.deltaTime;
            transform.localScale = new Vector3(1,1,1);
        } else {
            temp.x += amount * Time.deltaTime;
            transform.localScale = new Vector3(-1,1,1);
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
