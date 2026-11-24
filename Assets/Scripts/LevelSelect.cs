using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    [SerializeField] private LevelLoader loader;
    [SerializeField] private Button[] levelButtons;
    [SerializeField] private TMP_Text[] bestTimeTexts;



    void Start()
    {
        if (levelButtons.Length == 0)
            return;

        for (int i = 0; i < levelButtons.Length; i++)
        {
            int levelIndex = i + 1;
            bool unlocked = SaveData.IsUnlocked(levelIndex);

            levelButtons[i].interactable = unlocked;
            levelButtons[i].onClick.AddListener(() => loader.Load(levelIndex));

            if (bestTimeTexts != null && i < bestTimeTexts.Length && bestTimeTexts[i] != null)
            {
                if (!unlocked)
                    bestTimeTexts[i].text = "Locked";
                else if (SaveData.TryGetBest(levelIndex, out float best))
                    bestTimeTexts[i].text = $"Best: {best:F2}s";
                else
                    bestTimeTexts[i].text = "Unlocked";
            }
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Credits()
    {
        loader.Load(6);
    }

    public void MainMenu()
    {
        loader.Load(0);
    }
}