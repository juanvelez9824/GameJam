using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public string menuSceneName = "Menu";
    private bool isMenuLoaded = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // O cualquier otra tecla para pausar
        {
            if (isMenuLoaded)
            {
                UnloadMenu();
            }
            else
            {
                LoadMenu();
            }
        }
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(menuSceneName, LoadSceneMode.Additive);
        isMenuLoaded = true;
    }

    public void UnloadMenu()
    {
        SceneManager.UnloadSceneAsync(menuSceneName);
        isMenuLoaded = false;
    }
}
