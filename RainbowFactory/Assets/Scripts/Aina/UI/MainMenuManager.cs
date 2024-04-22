using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    public void LoadScene(int sceneIndex) 
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void ExitApp()
    {
        Application.Quit();
    }
}
