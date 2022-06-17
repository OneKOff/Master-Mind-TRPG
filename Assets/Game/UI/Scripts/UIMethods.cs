using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMethods : MonoBehaviour
{
    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1f;
    }
    
    public void StartGame()
    {
        UnpauseGame();
        SceneManager.LoadScene(2);
    }

    public void EditDeck()
    {
        UnpauseGame();
        SceneManager.LoadScene(1);
    }
    
    public void BackToMenu()
    {
        UnpauseGame();
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        UnpauseGame();
        Application.Quit();
    }
    
}
