using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class PlayerFootstepsScript : MonoBehaviour
{
    [SerializeField]
    private RepeatableSound footSteps;

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
        footSteps.Play();
    }

    public void StopFootsteps()
    {
        footSteps.Stop();
    }

}
