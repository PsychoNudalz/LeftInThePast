using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JukeBox : MonoBehaviour

{
    [Header("Components")]
    [SerializeField]
    private Transform slotDiscParent;

    [SerializeField]
    private Animator animator;

    [Header("Disc Slots")]
    [SerializeField]
    private List<JukeBoxDisc> jukeBoxDiscs;


    private DiscScript newDisc;
    public Transform SlotDiscParent => slotDiscParent;


    //Static
    public static List<DiscEnum> collectedDisks = new List<DiscEnum>();

    private void Awake()
    {
        if (!animator)
        {
            animator = GetComponent<Animator>();
        }
        if (!slotDiscParent)
        {
            slotDiscParent = transform;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        newDisc = collision.collider.GetComponentInParent<DiscScript>();
        if (newDisc)
        {
            newDisc.OnSlot();
            animator.SetTrigger("Insert");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddDisc(DiscScript d)
    {
        d.gameObject.SetActive(false);
        if (!collectedDisks.Contains(d.DiscEnum))
        {
            collectedDisks.Add(d.DiscEnum);
        }
        UpdateAllJukeBoxes();
        
    }

    public void AddNewDisc()
    {
        AddDisc(newDisc);
    }

    void UpdateJukeBox()
    {
        foreach (DiscEnum collectedDisk in collectedDisks)
        {
            foreach (JukeBoxDisc jukeBoxDisc in jukeBoxDiscs)
            {
                if (jukeBoxDisc.DiscEnum.Equals(collectedDisk))
                {
                    jukeBoxDisc.PlayAddAnimation();
                }
            }
        }
    }
    
    

    public static void UpdateAllJukeBoxes()
    {
        foreach (JukeBox jukeBox in FindObjectsOfType<JukeBox>())
        {
            jukeBox.UpdateJukeBox();
        }
    }
}
