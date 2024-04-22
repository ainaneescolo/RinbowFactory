using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIGameManager : MonoBehaviour
{
    public static UIGameManager instance;

    [Header("----- Game Panels-----")] 
    [Tooltip("Panel to activate when Pause")]
    [SerializeField] private GameObject pausePanel;
    [Tooltip("Panel to activate in Game Over")]
    [SerializeField] private GameObject endGamePanel;
    [SerializeField] private GameObject changeTimePanel;

    [Header("----- Game Over UI Variables -----")]
    [SerializeField] private TMP_Text packageSpawnedTxt;
    [SerializeField] private TMP_Text packagesDeliveredTxt;
    [SerializeField] private TMP_Text packageLostTxt;
    [SerializeField] private GameObject winTitle;
    [SerializeField] private GameObject loseTitle;
    
    [Header("----- Points UI Variables -----")]
    [Tooltip("Text to show the number of points")]
    [SerializeField] private TMP_Text playerPointsTxt;
    [Tooltip("Grid in points Txt to later show stars when reached the needed points")]
    [SerializeField] private Transform starsGrid;
    
    [Header("----- Timer Variables -----")]
    [Tooltip("Text to show the time left")]
    public TMP_Text timerTxt;

    [Header("----- Package Variables -----")]
    public PoolingItemsEnum packageUIPrefab;
    private GameObject packageUI;
    [SerializeField] private GameObject packageUIParent;
    
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
    
    #region In Game

    #region Pause UI / Game Over UI

    public void OpenPausePanel(InputAction.CallbackContext callbackContext)
    {
        if (!callbackContext.performed) return;
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }
    
    public void ClosePausePanel()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }
    
    public void EndGamePanel(int packageSpawned, int packagesDelivered, int packageLost, bool win)
    {
        if (win)
        {
            winTitle.SetActive(true);
            loseTitle.SetActive(false);
        }
        else
        {
            winTitle.SetActive(false);
            loseTitle.SetActive(true);
        }
        
        // quan cridis la funcio al Lvl Manger que comprobi si la partida es guanya o perd i mostra un panell (o txt + estrelles i punts) depenen
        endGamePanel.SetActive(true);
        packageSpawnedTxt.text = $"{packageSpawned}";
        packagesDeliveredTxt.text = $"{packagesDelivered}";
        packageLostTxt.text = $"{packageLost}";
    }

    public void PanelChangeTime(bool state)
    {
        changeTimePanel.SetActive(state);
    }
    
    #endregion
    
    #region Package
    
        public void CreatePackageUI(Package package)
        {
            // Setejar el prefab de la UI que hem carregat
            // Li passem les variables del package amb que s'ha creat
            
            packageUI = PoolingManager.Instance.GetPooledObject((int)packageUIPrefab);
            packageUI.transform.SetParent(packageUIParent.transform);

            package.PackageUI = packageUI.GetComponent<PackageUI>();
            packageUI.GetComponent<PackageUI>().packageSprite.sprite = package.PackageMesh.sprite;
            packageUI.GetComponent<PackageUI>().packageColor1.color = package.Color1Package.color;
            packageUI.GetComponent<PackageUI>().packageColor2.color = package.Color2Package.color;
            packageUI.GetComponent<PackageUI>().packageColor1Check.color = Color.white;
            packageUI.GetComponent<PackageUI>().packageColor2Check.color = Color.white;
            packageUI.GetComponent<PackageUI>().packageCountry.sprite = package.CountryPackage.countryImage;
            packageUI.GetComponent<PackageUI>().packageLifetime.maxValue = package.PackageLifetime;
            
            packageUI.SetActive(true);
        }
        
    #endregion

    #region Points

    public void RefreshPointsUI(int points)
    {
        playerPointsTxt.text = $"{points}";
    }
    
    public void ActivateStarUi(int index)
    {
        if (starsGrid.GetChild(index).transform.gameObject.activeSelf)return;
        starsGrid.GetChild(index).transform.gameObject.SetActive(true);
        ++LevelManager.instance.playerStars;
    }

    #endregion
    
    #endregion
}