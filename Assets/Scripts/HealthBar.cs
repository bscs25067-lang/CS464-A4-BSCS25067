using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Slider fillImage;
    [SerializeField] private Slider lagImage;
    [SerializeField] private float lagDelay = 0.4f;
    [SerializeField] private float lagSpeed = 0.8f;

    private float _targetFill = 1f;
    private float _lagTimer;

    void OnEnable()
    {
        if (playerHealth != null)
        {
            playerHealth.Damaged += Set;
            playerHealth.Restored += ResetBar;
        }
    }

    void OnDisable()
    {
        if (playerHealth != null)
        {
            playerHealth.Damaged -= Set;
            playerHealth.Restored -= ResetBar;
        }
    }

    void Start()
    {
        if (playerHealth != null)
            ResetBar(playerHealth.Current, playerHealth.Max);
    }

    public void Set(int current, int max)
    {
        _targetFill = max > 0 ? Mathf.Clamp01((float)current / max) : 0f;
        fillImage.value = _targetFill;
        _lagTimer = lagDelay;
    }

    private void ResetBar(int current, int max)
    {
        _targetFill = max > 0 ? Mathf.Clamp01((float)current / max) : 0f;
        fillImage.value = _targetFill;
        lagImage.value = _targetFill;
    }

    void Update()
    {
        if (lagImage.value > _targetFill)
        {
            if (_lagTimer > 0f)
                _lagTimer -= Time.deltaTime;
            else
                lagImage.value = Mathf.MoveTowards(lagImage.value, _targetFill, lagSpeed * Time.deltaTime);
        }
    }
}