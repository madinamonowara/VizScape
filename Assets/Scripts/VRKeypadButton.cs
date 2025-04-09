using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class KeypadButton : MonoBehaviour
{
    [SerializeField] private string buttonValue;
    [SerializeField] private float pressDepth = 0.003f;
    [SerializeField] private float returnSpeed = 10f;

    private Vector3 startPosition;
    private Vector3 pressedPosition;
    private bool isPressed;
    private KeypadController keypadController;
    private XRBaseInteractable interactable;

    private void Awake()
    {
        startPosition = transform.localPosition;
        pressedPosition = startPosition - (transform.forward * pressDepth);
        keypadController = GetComponentInParent<KeypadController>();
        interactable = GetComponent<XRBaseInteractable>();

        if (keypadController == null)
            Debug.LogError($"No KeypadController found for button {buttonValue}!");

        if (interactable != null)
        {
            interactable.selectEntered.AddListener(OnSelectEntered);
            interactable.selectExited.AddListener(OnSelectExited);
        }
        else
        {
            Debug.LogError($"No XRBaseInteractable found on button {buttonValue}!");
        }
    }

    private void OnDestroy()
    {
        if (interactable != null)
        {
            interactable.selectEntered.RemoveListener(OnSelectEntered);
            interactable.selectExited.RemoveListener(OnSelectExited);
        }
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        PressButton();
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        ReleaseButton();
    }

    private void PressButton()
    {
        if (!isPressed)
        {
            isPressed = true;
            transform.localPosition = pressedPosition;
            keypadController?.OnButtonPress(buttonValue);
        }
    }

    private void ReleaseButton()
    {
        isPressed = false;
    }

    private void Update()
    {
        // Smooth return to original position
        if (!isPressed && transform.localPosition != startPosition)
        {
            transform.localPosition = Vector3.Lerp(
                transform.localPosition, 
                startPosition, 
                Time.deltaTime * returnSpeed
            );
        }
    }

    public string GetButtonValue() => buttonValue;
    public void SetButtonValue(string value) => buttonValue = value;
}