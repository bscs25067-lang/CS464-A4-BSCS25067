using UnityEngine;
using UnityEngine.Audio;

public class SfxPlayer : MonoBehaviour
{
    public static SfxPlayer Instance { get; private set; }

    [SerializeField] private AudioMixerGroup sfxGroup;
    [SerializeField] private int poolSize = 4;

    private AudioSource[] _sources;
    private int _nextIndex;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        _sources = new AudioSource[poolSize];
        for (int i = 0; i < poolSize; i++)
        {
            AudioSource src = gameObject.AddComponent<AudioSource>();
            src.outputAudioMixerGroup = sfxGroup;
            src.playOnAwake = false;
            src.ignoreListenerPause = true;
            _sources[i] = src;
        }
    }

    public void Play(AudioClip clip, float volume = 1f)
    {
        if (clip == null) return;

        AudioSource src = _sources[_nextIndex];
        _nextIndex = (_nextIndex + 1) % _sources.Length;

        float semitones = Random.Range(-2f, 2f);
        src.pitch = Mathf.Pow(2f, semitones / 12f);
        src.PlayOneShot(clip, volume);
    }
}