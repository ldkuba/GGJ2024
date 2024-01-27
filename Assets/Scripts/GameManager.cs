using System;
using System.Collections;
using System.Collections.Generic;
using Assets;
using UnityEditor;
using UnityEditor.SearchService;
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
            }

            return m_instance;
        }
    }

    // Game levels
    public List<SceneAsset> gameLevels = new List<SceneAsset>();

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
    }

    public void LaunchGame() {
        // Load the first level
        ChangeLevel(gameLevels[0].name);
    }

    void ChangeLevel(String name) {
        scoreService.ClearPoints();
        UnityEngine.SceneManagement.SceneManager.LoadScene(name);
    }
}
