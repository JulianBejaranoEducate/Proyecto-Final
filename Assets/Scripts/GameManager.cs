using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Es una propiedad
    public int PuntoMonedas { get { return puntosMonedas; } }
    private int puntosMonedas;

    public void SumarPuntos(int puntos)
    {
        puntosMonedas = puntosMonedas + puntos;
        print("Monedas: " + puntosMonedas);
    }
}
