using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Professor : MonoBehaviour
{
    // 플레이어의 스크립트
    public Player playerSC;
    // 플레이어의 위치
    public Transform PlayerTransform;

    // 움직이는 속도
    public float speed;

    public NavMeshAgent nmAgent;

    private void Start()
    {
        nmAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        nmAgent.SetDestination(PlayerTransform.position);

        if (this.transform.position.y == PlayerTransform.position.y)
        {
            this.transform.LookAt(PlayerTransform);
        }
    }

    private void FixedUpdate()
    {
        //transform.position = Vector3.MoveTowards(transform.position, PlayerTransform.position, Time.deltaTime * speed);
    }
}
