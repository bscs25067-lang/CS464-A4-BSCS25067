using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
	public static bool IsPaused { get; private set; }

	[SerializeField] private GameObject pausePanel;
	[SerializeField] private GameObject firstSelectedButton;

	void Update()
	{
		// Don't allow pausing if Win or Game Over is already open
		if (GameManager.Instance != null && GameManager.Instance.IsGameEnded) return;

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (IsPaused) Resume();
			else Pause();
		}
	}

	public void Pause()
	{
		IsPaused = true;
		Time.timeScale = 0f;
		AudioListener.pause = true;
		pausePanel.SetActive(true);

		EventSystem.current.SetSelectedGameObject(null);
		EventSystem.current.SetSelectedGameObject(firstSelectedButton);
	}

	public void Resume()
	{
		IsPaused = false;
		Time.timeScale = 1f;
		AudioListener.pause = false;
		pausePanel.SetActive(false);
	}

	public void Restart()
	{
		Resume();
		// In Part D, replace this line with: LevelLoader.Instance.Reload();
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void QuitToMenu()
	{
		Resume();
		// In Part D, replace this line with: LevelLoader.Instance.LoadMenu();
		SceneManager.LoadScene(0);
	}
}