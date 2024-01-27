using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private List<LaunchController> LaunchControls;
    private int LaunchCounts;
    private bool MayLaunch;
    void Start()
    {
        
    }

    private void Awake()
    {
        LaunchControls = new List<LaunchController>();
        MayLaunch = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddLauncher(LaunchController item)
    {
        LaunchControls.Add(item);
        MayLaunch = true;
    }

    public void Launch()
    {
        LaunchCounts++;
        if(LaunchControls.Count == LaunchCounts && MayLaunch)
        {
            foreach(var launcher in LaunchControls)
            {
                launcher.ApplyForce();
            }
            MayLaunch = false;
        }
    }
}
