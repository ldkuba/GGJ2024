using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public Button playButton;
    public Button highscoresButton;
    public Button exitButton;

    public GameObject highscoresPanel;
    public GameObject highscoresContent;
    public TextMeshProUGUI highscoreEntryPrefab;
    public Button highscoresBackButton;

    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        mainMenuPanel.SetActive(true);
        highscoresPanel.SetActive(false);

        playButton.onClick.AddListener(Play);
        highscoresButton.onClick.AddListener(Highscores);
        exitButton.onClick.AddListener(Exit);
        highscoresBackButton.onClick.AddListener(HighscoresBack);
    }

    public void Play()
    {
        gameManager.LaunchGame();
    }

    public void Highscores()
    {
        // Fill in highscores
        foreach (Transform child in highscoresContent.transform) {
            Destroy(child.gameObject);
        }

        int index = 0;
        foreach (var highscore in GameManager.Instance.scoreService.Highscores) {
            var entry = Instantiate(highscoreEntryPrefab, highscoresContent.transform);
            entry.rectTransform.anchoredPosition += new Vector2(0.0f, -50.0f * index);
            entry.text = highscore.Points.ToString();
            index++;
        }

        mainMenuPanel.SetActive(false);
        highscoresPanel.SetActive(true);
    }

    public void HighscoresBack() {
        mainMenuPanel.SetActive(true);
        highscoresPanel.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
}
