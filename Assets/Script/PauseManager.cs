using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] GameObject menuPause;


    public bool pauseGame = false;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseGame)
            {
                Resume();
            }
            else
            {
                Pausa();
            }
        }
           
    }
    public void Pausa()
    {
        pauseGame = true;
        Time.timeScale = 0f;
        //buttonPause.SetActive(false);
        menuPause.SetActive(true);
    }

    public void Resume()
    {
        pauseGame = false;
        Time.timeScale = 1f;
        menuPause.SetActive(false);
    }

    public void Restart()
    {
        pauseGame = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    
    
}
