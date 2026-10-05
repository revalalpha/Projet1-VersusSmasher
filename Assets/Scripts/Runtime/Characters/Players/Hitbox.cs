using UnityEngine;

public class Hitbox : MonoBehaviour
{
    private int damage;
    private float knockback;
    private bool active;

    public void Enable(int dmg, float kb)
    {
        damage = dmg;
        knockback = kb;
        active = true;
    }

    public void Disable()
    {
        active = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!active) return;

        if (other.TryGetComponent(out Hurtbox hurtbox))
            hurtbox.TakeHit(damage, knockback, transform.position);
    }
}