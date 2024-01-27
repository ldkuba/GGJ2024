using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnControl : MonoBehaviour
{

    public float TimeToLive;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TimeToLive -= Time.deltaTime;
        if (TimeToLive < 0)
            Destroy(gameObject);
    }
}
