using UnityEngine;

public class HitSoundListener : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private AudioClip hitClip;

    void OnEnable() { if (health != null) health.Damaged += OnHit; }
    void OnDisable() { if (health != null) health.Damaged -= OnHit; }

    private void OnHit(int current, int max)
    {
        if (SfxPlayer.Instance != null)
            SfxPlayer.Instance.Play(hitClip);
    }
}