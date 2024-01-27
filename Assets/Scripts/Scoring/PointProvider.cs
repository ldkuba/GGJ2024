using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointProvider : BaseScoreProvider
{
    public System.Int64 mPointsGiven;

    protected override void ScoreFunction(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SendScore(mPointsGiven, collision.GetContact(0).point);
        }
    }
}
