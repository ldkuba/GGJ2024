using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TimedPose
{
    float Time;
    Pose Pose;
}

public class ScreenTunaBehaviour : MonoBehaviour
{
    private AnimationClip Clip;
    TimedPose OfScreen;
    TimedPose CenterScreen;
    TimedPose Wait;
    TimedPose SpinOne;
    TimedPose SpinTwo;
    TimedPose LeaveScreen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
