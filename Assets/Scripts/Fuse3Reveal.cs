using UnityEngine;

public class Fuse3Reveal : MonoBehaviour
{
    public Transform lid;            // Reference to Box_Lid
    public GameObject fuse3;         // Reference to Fuse3
    public float revealDistance = 0.5f;
    private bool revealed = false;

    void Update()
    {
        if (!revealed)
        {
            float distance = Vector3.Distance(transform.position, lid.position);
            if (distance > revealDistance)
            {
                revealed = true;
                EnableFuse();
            }
        }
    }

    void EnableFuse()
    {
        var grab = fuse3.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        var rb = fuse3.GetComponent<Rigidbody>();

        if (grab) grab.enabled = true;
        if (rb) rb.isKinematic = false;
        if (rb) rb.useGravity = true;
    }
}