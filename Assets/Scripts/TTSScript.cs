using UnityEngine;
using Meta.WitAi.TTS.Utilities;

public class TTSScript : MonoBehaviour
{
    public TTSSpeaker speaker;
    public GameObject script;
    private bool butOne;
    private bool butTwo;
    private bool butThree;


    void Start()
    {
        butOne = script.GetComponent<ButtonDoor>().butDoors;
        butTwo = script.GetComponent<ButtonDoor>().butBlocks;
        butThree = script.GetComponent<ButtonDoor>().butLadder;
    }


    void Update()
    {
        
    }

    private void Speak()
    {
        speaker.Speak("TEST");
    }
}
