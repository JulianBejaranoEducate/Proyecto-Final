using UnityEngine;

public class Player : MonoBehaviour
{
    public float velocidadMovimiento = 5f;
    public float fuerzaSalto = 10f;

    private Rigidbody2D rb;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // 1. MOVIMIENTO (Izquierda / Derecha)
        float movimientoHorizontal = Input.GetAxisRaw("Horizontal"); // Detecta teclas A/D o Flechas

        // Aplicamos movimiento físico
        rb.linearVelocity = new Vector2(movimientoHorizontal * velocidadMovimiento, rb.linearVelocity.y);

        // 2. ANIMACIÓN DE CORRER
        // Mathf.Abs convierte el valor a positivo (si vas a la izquierda es -1, lo convierte a 1)
        animator.SetFloat("Speed", Mathf.Abs(movimientoHorizontal));

        // 3. GIRAR EL PERSONAJE (Flip)
        if (movimientoHorizontal > 0) // Derecha
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (movimientoHorizontal < 0) // Izquierda
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        // 4. SALTAR
        // Si presionas Espacio Y la velocidad vertical es casi 0 (estás en el suelo)
        bool enElSuelo = Mathf.Abs(rb.linearVelocity.y) < 0.001f;

        // Le avisamos al Animator si estamos en el suelo o no
        animator.SetBool("IsGrounded", enElSuelo);

        // 4. SALTAR (Modificado ligeramente para usar la variable enElSuelo)
        if (Input.GetButtonDown("Jump") && enElSuelo)
        {
            rb.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
            animator.SetTrigger("Jump");
        }

        // 5. RODAR (Roll)
        // Usaremos la tecla Shift Izquierdo (LeftShift) o la tecla "C"
        if (Input.GetKeyDown(KeyCode.C))
        {
            animator.SetTrigger("Roll");
        }
    }
}
