using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneHelper : MonoBehaviour
{
    [SerializeField] private CartImputControl imputControl;
    [SerializeField] private StoneSpawner stoneSpawner;
    [SerializeField] private GameObject startGameMenu;
  


    private bool isPaused = false;

    private void Awake()
    {
        imputControl.enabled = false;
        stoneSpawner.enabled = false;
    }

    public void StartGame()
    {
        imputControl.enabled = true;
        stoneSpawner.enabled = true;
        startGameMenu.SetActive(false);
        
    }

    public void Restart()
    {   
        isPaused = false;
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);       
    }

    public void LoadScene(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }

    public void Pause()
    {
        if (!isPaused)
        {
            Time.timeScale = 0f;
            isPaused = true;
        }
    }

    public void Resume()
    {
        if (isPaused)
        {
            Time.timeScale = 1f;
            isPaused = false;
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
