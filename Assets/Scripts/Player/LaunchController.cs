using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ValueOscilator
{
    public float Min;
    public float Max;
    public float InitValue;
    public float Current;
    public float OsciSpeed;
    public float CurrentOsciSpeed;
    public float OsciFactor; // Increase of speed when reached min after reaching max

    public void Reinit()
    {
        Current = InitValue;
        CurrentOsciSpeed = OsciSpeed;
    }
    public void Step(float time)
    {
        Current += CurrentOsciSpeed * time;
        if(Current > Max)
        {
            Current = Max;
            CurrentOsciSpeed *= -1;
        }
        if(Current < Min)
        {
            Current = Min;
            CurrentOsciSpeed *= -OsciFactor;
        }
    }
    public float GetValue()
    {
        return Current;
    }
}

[System.Serializable]
public class LimitedValue
{
    public float Min;
    public float Max;
    public float InitValue;
    public float Current;

    public void Reinit()
    {
        Current = InitValue;
    }
    public void Change(float Diff)
    {
        Current = Mathf.Clamp(Current + Diff, Min, Max);
    }
    public float GetValue()
    {
        return Current;
    }
}

public class LaunchController : MonoBehaviour
{
    public string AxisVertical;
    public string AxisHorizonal;
    public Vector3 LaunchVector;
    public LimitedValue YRotationAngle;
    public LimitedValue XRotationAngle;
    public PlayerController PlayerController;
    public List<Rigidbody> ForceTargets;
    public Vector2 ForceFactor;
    public GameObject DisplayItem;
    public ValueOscilator Osci;

    private bool ReceivedInput;
    
    // Start is called before the first frame update
    void Start()
    {
        ReceivedInput = false;
        PlayerController.AddLauncher(this);
        Osci.Reinit();
        YRotationAngle.Reinit();
        XRotationAngle.Reinit();
    }

    // Update is called once per frame
    void Update()
    {
        if(ReceivedInput)
        {
            Osci.Step(Time.deltaTime);
        }
        float force_modifier_y = Input.GetAxis(AxisVertical) * Time.deltaTime;
        float force_modifier_x = Input.GetAxis(AxisHorizonal) * Time.deltaTime;
        if (Mathf.Approximately(force_modifier_y, 0.0f) && Mathf.Approximately(force_modifier_x, 0.0f) && ReceivedInput)
        {
            PlayerController.Launch();
            ReceivedInput = false;
            this.enabled = false;
            Osci.Reinit();
            YRotationAngle.Reinit();
            XRotationAngle.Reinit();
        }
        else if (!Mathf.Approximately(force_modifier_y, 0.0f) || !Mathf.Approximately(force_modifier_x, 0.0f))
        {
            ReceivedInput = true;
            YRotationAngle.Change(force_modifier_y * ForceFactor.y);
            XRotationAngle.Change(force_modifier_x * ForceFactor.x);
            LaunchVector = Quaternion.AngleAxis(XRotationAngle.GetValue(), Vector3.up) * Quaternion.AngleAxis(YRotationAngle.GetValue(), Vector3.left) * Vector3.forward * Osci.GetValue();
            if (DisplayItem != null)
                GameObject.Instantiate(DisplayItem);
        }
    }

    public void ApplyForce()
    {
        foreach (var target in ForceTargets)
        {
            target.AddForce(LaunchVector, ForceMode.VelocityChange);
        }

    }
}
