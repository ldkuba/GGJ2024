using UnityEngine;

namespace Assets
{
    [CreateAssetMenu(fileName = "ScoreService", menuName = "ScriptableObjects/ScoreService", order = 1)]
    public class ScoreService : ScriptableObject
    {
        public System.Int64 Points;

        private void Awake()
        {
            Points = 0;
        }
        public void AddPoints(System.Int64 NewPoints)
        {
            Debug.Log("got " + NewPoints + " points");
            Points += NewPoints;
        }
    }
}
