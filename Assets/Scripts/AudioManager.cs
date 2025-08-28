using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Main Audio Source")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Phát âm thanh hiệu ứng (SFX) tại vị trí cụ thể
    /// </summary>
    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
            sfxSource.PlayOneShot(clip);
        else
            Debug.LogWarning("⚠️ SFX clip is null!");
    }

    /// <summary>
    /// Phát nhạc nền (loop)
    /// </summary>
    public void PlayMusic(AudioClip musicClip)
    {
        if (musicSource == null) return;

        musicSource.clip = musicClip;
        musicSource.loop = true;
        musicSource.Play();
    }

    /// <summary>
    /// Điều chỉnh âm lượng tổng
    /// </summary>
    public void SetMusicVolume(float volume)
    {
        musicSource.volume = Mathf.Clamp01(volume);
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = Mathf.Clamp01(volume);
    }
}