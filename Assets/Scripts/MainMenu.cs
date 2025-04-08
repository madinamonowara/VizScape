using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
    }

    public void SceneOne()
    {
        SceneManager.LoadScene("Gavin");
    }
    
    public void SceneTwo()
    {
        SceneManager.LoadScene("SceneTwo");
    }
    
    public void SceneThree()
    {
        SceneManager.LoadScene("SceneThree");
    }
    
    public void SceneFour()
    {
        SceneManager.LoadScene("SceneFour");
    }
    
    public void SceneFive()
    {
        SceneManager.LoadScene("SceneFive");
    }

}
