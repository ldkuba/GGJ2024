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
    private int CollisionCount;
    protected abstract void ScoreFunction(Collision collision);
    protected bool EnterColliderType(string Tag)
    {
        if (Tag == "Player")
        {
            bool ret = CollisionCount == 0;
            CollisionCount++;
            return ret;
        }
        else return true;
    }

    protected void LeaveColliderType(string Tag)
    {
        if (Tag == "Player")
        {
            CollisionCount--;
        }
    }

    private void Start()
    {
        CollisionCount = 0;
        if (Clip)
            UsedClip = SubClip(Clip, Begin, End);
    }
    void OnCollisionEnter(Collision collision)
    {
        if (EnterColliderType(collision.gameObject.tag))
        {
            ScoreFunction(collision);
            if (UsedClip && Source && collision.gameObject.tag != "BlockedFromSoundPlay")
            {
                Source.PlayOneShot(UsedClip);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        LeaveColliderType(collision.gameObject.tag);
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
