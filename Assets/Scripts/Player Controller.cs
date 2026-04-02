using UnityEngine;
using UnityEngine.InputSystem;

//public class PlayerController : MonoBehaviour {
public class PlayerController : Singleton<PlayerController> {

    public bool FacingLeft { get { return facingLeft; }}

    [Header("Movimento")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("Pulo 2.5D")]
    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private float gravity = 20f;

    [Header("Referências")]
    [SerializeField] private Transform visual; // filho com sprite

    private bool facingLeft = false;

    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;

    private Animator myAnimator;
    private SpriteRenderer mySpriteRender;

    // --- Pulo ---
    private float altura = 0f;
    private float velocidadeY = 0f;
    private bool estaNoChao = true;

    private Collider2D playerCol;

    private Vector3 checkpointPosition;

    // =========================
    // INICIALIZAÇÃO
    // =========================

    // private void Awake()
    // {
    protected override void Awake() {
        base.Awake();

        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();

        myAnimator = GetComponentInChildren<Animator>();
        mySpriteRender = GetComponentInChildren<SpriteRenderer>();

        playerCol = GetComponent<Collider2D>();

        checkpointPosition = transform.position;
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    // =========================
    // LOOP
    // =========================

    private void Update()
    {
        PlayerInput();
        HandleJump();
        UpdateJumpPhysics();
        UpdateVisualHeight();
    }

    private void FixedUpdate()
    {
        AdjustPlayerFacingDirection();
        Move();
    }

    // =========================
    // INPUT
    // =========================

    private void PlayerInput()
    {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();

        myAnimator.SetFloat("moveX", movement.x);
        myAnimator.SetFloat("moveY", movement.y);
    }

    // =========================
    // MOVIMENTO
    // =========================

    private void Move()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    // =========================
    // PULO 2.5D
    // =========================

    private void HandleJump()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame && estaNoChao)
        {
            velocidadeY = jumpForce;
            estaNoChao = false;

            myAnimator.SetTrigger("jump");
        }
    }

    private void UpdateJumpPhysics()
    {
        if (!estaNoChao)
        {
            velocidadeY -= gravity * Time.deltaTime;
            altura += velocidadeY * Time.deltaTime;

            // CAIU NO CHÃO
            if (altura <= 0f)
            {
                altura = 0f;
                velocidadeY = 0f;
                estaNoChao = true;

                ReativarColisoes();
            }
        }

        myAnimator.SetBool("isGrounded", estaNoChao);
    }

    private void UpdateVisualHeight()
    {
        if (visual != null)
        {
            visual.localPosition = new Vector3(0, altura, 0);
        }
    }

    // =========================
    // OLHAR PARA O MOUSE
    // =========================

    private void AdjustPlayerFacingDirection()
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        mySpriteRender.flipX = mousePos.x < playerScreenPoint.x;
    }

    // =========================
    // SISTEMA DE OBSTÁCULOS PULÁVEIS
    // =========================

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Se estiver no ar e tocar algo pulável → atravessa
        if (!estaNoChao && collision.gameObject.CompareTag("Jumpable"))
        {
            Physics2D.IgnoreCollision(playerCol, collision.collider, true);
        }
    }

    private void ReativarColisoes()
    {
        // Detecta tudo ao redor
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, 2f);

        foreach (var c in cols)
        {
            if (c.CompareTag("Jumpable"))
            {
                Physics2D.IgnoreCollision(playerCol, c, false);
            }
        }
    }


private void OnTriggerEnter2D(Collider2D collision)
{
    // Se encostar na poça e NÃO estiver pulando
    if (collision.CompareTag("Deadly") && estaNoChao)
    {
        Die();
    }

        // CHECKPOINT
    if (collision.CompareTag("Checkpoint"))
    {
        checkpointPosition = collision.transform.position;
        Debug.Log("Checkpoint atualizado!");
    }

    // MORTE
    if (collision.CompareTag("Deadly") && estaNoChao)
    {
        Die();
    }
}


private void Die()
{
    Debug.Log("Morreu!");

    // aqui depois você pode colocar animação, som, etc

    Respawn();
}


private void Respawn()
{
    transform.position = checkpointPosition;

    altura = 0f;
    velocidadeY = 0f;
    estaNoChao = true;
}





}