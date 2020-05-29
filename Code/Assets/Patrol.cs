using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.AI;
using System.Collections;


public class Patrol : MonoBehaviour
{
    private AudioSource audioSource;
    public Transform[] points;
    private Transform PlayerTransform;
    private int destPoint = 0;
    private NavMeshAgent agent;
    private bool followPlayer = false;
    private bool waiting = false;
    private Animator anim;
    private bool Alive = true;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = true;
        GotoNextPoint();
    }


    void GotoNextPoint()
    {
        if (points.Length == 0)
            return;

        agent.destination = points[destPoint].position;
        destPoint = (destPoint + 1) % points.Length;
    }

    void GoToPlayer()
    {
        agent.destination = PlayerTransform.position;
    }


    void Update()
    {
        if (Alive)
        {
            if (followPlayer)
            {
                if (Vector3.Distance(gameObject.transform.position, PlayerTransform.position) < 1.9f)
                {
                    GetComponent<VampireController>().attack();

                }
                GoToPlayer();
            }
            else if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                anim.SetFloat("PosY", 0);
                if (!waiting)
                {
                    StartCoroutine("walkWait");
                    waiting = true;
                    audioSource.Play();
                    
                }
            }
            else
            {
                anim.SetFloat("PosY", 1);
                audioSource.Play();
            }
        }
        else
        {
            agent.isStopped = true;        }

    }


    public void playerPlaceMent(bool isHeIn,Transform player)
    {
        followPlayer = isHeIn;
        PlayerTransform = player;
    }

    public void death()
    {
        Alive = false;
    }

    private IEnumerator walkWait()
    {
        yield return new WaitForSeconds(2.0f);
        waiting = false;
        GotoNextPoint();

    }
}
