using UnityEngine;

public class MovementSolver : MonoBehaviour
{
    [Header("Collision")]
    [SerializeField] private BoxCollider2D m_collider2D;
    [SerializeField] private LayerMask m_groundLayer;
    [SerializeField] private float m_skinWidth = 0.01f;

    private CollisionInfo m_collisionInfo;
    private StateMachine<PlayerMovementStateKey> m_stateMachine;

    private void Awake()
    {
        m_collisionInfo = new CollisionInfo();
    }

    //Its a getter 
    public CollisionInfo CollisionInfo => m_collisionInfo;

    public void ProcessMove(ref Vector2 deltaPosition)
    {
        m_collisionInfo.Reset();

        if (deltaPosition.x != 0)
            ProcessHorizontalCollision(ref deltaPosition);

        if (deltaPosition.y != 0)
            ProcessVerticalCollision(ref deltaPosition);
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

        if (Mathf.Abs(deltaPosition.y) < 0.0001f)
            return;

        RaycastHit2D hit = Physics2D.BoxCast(
            origin + new Vector3(deltaPosition.x, 0),
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
}