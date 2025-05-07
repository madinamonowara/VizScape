using UnityEngine;
using System.Collections;
using Meta.WitAi.TTS.Utilities;

public class ButtonDoor : MonoBehaviour
{
    // Button and Door controls
    public Animator doorAnimator;
    public bool butDoors = false;
    public bool butBlocks = false;
    public bool butLadder = false;
    private int totalClicked = 0;

    public GameObject lightOne;
    public GameObject lightTwo;
    public GameObject lightThree;

    public Shader lightOn;

    // TTS
    public TTSSpeaker speaker;
    private string[] butAlreadyPressedList = { 
        "You seem to have already pressed this button.", 
        "It's done, the button was already pressed moments ago.",                                      
        "Woah, woah, no need to worry, you already pressed this button.", 
        "I think you have already pressed this button",
        "There seems to be other buttons that you need to find!" 
    };

    private string[] butInterruptedList = {
        "Sorry, where was I—ah, right: You seem to have already pressed this button.",
        "As I was saying earlier: the button was already pressed moments ago.",
        "Let me finish what I was trying to say: no need to worry, you already pressed this button.",
        "Picking up where I left off: you have already pressed this button.",
        "Anyway, like I was saying before: There seems to be other buttons that you need to find!"
    };
    private System.Random rnd = new System.Random();
    private int choice;

    // Animator for player model + TTS
    public Animator playerAnimator;

    private void buttonBehindDoors()
    {
        if(!butDoors)
        {
            butDoors = true;
            totalClicked += 1;
            buttonSpeakPressed();
        }
        else { 
            // play sound to show that button does nothing now
            buttonAlreadyPressed();
        }

        checkAllClicked();
    }

    private void buttonLadder()
    {
        if (!butLadder)
        {
            butLadder = true;
            totalClicked += 1;
            buttonSpeakPressed();
        }
        else
        {
            // play sound to show that button does nothing now
            buttonAlreadyPressed();
        }

        checkAllClicked();
    }

    private void buttonSurroundBlocks()
    {
        if (!butBlocks)
        {
            butBlocks = true;
            totalClicked += 1;
            buttonSpeakPressed();
        }
        else
        {
            // play sound to show that button does nothing now
            buttonAlreadyPressed();
        }

        checkAllClicked();
    }

    private void checkAllClicked()
    {
        if(totalClicked == 1)
        {
            lightOne.GetComponent<Renderer>().material.shader = lightOn;
            Debug.Log("First Light On.");
        }

        if (totalClicked == 2)
        {
            lightTwo.GetComponent<Renderer>().material.shader = lightOn;
            Debug.Log("Second Light On.");
        }

        if (totalClicked == 3)
        {
            lightThree.GetComponent<Renderer>().material.shader = lightOn;
            Debug.Log("Third Light On.");
        }

        if (butLadder && butBlocks && butDoors)
        {
            doorAnimator.Play("DoorOpen", 0, 0.0f);
            Debug.Log("Door has been opened.");
        }
    }

    private void buttonSpeakPressed()
    {
        switch (totalClicked)
        {
            case 1:
                speaker.Speak("Oh, you have pressed the first button of three!");
                break;

            case 2:
                speaker.Speak("You pressed the second button! There's one more left before the door opens.");
                break;

            case 3:
                speaker.Speak("You've pressed the final button. Quick! Escape to the next room.");
                break;
        }
    }

    private void buttonAlreadyPressed()
    {

        if(speaker.IsSpeaking)
        {
            speaker.Stop();
            speaker.Speak(butInterruptedList[choice]);
        } else
        {
            choice = rnd.Next(butAlreadyPressedList.Length);
            speaker.Speak(butAlreadyPressedList[choice]);
        }
    }

    public void playPlayerAnimations()
    {
        if (totalClicked == 3)
        {
            playerAnimator.Play("Waving", 0, 0.0f);
            Debug.Log("Playing player animation with door open.");
            speaker.Stop();
            speaker.Speak("Hey! You found me. Take this teleporter onto the next student's scene!");
        }
    
    }
}
