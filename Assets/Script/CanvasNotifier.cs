using UnityEngine;

public class CanvasNotifier : MonoBehaviour
{
    public CameraUpForward cameraScript; // Referencia al script de la cámara

    void OnDisable()
    {
        if (cameraScript != null)
        {
            cameraScript.OnPanelClosed(); // Notifica al script de la cámara
        }
    }
}
