using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
public class MainMenu : MonoBehaviour
{
    public Button playButton;
    public Button highscoresButton;
    public Button exitButton;

    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        playButton.onClick.AddListener(Play);
        highscoresButton.onClick.AddListener(Highscores);
        exitButton.onClick.AddListener(Exit);
    }

    public void Play()
    {
        gameManager.LaunchGame();
    }

    public void Highscores()
    {
        // Debug log
        Debug.Log("Highscores clicked");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
}
