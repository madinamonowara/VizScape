using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro; 

public class Platform_Script : MonoBehaviour
{
    public UnityEvent onAllSpheresPlaced;
    public List<GameObject> targetSpheres = new List<GameObject>();
    public GameObject gameCompletedTextObject; 

    private HashSet<GameObject> spheresOnPlatform = new HashSet<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (targetSpheres.Contains(other.gameObject))
        {
            spheresOnPlatform.Add(other.gameObject);

            //if all spheres are now on the platform
            if (spheresOnPlatform.Count >= targetSpheres.Count)
            {
                //all spheres placed, trigger game end
                onAllSpheresPlaced.Invoke();

                //game completed!
                if (gameCompletedTextObject != null)
                {
                    gameCompletedTextObject.SetActive(true);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (targetSpheres.Contains(other.gameObject))
        {
            spheresOnPlatform.Remove(other.gameObject);

        }
    }
}