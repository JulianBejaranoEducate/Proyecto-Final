using UnityEngine;

public class Camara : MonoBehaviour
{
    public Transform player;

    public Vector3 offset = new Vector3(0, 0, -10);
    public float velocidadSuave = 0.125f;

    void LateUpdate()
    {
        if (player != null)
        {
            Vector3 posicionDeseada = new Vector3(
                player.position.x + offset.x,
                transform.position.y,   
                offset.z 
            );

            Vector3 posicionSuavizada = Vector3.Lerp(transform.position, posicionDeseada, velocidadSuave);
            transform.position = posicionSuavizada;
        }
    }
}
