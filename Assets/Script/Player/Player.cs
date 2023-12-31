using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // Rigidbody
    private Rigidbody Rb;

    // 인벤토리 스크립트
    public InventoryObject inventoryObjectScript;

    // 카메라
    public GameObject playerCamera;
    // 손전등
    public GameObject playerSpotLight;

    // 애니메이션 커브
    public AnimationCurve MoveCurve;

    // 플레이어 애니메이터
    public Animator animator;

    // 애니메이션 트리거 Bool
    bool isIn = false;

    // 애니메이션 현재시간
    float currentTime = 0; 

    // 기본 이동 속도
    public float speed = 5f;
    // 달리기 속도
    public float runSpeed = 10f;
    // 카메라 회전 속도
    public float turnSpeed = 4f;

    // 스테미나 스크롤바
    public Scrollbar ScrollbarStemina;
    // 스테미나 값
    public float stemina = 10f;

    // 회전값 기준
    float xRotate = 0.0f;

    // 플레이어와 오브젝트의 거리
    float dist;

    public Item item;
    public Cabinet cabinet;

    private void Start()
    {
        Rb = GetComponent<Rigidbody>();
        inventoryObjectScript = FindObjectOfType<InventoryObject>();
        this.gameObject.transform.rotation = Quaternion.Euler(0, 180f, 0);

    }

    private void Update()
    {
        // 플레이어 달리기
        ShiftSpeed();
        // 플레이어 아이템 줍기
        EnterItem();
        // 아이템 사용
        UseItem();
    }

    private void FixedUpdate()
    {
        // 자동회전 차단
        FreezeRotate();
        // 플레이어 이동
        Move();
        // 플레이어 회전방향
        MouseRotation();

        Rb.velocity = new Vector3(0, -10, 0);
    }


    // 자동회전 차단
    void FreezeRotate()
    {
        Rb.angularVelocity = Vector3.zero;
        Rb.velocity = Vector3.zero;
    }

    // 플레이어 이동
    void Move()
    {
        float _moveDirX = Input.GetAxisRaw("Horizontal");
        float _moveDirZ = Input.GetAxisRaw("Vertical");

        Vector3 _moveHorizontal = transform.right * _moveDirX;
        Vector3 _moveVertical = transform.forward * _moveDirZ;
        Vector3 _velocity;
        
        _velocity = (_moveHorizontal + _moveVertical).normalized * speed;
        Rb.MovePosition(transform.position + _velocity * Time.deltaTime);

        if (Mathf.Approximately(_moveDirX, 0) && Mathf.Approximately(_moveDirZ, 0))
        {
            animator.SetBool("isMove", false);
        }
        else
        {
            animator.SetBool("isMove", true);
        }
        animator.SetFloat("Xdir", _moveDirX);
        animator.SetFloat("Zdir", _moveDirZ);
    }

    // 플레이어 회전방향
    void MouseRotation()
    {
        float yRotateSize = Input.GetAxis("Mouse X") * turnSpeed;
        float yRotate = transform.eulerAngles.y + yRotateSize;

        float xRotateSize = -Input.GetAxis("Mouse Y") * turnSpeed;
        xRotate = Mathf.Clamp(xRotate + xRotateSize, -45, 80);

        transform.eulerAngles = new Vector3(0, yRotate, 0);
        playerCamera.transform.eulerAngles = new Vector3(xRotate, transform.eulerAngles.y, 0);
        playerSpotLight.transform.eulerAngles = new Vector3(playerSpotLight.transform.rotation.x
            + xRotate, playerSpotLight.transform.rotation.y + transform.eulerAngles.y, 0);
    }

    // 플레이어 달리기 + 스테미나
    void ShiftSpeed()
    {
        stemina = Mathf.Clamp01(stemina);
        if (Input.GetKey(KeyCode.Space))
        {
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) ||
                Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)
                || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||
                Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                ScrollbarStemina.gameObject.transform.parent.gameObject.SetActive(true);
                if (stemina > 0)
                {
                    speed = runSpeed;
                    stemina -= Time.deltaTime * 0.1f;
                    ScrollbarStemina.size = stemina;
                    if (stemina < 0)
                    {
                        speed = 5f;
                    }
                }
            }
            else
            {
                speed = 5f;
                stemina += Time.deltaTime * 0.1f;
                ScrollbarStemina.size = stemina;
                if (stemina > 1f)
                {
                    ScrollbarStemina.gameObject.transform.parent.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            speed = 5f;
            stemina += Time.deltaTime * 0.1f;
            ScrollbarStemina.size = stemina;
            if (stemina > 1f)
            {
                ScrollbarStemina.gameObject.transform.parent.gameObject.SetActive(false);
            }
        }
    }

    
    // 플레이어 아이템 줍기 / 사물함 들어가기
    void EnterItem()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                dist = Vector3.Distance(this.gameObject.transform.position, hit.transform.position);
                if (dist < 2)
                {
                    if (hit.transform.gameObject.CompareTag("Item"))
                    {
                        // 클릭 오브젝트의 아이템 스크립트
                        item = hit.transform.gameObject.GetComponent<Item>();
                        for (int i = 0; i < inventoryObjectScript.slots.Length; i++)
                        {
                            if (inventoryObjectScript.slots[i].active == false)
                            {
                                // 오브젝트 저장 활성화
                                inventoryObjectScript.slots[i].active = true;
                                // 슬롯 스크립트에 클릭한 아이템 스크립트 전달
                                inventoryObjectScript.slots[i].item = item;

                                // 클릭 오브젝트 비활성화
                                hit.transform.gameObject.SetActive(false);
                                break;
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                    // 사물함 값 받기
                    else if (hit.transform.gameObject.CompareTag("Cabinet"))
                    {
                        cabinet = hit.transform.gameObject.GetComponent<Cabinet>();
                        cabinet.isOpen = true;
                        StartCoroutine(closeDoor());
                        StartCoroutine(MoveCabinet(this.gameObject.transform ,hit.transform.gameObject));

                        isIn = !isIn;
                    }
                }
            }
        }
    }

    // 아이템 사용
    void UseItem()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            inventoryObjectScript.SlotObject.Reset();
        }
    }

    IEnumerator closeDoor()
    {
        yield return null;
        cabinet.isOpen = false;
    }


    IEnumerator MoveCabinet(Transform thisObject, GameObject cabinetObject)
    {
        GameObject CabinetOBJ = cabinetObject.transform.parent.gameObject;
        playerSpotLight.SetActive(isIn);
        animator.SetBool("isInCabinet", !isIn);
        if (isIn == false)
        {
            currentTime = 0;
            Rb.constraints = RigidbodyConstraints.FreezePosition;
            while(currentTime <= 0.5f)
            {
                yield return null;
                currentTime += Time.deltaTime;
                Mathf.Clamp01(currentTime);

                this.transform.position = Vector3.Lerp(thisObject.position, CabinetOBJ.transform.position +
                    new Vector3(0, this.transform.position.y, 0),MoveCurve.Evaluate(currentTime));

                this.transform.rotation = Quaternion.Lerp(thisObject.rotation, CabinetOBJ.transform.rotation, MoveCurve.Evaluate(currentTime));
            }
        }
        else
        {
            currentTime = 0;
            Rb.constraints = ~RigidbodyConstraints.FreezePosition;
            while (currentTime <= 0.5f)
            {
                yield return null;
                currentTime += Time.deltaTime * 0.5f;
                Mathf.Clamp01(currentTime);

                this.transform.position = Vector3.Lerp(thisObject.position, CabinetOBJ.transform.transform.position +
                    CabinetOBJ.transform.forward * 2 + new Vector3(0, 0.75f, 0), MoveCurve.Evaluate(currentTime));
            }
        }
        yield break;
    }

    public void StartDialogue()
    {
        Rb.constraints = RigidbodyConstraints.FreezePosition;
    }
    public void EndDialogue()
    {
        Rb.constraints = ~RigidbodyConstraints.FreezePosition;
    }
}