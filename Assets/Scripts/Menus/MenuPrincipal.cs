using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public GameObject menuPrincipal;
    public GameObject menuOpciones;

    public void AbrirOpciones() 
    {
        menuPrincipal.SetActive(false);
        menuOpciones.SetActive(true);
    }

    public void AbrirMenu()
    {
        menuPrincipal.SetActive(true);
        menuOpciones.SetActive(false);
    }

    public void SalirJuego()
    {
        Application.Quit();
        Debug.Log("Saliendo del juego");
    }

    public void JugarJuego()
    {
        SceneManager.LoadScene("Nivel1");
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
