using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    public static bool _gameIsPaused = false;
    public GameObject _pauseMenuUI;
    private AudioManager _audioManager;
    
    private void Start()
    {
        _audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
    }

    public void Resume()
    {
        _audioManager.Play("Button Press");

        foreach (Sound sound in _audioManager.sounds)
        {
            _audioManager.UnPause(sound.name);
        }

        _pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        _gameIsPaused = false;
    }

    public void Pause()
    {
        _audioManager.Play("Button Press");

        foreach(Sound sound in _audioManager.sounds)
        {
            _audioManager.Pause(sound.name);
        }
            
        _pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        _gameIsPaused = true;
    }

    public void stopGame()
    {
        GetComponent<SceneLoader>().LoadScene("MainMenu");
        Time.timeScale = 1f;
        _gameIsPaused = false;
    }

    public GameObject getResumeButton()
    {
        GameObject ResumeButton = GameObject.Find("Resume Button");
        return ResumeButton;
    }
}
