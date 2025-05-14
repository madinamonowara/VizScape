using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class BaseballManager : MonoBehaviour
{
    public static BaseballManager instance;

    [Header("Target Spheres (Baseballs to Hit)")]
    public GameObject[] targetSpheres;

    [Header("Collectable Spheres (Original Spheres)")]
    public GameObject[] collectableSpheres;

    [Header("UI")]
    //public Text statusText;
    public TMPro.TextMeshProUGUI statusText;

    private int targetsHit = 0;
    private bool collectiblesUnlocked = false;
    public bool gameCompleted = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DisableCollectableSpheres();
        UpdateUI();
    }

    public void TargetHit()
    {
        if (!collectiblesUnlocked)
        {
            targetsHit++;
            UpdateUI();

            if (targetsHit >= targetSpheres.Length)
            {
                UnlockCollectableSpheres();
            }
        }
    }

    void DisableCollectableSpheres()
    {
        foreach (GameObject sphere in collectableSpheres)
        {
            UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabComponent = sphere.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
            if (grabComponent != null)
            {
                grabComponent.enabled = false;
            }

            Renderer renderer = sphere.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = Color.gray;
            }

            ParticleSystem particles = sphere.GetComponentInChildren<ParticleSystem>();
            if (particles != null && particles.isPlaying)
            {
                particles.Stop();
            }
        }
    }

    void UnlockCollectableSpheres()
    {
        collectiblesUnlocked = true;
        statusText.text = "Targets hit! Now collect the spheres and place them on the platform.";

        foreach (GameObject sphere in collectableSpheres)
        {
            UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabComponent = sphere.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
            if (grabComponent != null)
            {
                grabComponent.enabled = true;
            }

            Renderer renderer = sphere.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = Color.white;
            }

            ParticleSystem particles = sphere.GetComponentInChildren<ParticleSystem>();
            if (particles != null && !particles.isPlaying)
            {
                particles.Play();
            }
        }

        foreach (GameObject target in targetSpheres)
        {
            if (target != null)
            {
                Destroy(target);
            }
        }
    }

    void UpdateUI()
    {
        if (statusText != null)
        {
            statusText.text = "Targets hit: " + targetsHit + "/" + targetSpheres.Length;
        }
    }
}