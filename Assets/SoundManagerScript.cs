using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;  // For background music
    [SerializeField] private AudioSource SFXSource;    // For one-time sound effects
    [SerializeField] private AudioSource tickingSource; // For continuous ticking sound

    private bool isGameOverActive = false; // Tracks if GameOver is active

    [Header("--------Audio Clips--------")]
    public AudioClip Home;
    public AudioClip Level1;
    public AudioClip Level2;
    public AudioClip Level3;
    public AudioClip GameOver;
    public AudioClip Win;
    public AudioClip Clock;    // Clock ticking sound
    public AudioClip MeshRotate;

    private void Start()
    {
        PlayBackgroundMusic();
        PlayClockTicking(); // Start the ticking sound when a level starts
    }

    public void PlayBackgroundMusic()
    {
        musicSource.Stop(); // Stop any existing background music

        // Determine the active scene
        string currentScene = SceneManager.GetActiveScene().name;

        switch (currentScene)
        {
            case "HomeUI":
                musicSource.clip = Home;
                StopClockTicking(); // Stop ticking sound on Home screen
                break;
            case "Level One":
                musicSource.clip = Level1;
                PlayClockTicking();
                break;
            case "Level Two":
                musicSource.clip = Level2;
                PlayClockTicking();
                break;
            case "Level Three":
                musicSource.clip = Level3;
                PlayClockTicking();
                break;
            case "GameWonUI":
                musicSource.clip = Win;
                StopClockTicking(); // Stop ticking sound on Win screen
                break;
            default:
                Debug.LogWarning("No background music assigned for this scene.");
                return;
        }

        if (musicSource.clip != null)
        {
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning("Audio clip not assigned for the current scene.");
        }
    }

    public void PlaySFX(string clipName)
    {
        AudioClip clipToPlay = null;

        switch (clipName)
        {
            case "MeshRotate":
                clipToPlay = MeshRotate;
                break;
            default:
                Debug.LogWarning($"SFX clip {clipName} not found!");
                return;
        }

        if (clipToPlay != null)
        {
            SFXSource.PlayOneShot(clipToPlay);
        }
    }

    private void PlayClockTicking()
    {
        if (tickingSource != null && Clock != null)
        {
            if (!tickingSource.isPlaying)
            {
                tickingSource.clip = Clock;
                tickingSource.loop = true; // Loop the ticking sound
                tickingSource.Play();
            }
        }
    }

    private void StopClockTicking()
    {
        if (tickingSource != null && tickingSource.isPlaying)
        {
            tickingSource.Stop();
        }
    }

    private void Update()
    {
        // Check for GameOver UI activation
        CheckGameOver();
    }

    private void CheckGameOver()
    {
        GameObject gameOverCanvas = GameObject.Find("GameOverCanvas");
        if (gameOverCanvas != null)
        {
            bool isActive = gameOverCanvas.activeSelf;

            if (isActive && !isGameOverActive)
            {
                PlayGameOverSound();
                StopClockTicking(); // Stop ticking sound on GameOver screen
                isGameOverActive = true;
            }
            else if (!isActive)
            {
                isGameOverActive = false;
            }
        }
    }

    private void PlayGameOverSound()
    {
        musicSource.Stop();
        musicSource.clip = GameOver;
        musicSource.Play();
        Debug.Log("GameOver music playing.");
    }
}
