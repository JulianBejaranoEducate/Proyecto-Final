using UnityEngine;

public class Camara : MonoBehaviour
{
    public Transform player;

    // X=0, Y=0, Z=-10. 
    // La Y ya no importa tanto en el offset porque usaremos la altura fija de la cámara.
    public Vector3 offset = new Vector3(0, 0, -10);
    public float velocidadSuave = 0.125f;

    void LateUpdate()
    {
        if (player != null)
        {
            Vector3 posicionDeseada = new Vector3(
                player.position.x + offset.x,  // Eje X: Sigue al jugador
                transform.position.y,           // Eje Y: Se queda quieta en su altura actual
                offset.z                        // Eje Z: Mantiene la distancia (-10)
            );

            Vector3 posicionSuavizada = Vector3.Lerp(transform.position, posicionDeseada, velocidadSuave);
            transform.position = posicionSuavizada;
        }
    }
}
