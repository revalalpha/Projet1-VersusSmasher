using UnityEngine;

[RequireComponent(typeof(MovementSolver))]

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


    public Hitbox Hitbox;
    public Hurtbox Hurtbox;

    private Vector2 m_velocity;
    public Vector2 Velocity => m_velocity;
    public Vector2 MoveInput => m_moveInput;
    private Vector2 m_moveInput;
    private bool m_jumpInput;
    private bool m_canCoyoteJump;
    private CollisionInfo m_collisionInfo;

    private float m_lastJumpInputTime = float.MinValue;
    private float m_lastGroundedTime = float.MinValue;
    public float LastJumpInputTime => m_lastJumpInputTime;
    public float JumpInputBuffer => m_jumpInputBuffer;

    private PlayerMovementStateMachine m_movementStateMachine;
    public PlayerMovementStateMachine MovementMachine => m_movementStateMachine;
    private PlayerAttackStateMachine m_attackStateMachine;
    public PlayerAttackStateMachine AttackMachine => m_attackStateMachine;

    public Animator Animator => animator;
    public CollisionInfo CollisionInfo => m_movementSolver.CollisionInfo;
    public bool IsBlocking { get; private set; }

    private MovementSolver m_movementSolver;
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        m_movementSolver = GetComponent<MovementSolver>();
        m_movementStateMachine = new PlayerMovementStateMachine(this);
        m_attackStateMachine = new PlayerAttackStateMachine(this);

        m_movementStateMachine.RegisterState(PlayerMovementStateKey.Idle, new IdleState());
        m_movementStateMachine.RegisterState(PlayerMovementStateKey.Walking, new WalkingState());
        m_movementStateMachine.RegisterState(PlayerMovementStateKey.Falling, new FallingState());
        m_movementStateMachine.RegisterState(PlayerMovementStateKey.Jumping, new JumpState());
        //m_movementStateMachine.RegisterState(PlayerMovementStateKey.Blocking, new BlockingState());
        //m_movementStateMachine.RegisterState(PlayerMovementStateKey.Death, new DeathState());
        m_movementStateMachine.RegisterState(PlayerMovementStateKey.Attacking, new IsAttackingState());

        m_attackStateMachine.RegisterState(PlayerAttackStateKey.AxeKick, new AxeKickState());
        m_attackStateMachine.RegisterState(PlayerAttackStateKey.BackKick, new BackKickState());
        m_attackStateMachine.RegisterState(PlayerAttackStateKey.SpinningAxeKick, new SpinningAxeKickState());
        m_attackStateMachine.RegisterState(PlayerAttackStateKey.LowKick, new LowKickState());
        m_attackStateMachine.RegisterState(PlayerAttackStateKey.JumpingSideKick, new JumpingSideKickState());
        m_attackStateMachine.RegisterState(PlayerAttackStateKey.WebsterSideKick, new WebsterSideKickState());
        m_attackStateMachine.RegisterState(PlayerAttackStateKey.CressentKick, new CressentKickState());
        m_attackStateMachine.RegisterState(PlayerAttackStateKey.Punching, new PunchingState());
        m_attackStateMachine.RegisterState(PlayerAttackStateKey.FrontSweep, new FrontSweepState());
        m_attackStateMachine.RegisterState(PlayerAttackStateKey.ElbowChop, new ElbowChopState());
        m_attackStateMachine.RegisterState(PlayerAttackStateKey.SpinningElbow, new SpinningElbowState());
        m_attackStateMachine.RegisterState(PlayerAttackStateKey.BackFist, new BackFistState());
        m_attackStateMachine.RegisterState(PlayerAttackStateKey.JumpingUppercut, new JumpingUppercutState());
        m_attackStateMachine.RegisterState(PlayerAttackStateKey.ComboSamba, new ComboSambaState());
        m_attackStateMachine.RegisterState(PlayerAttackStateKey.ComboFist, new ComboFistState());
        m_attackStateMachine.RegisterState(PlayerAttackStateKey.None, new NoAttackState());

        m_movementStateMachine.SetState(PlayerMovementStateKey.Idle);
        m_attackStateMachine.SetState(PlayerAttackStateKey.None);
    }

    private void Update()
    {
        m_collisionInfo = m_movementSolver.CollisionInfo;
        if (m_collisionInfo.m_above && m_velocity.y > 0)
            m_velocity.y = 0;

        if (m_collisionInfo.m_below && m_velocity.y < 0)
            m_velocity.y = 0;

        //animator.SetFloat("Speed", Mathf.Abs(m_velocity.x));
        //animator.SetBool("IsGrounded", m_collisionInfo.m_below);
        //animator.SetFloat("VerticalVelocity", m_velocity.y);

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

        if(m_collisionInfo.m_below)
        {
            m_lastGroundedTime = Time.time;
            m_canCoyoteJump = true;
        }

        m_movementSolver.ProcessMove(ref deltaPosition);
        transform.Translate(deltaPosition);

        m_jumpInput = false;

        m_movementStateMachine.Update();
        m_attackStateMachine.Update();
    }

    private EnemyCharacter m_enemy;

    public void SetEnemy(EnemyCharacter enemy)
    {
        m_enemy = enemy;
    }

    public void ApplyKnockback(Vector2 force)
    {
        m_velocity.x += force.x;
        m_velocity.y += force.y;
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

    public void Attack(PlayerAttackStateKey key)
    {
        if (AttackMachine.CurrentKey != PlayerAttackStateKey.None)
            return;

        MovementMachine.SetState(PlayerMovementStateKey.Attacking);

        AttackMachine.SetState(key);
    }
}