using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;
using Yarn.Unity;

enum DialNumList
{
    StartPhoneCall,
    SecondFloorCorridor,
    FoundUSB,
    SoundProfessor,
    FirstFloorDoorClose,
    LockedCCRC

};

public class PlayerTriggerDialogue : MonoBehaviour
{
    [Header ("CanvasList")]
    public GameObject FirstFloorDoorCloseDialogue;
    public GameObject LockedCCRC;
    [Space (0.5f)]

    [Header ("Objects")]
    public GameObject ProfessorObject;
    public GameObject USBObject;
    public Door[] doors;
    public Transform[] keyPoints;
    [Space (0.5f)]

    [Header ("Prefab")]
    public GameObject classKeyPrefab;
    public GameObject PathWayKeyPrefab;
    [Space (0.5f)]

    [Header ("Manager")]
    public DialogueRunner DialRunner;
    
    void KeyRandomCreate()
    {
        //7중 4 클래스
        //7중 1 복도
        //7중 2 빈방
        int pathWayKeyPos = Random.Range(0, 6);
        int emptyPos1 = -1;
        int emptyPos2 = -1;
        
        while (emptyPos1 == pathWayKeyPos || emptyPos1 == -1)
        {
            emptyPos1 = Random.Range(0, 6);
        }
        while (emptyPos2 == pathWayKeyPos || emptyPos2 ==  emptyPos1 || emptyPos2 == -1)
        {
            emptyPos2 = Random.Range(0, 6);
        }

        for (int i = 0; i < keyPoints.Length; i++)
        {
            if (i == pathWayKeyPos)
                Instantiate(PathWayKeyPrefab, keyPoints[i]);
            else if (i == emptyPos1 || i == emptyPos2);
            else
                Instantiate(classKeyPrefab, keyPoints[i]);
        }
    }
    
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Dialogue"))
        {
            string triggername = collision.gameObject.name;
            DialNumList ActiveNum = (DialNumList)System.Enum.Parse(typeof(DialNumList), triggername.ToString());

            switch (ActiveNum)
            {
                case DialNumList.StartPhoneCall:
                    DialRunner.StartDialogue("StartPhoneCall");
                    break;
                case DialNumList.SecondFloorCorridor:
                    DialRunner.StartDialogue("SecondFloorCorridor");
                    break;
                case DialNumList.FoundUSB:
                    DialRunner.StartDialogue("FoundUSB");
                    FirstFloorDoorCloseDialogue.SetActive(true);
                    break;
                case DialNumList.SoundProfessor:
                    DialRunner.StartDialogue("SoundProfessor");
                    ProfessorObject.SetActive(true);
                    ProfessorObject.gameObject.GetComponent<AI>().audio.PlayOneShot(ProfessorObject.gameObject.GetComponent<AI>().sound.EffectAudio[0], 0.7f);
                    break;
                case DialNumList.FirstFloorDoorClose:
                    DialRunner.StartDialogue("FirstFloorDoorClose");
                    USBObject.SetActive(true);
                    LockedCCRC.SetActive(true);
                    foreach (Door dor in doors)
                    {
                        dor.lockType = DoorLockType.PathWayDoor;
                        dor.isOpen = false;
                    }
                    KeyRandomCreate();
                    break;
                case DialNumList.LockedCCRC:
                    DialRunner.StartDialogue("LockedCCRC");
                    break;
            }
            collision.gameObject.SetActive(false);
        }
    }
}
