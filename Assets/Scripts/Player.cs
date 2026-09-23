using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public bool isAlive;
    public bool isGrounded;

    [SerializeField] float speed;
    [SerializeField] float jumpForce;

    [SerializeField] float horizontaInput;
    [SerializeField] bool jumpRequest;

    [Header("Refs")]
    [SerializeField] Rigidbody rb;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundRadius = 0.2f;
    [SerializeField] LayerMask groundLayer;

    [SerializeField] GameObject gameoverPanel;

    [Header("HeadChecks")]
    [SerializeField] Transform headCheck;
    [SerializeField] float headCheckRadius = 0.2f;
    [SerializeField] LayerMask deadlyLayer;

    void Start()
    {
        gameoverPanel.SetActive(false);

        isAlive = true;

        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
    }

    private void Update()
    {
        
        isGrounded = Physics.CheckSphere(
            groundCheck.position,
            groundRadius,
            groundLayer,
            QueryTriggerInteraction.Collide
        );

        
        bool headCollision = Physics.CheckSphere(
            headCheck.position,
            headCheckRadius,
            deadlyLayer,
            QueryTriggerInteraction.Collide
        );

        if (headCollision)
        {
            Die();
        }
    }

    private void FixedUpdate()
    {
        if (!isAlive)
            return;

        
        rb.linearVelocity = new Vector3(
            horizontaInput * speed,
            rb.linearVelocity.y,
            rb.linearVelocity.z
        );

        
        if (jumpRequest)
        {
            jumpRequest = false;

            if (isGrounded)
            {
                rb.linearVelocity += new Vector3(
                    0f,
                    jumpForce,
                    0f
                );
            }
        }
    }

    public void WalkLeft()
    {
        horizontaInput = -1f;
    }

    public void WalkRight()
    {
        horizontaInput = 1f;
    }

    public void StopMoving()
    {
        horizontaInput = 0f;
    }

    public void JumpBttn()
    {
        jumpRequest = true;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Die()
    {
       
        if (!isAlive)
            return;

        print("Morreu");

        isAlive = false;
        horizontaInput = 0f;
        jumpRequest = false;

        gameoverPanel.SetActive(true);
    }

    private void OnDrawGizmos()
    {
        
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;

            Gizmos.DrawWireSphere(
                groundCheck.position,
                groundRadius
            );
        }

        
        if (headCheck != null)
        {
            Gizmos.color = Color.red;

            Gizmos.DrawWireSphere(
                headCheck.position,
                headCheckRadius
            );
        }
    }
}