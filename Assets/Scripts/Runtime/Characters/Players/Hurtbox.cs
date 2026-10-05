using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    public EnemyCharacter enemy;
    public PlayerCharacter player;

    public void TakeHit(int damage, float knockback, Vector3 attackerPos)
    {
        Vector2 dir = (transform.position - attackerPos).normalized;

        if (enemy != null)
        {
            enemy.Health -= damage;
            enemy.ApplyKnockback(dir * knockback);
        }

        if (player != null)
            player.ApplyKnockback(dir * knockback);
    }
}