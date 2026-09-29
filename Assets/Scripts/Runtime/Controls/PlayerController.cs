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
}