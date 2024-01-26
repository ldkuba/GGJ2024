using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointProvider : MonoBehaviour
{
    public Assets.ScoreService mScoreService;
    public System.Int64 mPointsGiven;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            mScoreService.AddPoints(mPointsGiven);
        }
    }
}
