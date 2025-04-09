using UnityEngine;

public class ExitTeleportActivator : MonoBehaviour
{
    public GameObject lockIndicatorLeft;
    public GameObject lockIndicatorRight;
    public GameObject teleportAnchor;

    private bool activated = false;

    void Update()
    {
        if (!activated)
        {
            Color leftColor = lockIndicatorLeft.GetComponent<Renderer>().material.GetColor("_EmissionColor");
            Color rightColor = lockIndicatorRight.GetComponent<Renderer>().material.GetColor("_EmissionColor");

            if (IsGreen(leftColor) && IsGreen(rightColor))
            {
                activated = true;
                teleportAnchor.SetActive(true);
                Debug.Log("Exit teleport activated.");
            }
        }
    }

    private bool IsGreen(Color color)
    {
        return color.g > 0.5f && color.r < 0.3f && color.b < 0.3f; // Adjust if needed
    }
}