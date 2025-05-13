using UnityEngine;
using System.Collections;
using Convai.Scripts.Runtime.Core; 

public class ConvaiAutoQuipManager : MonoBehaviour
{
    [Header("Assign your ConvaiNPC reference here")]
    public ConvaiNPC convaiNPC;

    [Header("Initial line spoken by NPC")]
    public string initialLine = "Welcome to the escape room!";

    [Header("How often (seconds) the NPC will quip")]
    public float quipInterval = 60f;

    [Header("Quip prompts to randomly choose from")]
    public string[] quipPrompts = new string[]
    {
        "Say something witty about escape rooms.",
        "Give a clever hint to the player.",
        "Share a fun fact about puzzles.",
        "Make a light-hearted comment about being trapped.",
        "Say something funny about the player's progress."
    };

    private bool started = false;

    void Start()
    {
        
        StartCoroutine(DelayedStart());
    }

    IEnumerator DelayedStart()
    {
        
        yield return new WaitForSeconds(2f);

        if (convaiNPC == null)
        {
            Debug.LogError("ConvaiNPC reference not set in ConvaiAutoQuipManager! Please assign it in the Inspector.");
            yield break;
        }

       
        if (string.IsNullOrEmpty(convaiNPC.characterID))
        {
            Debug.LogError("ConvaiNPC Character ID is not set! Please assign a valid Character ID in the ConvaiNPC component.");
            yield break;
        }

        convaiNPC.TriggerSpeech(initialLine);
        started = true;
        StartCoroutine(PeriodicQuips());
    }

    IEnumerator PeriodicQuips()
    {   yield return new WaitForSeconds(50f);
        while (started && convaiNPC != null)
        {
            yield return new WaitForSeconds(quipInterval);

            if (convaiNPC == null)
                yield break;

            string randomPrompt = quipPrompts[Random.Range(0, quipPrompts.Length)];
            convaiNPC.TriggerSpeech(randomPrompt);
        }
    }
}