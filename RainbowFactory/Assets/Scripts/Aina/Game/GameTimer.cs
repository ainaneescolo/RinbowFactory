using System;
using System.Collections;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public static GameTimer instance;

    [Header("----- Timer Variables -----")] 
    [SerializeField] private float gameDurationTime;
    public bool gameRunning;
    private float time;
    private int minutes, seconds;

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

    private void Start()
    {
        StartTimer();
    }

    private void StartTimer()
    {
        gameRunning = true;
        StartCoroutine("Timer");
    }

    public void StopTimer()
    {
        gameDurationTime = time;
        gameRunning = false;
    }
    
    public void SetTimer()
    {
        gameDurationTime = 120;
        // En un futur fer una llista de tots els temps dels nivells detectar quin nivell es i passar el idex
        // En un Game Manager
    }

    IEnumerator Timer()
    {
        time = gameDurationTime;
        
        while (gameRunning)
        {
            time -= Time.deltaTime;

            minutes = (int)(time / 60f);
            seconds = (int)(time - minutes * 60f);
            UIGameManager.instance.timerTxt.text = $"{minutes} : {seconds}";

            if (time <= 0)
            {
                gameRunning = false;
                LevelManager.instance.GameOver();
            }
            yield return null;
        }
    }
}
