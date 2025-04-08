using UnityEngine;

public class PuzzleButton : MonoBehaviour
{
    public int buttonID;
    public PuzzleManager puzzleManager;

    public void OnButtonPressed()
    {
        puzzleManager.RegisterButtonPress(buttonID);
    }
}