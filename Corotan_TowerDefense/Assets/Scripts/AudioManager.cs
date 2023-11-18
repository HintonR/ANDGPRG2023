using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    
    [SerializeField] public AudioClip _battleBGM, _buildBGM;
    [SerializeField] public AudioClip _coinSFX, _deathSFX, _invalidSFX, _selectSFX, _upgradeSFX;
    [SerializeField] public AudioClip _gameoverMFX;

    private AudioClip _currentBGM;

    private AudioSource _sfxSource;
    private AudioSource _bgmSource;

    void Awake()
    {
        Instance = this;
        _sfxSource = GetComponent<AudioSource>();
        _bgmSource = GetComponent<AudioSource>();
        _bgmSource.loop = true;
        _bgmSource.clip = _buildBGM;
        _bgmSource.Play();
        _currentBGM = _buildBGM;
    }
    public void PlaySFX(AudioClip clip)
    {
        _sfxSource.PlayOneShot(clip);
    }
    public void ChangeBGM(AudioClip bgm)
    {
        if (bgm == _currentBGM && _bgmSource.isPlaying) return;
        _bgmSource.Stop();
        _bgmSource.clip = bgm;
        _bgmSource.Play();
        _currentBGM = bgm;
    }
    public void StopBGM()
    {
        _bgmSource.Stop();
    }
}
