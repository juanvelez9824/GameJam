using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject menuPrefab; // Asigna el prefab del Canvas aquí
    private GameObject menuInstance;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // O cualquier otra tecla para pausar
        {
            if (menuInstance == null)
            {
                ShowMenu();
            }
            else
            {
                HideMenu();
            }
        }
    }

    public void ShowMenu()
    {
        menuInstance = Instantiate(menuPrefab);
        // Pausar el juego si es necesario
        Time.timeScale = 0f;
    }

    public void HideMenu()
    {
        Destroy(menuInstance);
        // Reanudar el juego si es necesario
        Time.timeScale = 1f;
    }
}
