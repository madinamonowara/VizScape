using UnityEngine;

public class TargetSphere : MonoBehaviour
{
    public VoiceGuide voiceGuide; 
    public bool hasBeenHit = false;
    public Material regularMaterial;
    public Material hitMaterial;
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material = regularMaterial;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!hasBeenHit && collision.gameObject.CompareTag("Bat"))
        {
            hasBeenHit = true;
            rend.material = hitMaterial;

            if (voiceGuide != null)
            {
                voiceGuide.Speak("Nice hit!");
            }

            BaseballManager.instance.TargetHit();
        }
    }
}
