using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void VolverJugar()
    {
        SceneManager.LoadScene("Nivel1");
    }

    public void MenuPrincipal()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
