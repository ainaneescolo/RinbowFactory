using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMenuManager : MonoBehaviour
{
    public static UIMenuManager instance;

    [Header("----- UI Manager -----")] 
    [SerializeField] private SkinsManager skinsPlayer1, skinsPlayer2;
    [SerializeField] private TMP_InputField namePlayer1, namePlayer2;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #region Main Menu

    public void CheckPlayerNull()
    {
        CheckPlayerName();
        if (skinsPlayer1.PlayerPrefab != null && skinsPlayer2.PlayerPrefab != null)
        {
            GameManager.instance.player1Prefab = skinsPlayer1.PlayerPrefab;
            GameManager.instance.player2Prefab = skinsPlayer2.PlayerPrefab;
            GameManager.instance.sceneToLoad = 1;
            SceneManager.LoadScene(3);
        }
        else
        {
            Debug.Log("Error");
        }
    }

    void CheckPlayerName()
    {
        GameManager.instance.namePlayer1 = namePlayer1.text == ""? "Player" : namePlayer1.text;
        GameManager.instance.namePlayer2 = namePlayer2.text == ""? "Player" : namePlayer2.text;
    }
    
    #endregion
}