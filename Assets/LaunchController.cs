using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchController : MonoBehaviour
{
    public string AxisVertical;
    public string AxisHorizonal;
    public Vector3 LaunchVector;
    public Vector3 MaxForce;
    public PlayerController PlayerController;
    public Rigidbody ForceTarget;

    private bool ReceivedInput;
    
    // Start is called before the first frame update
    void Start()
    {
        ReceivedInput = false;
        PlayerController.AddLauncher(this);
    }

    // Update is called once per frame
    void Update()
    {
        float force_modifier_y = Input.GetAxis(AxisVertical) * Time.deltaTime;
        float force_modifier_x = Input.GetAxis(AxisHorizonal) * Time.deltaTime;
        if (Mathf.Approximately(force_modifier_y, 0.0f) && Mathf.Approximately(force_modifier_x, 0.0f) && ReceivedInput)
        {
            PlayerController.Launch();
            this.enabled = false;
        }
        else if (!Mathf.Approximately(force_modifier_y, 0.0f) || !Mathf.Approximately(force_modifier_x, 0.0f))
        {
            ReceivedInput = true;
            LaunchVector.x += force_modifier_x;
            LaunchVector.z += force_modifier_y;
            LaunchVector.y = 3.0f;
        }


    }

    public void ApplyForce()
    {
        ForceTarget.AddForce(LaunchVector, ForceMode.VelocityChange);
    }
}
