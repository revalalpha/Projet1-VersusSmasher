using UnityEngine;

public class MovementSolver : MonoBehaviour
{
    [Header("Collision")]
    [SerializeField] private BoxCollider2D m_collider2D;
    [SerializeField] private LayerMask m_groundLayer;
    [SerializeField] private float m_skinWidth = 0.01f;

    private CollisionInfo m_collisionInfo;

    //Its a getter 
    private CollisionInfo CollisionInfo => m_collisionInfo;
}