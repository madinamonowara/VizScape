using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class UnlockLeftLock : MonoBehaviour
{
    public GameObject lockIndicatorLeft;
    private XRSocketInteractor socketInteractor;

    private bool unlocked = false;

    void Start()
    {
        socketInteractor = GetComponent<XRSocketInteractor>();
        socketInteractor.selectEntered.AddListener(OnFuseInserted);
    }

    void OnFuseInserted(SelectEnterEventArgs args)
    {
        if (unlocked) return;

        unlocked = true;
        Renderer renderer = lockIndicatorLeft.GetComponent<Renderer>();
        renderer.material.SetColor("_EmissionColor", Color.green);
        Debug.Log("Left lock indicator turned green.");
    }
}