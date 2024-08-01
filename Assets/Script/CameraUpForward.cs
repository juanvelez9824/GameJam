using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraUpForward : MonoBehaviour
{
    public float initialSpeed = 1.0f;  // Velocidad inicial de la cámara
    public float speedIncreaseInterval = 10f;  // Cada cuántas unidades de Y aumenta la velocidad
    public float speedIncreaseAmount = 0.5f;  // Cuánto aumenta la velocidad en cada intervalo
    public float bottomThreshold = -10f; // Distancia debajo de la cámara para reiniciar
    public float upperLimit = 100f; // Límite superior de la cámara

    private bool isPanelClosed = false;

    private float currentSpeed;
    private float lastSpeedIncreaseY;
    private Camera cam;
    private Transform playerTransform;

    void Start()
    {
        currentSpeed = initialSpeed;
        lastSpeedIncreaseY = transform.position.y;
        cam = GetComponent<Camera>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Mueve la cámara solo si el panel está cerrado
        if (isPanelClosed)
        {
            if (transform.position.y < upperLimit)
            {
                // Mueve la cámara hacia arriba
                transform.Translate(Vector3.up * currentSpeed * Time.deltaTime);

                // Verifica si es momento de aumentar la velocidad
                if (transform.position.y - lastSpeedIncreaseY >= speedIncreaseInterval)
                {
                    IncreaseSpeed();
                }
            }
        }

        CheckPlayerVisibility();
    }

    void IncreaseSpeed()
    {
        currentSpeed += speedIncreaseAmount;
        lastSpeedIncreaseY = transform.position.y;
        Debug.Log($"Velocidad aumentada a: {currentSpeed}");
    }

    void CheckPlayerVisibility()
    {
        if (playerTransform != null)
        {
            Vector3 viewportPosition = cam.WorldToViewportPoint(playerTransform.position);

            // Si el jugador está por debajo del umbral o fuera de la vista horizontalmente
            if (viewportPosition.y < 0 && playerTransform.position.y < transform.position.y + bottomThreshold ||
                viewportPosition.x < 0 || viewportPosition.x > 1)
            {
                RestartGame();
            }
        }
    }

    void RestartGame()
    {
        Debug.Log("Jugador fuera de rango. Reiniciando partida...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Método que se llama cuando el panel se cierra
    public void OnPanelClosed()
    {
        isPanelClosed = true;
    }
}
