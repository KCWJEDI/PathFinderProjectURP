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
    FirstFloorDoorClose

};

public class PlayerTriggerDialogue : MonoBehaviour
{
    public DialogueRunner DialRunner;
    public GameObject FirstFloorDoorCloseDialogue;
    public GameObject ProfessorObject;
    public GameObject USBObject;
    public Door[] doors;
    public Transform[] keyPoints;
    public GameObject classKeyPrefab;
    public GameObject PathWayKeyPrefab;

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
                    //USBObject.SetActive(true);
                    foreach (Door dor in doors)
                        dor.lockType = DoorLockType.PathWayDoor;
                    int classkeyCount = 0;
                    int pathkeyCount = 0;
                    while (classkeyCount == 4 || pathkeyCount == 1)
                    {
                        float cnt = Random.value * 10;
                        if (cnt < keyPoints.Length)
                        {
                            if (cnt < 0.5)
                            {
                                if (classkeyCount <= 3 )
                                {
                                    classkeyCount++;
                                    Instantiate(classKeyPrefab, keyPoints[(int)cnt]);
                                }
                                else continue;
                            }
                            else
                            {
                                if (pathkeyCount <= 1)
                                {
                                    pathkeyCount++;
                                    Instantiate(PathWayKeyPrefab, keyPoints[(int)cnt]);
                                }
                                else continue;
                            }
                        }
                    }
                    break;
            }
        collision.gameObject.SetActive(false);
        }
    }
}
