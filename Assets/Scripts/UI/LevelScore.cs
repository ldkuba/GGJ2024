using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
public class LevelScore : MonoBehaviour
{
    private TextMeshProUGUI m_text;

    // Start is called before the first frame update
    public void Initialize()
    {
        m_text = GetComponent<TextMeshProUGUI>();
        m_text.text = "Score: 0\nTotal Score: 0";
        GameManager.Instance.scoreService.SetLevelScoreText(this);
    }

    public void SetScore(System.Int64 score, System.Int64 totalScore) {
        m_text.text = "Total Score: " + totalScore.ToString() + "\nScore: " + score.ToString();
    }
}
}
