using UnityEngine;

public class PowerUp : MonoBehaviour
{
    enum PowerUpType
    {
        Speed,
        Shield
    }

    [SerializeField] PowerUpType type = PowerUpType.Speed;
    [SerializeField] private AudioClip sfxClip;
    [SerializeField] float duration = 5f;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        SfxPlayer.Instance.Play(sfxClip);

        if (type == PowerUpType.Speed)
            other.GetComponent<PlayerController>().BoostSpeed();
        else
            other.GetComponent<Health>().SetShield(true, duration);

        Destroy(gameObject);
    }
}