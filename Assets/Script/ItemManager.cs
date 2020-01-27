using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public void getItem() {
        Destroy(this.gameObject);
        Debug.Log("Score += 10");
    }
}
