using Unity.VisualScripting;
using UnityEngine;
using Yarn.Unity;

enum DialNumList
{
    StartPhoneCall,
    SecondFloorCorridor,
    FoundUSB,
    SoundProfessor,
    FirstFloorDoorClose,

};

public class PlayerTriggerDialogue : MonoBehaviour
{
    public DialogueRunner DialRunner;
    public GameObject FirstFloorDoorCloseDialogue;
    public GameObject ProfessorObject;
    public GameObject USBObject;

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
                    break;
                case DialNumList.FirstFloorDoorClose:
                    DialRunner.StartDialogue("FirstFloorDoorClose");
                    break;
            }
        collision.gameObject.SetActive(false);
        }
    }
}
