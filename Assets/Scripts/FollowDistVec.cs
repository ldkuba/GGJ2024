using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowDistVec : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ToFollow;
    public Vector3 StaticVector;
    public float Drag;
    public bool AllowIdleSpin;
    public float IdleSpin; // if value > 0 starts slowly spining after x seconds
    private Vector3 InternalVector;
    private float InitialAngleY;
    private float IdleCounter;
    void Start()
    {
        InternalVector = StaticVector;
        InitialAngleY = transform.eulerAngles.y;
    }

    public void SetIdleSpinAllowance(bool Value)
    {
        AllowIdleSpin = Value;
    }

    // Update is called once per frame
    void Update()
    {
        var target_pose = ToFollow.transform.position + InternalVector;
        transform.position += (target_pose - transform.position) * Drag;
        if (IdleSpin > 0)
        {
            if (Input.anyKey || !AllowIdleSpin)
            {
                InternalVector = StaticVector;
                IdleCounter = 0;
                var angles = transform.eulerAngles;
                angles.y = InitialAngleY;
                transform.eulerAngles = angles;
            }
            IdleCounter += Time.deltaTime;
            if(IdleCounter > IdleSpin && AllowIdleSpin)
            {
                float angle = Time.deltaTime * 30;
                InternalVector = Quaternion.AngleAxis(angle, Vector3.up) * InternalVector;
                var angles = transform.eulerAngles;
                angles.y += angle;
                transform.eulerAngles = angles;

            }
        }
    }
}
