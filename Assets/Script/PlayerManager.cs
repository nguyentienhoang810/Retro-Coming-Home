using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        animator.SetBool("ifFalling", true);
    }

    private void FixedUpdate() {
        
    }

    void Update()
    {
        
    }

    public void Jump() {
        Debug.Log("Jump");
    }

    public void Attack() {
        Debug.Log("Attack");
    }
}
