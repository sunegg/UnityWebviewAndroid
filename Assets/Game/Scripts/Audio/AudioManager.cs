using HAHAHA;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public sealed class AudioManager : USingleton<AudioManager> {

    [SerializeField] private AudioClip _buttonSfx;

    [SerializeField] private bool _saveSettings = true;

    private AudioSource _audioSource;

    private float _musicVolume = 1f;
    private float _soundVolume = 1f;

    private const string MusicKey = "Music", SoundKey = "Sound";

    public float MusicVolume {
        get => _musicVolume;
        set {
            _musicVolume = Mathf.Clamp01(value);
            _audioSource.volume = _musicVolume;
            if (_saveSettings) {
                PlayerPrefs.SetFloat(MusicKey, _musicVolume);
                PlayerPrefs.Save();
            }
        }
    }

    public float SoundVolume {
        get => _soundVolume;
        set {
            _soundVolume = Mathf.Clamp01(value);
            if (_saveSettings) {
                PlayerPrefs.SetFloat(SoundKey, _soundVolume);
                PlayerPrefs.Save();
            }
        }
    }

    protected override void Awake() {
        base.Awake();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start() {
        _musicVolume = PlayerPrefs.GetFloat(MusicKey, 1f);
        _soundVolume = PlayerPrefs.GetFloat(SoundKey, 1f);
        _audioSource.volume = _musicVolume;
    }

    public bool IsMusicMute => !(MusicVolume > 0);
    public bool IsSoundMute => !(SoundVolume > 0);

    public void ToggleMusic() => MusicVolume = MusicVolume < 1f ? 1f : 0f;

    public void ToggleSound() => SoundVolume = SoundVolume < 1f ? 1f : 0f;

    public void PlayOneShot(AudioClip audioClip) {
        PlaySfx(audioClip);
    }

    public void PlayButtonSfx() {
        if (_buttonSfx != null)
            PlaySfx(_buttonSfx);
    }

    private void PlaySfx(AudioClip audioClip) {
        if (!IsSoundMute) {
            GameObject go = new GameObject("Audio: " + _buttonSfx.name);
            go.transform.parent = transform;
            AudioSource source = go.AddComponent<AudioSource>();
            source.clip = audioClip;
            source.volume = 1f;
            source.pitch = 1;
            source.Play();
            Destroy(go, audioClip.length);
        }
    }

    public void Play() => _audioSource.Play();

    public void Pause() => _audioSource.Pause();

    public void PlayBgm(AudioClip clip) {
        _audioSource.clip = clip;
        _audioSource.Play();
    }

    public void StopBgm() {
        _audioSource.Stop();
    }
}