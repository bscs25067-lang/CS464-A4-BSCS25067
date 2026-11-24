using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UIButtonSound : MonoBehaviour, IPointerEnterHandler, ISelectHandler
{
    [SerializeField] private AudioClip hoverClip;
    [SerializeField] private AudioClip clickClip;

    private Button _button;

    void Awake() => _button = GetComponent<Button>();

    void OnEnable() => _button.onClick.AddListener(PlayClick);
    void OnDisable() => _button.onClick.RemoveListener(PlayClick);

    public void OnPointerEnter(PointerEventData eventData) => PlayHover();
    public void OnSelect(BaseEventData eventData) => PlayHover();

    private void PlayHover()
    {
        if (!_button.interactable || hoverClip == null || SfxPlayer.Instance == null) return;
        SfxPlayer.Instance.Play(hoverClip, 0.7f);
    }

    private void PlayClick()
    {
        if (clickClip == null || SfxPlayer.Instance == null) return;
        SfxPlayer.Instance.Play(clickClip);
    }
}