using TMPro;
using UnityEngine;

public class ContadorMonedas : MonoBehaviour
{
    public GameManager gameManager;
    public TextMeshProUGUI puntos;

    void Update()
    {
        puntos.text = gameManager.PuntoMonedas.ToString();
    }
}
