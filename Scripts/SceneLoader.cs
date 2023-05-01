using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(string name)
    {
        ResetTimeScale();
        SceneManager.LoadScene(name);
    }

    public void ReloadScene()
    {
        string CurrentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(CurrentScene);
    }

    public void QuitGame()
    {
        Debug.Log("Request to Quit");
        Application.Quit();
    }

    private void ResetTimeScale()
    {
        if(Time.timeScale != 1f)
        {
            Time.timeScale = 1f;
        }
    }
   
}
