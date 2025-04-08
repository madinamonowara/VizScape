using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public int[] correctSequence = { 3, 1, 4, 2 };
    private int currentIndex = 0;
    private bool puzzleSolved = false;

    public GameObject lockIndicatorRight;
    public GameObject hatchRight;

    public void RegisterButtonPress(int buttonID)
    {
        if (puzzleSolved) return;

        if (buttonID == correctSequence[currentIndex])
        {
            currentIndex++;

            if (currentIndex == correctSequence.Length)
            {
                puzzleSolved = true;
                lockIndicatorRight.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green);
                hatchRight.SetActive(true);
                Debug.Log("Puzzle Solved");
            }
        }
        else
        {
            currentIndex = 0;
            Debug.Log("Incorrect. Resetting.");
        }
    }
}