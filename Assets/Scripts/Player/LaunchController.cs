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
    public string EasyAxis;
    public Vector3 LaunchVector;
    public LimitedValue YRotationAngle;
    public LimitedValue XRotationAngle;
    public PlayerController PlayerController;
    public List<Rigidbody> ForceTargets;
    public Vector2 ForceFactor;
    public LaunchArrow Arrow;
    public ValueOscilator Osci;

    private bool ReceivedInput;
    
    // Start is called before the first frame update
    void Start()
    {
        PlayerController.AddLauncher(this);
        Osci.Reinit();
        Reset();
    }

    public void Reset() {
        this.enabled = true;
        ReceivedInput = false;
        Osci.Reinit();
        YRotationAngle.Reinit();
        XRotationAngle.Reinit();
        Arrow.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.HardMode)
            HardInput();
        else
            EasyInput();
    }

    public void EasyInput()
    {
        if (ReceivedInput)
        {
            Osci.Step(Time.deltaTime);
        }
        float force_modifier_y = Input.GetAxis(AxisVertical) * Time.deltaTime;
        float force_modifier_x = Input.GetAxis(AxisHorizonal) * Time.deltaTime;
        YRotationAngle.Change(force_modifier_y * ForceFactor.y);
        XRotationAngle.Change(force_modifier_x * ForceFactor.x);
        if (ReceivedInput && Mathf.Approximately(Input.GetAxis(EasyAxis), 0))
        {
            Lockdown();
        }
        else if(!Mathf.Approximately(Input.GetAxis(EasyAxis), 0))
        {
            ReceivedInput = true;
        }
        LaunchVector = Quaternion.AngleAxis(XRotationAngle.GetValue(), Vector3.up) * Quaternion.AngleAxis(YRotationAngle.GetValue(), Vector3.left) * Vector3.forward * Osci.GetValue();
        Arrow.SetArrow(LaunchVector, Osci.GetValue() / Osci.Max);
    }

    public void HardInput()
    {
        if (ReceivedInput)
        {
            Osci.Step(Time.deltaTime);
        }
        float force_modifier_y = Input.GetAxis(AxisVertical) * Time.deltaTime;
        float force_modifier_x = Input.GetAxis(AxisHorizonal) * Time.deltaTime;
        if (Mathf.Approximately(force_modifier_y, 0.0f) && Mathf.Approximately(force_modifier_x, 0.0f) && ReceivedInput)
        {
            Lockdown();
        }
        else if (!Mathf.Approximately(force_modifier_y, 0.0f) || !Mathf.Approximately(force_modifier_x, 0.0f))
        {
            ReceivedInput = true;
            YRotationAngle.Change(force_modifier_y * ForceFactor.y);
            XRotationAngle.Change(force_modifier_x * ForceFactor.x);
            LaunchVector = Quaternion.AngleAxis(XRotationAngle.GetValue(), Vector3.up) * Quaternion.AngleAxis(YRotationAngle.GetValue(), Vector3.left) * Vector3.forward * Osci.GetValue();

            // Pass in normalized force 0-1
            Arrow.SetArrow(LaunchVector, Osci.GetValue() / Osci.Max);
        }
    }
    public void ApplyForce()
    {
        ApplyForce(LaunchVector);
    }

    private void Lockdown()
    {
        PlayerController.Launch();
        ReceivedInput = false;
        this.enabled = false;
        Osci.Reinit();
        YRotationAngle.Reinit();
        XRotationAngle.Reinit();

        Arrow.Reset();
        Arrow.gameObject.SetActive(false);
    }

    public void ApplyForce(Vector3 Force)
    {
        foreach (var target in ForceTargets)
        {
            target.AddForce(Force, ForceMode.VelocityChange);
        }
    }

    public void ApplyYForce(float Power)
    {
        ApplyForce(new Vector3(0, Power, 0));
    }
}
