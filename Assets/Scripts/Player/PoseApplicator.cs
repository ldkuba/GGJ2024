using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoseApplicator : MonoBehaviour
{
    public List<AnimationClip> Animations;
    public int index;
    public string SwapAxis;
    public float SwapState;
    public GameObject ApplicationTarget;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        bool allow_interaction = SwapState == 0;
        SwapState = Input.GetAxisRaw(SwapAxis); //should be -1, 0, 1
        if(allow_interaction && SwapState != 0)
        {
            index += (int)SwapState;
            if (index == -1)
                index = Animations.Count - 1;
            if (index == Animations.Count)
                index = 0;
            Apply();

        }
    }

    public void Apply()
    {
        Animations[index].SampleAnimation(ApplicationTarget, 0);
    }
}
