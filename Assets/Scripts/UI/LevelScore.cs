using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
public class LevelScore : MonoBehaviour
{
    private TextMeshProUGUI m_text;
    public Assets.ScoreService ScoreService;

    // Start is called before the first frame update
    void Start()
    {
        m_text = GetComponent<TextMeshProUGUI>();
        m_text.text = "Score: 0";
        ScoreService.SetLevelScoreText(this);
    }

    public void SetScore(System.Int64 score) {
        m_text.text = "Score: " + score.ToString();
    }
}
}
