using System.Collections;
using System.Collections.Generic;
using Mono.CSharp;
using StarterAssets;
using UnityEngine;

public class PlayerFootstepsScript : MonoBehaviour
{
    [SerializeField]
    private FootstepSet footSteps;

    [SerializeField]
    private Transform footstepParent;
    
    
    [Header("Components")]
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

    public void SetNewSet(FootstepSet newFootstepSet)
    {
        print($"Setting footsteps to: {newFootstepSet}");
        foreach (Transform t in footstepParent.GetComponentsInChildren<Transform>())
        {
            if (!t.Equals(footstepParent))
            {
                Destroy(t.gameObject);
            }
        }

        GameObject temp = Instantiate(newFootstepSet.gameObject, footstepParent);
        temp.transform.localPosition = new Vector3();
        footSteps = temp.GetComponent<FootstepSet>();
    }

    public void PlayFootsteps()
    {
        if (!footSteps)
        {
            return;
        }
        footSteps.PlayFootsteps();
    }

    public void StopFootsteps()
    {
        if (!footSteps)
        {
            return;
        }
        footSteps.StopFootsteps();
    }

}
