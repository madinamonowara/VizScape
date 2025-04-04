using UnityEngine;
using System.Collections;

public class ButtonDoor : MonoBehaviour
{
    public Animator doorAnimator;
    public bool butDoors = false;
    public bool butBlocks = false;
    public bool butLadder = false;   

    public void buttonBehindDoors()
    {
        if(!butDoors)
        {
            butDoors = true;
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
        }
        else
        {
            // play sound to show that button does nothing now
        }

        checkAllClicked();
    }

    private void checkAllClicked()
    {
        if(butLadder && butBlocks && butDoors)
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
