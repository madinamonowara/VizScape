using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Platform_Script : MonoBehaviour
{
    public VoiceGuide voiceGuide; 
    public UnityEvent onAllSpheresPlaced;
    public List<GameObject> targetSpheres = new List<GameObject>();
    public GameObject gameCompletedTextObject;

    private HashSet<GameObject> spheresOnPlatform = new HashSet<GameObject>();
    private bool gameCompletedSpoken = false; 

    private void OnTriggerEnter(Collider other)
    {
        if (targetSpheres.Contains(other.gameObject))
        {
            spheresOnPlatform.Add(other.gameObject);


            if (spheresOnPlatform.Count >= targetSpheres.Count && !gameCompletedSpoken)
            {
               
                onAllSpheresPlaced.Invoke();

                //game completed!
                if (gameCompletedTextObject != null)
                {
                    gameCompletedTextObject.SetActive(true);
                }

                if (voiceGuide != null)
                {
                    voiceGuide.Speak("Congratulations! You've completed the game!");
                }

                gameCompletedSpoken = true; 
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (targetSpheres.Contains(other.gameObject) && gameCompletedSpoken)
        {
            gameCompletedSpoken = false; 
        }
        if (targetSpheres.Contains(other.gameObject))
        {
            spheresOnPlatform.Remove(other.gameObject);
        }
    }
}