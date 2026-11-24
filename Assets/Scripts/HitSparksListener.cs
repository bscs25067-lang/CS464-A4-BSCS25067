using UnityEngine;

public class HitSparksListener : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private ParticleSystem hitSparks;

    void OnEnable() { if (health != null) health.Damaged += OnHit; }
    void OnDisable() { if (health != null) health.Damaged -= OnHit; }

    private void OnHit(int current, int max)
    {
        if (hitSparks != null) hitSparks.Play();
    }
}