using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHitActivateGravity : MonoBehaviour
{
    private Rigidbody Rigidy;
    // Start is called before the first frame update

    private void Start()
    {
        Rigidy = gameObject.GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Rigidy.useGravity = true;

    }
}
