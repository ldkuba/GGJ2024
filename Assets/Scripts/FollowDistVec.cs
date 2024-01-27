using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowDistVec : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ToFollow;
    public Vector3 StaticVector;
    public float Drag;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var target_pose = ToFollow.transform.position + StaticVector;
        transform.position += (target_pose - transform.position) * Drag;
    }
}
