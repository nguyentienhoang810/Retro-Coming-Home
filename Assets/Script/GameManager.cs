using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Text scoreText;
    private int playerScore = 0;

    public void updateScore(Collider2D objectInfo) {
        if(objectInfo.gameObject.tag == "gem") {
            ItemManager gemObj = objectInfo.gameObject.GetComponent<ItemManager>();
            gemObj.destroy();
            playerScore += gemObj.gemScore;
            scoreText.text = "SCORE: " + playerScore;
        }
    }
}
