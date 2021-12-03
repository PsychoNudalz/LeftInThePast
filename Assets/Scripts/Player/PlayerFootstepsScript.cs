using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class PlayerFootstepsScript : MonoBehaviour
{
    [SerializeField]
    private List<Sound> footstepSounds;

    [SerializeField]
    private Transform parentFootstep;
    
    [SerializeField]
    private Vector2 stepInterval = new Vector2(.7f, 1f);

    [SerializeField]
    private GameObject baseSoundPF;
    private float lastStepTime = 0f;

    [SerializeField]
    private FirstPersonController firstPersonController;
    // Start is called before the first frame update
    void Start()
    {
        if (!firstPersonController)
        {
            firstPersonController = GetComponent<FirstPersonController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void PlayFootsteps()
    {
        if (lastStepTime + Random.Range(stepInterval.x, stepInterval.y) < Time.time)
        {
            if (footstepSounds.Count != 0)
            {
                footstepSounds[Random.Range(0, footstepSounds.Count - 1)].PlayF();
            }

            lastStepTime = Time.time;
        }
    }

    void ClearFootstepSounds()
    {
        foreach (Sound sound in parentFootstep.GetComponents<Sound>())
        {
            Destroy(sound);
        }
    }

    public void SetFootstepSounds(List<Sound> newSounds)
    {
        for (int i = 0; i < footstepSounds.Count; i++)
        {
            footstepSounds[i].UpdateSourceClip(newSounds[i].Source);
        }
    }
}
