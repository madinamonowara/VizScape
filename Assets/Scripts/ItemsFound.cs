using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class ItemsFound : MonoBehaviour
{
    public XRSocketInteractor[] sockets;
    private bool[] isFilled;
    public AudioSource soundSource;
    public GameObject objectToBreak;

    void Start()
    {
        isFilled = new bool[sockets.Length];

        for (int i = 0; i < sockets.Length; i++)
        {
            int index = i;
            sockets[i].selectEntered.AddListener((args) => OnSocketFilled(index));
            sockets[i].selectExited.AddListener((args) => OnSocketEmptied(index));
        }
    }

    void OnSocketFilled(int index)
    {
        isFilled[index] = true;
        CheckAllSockets();
    }

    void OnSocketEmptied(int index)
    {
        isFilled[index] = false;
    }

    void CheckAllSockets()
    {
        foreach (bool filled in isFilled)
        {
            if (!filled) return;
        }
        nextLevel();

    }

    public void nextLevel(){
        if (soundSource != null){
            soundSource.Play();
        }
        if (objectToBreak != null){
            Destroy(objectToBreak);
        }
    }
}