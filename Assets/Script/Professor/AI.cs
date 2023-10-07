using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.GlobalIllumination;

public class AI : MonoBehaviour
{
    public enum currentState { idle, trace, attack, damaged, partol ,dead};
    public currentState curState = currentState.idle;

    private Transform tr;
    private Transform playerTr;
    public Transform[] points;

    private NavMeshAgent nvAgent;
    private Animator animator;

    public float traceDist = 15f;
    public float attackDist = 3.2f;

    public float idleTime = 0f;

    private bool isDead = false;

    private void Start()
    {
        tr = this.gameObject.GetComponent<Transform>();
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
        nvAgent = this.gameObject.GetComponent<NavMeshAgent>();
        animator = this.transform.GetChild(0).gameObject.GetComponent<Animator>();

        StartCoroutine(this.CheckState());
        StartCoroutine(this.CheckStateForAction());
    }

    IEnumerator CheckState()
    {
        while(!isDead)
        {
            yield return new WaitForSeconds(0.2f);
            float dist = Vector3.Distance(playerTr.position, tr.position);

            if (dist <= attackDist)
            {
                curState = currentState.attack;
            }
            else if (dist <= traceDist)
            {
                idleTime = 0;
                curState = currentState.trace;
            }
            else if (idleTime > 5f)
            {
                curState = currentState.partol;
                Transform selectPoint = points[0];
                float selectdis = Vector3.Distance(tr.position, selectPoint.position);
                foreach (Transform point in points)
                {
                    float dis = Vector3.Distance(tr.position, point.position);
                    if (dis < selectdis)
                        selectPoint = point;
                }
            }
            else
            {
                curState = currentState.idle;
            }
        }
    }

    IEnumerator CheckStateForAction()
    {
        while (!isDead)
        {
            switch (curState)
            {
                case currentState.idle:
                    idleTime += Time.deltaTime;

                    nvAgent.destination = tr.position;
                    //animator.SetBool("isTrace", false);
                    break;

                case currentState.trace:
                    nvAgent.destination = playerTr.position;
                    //animator.SetBool("isTrace", true);
                    break;

                case currentState.attack:
                    break;

                case currentState.partol:
                    
                    //StartCoroutine(test());

                    break;
            }
            yield return null;
        }
    }

    IEnumerator test()
    {
        
        yield return null;
    }
}
