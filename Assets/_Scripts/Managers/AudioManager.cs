using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("--- Musicas (BGM) ---")]
    public AudioSource bgmSource;
    public AudioClip bgmMenu;
    public AudioClip bgmGameplay;

    [Header("--- Efeitos Sonoros (SFX) ---")]
    public AudioSource sfxSource;
    public AudioClip sfxLaser;
    public AudioClip sfxExplosionEnemy;
    public AudioClip sfxExplosionPlayer;
    public AudioClip sfxPowerUp;
    public AudioClip sfxUiClick;
    public AudioClip sfxGameOver;

    private void Awake()
    {
        // Garante que só existe um AudioManager na cena
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Método para tocar músicas de fundo
    public void PlayBGM(AudioClip clip)
    {
        if (clip == null) return;
        bgmSource.clip = clip;
        bgmSource.loop = true;
        bgmSource.Play();
    }

    // Método para tocar efeitos sonoros uma vez
    public void PlaySFX(AudioClip clip)
    {
        if (clip == null) return;
        sfxSource.PlayOneShot(clip);
    }
    private void Start()
{
    // Toca a música da fase assim que a cena inicia
    PlayBGM(bgmGameplay);
}
}