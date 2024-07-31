using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 50f; // Velocidad de rotación del power-up
    [SerializeField] private float bobAmplitude = 0.5f; // Amplitud del movimiento de bobbing
    [SerializeField] private float bobFrequency = 1f; // Frecuencia del movimiento de bobbing
    [SerializeField] private float  powerUpDuration =5f; // Duracion del power-up

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        // Rotar el power-up
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        // Hacer que el power-up flote arriba y abajo
        float newY = startPosition.y + Mathf.Sin(Time.time * bobFrequency) * bobAmplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerMovement playerMovement = collision.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.ActivateDoubleJump(powerUpDuration);
                Destroy(gameObject);
            }
        }
    }
}
