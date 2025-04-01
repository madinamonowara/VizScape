using UnityEngine;

public class FlickerLight : MonoBehaviour
{
    private Light _light;
    public float flickerSpeed = 0.1f;
    public float minIntensity = 0.8f;
    public float maxIntensity = 1.2f;

    void Start()
    {
        _light = GetComponent<Light>();
        InvokeRepeating("Flicker", 0f, flickerSpeed);
    }

    void Flicker()
    {
        _light.intensity = Random.Range(minIntensity, maxIntensity);
    }
}