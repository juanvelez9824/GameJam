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
    public float stopPositionY = 140.11f;

    private float currentSpeed;
    private float lastSpeedIncreaseY;
    private Camera cam;
    private Transform playerTransform;
    private bool isCameraStopped = false;

    void Start()
    {
        currentSpeed = initialSpeed;
        lastSpeedIncreaseY = transform.position.y;
        cam = GetComponent<Camera>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (!isCameraStopped)
        {
            // Mueve la cámara hacia arriba
            MovementCamera();




        // Verifica si es momento de aumentar la velocidad
        if (transform.position.y - lastSpeedIncreaseY >= speedIncreaseInterval)
        {
            IncreaseSpeed();
        }
            if (transform.position.y >= stopPositionY)
            {
                StopCamera();
            }
        }
        //CheckPlayerVisibility();
    }

    void IncreaseSpeed()
    {
        currentSpeed += speedIncreaseAmount;
        lastSpeedIncreaseY = transform.position.y;
        Debug.Log($"Velocidad aumentada a: {currentSpeed}");
    }

    void MovementCamera()
    {
        Vector3 newPosition = transform.position + Vector3.up * currentSpeed * Time.deltaTime;
        newPosition.y = Mathf.Min(newPosition.y, stopPositionY); // Asegura que no sobrepase la posición de parada
        transform.position = newPosition;
    }
    void StopCamera()
    {
        isCameraStopped = true;
        currentSpeed = 0;
        Debug.Log("Cámara detenida en Y = " + stopPositionY);
    }
}

    /*void CheckPlayerVisibility()
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
}
    */