using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerCharacter m_controlledCharacter;

    public void SetPlayerCharacter(PlayerCharacter playerCharacter)
    {
        m_controlledCharacter = playerCharacter;
    }

    public void ReceiveMoveInput(InputAction.CallbackContext context)
    {
        Vector2 moveInput = context.ReadValue<Vector2>();
        m_controlledCharacter.Move(moveInput);
    }

    public void ReceiveJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
            m_controlledCharacter.Jump();
    }

    public void ReceiveLightAttack(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        PlayerAttackStateKey[] lightAttacks =
        {
            PlayerAttackStateKey.Punching,
            PlayerAttackStateKey.ElbowChop,
            PlayerAttackStateKey.BackFist,
            PlayerAttackStateKey.FrontSweep
        };

        m_controlledCharacter.Attack(lightAttacks[UnityEngine.Random.Range(0, lightAttacks.Length)]);
    }

    public void ReceiveKickAttack(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        PlayerAttackStateKey[] kickAttacks =
        {
            PlayerAttackStateKey.AxeKick,
            PlayerAttackStateKey.LowKick,
            PlayerAttackStateKey.SpinningAxeKick,
            PlayerAttackStateKey.BackKick
        };

        m_controlledCharacter.Attack(kickAttacks[UnityEngine.Random.Range(0, kickAttacks.Length)]);
    }

    public void ReceiveSpecialAttack(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        PlayerAttackStateKey[] specialAttacks = 
        {
            PlayerAttackStateKey.JumpingUppercut,
            PlayerAttackStateKey.WebsterSideKick,
            PlayerAttackStateKey.SpinningElbow,
            PlayerAttackStateKey.CressentKick,
            PlayerAttackStateKey.JumpingSideKick
        };

        m_controlledCharacter.Attack(specialAttacks[UnityEngine.Random.Range(0, specialAttacks.Length)]);
    }

    public void ReceiveComboAttack(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        PlayerAttackStateKey[] comboAttacks =
        {
            PlayerAttackStateKey.ComboSamba,
            PlayerAttackStateKey.ComboFist
        };

        m_controlledCharacter.Attack(comboAttacks[UnityEngine.Random.Range(0, comboAttacks.Length)]);
    }
}