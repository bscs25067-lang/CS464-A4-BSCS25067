using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private string parameterName = "MusicVolume";

    void Start()
    {
        float saved = PlayerPrefs.GetFloat(parameterName, 1f);
        musicSlider.value = saved;
        ApplyVolume(saved);

        musicSlider.onValueChanged.AddListener(OnSliderChanged);
    }

    void OnDestroy()
    {
        if (musicSlider != null)
            musicSlider.onValueChanged.RemoveListener(OnSliderChanged);
    }

    private void OnSliderChanged(float value)
    {
        ApplyVolume(value);
        PlayerPrefs.SetFloat(parameterName, value);
        PlayerPrefs.Save();
    }

    private void ApplyVolume(float value)
    {
        float clamped = Mathf.Clamp(value, 0.0001f, 1f);
        float dB = Mathf.Log10(clamped) * 20f; // 1 -> 0 dB, 0.5 -> -6 dB, 0.0001 -> -80 dB
        mixer.SetFloat(parameterName, dB);
    }
}