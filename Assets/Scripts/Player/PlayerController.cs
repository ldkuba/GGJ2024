using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    private PoseApplicator m_poseApplicator;
    public bool HardMode;
    // Start is called before the first frame update
    [Header("Launch controls")]
    public UnityEvent onLaunch;
    private List<LaunchController> LaunchControls;
    private int LaunchCounts;
    private bool m_inMotion;

    // End of round timer
    [Header("End of round")]
    public float EndOfRoundDuration = 3.0f;
    public float StopVelocityThreshold = 0.000001f;
    public UnityEvent onEndOfRound;
    public UnityEvent endCounterStarted;
    public GameObject targetForMotionCheck;
    bool m_isCountingDown = false;
    float m_endOfRoundTimer = 0.0f;
    Vector3 m_lastPosition;

    void Start()
    {
        HardMode = PlayerPrefs.GetString("Difficulty") == "HARD";
        onEndOfRound.AddListener(EndOfRoundCallback);
        endCounterStarted.AddListener(EndOfRoundStartCallback);

        m_lastPosition = targetForMotionCheck.transform.position;
        m_inMotion = false;
    }

    private void Awake()
    {
        LaunchControls = new List<LaunchController>();
        m_poseApplicator = GetComponent<PoseApplicator>();
    }

    void OnDestroy() {
        onEndOfRound.RemoveAllListeners();
        endCounterStarted.RemoveAllListeners();
        onLaunch.RemoveAllListeners();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(m_inMotion) {
            // Calculate position delta and update last position
            float positionDelta = (targetForMotionCheck.transform.position - m_lastPosition).magnitude / Time.deltaTime;
            m_lastPosition = targetForMotionCheck.transform.position;

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
        m_inMotion = false;
    }

    void EndOfRoundStartCallback() {
    }

    public void AddLauncher(LaunchController item)
    {
        LaunchControls.Add(item);
    }

    public void Launch()
    {
        LaunchCounts++;
        LaunchInternal();
    }

    private void LaunchInternal()
    {
        if(LaunchControls.Count <= LaunchCounts && LaunchControls.Count > 0)
        {
            onLaunch?.Invoke();
            foreach (var launcher in LaunchControls)
            {
                launcher.ApplyForce();
            }
            m_inMotion = true;
        }
    }

    public void Reset() {
        m_poseApplicator.Apply();
        Debug.Log("Resetting position");

        LaunchCounts = 0;
        foreach(var launcher in LaunchControls) {
            launcher.Reset();
        }
    }
}
