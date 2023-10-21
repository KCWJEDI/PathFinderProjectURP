using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    [SerializeField] private Transform thisTr;
    [SerializeField] private Transform playerTr;
    [SerializeField] private Player player;
    [SerializeField] private Transform[] points;
    [SerializeField] private NavMeshAgent nvAgent;
    public AudioSource audio;
    public SpeechSound sound;
    [SerializeField] private float speed = 2;
    [SerializeField] private float angerSpeed = 3;
    [SerializeField] private State currentState;
    [SerializeField] private float idleTime;
    [SerializeField] private int lastPointIdx;
    [SerializeField] private int destPointIdx;
    [SerializeField] private GameObject auraEffect;
    [SerializeField] private Animator animator;
    [SerializeField] private bool isplayingSound = true;
    [SerializeField] private PlayerTriggerDialogue PTD;

    [SerializeField] private GameObject Canvas_AttackButton;
    [SerializeField] private GameObject Canvas_GameOver;
    public int count = 0;
    private static readonly int MotionCount = Animator.StringToHash("MotionCount");

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
        currentState = State.START;

        thisTr = this.gameObject.GetComponent<Transform>();
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
        player = playerTr.GetComponent<Player>();
        nvAgent = this.gameObject.GetComponent<NavMeshAgent>();
        animator = this.transform.GetChild(0).gameObject.GetComponent<Animator>();
        audio = this.gameObject.GetComponent<AudioSource>();
        sound = this.gameObject.GetComponent<SpeechSound>();
        idleTime = 0;
        lastPointIdx = 0;

        auraEffect = this.gameObject.transform.GetChild(0).GetChild(2).gameObject;

            
        this.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (currentState == State.CHASE && Vector3.Distance(playerTr.position, thisTr.position) <= 4)
        {
            Canvas_AttackButton.SetActive(true);
        }
        else
        {
            Canvas_AttackButton.SetActive(false);
        }

        if (Canvas_AttackButton.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                AttakProfesser();
            }
        }
        Routine();
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

    public void AttakProfesser()
    {
        if (Random.value < 0.5)
        {
            idleTime = 0;
            currentState = State.DAMAGED;
        }
        else
        {
            currentState = State.ANGER;
        }
    }

   
    public void Routine()
    {
        switch (currentState)
        {
            case State.START:
                {
                    animator.SetInteger("MotionCount", 1);
                    nvAgent.destination = thisTr.position;
                    currentState = State.RETURN;
                }
                break;
            case State.IDLE:
                {
                    animator.SetInteger("MotionCount", 0);
                    nvAgent.destination = thisTr.position;
                    nvAgent.speed = speed;
                    idleTime += Time.deltaTime;
                    if (idleTime >= 5)
                    {
                        if (IsPlayerAround())
                            currentState = State.CHASE;
                        else
                            currentState = State.RETURN;
                        idleTime = 0;
                    }
                }
                break;
            case State.PATROL:
                {
                    StartCoroutine(RoopSpeech());

                    animator.SetInteger("MotionCount", 1);
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
                        currentState = State.CHASE;
                }
                break;
            case State.CHASE:
                //audio.Stop();
                animator.SetInteger("MotionCount", 1);
                nvAgent.destination = playerTr.position;
                nvAgent.speed = speed;
                if (IsPlayerAttackable())
                {
                    currentState = State.END;
                    //GameOver
                }
                if (IsPlayerAround() == false)
                    currentState = State.RETURN;
                if (playerTr.GetComponent<Player>().isCabinetIn)
                    currentState = State.IDLE;
                break;
            case State.ANGER:
                if (!isplayingSound)
                {
                    audio.PlayOneShot(sound.FailSkillAudio[0]);
                    isplayingSound = true;
                }
                animator.SetInteger("MotionCount", 2);
                nvAgent.destination = playerTr.position;
                nvAgent.speed = angerSpeed;
                auraEffect.SetActive(true);

                if (IsPlayerAttackable())
                {
                    currentState = State.END;
                    isplayingSound = false;
                    //GameOver
                }
                if (IsPlayerAround() == false)
                {
                    currentState = State.RETURN;
                    isplayingSound = false;
                    auraEffect.SetActive(false);
                }
                if (playerTr.GetComponent<Player>().isCabinetIn)
                {
                    currentState = State.IDLE;
                    isplayingSound = false;
                    auraEffect.SetActive(false);
                }
                break;
            case State.DAMAGED:
                if (!isplayingSound)
                {
                    audio.PlayOneShot(sound.SuccessSkillAudio[0]);
                    isplayingSound = true;
                }
                    
                animator.SetInteger("MotionCount", 4);
                    
                idleTime += Time.deltaTime;
                nvAgent.destination = thisTr.position;

                if (idleTime >= 5)
                {
                    if (IsPlayerAround())
                    {
                        currentState = State.CHASE;
                        isplayingSound = false;
                    }
                    else
                    {
                        currentState = State.RETURN;
                        isplayingSound = false;
                    }
                    idleTime = 0;
                }
                break;
            case State.RETURN:
                {
                    nvAgent.speed = speed;
                    lastPointIdx = FindNearPointIdx() - 1;
                    if (lastPointIdx < 0)
                        lastPointIdx = points.Length - 1;
                    currentState = State.PATROL;
                }
                break;
            case State.END:
                animator.SetInteger("MotionCount", 0);
                nvAgent.destination = thisTr.position;
                PTD.DialRunner.StartDialogue("AttackProfessorEnding");
                if (PTD.DialRunner.CurrentNodeName == "AttackProfessorEnding")
                {
                    Canvas_GameOver.SetActive(true);
                    Cursor.lockState = CursorLockMode.Confined;
                    Time.timeScale = 0;
                }
                break;
        }
    }

    IEnumerator RoopSpeech()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(2f);
            if (currentState != State.PATROL)
                break;
            if (!audio.isPlaying)
            {
                audio.clip = sound.speechAudio[count];
                audio.Play();
                count++; 
            }
            if (count >= sound.speechAudio.Length)
            {
                count = 0;
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