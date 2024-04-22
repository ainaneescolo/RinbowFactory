using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("----- Game Var -----")]
    [Tooltip("Number of total stars the players have gained in all games")] 
    public int playerStars;

    [Header("----- Player Json -----")] 
    public string namePlayer1;
    public string namePlayer2;
    
    [Header("----- Player Skins -----")]
    public GameObject player1Prefab;
    public GameObject player2Prefab;

    [Header("----- Change Scene Parameters -----")] 
    public int sceneToLoad;
    public bool resultGame;

    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }

        if (!PlayerPrefs.HasKey("Stars")) return;
        playerStars = PlayerPrefs.GetInt("Stars");
    }
    
    public void LoadScene(int sceneNum)
    {
        SceneManager.LoadScene(sceneNum);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
