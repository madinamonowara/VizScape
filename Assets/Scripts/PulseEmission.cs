using UnityEngine;

public class PulseEmission : MonoBehaviour
{
    public Renderer targetRenderer;
    public Color emissionColor = new Color(0.4f, 1f, 0.8f);
    public float pulseSpeed = 2f;
    public float minIntensity = 1f;
    public float maxIntensity = 3f;

    private Material mat;
    private float t;

    void Start()
    {
        mat = targetRenderer.material;
    }

    void Update()
    {
        t += Time.deltaTime * pulseSpeed;
        float intensity = Mathf.Lerp(minIntensity, maxIntensity, (Mathf.Sin(t) + 1f) / 2f);
        mat.SetColor("_EmissionColor", emissionColor * intensity);
    }
}