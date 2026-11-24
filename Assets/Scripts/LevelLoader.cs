using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private int creditsIndex = 6;
    [SerializeField] private GameObject loadingPanel;
    [SerializeField] private Slider loadingBar;

    private bool _isLoading;

    public void Load(int buildIndex)
    {
        if (_isLoading) return;
        StartCoroutine(LoadRoutine(buildIndex));
    }

    public void LoadNext()
    {
        int current = SceneManager.GetActiveScene().buildIndex;
        int next = current >= creditsIndex ? 0 : current + 1;
        Load(next);
    }

    public void Reload()
    {
        Load(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMenu()
    {
        Load(0);
    }

    private IEnumerator LoadRoutine(int buildIndex)
    {
        _isLoading = true;
        Time.timeScale = 1f;
        AudioListener.pause = false;

        if (loadingPanel != null) loadingPanel.SetActive(true);
        if (loadingBar != null) loadingBar.value = 0f;

        AsyncOperation op = SceneManager.LoadSceneAsync(buildIndex);
        while (!op.isDone)
        {
            float progress = Mathf.Clamp01(op.progress / 0.9f);
            if (loadingBar != null) loadingBar.value = progress;
            yield return null;
        }
    }
}