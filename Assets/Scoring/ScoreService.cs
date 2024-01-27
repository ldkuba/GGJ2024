using UnityEngine;

namespace Assets
{
    [CreateAssetMenu(fileName = "ScoreService", menuName = "ScriptableObjects/ScoreService", order = 1)]
    public class ScoreService : ScriptableObject
    {
        public System.Int64 Points;
        public GameObject Prefab;

        private void Awake()
        {
            Points = 0;
        }
        public void AddPoints(System.Int64 NewPoints, Vector3 CollisionPoint)
        {
            Debug.Log("got " + NewPoints + " points");
            Points += NewPoints;
            if (Prefab)
            {
                var point_score_display = GameObject.Instantiate(Prefab);
                point_score_display.transform.position = CollisionPoint;
                var tmp = point_score_display.GetComponent<TMPro.TextMeshPro>();
                if (tmp)
                    tmp.text = NewPoints.ToString();
            }
        }
    }
}
