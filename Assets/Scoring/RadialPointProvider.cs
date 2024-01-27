using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RadPoint
{
    public float Rad;
    public System.Int64 Points;
}

public static class VecExt
{
    public static Vector3 InverseScale(this Vector3 Base, Vector3 Scale)
    {
        return new Vector3( Base.x / Scale.x, Base.y / Scale.y, Base.z / Scale.z);
    }
}

public class RadialPointProvider : MonoBehaviour
{
    public RadPoint[] Scores;
    public float ObjectRadius;
    public Assets.ScoreService ScoreService;
    // Start is called before the first frame update

    void OnCollisionEnter(Collision collision)
    {

        Vector3 optimal_point = Vector3.left * 500;
        float dist = float.MaxValue;
        System.Int64 awarded_points = 0;

        if (collision.gameObject.tag == "Player")
        {
            Plane this_plane = new Plane(transform.up, transform.position);
            for (int i = 0; i < collision.contactCount; i++)
            {
                ContactPoint this_contact = collision.GetContact(i);
                var dir_vec = (this_contact.point - transform.position);
                // check if behind -> no relevant collision
                if(!this_plane.GetSide(this_contact.point))
                {
                    continue;
                }
                var dir_dist = dir_vec.magnitude;
                if (dir_dist < dist)
                {
                    dist = dir_dist;
                    optimal_point = dir_vec;
                }
            }
            Vector4 tmp = transform.worldToLocalMatrix * optimal_point;
            Vector3 projected_point = new Vector3(tmp.x, tmp.y, tmp.z);
            float projected_magnitude = projected_point.magnitude;
            Debug.Log(projected_point.ToString());
            Debug.Log(projected_magnitude.ToString());
            foreach(var score_data in Scores)
            {
                if(projected_magnitude < score_data.Rad)
                {
                    awarded_points = score_data.Points;
                    break;
                }
            }
            ScoreService.AddPoints(awarded_points, collision.GetContact(0).point + Vector3.up);
        }
    }
}
