using UnityEngine;
using System.Collections;

public class ButtonDoor : MonoBehaviour
{
    public Animator doorAnimator;
    public bool butDoors = false;
    public bool butBlocks = false;
    public bool butLadder = false;
    private int totalClicked = 0;

    public GameObject lightOne;
    public GameObject lightTwo;
    public GameObject lightThree;

    public Shader lightOn;

    public void buttonBehindDoors()
    {
        if(!butDoors)
        {
            butDoors = true;
            totalClicked += 1; 
        } else { 
            // play sound to show that button does nothing now
        }

        checkAllClicked();
    }

    public void buttonLadder()
    {
        if (!butLadder)
        {
            butLadder = true;
            totalClicked += 1; 
        }
        else
        {
            // play sound to show that button does nothing now
        }

        checkAllClicked();
    }

    public void buttonSurroundBlocks()
    {
        if (!butBlocks)
        {
            butBlocks = true;
            totalClicked += 1; 
        }
        else
        {
            // play sound to show that button does nothing now
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

    //public void checkButtonPressed()
    //{
    //    if(totalButtonPressed == 1)
    //    {
    //        doorAnimator.Play("DoorOpen", 0, 0.0f);
    //        Debug.Log("Button has been pressed.");
    //    }
    //}

}
