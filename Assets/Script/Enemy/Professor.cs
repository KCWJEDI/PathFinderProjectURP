using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Professor : MonoBehaviour
{
    // 플레이어 스크립트
    public Player playerSC;
    // 플레이어 Transform
    public Transform PlayerTransform;
    private Vector3 targetPosition;

    // 교수님 이동 속도
    public float speed;

    public NavMeshAgent nmAgent;

    private void Start()
    {
        nmAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        nmAgent.SetDestination(PlayerTransform.position);
        targetPosition = new Vector3(PlayerTransform.position.x, transform.position.y, PlayerTransform.position.z);
        this.transform.LookAt(targetPosition);
    }

    private void FixedUpdate()
    {
        //float dist = Vector3.Distance(playerSC.transform.position, this.gameObject.transform.position);
        //if (dist > 1.5f)
        //    transform.position = Vector3.MoveTowards(transform.position,
        //        PlayerTransform.position - new Vector3(-0.25f, 0.8f, 0), Time.deltaTime * speed);
    }
}
