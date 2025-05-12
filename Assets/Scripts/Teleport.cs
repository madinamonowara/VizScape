using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform destination;
    public GameObject playerg;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(TeleportPlayer());
        }
    }

    private System.Collections.IEnumerator TeleportPlayer()
    {
        playerg.SetActive(false);

        // Wait one frame to ensure the rig deactivates properly
        yield return null;

        // Move the XR rig to the destination
        playerg.transform.position = destination.position;

        playerg.SetActive(true);
    }
}