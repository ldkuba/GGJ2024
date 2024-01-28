using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets
{
    [CreateAssetMenu(fileName = "ScoreService", menuName = "ScriptableObjects/ScoreService", order = 1)]
    public class ScoreService : ScriptableObject
    {
        public System.Int64 TotalPoints;
        public System.Int64 Points;
        public GameObject Prefab;

        private UI.LevelScore m_levelScore;

        [Serializable]
        public struct Highscore
        {
            public System.Int64 Points;
            // Whatever else we want to save, maybe per-level highscores?
            // Maybe name of player or date and time of run?
        }

        [Serializable]
        private struct SerializableHighscores
        {
            public List<Highscore> Highscores;
        }

        // Storage for user highscores between runs
        public List<Highscore> Highscores = new List<Highscore>();

        private void Awake()
        {
            Points = 0;
            TotalPoints = 0;
            Highscores.Clear();
        }

        public void ClearPoints() {
            Points = 0;
            TotalPoints = 0;
        }

        public void ClearJumpPoints() {
            Points = 0;
            UpdateLevelScoreText(Points, TotalPoints);
        }

        public void AddPoints(System.Int64 NewPoints, Vector3 CollisionPoint)
        {
            Debug.Log("got " + NewPoints + " points");
            Points += NewPoints;
            TotalPoints += NewPoints;
            if (Prefab && NewPoints != 0)
            {
                var point_score_display = GameObject.Instantiate(Prefab);
                point_score_display.transform.position = CollisionPoint;
                var tmp = point_score_display.GetComponent<TMPro.TextMeshPro>();
                if (tmp)
                    tmp.text = NewPoints.ToString();
            }

            // Update current score level
            UpdateLevelScoreText(Points, TotalPoints);
        }

        public void SetLevelScoreText(UI.LevelScore levelScore)
        {
            m_levelScore = levelScore;
            m_levelScore.SetScore(Points, TotalPoints);
        }

        private void UpdateLevelScoreText(System.Int64 NewPoints, System.Int64 TotalPoints)
        {
            if (m_levelScore != null)
                m_levelScore.SetScore(NewPoints, TotalPoints);
            else
                Debug.LogError("No level score text set");
        }

        public void SaveHighscore()
        {
            // Add new highscore sorted
            for(int i = 0; i < Highscores.Count; i++) {
                if(Highscores[i].Points < this.TotalPoints) {
                    Highscores.Insert(i, new Highscore { Points = this.TotalPoints });
                    break;
                }
            }

            // Save to file
            string highscoresString = JsonUtility.ToJson(new SerializableHighscores {Highscores = this.Highscores});
            System.IO.File.WriteAllText(Application.persistentDataPath + "/highscores.json", highscoresString);
        }

        public void LoadHighscores() {
            // Load from file
            try {
                string highscoresString = System.IO.File.ReadAllText(Application.persistentDataPath + "/highscores.json");
                SerializableHighscores serialHighscores = JsonUtility.FromJson<SerializableHighscores>(highscoresString);
                Highscores = serialHighscores.Highscores;

                // Sort highscores to be safe
                Highscores.Sort((a, b) => b.Points.CompareTo(a.Points));
            } catch (Exception) {
                Debug.Log("No highscores file found");
                return;
            }
        }
    }
}
