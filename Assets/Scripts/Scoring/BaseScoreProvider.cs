using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseScoreProvider : MonoBehaviour
{
    public Assets.ScoreService ScoreService;
    public AudioClip Clip;
    private AudioClip UsedClip;
    public AudioSource Source;
    public float Begin;
    public float End;

    protected abstract void ScoreFunction(Collision collision);

    private void Start()
    {
        if (Clip)
            UsedClip = SubClip(Clip, Begin, End);
    }
    void OnCollisionEnter(Collision collision)
    {
        ScoreFunction(collision);
        if (UsedClip && Source)
        {
            Source.PlayOneShot(UsedClip);
        }
    }

    protected void SendScore(System.Int64 points, Vector3 Location)
    {
        ScoreService.AddPoints(points, Location + Vector3.up);
    }

    static protected AudioClip SubClip(AudioClip clip, float start, float stop)
    {
        // See https://discussions.unity.com/t/how-to-play-specific-part-of-the-audio/142016
        /* Create a new audio clip */
        int frequency = clip.frequency;
        float timeLength = stop - start;
        int samplesLength = (int)(frequency * timeLength);
        AudioClip newClip = AudioClip.Create(clip.name + "-sub", samplesLength, 1, frequency, false);
        
        /* Create a temporary buffer for the samples */
        float[] data = new float[samplesLength];
        /* Get the data from the original clip */
        clip.GetData(data, (int)(frequency * start));
        /* Transfer the data to the new clip */
        newClip.SetData(data, 0);
        
        /* Return the sub clip */
        return newClip;
    }
}
