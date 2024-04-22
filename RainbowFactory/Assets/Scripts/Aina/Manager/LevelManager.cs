using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    [SerializeField] private AudioSource _audioSource;
    
    [Header("----- Player Variables -----")]
    [SerializeField] private GameObject Player1;
    [SerializeField] private GameObject Player2;
    
    [Header("----- Game Variables -----")]
    private int playerPoints;
    public int playerStars;
    private int packageSpawned;
    private int packagesDelivered;
    private int packageLost;

    [Header("----- Package Variables -----")]
    public List<ColorPackage> colorList = new List<ColorPackage>();
    public List<Country> countryList = new List<Country>();
    public List<PackageMesh> meshList = new List<PackageMesh>();

    // Getters
    public int PackageSpawned
    {
        get {return packageSpawned; }
        set {packageSpawned = value; }
    }
    
    public int PackagesDelivered
    {
        get {return packagesDelivered; }
        set {packagesDelivered = value; }
    }
    
    public int PackageLost
    {
        get {return packageLost; }
        set {packageLost = value; }
    }
    
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
        
        ChargeNewPlayerSkins();
    }

    private void ChargeNewPlayerSkins()
    {
        //Destroy(Player1.transform.GetChild(1));
        //Destroy(Player2.transform.GetChild(1));
        Instantiate(GameManager.instance.player1Prefab, Player1.transform);
        Instantiate(GameManager.instance.player2Prefab, Player2.transform);
        //Player1.GetComponent<PlayerAnimations>()
    }
    
    public void AddPointsPlayer(int pointsToGive)
    {
        playerPoints += pointsToGive;
        UIGameManager.instance.RefreshPointsUI(playerPoints);
        CheckPointsInGame();
    }
    
    private void CheckPointsInGame()
    {
        switch (playerPoints)
        {
            case > 25:
                UIGameManager.instance.ActivateStarUi(1);
                break;
            case > 20:
                UIGameManager.instance.ActivateStarUi(2);
                break;
            case > 10:
                UIGameManager.instance.ActivateStarUi(0);
                break;
        }
    }

    public void GameOver()
    {
        //UIGameManager.instance.EndGamePanel(packageSpawned, packagesDelivered, packageLost, win);
        LocalRequest_GameData.instance.Create_ScoreList(playerPoints, GameManager.instance.namePlayer1, GameManager.instance.namePlayer2);
        
        GameManager.instance.playerStars = playerStars > GameManager.instance.playerStars? playerStars : GameManager.instance.playerStars;
        PlayerPrefs.SetInt("Stars", GameManager.instance.playerStars);
        GameManager.instance.resultGame = playerPoints >= 10;
        GameManager.instance.LoadScene(2);
    }

    public void RestartGame(int sceneNum)
    {
        Time.timeScale = 1;
        GameManager.instance.LoadScene(sceneNum);
    }

    public void OpenMainMenu()
    {
        Time.timeScale = 1;
        GameManager.instance.sceneToLoad = 0;
        GameManager.instance.LoadScene(3);
    }

    public void PlaySound(AudioClip _audioClip)
    {
        _audioSource.PlayOneShot(_audioClip);
    }
}