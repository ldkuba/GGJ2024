﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets
{
    [CreateAssetMenu(fileName = "ScoreService", menuName = "ScriptableObjects/ScoreService", order = 1)]
    public class ScoreService : ScriptableObject
    {
        public System.Int64 Points;
        public GameObject Prefab;

        private UI.LevelScore m_levelScore;

        [Serializable]
        public struct Highscore
        {
            public System.Int64 Points;
            public System.Int64 NumJumps;
            // Whatever else we want to save, maybe per-level highscores?
            // Maybe name of player or date and time of run?
        }

        // Storage for user highscores between runs
        public List<Highscore> Highscores = new List<Highscore>();

        private void Awake()
        {
            Points = 0;
            Highscores.Clear();
        }

        public void ClearPoints() {
            Points = 0;
        }

        public void AddPoints(System.Int64 NewPoints, Vector3 CollisionPoint)
        {
            Debug.Log("got " + NewPoints + " points");
            Points += NewPoints;
            if (Prefab && NewPoints != 0)
            {
                var point_score_display = GameObject.Instantiate(Prefab);
                point_score_display.transform.position = CollisionPoint;
                var tmp = point_score_display.GetComponent<TMPro.TextMeshPro>();
                if (tmp)
                    tmp.text = NewPoints.ToString();
            }

            // Update current score level
            UpdateLevelScoreText(Points);
        }

        public void SetLevelScoreText(UI.LevelScore levelScore)
        {
            m_levelScore = levelScore;
            m_levelScore.SetScore(Points);
        }

        private void UpdateLevelScoreText(System.Int64 NewPoints)
        {
            if (m_levelScore != null)
                m_levelScore.SetScore(NewPoints);
            else
                Debug.LogError("No level score text set");
        }
    }
}
