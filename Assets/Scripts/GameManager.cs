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
    [SerializeField] private LevelLoader loader;
    [SerializeField] private TextMeshProUGUI winTimeText;

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
        int level = gameObject.scene.buildIndex;
        float time = Time.timeSinceLevelLoad;

        bool isNewBest = SaveData.Record(level, time);
        SaveData.Unlock(level + 1);

        ShowWinScreen(time, isNewBest);
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

    private void ShowWinScreen(float time, bool isNewBest)
    {
        if (winTimeText != null)
            winTimeText.text = isNewBest ? $"Time: {time:F2}s (New Best!)" : $"Time: {time:F2}s";

        winPanel.SetActive(true);
        Time.timeScale = 0f;
        SelectButton(winFirstButton);
    }

    public void NextLevel() { Time.timeScale = 1f; AudioListener.pause = false; loader.LoadNext(); }
    public void Restart() { Time.timeScale = 1f; AudioListener.pause = false; loader.Reload(); }
    public void QuitToMenu() { Time.timeScale = 1f; AudioListener.pause = false; loader.LoadMenu(); }
}