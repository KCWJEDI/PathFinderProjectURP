using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.GlobalIllumination;

public class AI : MonoBehaviour
{
    [SerializeField] private Transform thisTr;
    [SerializeField] private Transform playerTr;
    [SerializeField] private Player player;
    [SerializeField] private Transform[] points;
    [SerializeField] private NavMeshAgent nvAgent;
    [SerializeField] private float speed = 2;
    [SerializeField] private float angerSpeed = 3;
    [SerializeField] private State curruntState;
    [SerializeField] private float idleTime;
    [SerializeField] private int lastPointIdx;
    [SerializeField] private int destPointIdx;
    [SerializeField] private GameObject auraEffect;

    public enum State {
        START,
        IDLE,
        PATROL,
        CHASE,
        ANGER,
        DAMAGED,
        RETURN,
        END
    };

    private void Start()
    {
        curruntState = State.START;

        thisTr = this.gameObject.GetComponent<Transform>();
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
        player = playerTr.GetComponent<Player>();
        nvAgent = this.gameObject.GetComponent<NavMeshAgent>();
        idleTime = 0;
        lastPointIdx = 0;

        auraEffect = this.gameObject.transform.GetChild(0).GetChild(2).gameObject;

        StartCoroutine(this.Routine());
    }
    private int FindNearPointIdx()
    {
        int nearPointIdx = 0;

        for (int i = 0; i < points.Length; i++)
        {
            if (Vector3.Distance(thisTr.position, points[nearPointIdx].position) > Vector3.Distance(thisTr.position, points[i].position))
                nearPointIdx = i;
        }

        return nearPointIdx;
    }

    private bool IsPlayerAround()
    {
        if (player.isCabinetIn)
            return false;
        if (Vector3.Distance(thisTr.position, playerTr.position) < 15f)
            return true;
        return false;
    }

    private bool IsPlayerAttackable()
    {
        if (Vector3.Distance(thisTr.position, playerTr.position) < 1.5f)
            return true;
        return false;
    }

    //스킬 사용시 호출
    public void AttakProfesser()
    {
        if (Random.value < 0.8)
        {
            idleTime = 0;
            curruntState = State.DAMAGED;
            Debug.Log("DAMAGED");
        }
        else
        {
            Debug.Log("ANGER");
            curruntState = State.ANGER;
        }
    }

    IEnumerator Routine()
    {
        while(true)
        {
            yield return null;

            switch (curruntState)
            {
                case State.START:
                    {
                        nvAgent.destination = thisTr.position;
                        curruntState = State.RETURN;
                    }
                    break;
                case State.IDLE:
                    {
                        nvAgent.destination = thisTr.position;
                        idleTime += Time.deltaTime;
                        if (idleTime >= 5)
                        {
                            if (IsPlayerAround())
                                curruntState = State.CHASE;
                            else
                                curruntState = State.RETURN;
                            idleTime = 0;
                        }
                    }
                    break;
                case State.PATROL:
                    {
                        if (lastPointIdx == points.Length - 1)
                            destPointIdx = 0;
                        else
                            destPointIdx = lastPointIdx + 1;

                        nvAgent.destination = points[destPointIdx].position;

                        if (Vector3.Distance(thisTr.position, points[destPointIdx].position) < 0.5)
                        {
                            lastPointIdx = destPointIdx;
                        }

                        if (IsPlayerAround())
                            curruntState = State.CHASE;
                    }
                    break;
                case State.CHASE:
                    nvAgent.destination = playerTr.position;
                    nvAgent.speed = speed;
                    if (IsPlayerAttackable())
                    {
                        curruntState = State.END;
                        //GameOver
                    }
                    if (IsPlayerAround() == false)
                        curruntState = State.RETURN;
                    if (playerTr.GetComponent<Player>().isCabinetIn)
                        curruntState = State.IDLE;
                    break;
                case State.ANGER:
                    nvAgent.destination = playerTr.position;
                    nvAgent.speed = angerSpeed;
                    auraEffect.SetActive(true);

                    if (IsPlayerAttackable())
                    {
                        curruntState = State.END;
                        //GameOver
                    }
                    if (IsPlayerAround() == false)
                        curruntState = State.RETURN;
                    if (playerTr.GetComponent<Player>().isCabinetIn)
                        curruntState = State.IDLE;
                    break;
                case State.DAMAGED:
                    {
                        idleTime += Time.deltaTime;

                        nvAgent.destination = thisTr.position;
                        if (idleTime >= 5)
                        {
                            if (IsPlayerAround())
                                curruntState = State.CHASE;
                            else
                                curruntState = State.RETURN;
                            idleTime = 0;
                        }
                    }
                    break;
                case State.RETURN:
                    {
                        int lastPointIdx = FindNearPointIdx();
                        curruntState = State.PATROL;
                    }
                    break;
                case State.END:
                    nvAgent.destination = thisTr.position;
                    break;
            }
        }
    }
}



