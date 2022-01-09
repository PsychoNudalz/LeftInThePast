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

    [SerializeField]
    private GameObject dgPlate;

    [Header("Disc Slots")]
    [SerializeField]
    private List<JukeBoxDisc> jukeBoxDiscs;


    private DiscScript newDisc;
    private JukeBoxDisc currentDisc;
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
            StopAllDisc();
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
        CheckHasWin();

    }

    public void PlayDisc(int i)
    {
        if (!collectedDisks.Contains((DiscEnum) i))
        {
            Debug.Log($"Player do not have {((DiscEnum) i).ToString()}");
            return;
        }
        Debug.Log($"Player play {((DiscEnum) i).ToString()}");

        StopAllDisc();
        foreach (JukeBoxDisc jukeBoxDisc in jukeBoxDiscs)
        {
            if (jukeBoxDisc.DiscEnum.Equals((DiscEnum) i))
            {
                currentDisc = jukeBoxDisc;
                PlayDiscAnimation(i);
                return;
            }
        }
    }

    public void PlayDiscForAllDiscs(int i)
    {
        foreach (JukeBox jukeBox in FindObjectsOfType<JukeBox>())
        {
            jukeBox.PlayDisc(i);
        }
    }



    public void PlayDisc(DiscEnum discEnum)
    {
        PlayDisc((int)discEnum);
    }

    void PlayDiscAnimation(int i)
    {
        animator.SetTrigger("Play");
        animator.SetInteger("CurrentDisc",i);

    }

    public void PlayCurrentDisc()
    {
        currentDisc.PlayMusic();
    }

    public void StopAllDisc()
    {
        foreach (JukeBoxDisc jukeBoxDisc in jukeBoxDiscs)
        {
            jukeBoxDisc.StopMusic();
        }
        animator.SetTrigger("ToIdle");
    }


    public static void UpdateAllJukeBoxes()
    {
        foreach (JukeBox jukeBox in FindObjectsOfType<JukeBox>())
        {
            jukeBox.UpdateJukeBox();
        }
    }

    public void CheckHasWin()
    {
        foreach (var dEnum in Enum.GetValues(typeof(DiscEnum)))
        {
            if (!collectedDisks.Contains((DiscEnum)dEnum))
            {
                return;
            }
        }
        //GameManagerScript.SetGameWin();
        dgPlate.SetActive(true);
    }
}