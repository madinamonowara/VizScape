using UnityEngine;

public class TargetSphere : MonoBehaviour
{
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
            // AudioManager.instance.PlaySFX("TargetHit"); // Removed the audio line
            BaseballManager.instance.TargetHit();
        }
    }
}
