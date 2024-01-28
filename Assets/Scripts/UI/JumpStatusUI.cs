using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI {
public class JumpStatusUI : MonoBehaviour
{
    public TextMeshProUGUI StatusText;
    public TextMeshProUGUI NextJumpText;
    public TextMeshProUGUI EndOfRoundText;

    public LevelManager LevelManager;

    public void Reset(int maxJumps) {
        StatusText.text = "Jumps: 1/" + maxJumps.ToString();
        NextJumpText.gameObject.SetActive(false);
        EndOfRoundText.gameObject.SetActive(false);
    }

    public void SetJump(int jump, int maxJumps) {
        StatusText.text = "Jumps: " + jump.ToString() + "/" + maxJumps.ToString();
    }

    public void ShowNextJump() {
        NextJumpText.gameObject.SetActive(true);
        EndOfRoundText.gameObject.SetActive(false);

        NextJumpText.text = "Score: " + GameManager.Instance.scoreService.Points + "\nPress Space/X/A to start next jump";
    }

    public void ShowEndOfRound() {
        NextJumpText.gameObject.SetActive(false);
        EndOfRoundText.gameObject.SetActive(true);

        String message = "";
        if(GameManager.Instance.scoreService.IsNewHighscore()) {
            message += "New Highscore!\n";
        }
        message += "Final Score: " + GameManager.Instance.scoreService.TotalPoints + "\nPress Space/X/A to restart or Escape/O/B to return to menu";
        EndOfRoundText.text = message;
    }

    void Update() {
        if(NextJumpText.gameObject.activeInHierarchy) {
            if(Input.GetAxis("Submit") > 0) {
                NextJumpText.gameObject.SetActive(false);
                LevelManager.StartNextJump();
                return;
            }
        }

        if(EndOfRoundText.gameObject.activeInHierarchy) {
            if(Input.GetAxis("Submit") > 0) {
                EndOfRoundText.gameObject.SetActive(false);
                LevelManager.RestartLevel();
                return;
            }else if(Input.GetAxis("Cancel") > 0) {
                EndOfRoundText.gameObject.SetActive(false);
                GameManager.Instance.LoadMainMenu();
                return;
            }
        }
    }
}
}
