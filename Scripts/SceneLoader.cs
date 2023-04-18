using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(string name)
    {
        Debug.Log("loadlevel request for: " + name);
        resetTimeScale();
        SceneManager.LoadScene(name);
    }

    public void ReloadScene()
    {
        string CurrentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(CurrentScene);
        Debug.Log("Reloading scene" + CurrentScene);
    }

    public void QuitGame(){
        Debug.Log("Request to Quit");
        Application.Quit();
    }

    private void resetTimeScale()
    {
        if(Time.timeScale != 1f)
        {
            Time.timeScale = 1f;
        }
    }
   
}
