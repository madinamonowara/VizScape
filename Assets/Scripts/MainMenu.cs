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
        SceneManager.LoadScene("Madina");
    }
    
    public void SceneThree()
    {
        SceneManager.LoadScene("James");
    }
    
    public void SceneFour()
    {
        SceneManager.LoadScene("Shenghao");
    }
    
    public void SceneFive()
    {
        SceneManager.LoadScene("Derek");
    }

}
