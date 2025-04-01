using UnityEngine;

public class StrobeLight : MonoBehaviour
{
    private Light _light;
    public float strobeSpeed = 0.5f;

    void Start()
    {
        _light = GetComponent<Light>();
        InvokeRepeating("Toggle", 0f, strobeSpeed);
    }

    void Toggle()
    {
        _light.enabled = !_light.enabled;
    }
}