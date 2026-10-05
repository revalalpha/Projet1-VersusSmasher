using UnityEngine;

public class JumpState : PlayerMovementState
{
    private float fallDelay = 1.0f;
    private float timer;

    public override void OnEnter()
    {
        timer = 0f;
        if (Mathf.Abs(Character.MoveInput.x) > 0.1f)
            Character.Animator.Play("RunningJump");
        else
            Character.Animator.Play("Jump");

        Character.ApplyJumpForce();
    }

    public override void OnUpdate()
    {
        timer += Time.deltaTime;

        bool isFalling = Character.Velocity.y < 0f;

        if (isFalling && timer >= fallDelay)
        {
            Machine.SetState(PlayerMovementStateKey.Falling);
        }
    }
}