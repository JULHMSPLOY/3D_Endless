using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed = 5f;
    public float laneChangeSpeed = 10f;
    public float jumpForce = 8f; // Reduced jump force
    public bool canMove = false;
    private bool isGrounded = false;

    private int currentLane = 1;
    private Vector3 targetPosition;

    private Rigidbody rb;
    private Animator animator;

    AudioManager audioManager;

    private void Awake()
    {
        GameObject audioObj = GameObject.FindGameObjectWithTag("Audio");
        if (audioObj != null)
        {
            audioManager = audioObj.GetComponent<AudioManager>();
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        targetPosition = transform.position;
    }

    private void Update()
    {
        if (canMove) 
        {
            // Move forward
            transform.Translate(Vector3.forward * playerSpeed * Time.deltaTime, Space.World);

            targetPosition.z = transform.position.z;

            // Handle lane switching input
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                ChangeLane(-1);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                ChangeLane(1);
            }

            // Jumping
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                Jump();
            }
        }

        // Smooth transition to target lane
        targetPosition = new Vector3(targetPosition.x, targetPosition.y, transform.localPosition.z);
        transform.position = Vector3.Slerp(transform.position, targetPosition, laneChangeSpeed * Time.deltaTime);
    }

    void ChangeLane(int direction)
    {
        int targetLane = currentLane + direction;
    if (targetLane < 0 || targetLane > 2) return;

    currentLane = targetLane;
    
    // กำหนดค่า X ใหม่: เลน 0 = -2, เลน 1 = 0, เลน 2 = 2 (ปรับตัวเลขตามความกว้างถนนของคุณ)
    float laneDistance = 2.0f; 
    float targetX = (currentLane - 1) * laneDistance;
    
    targetPosition = new Vector3(targetX, transform.position.y, transform.position.z);
    }

    void Jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, Mathf.Sqrt(2 * jumpForce * Mathf.Abs(Physics.gravity.y)), rb.linearVelocity.z);
        isGrounded = false;
        animator.SetBool("Jumping", true);

        if (audioManager != null) audioManager.PlaySFX(audioManager.jump);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetBool("Jumping", false);
        }
    }
}
