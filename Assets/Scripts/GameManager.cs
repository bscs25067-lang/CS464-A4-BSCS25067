using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool IsGameEnded { get; private set; }

    [SerializeField] int targetScore = 10, lives = 3;
    [SerializeField] float respawnDelay = 0.6f, spawnProtection = 1.5f;
    [SerializeField] Health player;
    [SerializeField] Transform respawnPoint;
    [SerializeField] TMP_Text scoreText, livesText;
    [SerializeField] GameObject winPanel, losePanel;
    [SerializeField] GameObject winFirstButton, loseFirstButton;

    int score;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start() => UpdateUI();

    public void AddScore(int amount)
    {
        score += amount;
        UpdateUI();
        if (score >= targetScore) Win();
    }

    public void UpdateUI()
    {
        scoreText.text = $"Score: {score} / {targetScore}";
        livesText.text = $"Lives: {lives}";
    }

    public void PlayerDied()
    {
        lives--;
        UpdateUI();

        if (lives <= 0) Lose();
        else StartCoroutine(Respawn());
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawnDelay);
        Rigidbody body = player.GetComponent<Rigidbody>();
        body.position = respawnPoint.position;
        body.linearVelocity = Vector3.zero;
        player.Restore(spawnProtection);
    }

    void Win()
    {
        IsGameEnded = true;
        winPanel.SetActive(true);
        Time.timeScale = 0f;
        SelectButton(winFirstButton);
    }

    void Lose()
    {
        IsGameEnded = true;
        losePanel.SetActive(true);
        Time.timeScale = 0f;
        SelectButton(loseFirstButton);
    }

    private void SelectButton(GameObject button)
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(button);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        // In Part D, replace with LevelLoader.Instance.Reload();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitToMenu()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        // In Part D, replace with LevelLoader.Instance.LoadMenu();
        SceneManager.LoadScene(0);
    }
}