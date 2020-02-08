using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public int gemScore = 10;
    [SerializeField] GameObject getItemAnimation;
    public void destroy() {
        Instantiate(getItemAnimation, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}
