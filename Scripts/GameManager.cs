using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;


public class GameManager : MonoBehaviour
{
    float waititme = 1f;
    public UnityEvent SetupLevelEvenet;
    public UnityEvent startLevelEvent;
    public UnityEvent GameOverEvent;
    public UnityEvent RestartLevelEvent;

    bool isSetupReady;
    bool hasLevelStarted;
    bool isGameOver;
    bool isLeveRestart;

    public bool IsGameOver { get => isGameOver; set => isGameOver = value; }
    public bool IsLeveRestart { get => isLeveRestart; set => isLeveRestart = value; }

    private void Start()
    {
        StartCoroutine(GameLoop());
    }

    IEnumerator GameLoop()
    {
        yield return StartCoroutine(StartLevel());
        yield return StartCoroutine(GameLevel());
        yield return StartCoroutine(RestartLevel());
    }

    IEnumerator StartLevel()
    {
        if (SetupLevelEvenet != null)
        {
            SetupLevelEvenet.Invoke();

        }
        while (!isSetupReady)
        {
            yield return null;
        }
        if (startLevelEvent != null)
        {
            startLevelEvent.Invoke();
        }
    }

    IEnumerator GameLevel()
    {
        yield return new WaitForSeconds(waititme);
        while (!isGameOver)
        {
            yield return null;
        }
        if (GameOverEvent != null)
        {
            GameOverEvent.Invoke();
        }
    }

    IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(waititme);
        while (!IsLeveRestart)
        {
            yield return null;
        }
        if (RestartLevelEvent != null)
        {
            RestartLevelEvent.Invoke();
        }
        ReloadScence();
    }
    public void Startgame()
    {
        isSetupReady = true;
    }
    public void RestartGame()
    {
        IsLeveRestart = true;
    }
    void ReloadScence()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void Quitapp()
    {
        Application.Quit();
    }
}


