using UnityEngine;

public class ExitTeleportActivator : MonoBehaviour
{
    public GameObject lockIndicatorLeft;
    public GameObject lockIndicatorRight;
    public GameObject wall1;
    public GameObject wall2;
    public GameObject wall3;
    public GameObject door;

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
                wall1.SetActive(true);
                wall2.SetActive(true);
                wall3.SetActive(false);
                door.SetActive(false);
            }
        }
    }

    private bool IsGreen(Color color)
    {
        return color.g > 0.5f && color.r < 0.3f && color.b < 0.3f; // Adjust if needed
    }
}