using UnityEngine;

public class PulseLight : MonoBehaviour
{
    private Light _light;
    public float pulseSpeed = 2.0f;
    public float minIntensity = 1f;
    public float maxIntensity = 2f;

    void Start()
    {
        _light = GetComponent<Light>();
    }

    void Update()
    {
        float t = Mathf.PingPong(Time.time * pulseSpeed, 1f);
        _light.intensity = Mathf.Lerp(minIntensity, maxIntensity, t);
    }
}