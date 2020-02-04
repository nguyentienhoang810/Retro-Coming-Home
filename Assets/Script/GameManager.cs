using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Text scoreText;
    [SerializeField] AudioClip jumpSE;
    AudioSource audioSource;
    private int playerScore = 0;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    public void updateScore(int score) {
        playerScore += score;
        scoreText.text = "SCORE: " + playerScore;
    }

    public void activeJumpSE() {
        audioSource.PlayOneShot(jumpSE);
    }
}
