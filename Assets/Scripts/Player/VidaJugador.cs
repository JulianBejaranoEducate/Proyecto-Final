using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class VidaJugador : MonoBehaviour
{
    [Header("Configuración")]
    public int vidaMaxima = 3;
    public Animator barraVidaAnimator;

    private int vidaActual;
    private Animator playerAnimator;

    void Start()
    {
        vidaActual = vidaMaxima;
        playerAnimator = GetComponent<Animator>();

        if (barraVidaAnimator != null)
            barraVidaAnimator.SetInteger("NivelVida", vidaActual);
    }

    public void RecibirDano(int cantidad)
    {
        vidaActual -= cantidad;

        if (barraVidaAnimator != null)
        {
            barraVidaAnimator.SetInteger("NivelVida", vidaActual);
        }

        if (playerAnimator != null)
        {
            playerAnimator.SetTrigger("Hit");
        }

        if (vidaActual <= 0)
        {
            Morir();
        }
    }

    void Morir()
    {
        Debug.Log("¡El jugador ha muerto!");

        if (playerAnimator != null)
        {
            playerAnimator.SetBool("IsDead", true);
        }

        Player movimientoScript = GetComponent<Player>();
        if (movimientoScript != null)
        {
            movimientoScript.enabled = false;
        }

        StartCoroutine(CambiarEscenaConRetraso());
    }

    IEnumerator CambiarEscenaConRetraso()
    {
        // Esperamos 2 segundos (o lo que dure tu animación de muerte)
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("GameOver");
    }
}
