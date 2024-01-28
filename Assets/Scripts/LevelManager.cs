using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;

public class LevelManager : MonoBehaviour
{
    public int NumJumps = 3;
    private int m_jumpCount;

    public JumpStatusUI JumpUI;
    public LevelScore LevelScore;

    public PlayerController PlayerController; 

    void Start() {
        m_jumpCount = 1;
        JumpUI.Reset(NumJumps);
        PlayerController.onEndOfRound.AddListener(JumpEnded);
        GameManager.Instance.scoreService.ClearPoints();
        LevelScore.Initialize();
    }

    public void JumpEnded() {
        if(m_jumpCount >= NumJumps) {
            // Save the highscore
            GameManager.Instance.scoreService.SaveHighscore();

            // Show game over UI
            JumpUI.ShowEndOfRound();
        }else {
            JumpUI.ShowNextJump();
        }
    }

    public void StartNextJump() {
        JumpUI.SetJump(++m_jumpCount, NumJumps);

        // Reset Jump points
        GameManager.Instance.scoreService.ClearJumpPoints();

        PlayerController.Reset();
    }

    public void RestartLevel() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
