using UnityEngine;

public class CompleteAnimation : MonoBehaviour
{
    public void OnCompleteAnimation() {
        Destroy(this.gameObject);
    }
}
