using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchArrow : MonoBehaviour
{
    [SerializeField]
    private GameObject Arrow;
    // The general size of the arrow in all axis
    public float ArrowSize = 0.2f;
    // The minimum length of the arrow at force = 0
    public float ArrowMinLength = 0.5f;

    // How much the arrow is stretched by force
    public float ArrowLengthScale = 3.0f;

    public void SetArrow(Vector3 launchDirection, float launchForce) {
        this.transform.rotation = Quaternion.LookRotation(launchDirection);
        Arrow.transform.localScale = new Vector3(ArrowSize, ArrowSize, ArrowSize * (ArrowMinLength + launchForce * ArrowLengthScale));
    }

    public void Reset(){
        this.transform.rotation = Quaternion.identity;
        Arrow.transform.localScale = new Vector3(ArrowSize, ArrowSize, ArrowSize);
    }
}
