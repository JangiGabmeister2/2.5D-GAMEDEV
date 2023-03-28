using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject pauseCanvas;
    public bool isPaused;

    public GameObject gameOverCanvas;

    public BeetleManager beetleManager;
    public void NextScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex + 1);
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void BackToMain()
    {
        SceneManager.LoadScene(0);
    }

    public void Retry()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
        Time.timeScale = 1f;
    }

    public void Pause()
    {
        pauseCanvas.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Resume()
    {
        pauseCanvas.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void GameOver()
    {
        if (beetleManager.currentHealth <= 0)
        {
            gameOverCanvas.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    void Start()
    {
        pauseCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape");
            if (isPaused)
            {
                Resume();
                Debug.Log("resumed)");
            }
            else
            {
                Pause();
                Debug.Log("paused");
            }
        }

        GameOver();
    }
}
