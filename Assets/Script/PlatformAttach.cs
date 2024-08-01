using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformAttach : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Si el personaje entra en contacto con la plataforma
        if (collision.gameObject.CompareTag("Player"))
        {
            // Establecer la plataforma como padre del personaje
            collision.transform.SetParent(transform);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // Si el personaje deja de estar en contacto con la plataforma
        if (collision.gameObject.CompareTag("Player"))
        {
            // Eliminar la plataforma como padre del personaje
            collision.transform.SetParent(null);
        }
    }
}
