using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Text scoreText;
    [SerializeField] AudioClip jumpSE;
    [SerializeField] AudioClip destroyEnemySE;
    [SerializeField] AudioClip getHurtSE;
    [SerializeField] AudioClip pickGemSE;

    AudioSource audioSource;
    private int playerScore = 0;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    public void updateScore(int score) {
        playerScore += score;
        scoreText.text = "SCORE: " + playerScore;
    }

    public void playJumpSE() {
        audioSource.PlayOneShot(jumpSE);
    }

    public void playHurtSE() {
        audioSource.PlayOneShot(getHurtSE);
    }

    public void playGemSE() {
        audioSource.PlayOneShot(pickGemSE);
    }

    public void playDestroyEnemySE() {
        audioSource.PlayOneShot(destroyEnemySE);
    }
}
