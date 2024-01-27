using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHitDestroyOther : MonoBehaviour
{
    public GameObject OtherObject;

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(OtherObject);

    }
}
