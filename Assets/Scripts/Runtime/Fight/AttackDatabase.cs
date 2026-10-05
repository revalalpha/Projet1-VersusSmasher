using UnityEngine;
using System.Collections.Generic;

public static class AttackDatabase
{
    public static readonly Dictionary<PlayerAttackStateKey, AttackData> Data = new Dictionary<PlayerAttackStateKey, AttackData>()
    {
            { PlayerAttackStateKey.None, new AttackData { Damage = 0, KnockbackForce = 0f, AnimationName = "Idle", Duration = 0f } },
            // LIGHT ATTACKS
            { PlayerAttackStateKey.Punching, new AttackData { Damage = 5, KnockbackForce = 2f, AnimationName = "Punching", Duration = 1.25f }},
            { PlayerAttackStateKey.ElbowChop, new AttackData { Damage = 5, KnockbackForce = 2f, AnimationName = "ElbowChop", Duration = 0.9333f }},
            { PlayerAttackStateKey.BackFist, new AttackData { Damage = 5, KnockbackForce = 2f, AnimationName = "BackFist", Duration = 1.1667f }},
            { PlayerAttackStateKey.FrontSweep, new AttackData { Damage = 5, KnockbackForce = 2f, AnimationName = "FrontSweep", Duration = 1.0667f }},

            // KICK ATTACKS
            { PlayerAttackStateKey.AxeKick, new AttackData { Damage = 5, KnockbackForce = 3f, AnimationName = "AxeKick", Duration = 1.5f }},
            { PlayerAttackStateKey.LowKick, new AttackData { Damage = 5, KnockbackForce = 3f, AnimationName = "LowKick", Duration = 1.667f }},
            { PlayerAttackStateKey.SpinningAxeKick, new AttackData { Damage = 5, KnockbackForce = 3f, AnimationName = "SpinningAxeKick", Duration = 1.2667f }},
            { PlayerAttackStateKey.BackKick, new AttackData { Damage = 5, KnockbackForce = 3f, AnimationName = "BackKick", Duration = 1.1667f }},

            // SPECIAL ATTACKS
            { PlayerAttackStateKey.JumpingUppercut, new AttackData { Damage = 10, KnockbackForce = 4f, AnimationName = "JumpingUppercut", Duration = 1.5f }},
            { PlayerAttackStateKey.WebsterSideKick, new AttackData { Damage = 10, KnockbackForce = 4f, AnimationName = "WebsterSideKick", Duration = 1.733f }},
            { PlayerAttackStateKey.SpinningElbow, new AttackData { Damage = 10, KnockbackForce = 4f, AnimationName = "SpinningElbow", Duration = 1.2667f }},
            { PlayerAttackStateKey.CressentKick, new AttackData { Damage = 10, KnockbackForce = 4f, AnimationName = "CressentKick", Duration = 2.5f }},
            { PlayerAttackStateKey.JumpingSideKick, new AttackData { Damage = 10, KnockbackForce = 4f, AnimationName = "JumpingSideKick", Duration = 1.0667f }},

            // COMBO ATTACKS
            { PlayerAttackStateKey.ComboSamba, new AttackData { Damage = 20, KnockbackForce = 6f, AnimationName = "ComboSamba", Duration = 2.2666f }},
            { PlayerAttackStateKey.ComboFist, new AttackData { Damage = 20, KnockbackForce = 6f, AnimationName = "ComboFist", Duration = 2.8333f }},
    };

    public static readonly Dictionary<EnemyAttackStateKey, AttackData> EnemyData = new Dictionary<EnemyAttackStateKey, AttackData>()
    {
            { EnemyAttackStateKey.None, new AttackData { Damage = 0, KnockbackForce = 0f, AnimationName = "Idle", Duration = 0f } },

            // LIGHT ATTACKS
            { EnemyAttackStateKey.EnemyPunching, new AttackData { Damage = 5, KnockbackForce = 2f, AnimationName = "Punching", Duration = 1.25f }},
            { EnemyAttackStateKey.EnemyElbowChop, new AttackData { Damage = 5, KnockbackForce = 2f, AnimationName = "ElbowChop", Duration = 0.9333f }},
            { EnemyAttackStateKey.EnemyBackFist, new AttackData { Damage = 5, KnockbackForce = 2f, AnimationName = "BackFist", Duration = 1.1667f }},
            { EnemyAttackStateKey.EnemyFrontSweep, new AttackData { Damage = 5, KnockbackForce = 2f, AnimationName = "FrontSweep", Duration = 1.0667f }},

            // KICK ATTACKS
            { EnemyAttackStateKey.EnemyAxeKick, new AttackData { Damage = 5, KnockbackForce = 3f, AnimationName = "AxeKick", Duration = 1.5f }},
            { EnemyAttackStateKey.EnemyLowKick, new AttackData { Damage = 5, KnockbackForce = 3f, AnimationName = "LowKick", Duration = 1.667f }},
            { EnemyAttackStateKey.EnemySpinningAxeKick, new AttackData { Damage = 5, KnockbackForce = 3f, AnimationName = "SpinningAxeKick", Duration = 1.2667f }},
            { EnemyAttackStateKey.EnemyBackKick, new AttackData { Damage = 5, KnockbackForce = 3f, AnimationName = "BackKick", Duration = 1.1667f }},

            // SPECIAL ATTACKS
            { EnemyAttackStateKey.EnemyJumpingUppercut, new AttackData { Damage = 10, KnockbackForce = 4f, AnimationName = "JumpingUppercut", Duration = 1.5f }},
            { EnemyAttackStateKey.EnemyWebsterSideKick, new AttackData { Damage = 10, KnockbackForce = 4f, AnimationName = "WebsterSideKick", Duration = 1.733f }},
            { EnemyAttackStateKey.EnemySpinningElbow, new AttackData { Damage = 10, KnockbackForce = 4f, AnimationName = "SpinningElbow", Duration = 1.2667f }},
            { EnemyAttackStateKey.EnemyCressentKick, new AttackData { Damage = 10, KnockbackForce = 4f, AnimationName = "CressentKick", Duration = 2.5f }},
            { EnemyAttackStateKey.EnemyJumpingSideKick, new AttackData { Damage = 10, KnockbackForce = 4f, AnimationName = "JumpingSideKick", Duration = 1.0667f }},

            // COMBO ATTACKS
            { EnemyAttackStateKey.EnemyComboSamba, new AttackData { Damage = 20, KnockbackForce = 6f, AnimationName = "ComboSamba", Duration = 2.2666f }},
            { EnemyAttackStateKey.EnemyComboFist, new AttackData { Damage = 20, KnockbackForce = 6f, AnimationName = "ComboFist", Duration = 2.8333f }},

    };
}