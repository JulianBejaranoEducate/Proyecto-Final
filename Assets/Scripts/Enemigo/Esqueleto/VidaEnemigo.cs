using UnityEngine;

public class VidaEnemigo : MonoBehaviour
{
    public int vidaMaxima = 3;
    public Animator animator;

    private int vidaActual;

    void Start()
    {
        vidaActual = vidaMaxima;
        if (animator == null) animator = GetComponent<Animator>();
    }

    public void RecibirDano(int dano)
    {
        vidaActual -= dano;

        // Animación de dolor
        if (animator != null) animator.SetTrigger("Hit");

        if (vidaActual <= 0)
        {
            Morir();
        }
    }

    void Morir()
    {
        Debug.Log("¡Enemigo eliminado!");

        if (animator != null) animator.SetBool("IsDead", true);

        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;

        Destroy(gameObject, 2f);
    }
}
