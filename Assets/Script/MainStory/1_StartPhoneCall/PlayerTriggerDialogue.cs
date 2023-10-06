using UnityEngine;
using Yarn.Unity;

enum DialNumList
{
    StartPhoneCall,
    SecondFloorCorridor
};

public class PlayerTriggerDialogue : MonoBehaviour
{

    public DialogueRunner DialRunner;
    Player PlayerScript;


    private void Start()
    {
        PlayerScript = FindObjectOfType<Player>();
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
            }
        collision.gameObject.SetActive(false);
        }
    }
}
