using System;
using System.Collections;
using System.Collections.Generic;
using Assets;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager m_instance;
    public static GameManager Instance
    {
        get
        {
            if (m_instance == null)
            {
                // We don't want to create a new instance. One must be present
                m_instance = GameObject.FindObjectOfType<GameManager>();
                if(m_instance == null)
                {
                    m_instance = UnityEngine.SceneManagement.SceneManager.GetSceneAt(0).GetRootGameObjects()[0].AddComponent<GameManager>();
                    m_instance.scoreService = null;
                }
            }

            return m_instance;
        }
    }

    // Game levels
    public List<String> gameLevels = new List<String>();

    public ScoreService scoreService;

    void Start()
    {
        if (m_instance != null && m_instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
        m_instance = this;

        // Load highscores
        scoreService?.LoadHighscores();
    }

    public void LaunchGame() {
        // Load the first level
        ChangeLevel(gameLevels[0]);
    }

    void ChangeLevel(String name) {
        UnityEngine.SceneManagement.SceneManager.LoadScene(name);
    }

    public void LoadMainMenu() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
