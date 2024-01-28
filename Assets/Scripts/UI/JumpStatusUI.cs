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

        NextJumpText.text = "Score: " + GameManager.Instance.scoreService.Points + "\nPress space to start next jump";
    }

    public void ShowEndOfRound() {
        NextJumpText.gameObject.SetActive(false);
        EndOfRoundText.gameObject.SetActive(true);

        EndOfRoundText.text = "Final Score: " + GameManager.Instance.scoreService.TotalPoints + "\nPress space to restart or escape to return to menu";
    }

    void Update() {
        if(NextJumpText.gameObject.activeInHierarchy) {
            if(Input.GetKeyDown(KeyCode.Space)) {
                NextJumpText.gameObject.SetActive(false);
                LevelManager.StartNextJump();
                return;
            }
        }

        if(EndOfRoundText.gameObject.activeInHierarchy) {
            if(Input.GetKeyDown(KeyCode.Space)) {
                EndOfRoundText.gameObject.SetActive(false);
                LevelManager.RestartLevel();
                return;
            }else if(Input.GetKeyDown(KeyCode.Escape)) {
                EndOfRoundText.gameObject.SetActive(false);
                GameManager.Instance.LoadMainMenu();
                return;
            }
        }
    }
}
}
