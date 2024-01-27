using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiminishingMove : MonoBehaviour
{
    public Vector3 MoveVector;
    public float FactorPerSecond;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveVector *= (1 - (FactorPerSecond * Time.deltaTime));
        transform.position += MoveVector * Time.deltaTime;
    }
}
