using UnityEngine;
using System.Collections;

public class ButtonDoor : MonoBehaviour
{
    public int totalButtonPressed = 0;
    public Animator doorAnimator;

    public void Start()
    {
        //doorAnimator = door.GetComponent<Animator>();
        Debug.Log(doorAnimator);
    }
    
    public void incrementPressed()
    {
        totalButtonPressed++;

        checkButtonPressed();
    }

    public void checkButtonPressed()
    {
        if(totalButtonPressed == 1)
        {
            doorAnimator.Play("DoorOpen", 0, 0.0f);
            Debug.Log("Button has been pressed.");
        }
    }

}
