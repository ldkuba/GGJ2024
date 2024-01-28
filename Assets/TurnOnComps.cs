using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnComps : MonoBehaviour
{
    public List<string> CompTypes;
    private List<Behaviour> Comps;
    // Start is called before the first frame update
    public void Start()
    {
        Comps = new List<Behaviour>();
        foreach(var comp in CompTypes)
        {
            Comps.Add((Behaviour)GetComponent(comp));
        }
        
    }
    public void TurnOn()
    {
        foreach (var comp in Comps)
            if(comp)
                comp.enabled = true;
    }
}
