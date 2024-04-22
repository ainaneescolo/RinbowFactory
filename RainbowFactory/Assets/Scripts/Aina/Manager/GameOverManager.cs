using System;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    [Header("----- Player Variables -----")]
    [SerializeField] private GameObject Player1;
    [SerializeField] private GameObject Player2;
    
    [SerializeField] private GameObject winTitle;
    [SerializeField] private GameObject loseTitle;

    private void Awake()
    {
        Instantiate(GameManager.instance.player1Prefab, Player1.transform);
        Instantiate(GameManager.instance.player2Prefab, Player2.transform);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.instance.resultGame)
        { 
            winTitle.SetActive(true);
            loseTitle.SetActive(false);
            Player1.transform.GetChild(2).GetComponent<Animator>().SetTrigger("Win");
            Player2.transform.GetChild(2).GetComponent<Animator>().SetTrigger("Win");
        }
        else
        {
            winTitle.SetActive(false);
            loseTitle.SetActive(true);
            Player1.transform.GetChild(2).GetComponent<Animator>().SetTrigger("Lose");
            Player2.transform.GetChild(2).GetComponent<Animator>().SetTrigger("Lose");
        }
    }

    public void OpenMainMenu()
    {
        GameManager.instance.sceneToLoad = 0;
        GameManager.instance.LoadScene(3);
    }
}
