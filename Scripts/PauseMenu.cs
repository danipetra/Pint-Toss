using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;
    private AudioManager audioManager;
    
    private void Start()
    {
        audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
    }

    public void Resume()
    {
        audioManager.Play("Button Press");

        foreach (Sound sound in audioManager.sounds)
        {
            audioManager.UnPause(sound.name);
        }

        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void Pause()
    {
        audioManager.Play("Button Press");

        foreach(Sound sound in audioManager.sounds)
        {
            audioManager.Pause(sound.name);
        }
            
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void stopGame()
    {
        GetComponent<SceneLoader>().LoadScene("MainMenu");
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public GameObject getResumeButton()
    {
        GameObject ResumeButton = GameObject.Find("Resume Button");
        return ResumeButton;
    }
}
