using UnityEngine;

public class move : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] public float speed = 8f;
    [SerializeField] public float jumpForce = 12f;
    [SerializeField] private float groundCheckDistance = 0.2f;
    [SerializeField] private float recoilForce = 4f; // Nueva variable para el retroceso
    
    [Header("Object References")]
    public GameObject miObjeto;
    public GameObject miOtroObjeto;
    public bool cambioObjeto = false;

    // Component references
    private Rigidbody rb;
    private Renderer rend;
    private Color originalColor;
    private Vector3 initialPosition;
    private Transform cameraTransform;
    private Vector3 cameraOffset;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;
        initialPosition = transform.position;
        
        // Configuración de cámara
        cameraTransform = Camera.main.transform;
        cameraOffset = cameraTransform.position - transform.position;
        
        // Configuración física óptima
        rb.drag = 1f;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
        HandleShooting();
        CheckBoundaries();
    }

    private void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        transform.Translate(movement * speed * Time.deltaTime);
    }

    private void HandleJump()
    {
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);
        
        if (Input.GetKeyDown(KeyCode.Q) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void HandleShooting()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Disparo normal
            Instantiate(cambioObjeto ? miOtroObjeto : miObjeto, transform.position, Quaternion.identity);
            
            // Retroceso hacia atrás (sin afectar cámara)
            rb.AddForce(-transform.forward * recoilForce, ForceMode.Impulse);
        }
    }

    private void CheckBoundaries()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -22.70373f, 22.65776f);
        pos.z = Mathf.Clamp(pos.z, -23.18279f, 15.6f);
        transform.position = pos;
    }

    private void LateUpdate()
    {
        // Mantiene la posición relativa de la cámara
        cameraTransform.position = transform.position + cameraOffset;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wood"))
        {
            ResetPosition();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            rend.material.color = Color.black;
            Invoke(nameof(ResetColor), 1f);
        }
    }


    private void ResetPosition()
    {
        rb.velocity = Vector3.zero;
        transform.position = initialPosition;
        Debug.Log("Chocaste con madera - Volviendo al inicio");
    }

    private void ResetColor()
    {
        rend.material.color = originalColor;
    }
}