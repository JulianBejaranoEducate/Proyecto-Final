using UnityEngine;

public class Player : MonoBehaviour
{
    public float velocidadMovimiento = 5f;
    public float fuerzaSalto = 10f;

    private Rigidbody2D rb;
    private Animator animator;

    [Header("Combate")]
    public Transform puntoAtaque; // Arrastra aquí el objeto vacío "PuntoAtaque"
    public float radioAtaque = 0.5f; // Tamaño del círculo
    public LayerMask capaEnemigos;   // Selecciona aquí la capa "Enemigos"
    public int danoAtaque = 1;

    public float tiempoEntreAtaques = 0.5f;
    private float tiempoSiguienteAtaque = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float movimientoHorizontal = Input.GetAxisRaw("Horizontal"); // Detecta teclas A/D o Flechas

        rb.linearVelocity = new Vector2(movimientoHorizontal * velocidadMovimiento, rb.linearVelocity.y);

        animator.SetFloat("Speed", Mathf.Abs(movimientoHorizontal));

        if (movimientoHorizontal > 0) // Derecha
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (movimientoHorizontal < 0) // Izquierda
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        bool enElSuelo = Mathf.Abs(rb.linearVelocity.y) < 0.001f;

        animator.SetBool("IsGrounded", enElSuelo);

        if (Input.GetButtonDown("Jump") && enElSuelo)
        {
            rb.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
            animator.SetTrigger("Jump");
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            animator.SetTrigger("Roll");
        }

        // TEMPORIZADOR DE ATAQUE
        if (tiempoSiguienteAtaque > 0)
        {
            tiempoSiguienteAtaque -= Time.deltaTime;
        }

        // DETECTAR CLIC IZQUIERDO O TECLA CONTROL
        if (Input.GetButtonDown("Fire1") && tiempoSiguienteAtaque <= 0)
        {
            Atacar();
            tiempoSiguienteAtaque = tiempoEntreAtaques;
        }
    }

    void Atacar()
    {
        animator.SetTrigger("Attack");

        Collider2D[] enemigosGolpeados = Physics2D.OverlapCircleAll(puntoAtaque.position, radioAtaque, capaEnemigos);

        foreach (Collider2D enemigo in enemigosGolpeados)
        {
            Debug.Log("Golpeaste a: " + enemigo.name);

            VidaEnemigo vidaScript = enemigo.GetComponent<VidaEnemigo>();

            if (vidaScript != null)
            {
                vidaScript.RecibirDano(danoAtaque);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (puntoAtaque == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(puntoAtaque.position, radioAtaque);
    }
}
