using UnityEngine;

public class Esqueleto : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    public float velocidadPatrulla = 2f;
    public float velocidadPersecucion = 3.5f;

    [Header("Puntos de Patrulla")]
    public Transform puntoA;
    public Transform puntoB;

    [Header("Detección y Ataque")]
    public Transform jugador;
    public float distanciaDeteccion = 5f;
    public float distanciaAtaque = 1f;

    private Animator animator;
    private Transform puntoDestinoActual;
    private bool estaAtacando = false;

    private float tiempoSiguienteAtaque = 0f;
    public float cooldownAtaque = 1.5f; // Tiempo entre espadazos

    void Start()
    {
        animator = GetComponent<Animator>();
        puntoDestinoActual = puntoB;
    }

    void Update()
    {
        if (jugador == null) return;

        // Reducimos el contador de tiempo
        if (tiempoSiguienteAtaque > 0)
            tiempoSiguienteAtaque -= Time.deltaTime;

        float distanciaAlJugador = Vector2.Distance(transform.position, jugador.position);

        // Lógica de Ataque modificada
        if (distanciaAlJugador < distanciaAtaque)
        {
            // Solo ataca si el contador llegó a 0
            if (tiempoSiguienteAtaque <= 0)
            {
                Atacar();
                tiempoSiguienteAtaque = cooldownAtaque; // Reiniciamos el contador
            }
        }
        else if (distanciaAlJugador < distanciaDeteccion)
        {
            PerseguirJugador();
        }
        else
        {
            Patrullar();
        }
    }

    void Patrullar()
    {
        if (estaAtacando) return; // Si está atacando, no se mueve

        // Activar animación de caminar
        animator.SetBool("IsWalking", true);

        // Moverse hacia el punto de destino actual (A o B)
        transform.position = Vector2.MoveTowards(transform.position, puntoDestinoActual.position, velocidadPatrulla * Time.deltaTime);

        // Mirar hacia el objetivo
        Girar(puntoDestinoActual.position);

        // Si llegamos al punto, cambiamos al otro
        if (Vector2.Distance(transform.position, puntoDestinoActual.position) < 0.2f)
        {
            if (puntoDestinoActual == puntoA) puntoDestinoActual = puntoB;
            else puntoDestinoActual = puntoA;
        }
    }

    void PerseguirJugador()
    {
        if (estaAtacando) return;

        animator.SetBool("IsWalking", true);

        transform.position = Vector2.MoveTowards(transform.position, jugador.position, velocidadPersecucion * Time.deltaTime);

        Girar(jugador.position);
    }

    void Atacar()
    {
        // Lógica visual (Animación)
        animator.SetBool("IsWalking", false);
        animator.SetTrigger("Attack");

        // Lógica de Daño (NUEVO)
        // Buscamos el script de vida en el jugador y le restamos 1
        VidaJugador vidaScript = jugador.GetComponent<VidaJugador>();
        if (vidaScript != null)
        {
            vidaScript.RecibirDano(1);
        }
    }

    void Girar(Vector3 objetivo)
    {
        if (objetivo.x > transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distanciaDeteccion);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distanciaAtaque);
    }
}
