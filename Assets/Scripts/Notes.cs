using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class GrabbableUIController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Canvas uiCanvas; // Reference to the UI Canvas
    [SerializeField] private float distanceFromCamera = 0.5f;
    [SerializeField] private XROrigin xrOrigin;
    [SerializeField] private CharacterController characterController;

    [Header("Visual Settings")]
    [SerializeField] private Vector3 canvasRotationOffset = new Vector3(0, 0, 0);
    [SerializeField] private bool resetCanvasPositionOnRelease = true;
    [SerializeField] private bool hideGrabbableWhenHeld = true;

    private bool isHeld = false;
    private Vector3 originalCanvasPosition;
    private Quaternion originalCanvasRotation;
    private XRGrabInteractable grabInteractable;
    private Transform cameraTransform;
    private MeshRenderer meshRenderer;
    private bool wasRendererEnabled;

    private void Start()
    {
        // Get the MeshRenderer component
        meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer != null)
        {
            wasRendererEnabled = meshRenderer.enabled;
        }

        // Store original canvas position and rotation
        if (uiCanvas != null)
        {
            originalCanvasPosition = uiCanvas.transform.position;
            originalCanvasRotation = uiCanvas.transform.rotation;
            uiCanvas.gameObject.SetActive(false); // Hide canvas initially
        }

        // Get or add the XRGrabInteractable component to THIS object (the cube)
        grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable == null)
        {
            grabInteractable = gameObject.AddComponent<XRGrabInteractable>();
        }

        // Setup grab events
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);

        // Find required components if not assigned
        if (xrOrigin == null)
        {
            xrOrigin = FindFirstObjectByType<XROrigin>();
        }

        if (xrOrigin != null)
        {
            cameraTransform = xrOrigin.Camera.transform;
            if (characterController == null)
            {
                characterController = xrOrigin.GetComponent<CharacterController>();
            }
        }
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        if (uiCanvas == null || cameraTransform == null) return;

        isHeld = true;
        uiCanvas.gameObject.SetActive(true); // Show canvas when grabbed
        
        // Hide the grabbable object when held
        if (hideGrabbableWhenHeld && meshRenderer != null)
        {
            wasRendererEnabled = meshRenderer.enabled;
            meshRenderer.enabled = false;
        }
        
       
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        isHeld = false;
        
        // Show the grabbable object again
        if (hideGrabbableWhenHeld && meshRenderer != null)
        {
            meshRenderer.enabled = wasRendererEnabled;
        }

        if (resetCanvasPositionOnRelease && uiCanvas != null)
        {
            uiCanvas.gameObject.SetActive(false); // Hide canvas when released
            uiCanvas.transform.position = originalCanvasPosition;
            uiCanvas.transform.rotation = originalCanvasRotation;
        }

       
    }

    private void LateUpdate()
    {
        if (isHeld && uiCanvas != null && cameraTransform != null)
        {
            // Calculate the desired position in front of the camera
            Vector3 targetPosition = cameraTransform.position + (cameraTransform.forward * distanceFromCamera);
            
            // Update canvas position and rotation
            uiCanvas.transform.position = targetPosition;
            
            // Make the canvas face the camera with optional rotation offset
            Quaternion targetRotation = Quaternion.LookRotation(
                uiCanvas.transform.position - cameraTransform.position
            );
            uiCanvas.transform.rotation = targetRotation * Quaternion.Euler(canvasRotationOffset);
        }
    }

    private void OnDestroy()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(OnGrab);
            grabInteractable.selectExited.RemoveListener(OnRelease);
        }
    }

    // Helper method to set the canvas reference at runtime if needed
    public void SetUICanvas(Canvas canvas)
    {
        uiCanvas = canvas;
        if (uiCanvas != null)
        {
            originalCanvasPosition = uiCanvas.transform.position;
            originalCanvasRotation = uiCanvas.transform.rotation;
            uiCanvas.gameObject.SetActive(false);
        }
    }
}