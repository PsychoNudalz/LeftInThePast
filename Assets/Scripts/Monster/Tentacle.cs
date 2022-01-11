using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class Tentacle : MonoBehaviour
{
    [SerializeField]
    private float maxRange = 3.4f;

    [SerializeField]
    private Transform ballLaunchPoint;

    [SerializeField]
    private float shootStrength = 300f;

    [SerializeField]
    private float shootDelay = 1f;


    [Header("Components")]
    [SerializeField]
    private ChainIKConstraint chainIKConstraint;

    [SerializeField]
    private TentacleBall tentacleBall;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private MonsterAI monsterAI;

    [SerializeField]
    private TentaclesHandler tentaclesHandler;

    [SerializeField]
    private Renderer tentacleRenderer;

    public TentaclesHandler TentaclesHandler
    {
        get => tentaclesHandler;
        set => tentaclesHandler = value;
    }




    [Header("Debug")]
    [SerializeField]
    private Vector3 worldPoint = new Vector3();

    [SerializeField]
    private bool isExtended = false;

    public bool IsExtended => isExtended;

    [SerializeField]
    private bool isRecalled = false;

    private Coroutine coroutine;

    [ContextMenu("Awake")]
    private void Awake()
    {
        if (!tentacleBall)
        {
            tentacleBall = GetComponent<TentacleBall>();
        }

        if (!animator)
        {
            animator = GetComponent<Animator>();
        }

        if (!monsterAI)
        {
            monsterAI = GetComponentInParent<MonsterAI>();
        }

        worldPoint = tentacleBall.transform.position;
        ShootTentacle();
    }

    private void Update()
    {
        if (!isExtended)
        {
            SetTentacle(tentacleBall.transform.position);
        }
        else
        {
            SetTentacle(worldPoint);
        }

// if the extended tentacle reached max distance
        if (isExtended && Vector3.Distance(transform.position, worldPoint) >= maxRange)
        {
            if (!isRecalled)
            {
                //coroutine = StartCoroutine(RetractAndShoot());
                RetractTentacle();
            }
        }
        // if the tentacle reached max distance and a bit without hitting anything
        else if (Vector3.Distance(transform.position, tentacleBall.transform.position) > maxRange * 1.1f)
        {
            if (!isRecalled)
            {
                //coroutine = StartCoroutine(RetractAndShoot());
                RetractTentacle();
            }
        }
    }

    public void ShootTentacle(Vector3 v)
    {
        // print("Shoot Tentacle");
        isExtended = false;
        tentacleBall.gameObject.SetActive(true);

        tentacleBall.transform.position = ballLaunchPoint.position;
        tentacleBall.LaunchBall(v);
    }

    public void ShootTentacle()
    {
        isRecalled = false;
        animator.SetBool("Extended", true);
        ShootTentacle(ballLaunchPoint.forward * shootStrength);
    }

    public void SetTentacleTarget(Vector3 target)
    {
        worldPoint = target;
        isExtended = true;
        tentacleBall.gameObject.SetActive(false);
    }

    public void SetTentacle(Vector3 position)
    {
        chainIKConstraint.data.target.position = position;
    }

    public void RetractTentacle()
    {
        animator.SetBool("Extended", false);
        isRecalled = true;
        tentaclesHandler?.AddToFreeStack(this);
    }

    IEnumerator RetractAndShoot()
    {
        // print($"{name} Retract and Shoot");
        RetractTentacle();
        yield return new WaitForSeconds(shootDelay);
        ShootTentacle();
        coroutine = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (tentaclesHandler)
        {
            if (tentaclesHandler.PlayerTagList.Contains(other.tag) && !PlayerHandlerScript.IgnorePlayer)
            {
                // print($"{name} triggered by: {other.name}");
                monsterAI.RecieveSoi(new SourceOfInterest("Player hit tentacle", other.ClosestPoint(transform.position),
                    SourceOfInterestType.Tentacle,
                    Vector3.Distance(other.transform.position, transform.position) * 2f));
            }
            else if (tentaclesHandler.ItemTagList.Contains(other.tag))
            {
                PickUpInteractables temp = other.GetComponentInParent<PickUpInteractables>();
                if (temp && temp.isMoving())
                {
                    monsterAI.RecieveSoi(new SourceOfInterest(temp.name, other.ClosestPoint(transform.position),
                        SourceOfInterestType.Tentacle,
                        Vector3.Distance(other.transform.position, transform.position) * 2f));
                }
            }
            else if (tentaclesHandler.CloneTagList.Contains(other.tag))
            {
                MonsterHandlerScript.Monster_TeleportToPlayerDimension();
            }
        }
    }
}