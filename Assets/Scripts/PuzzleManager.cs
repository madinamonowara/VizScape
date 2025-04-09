using UnityEngine;
using System.Collections;

public class PuzzleManager : MonoBehaviour
{
    public GameObject puzzleScreen;       // Reference to screen object
    private Material screenMat;

    public Color correctColor = Color.green;
    public Color incorrectColor = Color.red;
    public float feedbackDuration = 0.5f; // Time in seconds
    private Coroutine feedbackCoroutine;
    public int[] correctSequence = { 3, 1, 4, 2 };
    private int currentIndex = 0;
    private bool puzzleSolved = false;

    public GameObject lockIndicatorRight;
    public GameObject hatchRight;

    public AudioClip buttonClickSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        screenMat = puzzleScreen.GetComponent<Renderer>().material;
    }

    public void RegisterButtonPress(int buttonID)
    {
        if (buttonClickSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(buttonClickSound);
        }

        if (puzzleSolved) return;

        if (buttonID == correctSequence[currentIndex])
        {
            currentIndex++;
            ShowFeedback(correctColor);

            if (currentIndex == correctSequence.Length)
            {
                puzzleSolved = true;
                lockIndicatorRight.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green);
                hatchRight.SetActive(true);
            }
        }
        else
        {
            ShowFeedback(incorrectColor);
            currentIndex = 0;
        }
    }

    void ShowFeedback(Color color)
    {
        if (feedbackCoroutine != null)
            StopCoroutine(feedbackCoroutine);

        feedbackCoroutine = StartCoroutine(FlashColor(color));
    }

    IEnumerator FlashColor(Color color)
    {
        screenMat.color = color;
        screenMat.SetColor("_EmissionColor", color);

        yield return new WaitForSeconds(feedbackDuration);

        Color reset = new Color(0.133f, 0.2f, 0.267f); // Default blue
        screenMat.color = reset;
        screenMat.SetColor("_EmissionColor", reset);
    }
}