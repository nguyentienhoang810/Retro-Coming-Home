using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public int gemScore = 10;
    public void destroy() {
        Destroy(this.gameObject);
    }
}