//public enum currentState { idle, trace, attack, damaged, partol ,dead , goback};
//public currentState curState = currentState.idle;

//private Transform tr;
//private Transform playerTr;
//public Transform[] points;

//Transform selectPoint;

//private NavMeshAgent nvAgent;
//private Animator animator;

//public float traceDist = 15f;
//public float attackDist = 3.2f;

//public float idleTime = 0f;

//private bool isDead = false;

//private void Start()
//{
//    tr = this.gameObject.GetComponent<Transform>();
//    playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
//    nvAgent = this.gameObject.GetComponent<NavMeshAgent>();
//    animator = this.transform.GetChild(0).gameObject.GetComponent<Animator>();

//    selectPoint = points[0];

//    StartCoroutine(this.CheckState());
//    StartCoroutine(this.CheckStateForAction());
//}

//IEnumerator CheckState()
//{
//    while(!isDead)
//    {
//        yield return new WaitForSeconds(0.2f);
//        float dist = Vector3.Distance(playerTr.position, tr.position);

//        if (dist <= attackDist)
//        {
//            curState = currentState.attack;
//        }
//        else if (dist <= traceDist)
//        {
//            idleTime = 0;
//            curState = currentState.trace;
//        }
//        else if (idleTime > 5f)
//        {
//            curState = currentState.goback;
//        }
//        else if (Vector3.Distance(tr.position, selectPoint.position) < 0.5f)
//        {
//            curState = currentState.partol;
//        }
//        else
//        {
//            curState = currentState.idle;
//        }
//    }
//}

//IEnumerator CheckStateForAction()
//{
//    while (!isDead)
//    {
//        switch (curState)
//        {
//            case currentState.idle:
//                idleTime += Time.deltaTime;

//                nvAgent.destination = tr.position;
//                //animator.SetBool("isTrace", false);
//                break;

//            case currentState.trace:
//                nvAgent.destination = playerTr.position;
//                //animator.SetBool("isTrace", true);
//                break;

//            case currentState.attack:
//                break;

//            case currentState.partol:
//                {
//                    idleTime = 0; 
//                    int currentIndex = 0;
//                    int desIndex;
//                    float selectdis = Vector3.Distance(tr.position, selectPoint.position);
//                    for (int i = 0; i < points.Length; i++)
//                    {
//                        float dis = Vector3.Distance(tr.position, points[i].position);
//                        if (dis < selectdis)
//                            currentIndex = i;
//                    }
//                    if (currentIndex + 1 == points.Length - 1)
//                    {
//                        desIndex = 0;
//                    }
//                    else
//                    {
//                        desIndex = currentIndex + 1;
//                    }
//                    selectPoint = points[desIndex];
//                    break;
//                }

//            case currentState.goback:
//                {
//                    curState = currentState.partol;
//                    float selectdis = Vector3.Distance(tr.position, selectPoint.position);
//                    foreach (Transform point in points)
//                    {
//                        float dis = Vector3.Distance(tr.position, point.position);
//                        if (dis < selectdis)
//                            selectPoint = point;
//                    }
//                    break;
//                }
//        }
//        yield return null;
//    }
//}

//IEnumerator test()
//{

//    yield return null;
//}