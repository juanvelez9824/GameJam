using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandMovement : MonoBehaviour
{
    public float verticalSpeed = 2.0f;       // Velocidad del movimiento vertical
    public float maxVerticalHeight = 2.0f;   // Altura máxima del movimiento vertical
    public float minVerticalHeight = -2.0f;  // Altura mínima del movimiento vertical

    public float horizontalSpeed = 2.0f;     // Velocidad del movimiento horizontal
    public float maxHorizontalDistance = 2.0f; // Distancia máxima del movimiento horizontal
    public float minHorizontalDistance = -2.0f; // Distancia mínima del movimiento horizontal

    private Vector3 startPos;

    void Start()
    {
        // Guardar la posición inicial de la plataforma
        startPos = transform.position;
    }

    void Update()
    {
        // Calcular la nueva posición usando Mathf.PingPong para un movimiento suave
        float newY = startPos.y + Mathf.PingPong(Time.time * verticalSpeed, maxVerticalHeight - minVerticalHeight) + minVerticalHeight;
        float newX = startPos.x + Mathf.PingPong(Time.time * horizontalSpeed, maxHorizontalDistance - minHorizontalDistance) + minHorizontalDistance;

        transform.position = new Vector3(newX, newY, startPos.z);
    }
}