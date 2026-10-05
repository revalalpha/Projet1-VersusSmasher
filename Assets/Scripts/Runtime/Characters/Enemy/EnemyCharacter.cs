using UnityEngine;

[RequireComponent(typeof(MovementSolver))]
public class EnemyCharacter : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float m_moveSpeed = 3f;
    [SerializeField] private float m_groundAcceleration = 30f;
    [SerializeField] private float m_groundDeceleration = 30f;
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

    public Hitbox Hitbox;
    public Hurtbox Hurtbox;

    private Vector2 m_velocity;
    public Vector2 Velocity => m_velocity;

    private Vector2 m_moveInput;
    public Vector2 MoveInput => m_moveInput;

    private bool m_jumpInput;
    private bool m_canCoyoteJump;

    private CollisionInfo m_collisionInfo;
    public CollisionInfo CollisionInfo => m_movementSolver.CollisionInfo;

    private float m_lastJumpInputTime = float.MinValue;
    private float m_lastGroundedTime = float.MinValue;
    public float LastJumpInputTime => m_lastJumpInputTime;
    public float JumpInputBuffer => m_jumpInputBuffer;

    private EnemyMovementStateMachine m_movementMachine;
    public EnemyMovementStateMachine MovementMachine => m_movementMachine;

    private EnemyAttackStateMachine m_attackMachine;
    public EnemyAttackStateMachine AttackMachine => m_attackMachine;

    public Animator Animator => animator;

    private MovementSolver m_movementSolver;
    private Animator animator;

    private PlayerCharacter m_player;
    public PlayerCharacter Player => m_player;

    public int Health = 100;

    public void SetPlayer(PlayerCharacter player)
    {
        m_player = player;
    }


    private void Awake()
    {
        animator = GetComponent<Animator>();
        m_movementSolver = GetComponent<MovementSolver>();
        m_movementMachine = new EnemyMovementStateMachine(this);
        m_attackMachine = new EnemyAttackStateMachine(this);

        m_movementMachine.RegisterState(EnemyMovementStateKey.EnemyIdle, new EnemyIdleState());
        m_movementMachine.RegisterState(EnemyMovementStateKey.EnemyWalking, new EnemyWalkingState());
        m_movementMachine.RegisterState(EnemyMovementStateKey.EnemyFalling, new EnemyFallingState());
        m_movementMachine.RegisterState(EnemyMovementStateKey.EnemyJumping, new EnemyJumpState());
        //m_movementMachine.RegisterState( EnemyMovementStateKey.EnemyBlocking, new EnemyBlockingState());
        //m_movementMachine.RegisterState(EnemyMovementStateKey.EnemyDeath, new EnemyDeathState());
        m_movementMachine.RegisterState(EnemyMovementStateKey.EnemyAttacking, new EnemyIsAttackingState());

        m_attackMachine.RegisterState(EnemyAttackStateKey.EnemyAxeKick, new EnemyAxeKickState());
        m_attackMachine.RegisterState(EnemyAttackStateKey.EnemyBackKick, new EnemyBackKickState());
        m_attackMachine.RegisterState(EnemyAttackStateKey.EnemySpinningAxeKick, new EnemySpinningAxeKickState());
        m_attackMachine.RegisterState(EnemyAttackStateKey.EnemyLowKick, new EnemyLowKickState());
        m_attackMachine.RegisterState(EnemyAttackStateKey.EnemyJumpingSideKick, new EnemyJumpingSideKickState());
        m_attackMachine.RegisterState(EnemyAttackStateKey.EnemyWebsterSideKick, new EnemyWebsterSideKickState());
        m_attackMachine.RegisterState(EnemyAttackStateKey.EnemyCressentKick, new EnemyCressentKickState());
        m_attackMachine.RegisterState(EnemyAttackStateKey.EnemyPunching, new EnemyPunchingState());
        m_attackMachine.RegisterState(EnemyAttackStateKey.EnemyFrontSweep, new EnemyFrontSweepState());
        m_attackMachine.RegisterState(EnemyAttackStateKey.EnemyElbowChop, new EnemyElbowChopState());
        m_attackMachine.RegisterState(EnemyAttackStateKey.EnemySpinningElbow, new EnemySpinningElbowState());
        m_attackMachine.RegisterState(EnemyAttackStateKey.EnemyBackFist, new EnemyBackFistState());
        m_attackMachine.RegisterState(EnemyAttackStateKey.EnemyJumpingUppercut, new EnemyJumpingUppercutState());
        m_attackMachine.RegisterState(EnemyAttackStateKey.EnemyComboSamba, new EnemyComboSambaState());
        m_attackMachine.RegisterState(EnemyAttackStateKey.EnemyComboFist, new EnemyComboFistState());
        m_attackMachine.RegisterState(EnemyAttackStateKey.None, new EnemyNoAttackState());

        m_movementMachine.SetState(EnemyMovementStateKey.EnemyIdle);
        m_attackMachine.SetState(EnemyAttackStateKey.None);
    }

    private void Update()
    {
        Think();
        m_collisionInfo = m_movementSolver.CollisionInfo;

        if (m_collisionInfo.m_above && m_velocity.y > 0)
            m_velocity.y = 0;

        if (m_collisionInfo.m_below && m_velocity.y < 0)
            m_velocity.y = 0;

        float gravity = m_velocity.y >= 0 ? m_risingGravity : m_fallingGravity;
        m_velocity.y -= gravity * Time.deltaTime;

        float targetVelocityX = m_moveInput.x * m_moveSpeed;
        float acceleration = m_moveInput.x != 0 ? m_groundAcceleration : m_groundDeceleration;

        if (!m_collisionInfo.m_below)
            acceleration = m_moveInput.x != 0 ? m_airAcceleration : m_airDeceleration;

        m_velocity.x = Mathf.MoveTowards(m_velocity.x, targetVelocityX, acceleration * Time.deltaTime);

        Vector2 deltaPosition = m_velocity * Time.deltaTime;

        if (m_collisionInfo.m_below)
        {
            m_lastGroundedTime = Time.time;
            m_canCoyoteJump = true;
        }

        m_movementSolver.ProcessMove(ref deltaPosition);
        transform.Translate(deltaPosition);

        m_jumpInput = false;

        m_movementMachine.Update();
        m_attackMachine.Update();
    }

    public void Move(Vector2 moveInput)
    {
        m_moveInput = moveInput;
    }

    public void Jump()
    {
        m_lastJumpInputTime = Time.time;
    }

    public void ApplyJumpForce()
    {
        m_velocity.y = m_jumpForce;
    }

    public void ApplyKnockback(Vector2 force)
    {
        GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
    }

    public void Attack(EnemyAttackStateKey key)
    {
        if (AttackMachine.CurrentKey != EnemyAttackStateKey.None)
            return;

        MovementMachine.SetState(EnemyMovementStateKey.EnemyAttacking);
        AttackMachine.SetState(key);
    }

    private void Think()
    {
        if (m_player == null)
            return;

        float dist = Vector2.Distance(transform.position, m_player.transform.position);

        Vector3 scale = transform.localScale;
        if (m_player.transform.position.x < transform.position.x)
            scale.x = Mathf.Abs(scale.x);
        else
            scale.x = -Mathf.Abs(scale.x);
        transform.localScale = scale;

 
        Vector2 moveInput = Vector2.zero;
        if (dist > 1.5f)
            moveInput.x = Mathf.Sign(m_player.transform.position.x - transform.position.x);

        Move(moveInput);

        if (dist < 1.5f && m_attackMachine.CurrentKey == EnemyAttackStateKey.None)
        {
            EnemyAttackStateKey[] attacks =
            {
            EnemyAttackStateKey.EnemyPunching,
            EnemyAttackStateKey.EnemyBackKick,
            EnemyAttackStateKey.EnemyLowKick,
            EnemyAttackStateKey.EnemySpinningElbow,
            EnemyAttackStateKey.EnemyCressentKick,
            EnemyAttackStateKey.EnemyComboFist
            };

            m_attackMachine.SetState(attacks[Random.Range(0, attacks.Length)]);
            m_movementMachine.SetState(EnemyMovementStateKey.EnemyAttacking);
        }
    }

}
