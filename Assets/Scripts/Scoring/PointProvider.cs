using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointProvider : BaseScoreProvider
{
    public System.Int64 mPointsGiven;
    public System.Int64 Diminisher;

    protected override void ScoreFunction(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SendScore(mPointsGiven, collision.GetContact(0).point);
            gameObject.tag = "Projectile";
            mPointsGiven = System.Math.Max(mPointsGiven - Diminisher, 0);
        }
        if (collision.gameObject.tag == "Projectile")
        {
            SendScore((System.Int64)(mPointsGiven * 0.5), collision.GetContact(0).point);
        }
    }


}
