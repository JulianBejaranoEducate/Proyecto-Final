using UnityEngine;
using UnityEngine.SceneManagement;

public class PantallaInicio : MonoBehaviour
{
    public void EmpezarJuego()
    {
        SceneManager.LoadScene("MenuPrincipal");
        Debug.Log("Entrando al Menu Principal del Juego");
    }

    void Start()
    {

    }

    void Update()
    {

    }
}
