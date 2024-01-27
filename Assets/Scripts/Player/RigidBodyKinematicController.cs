using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyKinematicController : MonoBehaviour
{
    public List<Rigidbody> ControlledBodies;
    public bool DefaultState;
    // Start is called before the first frame update
    void Start()
    {
        SetKinematic(DefaultState);
    }

    public void SetKinematic(bool State)
    {
        foreach (var body in ControlledBodies)
        {
            body.isKinematic = DefaultState;
        }
    }

}
