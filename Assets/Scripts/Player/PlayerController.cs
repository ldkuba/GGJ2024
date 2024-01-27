using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Launch controls")]
    public UnityEvent onLaunch;
    private List<LaunchController> LaunchControls;
    private int LaunchCounts;
    private bool MayLaunch;

    // End of round timer
    [Header("End of round")]
    public float EndOfRoundDuration = 3.0f;
    public float StopVelocityThreshold = 0.000001f;
    public UnityEvent onEndOfRound;
    public UnityEvent endCounterStarted;
    bool m_isCountingDown = false;
    float m_endOfRoundTimer = 0.0f;
    Vector3 m_lastPosition;

    void Start()
    {
        onEndOfRound.AddListener(EndOfRoundCallback);
        endCounterStarted.AddListener(EndOfRoundStartCallback);
    }

    private void Awake()
    {
        LaunchControls = new List<LaunchController>();
        MayLaunch = false;
    }

    void OnDestroy() {
        onEndOfRound.RemoveAllListeners();
        endCounterStarted.RemoveAllListeners();
        onLaunch.RemoveAllListeners();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!MayLaunch) {
            // Calculate position delta and update last position
            float positionDelta = (transform.position - m_lastPosition).magnitude / Time.deltaTime;
            m_lastPosition = transform.position;

            if(m_isCountingDown) {
                // If we're counting down, check if we should stop
                if(positionDelta > StopVelocityThreshold) {
                    m_isCountingDown = false;
                }

                m_endOfRoundTimer -= Time.deltaTime;
                if(m_endOfRoundTimer <= 0.0f) {
                    m_isCountingDown = false;
                    onEndOfRound?.Invoke();
                }
            } else {
                // Otherwise, check if we should start
                if(positionDelta < StopVelocityThreshold) {
                    m_isCountingDown = true;
                    m_endOfRoundTimer = EndOfRoundDuration;
                    endCounterStarted?.Invoke();
                }
            }
        }
    }

    void EndOfRoundCallback() {
        Debug.Log("End of round callback");
        MayLaunch = true;
    }

    void EndOfRoundStartCallback() {
        Debug.Log("End of round start callback");
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
            onLaunch?.Invoke();
        }
    }
}
