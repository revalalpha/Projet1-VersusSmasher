using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float m_moveSpeed = 10f;
    [SerializeField] private float m_groundAcceleration = 100f;
    [SerializeField] private float m_groundDeceleration = 100f;
    [SerializeField] private float m_airAcceleration = 50f;
    [SerializeField] private float m_airDeceleration = 50f;

    [Header("Gravity")]
    [SerializeField] private float m_fallingGravity = 15f;
    [SerializeField] private float m_risingGravity = 25f;

    [Header("Jump")]
    [SerializeField] private float m_jumpForce = 10f;
    [SerializeField] private float m_jumpInputBuffer = 0.1f;
    [SerializeField] private float m_delayBeforeFalling = 0.1f;
    [SerializeField] private float m_coyoteTime = 0.08f;

    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private CollisionInfo m_collisionInfo;
    private Vector2 m_velocity;
    private Vector2 m_moveInput;
    private bool m_jumpInput;
    private bool m_canCoyoteJump;

    private float m_lastJumpInputTime = float.MinValue;
    private float m_lastGroundedTime = float.MinValue;

    private void Update()
    {
        if (m_collisionInfo.m_below || m_collisionInfo.m_above)
            m_velocity.y = 0;

        animator.SetFloat("Speed", Mathf.Abs(m_velocity.x));
        animator.SetBool("IsGrounded", m_collisionInfo.m_below);
        animator.SetFloat("VerticalVelocity", m_velocity.y);

        ProcessJump();

        float gravity = m_velocity.y >= 0 ? m_risingGravity : m_fallingGravity;
        m_velocity.y -= gravity * Time.deltaTime;

        float targetVelocityX = m_moveInput.x * m_moveSpeed;
        float acceleration = m_moveInput.x != 0 ? m_groundAcceleration : m_groundDeceleration;

        if (!m_collisionInfo.m_below)
            acceleration = m_moveInput.x != 0 ? m_airAcceleration : m_airDeceleration;
        else
            acceleration = m_moveInput.x != 0 ? m_groundAcceleration : m_groundDeceleration;

        m_velocity.x = Mathf.MoveTowards(m_velocity.x, targetVelocityX, acceleration * Time.deltaTime);

        Vector2 deltaPosition = m_velocity * Time.deltaTime;

        m_collisionInfo.Reset();

        ProcessMove(ref deltaPosition);

        if(m_collisionInfo.m_below)
        {
            m_lastGroundedTime = Time.time;
            m_canCoyoteJump = true;
        }


        transform.Translate(deltaPosition);

        m_jumpInput = false;
    }

    private void ProcessMove(ref Vector2 deltaPosition)
    {
        if (deltaPosition.x != 0)
            ProcessHorizontalCollision(ref deltaPosition);

        if (deltaPosition.y != 0)
            ProcessVerticalCollision(ref deltaPosition);
    }

    private void ProcessJump()
    {
        bool mustJump = false;
        if (Time.time - m_lastJumpInputTime <= m_jumpInputBuffer && m_collisionInfo.m_below)
            mustJump = true;
        if (m_canCoyoteJump && Time.time - m_lastGroundedTime <= m_coyoteTime && !m_collisionInfo.m_below && m_lastJumpInputTime >= 0)
            mustJump = true;

        if(mustJump)
        {
            animator.SetTrigger("Jump");
            m_velocity.y = m_jumpForce;
            m_lastJumpInputTime = float.MinValue; //Reset the buffer time so it doesn't keep jumping
            m_canCoyoteJump = false; //Reset coyote jump so it doesn't keep jumping
        }

        //if (Time.time - m_lastJumpInputTime <= m_jumpInputBuffer && m_collisionInfo.m_below)
        //{
        //    m_velocity.y = m_jumpForce;
        //    m_lastJumpInputTime = float.MinValue; //Reset the buffer time so it doesn't keep jumping
        //    m_canCoyoteJump = false;
        //}

        //if (m_canCoyoteJump && Time.time - m_lastGroundedTime <= m_coyoteTime && !m_collisionInfo.m_below && m_lastJumpInputTime >= 0)
        //{
        //    m_velocity.y = m_jumpForce;
        //    m_lastJumpInputTime = float.MinValue; //Reset the buffer time so it doesn't keep jumping
        //    m_canCoyoteJump = false; //Reset coyote jump so it doesn't keep jumping

        //}
    }

    private void ProcessHorizontalCollision(ref Vector2 deltaPosition)
    {
        float directionX = Mathf.Sign(deltaPosition.x);

        Vector3 origin = transform.position + (Vector3)m_collider2D.offset;

        RaycastHit2D hit = Physics2D.BoxCast(
            origin,
            m_collider2D.size,
            0f,
            Vector2.right * directionX,
            Mathf.Abs(deltaPosition.x) + m_skinWidth,
            m_groundLayer
        );

        if (hit)
        {
            deltaPosition.x = (hit.distance - m_skinWidth) * directionX;
            m_collisionInfo.m_left = directionX < 0;
            m_collisionInfo.m_right = directionX > 0;
        }
    }

    private void ProcessVerticalCollision(ref Vector2 deltaPosition)
    {
        float directionY = Mathf.Sign(deltaPosition.y);

        Vector3 origin = transform.position + (Vector3)m_collider2D.offset;

        RaycastHit2D hit = Physics2D.BoxCast(
            origin + new Vector3(deltaPosition.x,0),
            m_collider2D.size,
            0f,
            Vector2.up * directionY,
            Mathf.Abs(deltaPosition.y) + m_skinWidth,
            m_groundLayer
        );

        if (hit)
        {
            deltaPosition.y = (hit.distance - m_skinWidth) * directionY;
            m_collisionInfo.m_below = directionY < 0;
            m_collisionInfo.m_above = directionY > 0;
        }
    }

    public void Move(Vector2 moveInput)
    {
        m_moveInput = moveInput;
    }

    public void Jump()
    {
        m_lastJumpInputTime = Time.time;
    }
}