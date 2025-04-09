using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class KeypadController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Renderer panelMesh;
    [SerializeField] private TMP_Text keypadDisplayText;
    
    [Header("Settings")]
    [SerializeField] private string correctCode = "1234"; // Set your desired code in inspector
    [SerializeField] private int maxCodeLength = 4;
    
    [Header("Display Settings")]
    [SerializeField] private string successText = "ACCESS GRANTED";
    [SerializeField] private string failureText = "ACCESS DENIED";
    [SerializeField] private float messageDisplayTime = 2f;
    
    [Header("Optional Colors")]
    [SerializeField] private Color defaultColor = Color.white;
    [SerializeField] private Color successColor = Color.green;
    [SerializeField] private Color failureColor = Color.red;
    
    [Header("Events")]
    public UnityEvent onAccessGranted;
    public UnityEvent onAccessDenied;
    
    [Header("Optional Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip buttonPressSound;
    [SerializeField] private AudioClip accessGrantedSound;
    [SerializeField] private AudioClip accessDeniedSound;

    private string currentInput = "";
    private bool isProcessing = false;
    private float messageTimer = 0f;
    private Material panelMaterial;

    private void Start()
    {
        if (panelMesh != null)
        {
            panelMaterial = panelMesh.material;
            panelMaterial.color = defaultColor;
        }
        
        ResetDisplay();
    }

    private void Update()
    {
        // Reset display after showing result
        if (isProcessing)
        {
            messageTimer -= Time.deltaTime;
            if (messageTimer <= 0)
            {
                isProcessing = false;
                ResetDisplay();
            }
        }
    }

    public void OnButtonPress(string value)
    {
        if (isProcessing) return;

        PlayButtonSound();

        switch (value.ToLower())
        {
            case "enter":
                ValidateCode();
                break;

            case "clear":
                ClearInput();
                break;

            default:
                // Add digit if within max length
                if (currentInput.Length < maxCodeLength)
                {
                    currentInput += value;
                    UpdateDisplay();
                }
                break;
        }
    }

    private void ValidateCode()
    {
        if (string.IsNullOrEmpty(currentInput))
            return;

        bool isCorrect = currentInput == correctCode;
        
        if (isCorrect)
        {
            ShowSuccess();
            onAccessGranted?.Invoke();
        }
        else
        {
            ShowFailure();
            onAccessDenied?.Invoke();
        }

        isProcessing = true;
        messageTimer = messageDisplayTime;
    }

    private void ShowSuccess()
    {
        keypadDisplayText.text = successText;
        if (panelMaterial != null)
        {
            panelMaterial.color = successColor;
        }
        PlaySound(accessGrantedSound);
        Debug.Log("Access Granted!");
    }

    private void ShowFailure()
    {
        keypadDisplayText.text = failureText;
        if (panelMaterial != null)
        {
            panelMaterial.color = failureColor;
        }
        PlaySound(accessDeniedSound);
        currentInput = "";
    }

    private void UpdateDisplay()
    {
        keypadDisplayText.text = new string('*', currentInput.Length);
    }

    private void ClearInput()
    {
        currentInput = "";
        ResetDisplay();
    }

    private void ResetDisplay()
    {
        keypadDisplayText.text = "";
        currentInput = "";
        if (panelMaterial != null)
        {
            panelMaterial.color = defaultColor;
        }
    }

    private void PlayButtonSound()
    {
        PlaySound(buttonPressSound);
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    // Optional: Method to set code at runtime
    public void SetCode(string newCode)
    {
        correctCode = newCode;
    }
}