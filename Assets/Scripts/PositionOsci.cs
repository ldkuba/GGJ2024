using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionOsci : MonoBehaviour
{
    public ValueOscilator Osci;
    public Vector3 PositionMemory;
    public int Axis;
    public float LastOsciValue;
    // Start is called before the first frame update
    void Start()
    {
        Osci.Reinit();
        LastOsciValue = Osci.GetValue();
        PositionMemory = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(PositionMemory != transform.position)
        {
            Osci.Reinit();
        }
        Osci.Step(Time.deltaTime);
        Vector3 position = transform.position;
        float osci_value = Osci.GetValue();
        position[Axis] += osci_value - LastOsciValue;
        LastOsciValue = osci_value;
        transform.position = position;


        PositionMemory = transform.position;
    }
}
