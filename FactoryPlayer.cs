using NotEnoughFlareGuns.Buffs.PlayerBuff;
using NotEnoughFlareGuns.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns
{
    public class FactoryPlayer : ModPlayer
    {
        Mod shards;
        double totalDamageTaken;
        bool factorySoul;
        public bool coreOverheat;

        public override void ResetEffects()
        {
            if (shards != null)
            {
                factorySoul = (bool)shards.Call("checkHasSoulCrystal", Player, ModContent.ItemType<FactorySoulCrystal>());
            }
            coreOverheat = false;
        }

        public override void Initialize()
        {
            if (ModLoader.TryGetMod("ShardsOfAtheria", out Mod shards))
            {
                this.shards = shards;
            }
            base.Initialize();
        }

        public override void UpdateEquips()
        {
            if (shards != null)
            {
                if (coreOverheat)
                {
                    Player.moveSpeed += 0.05f;
                }
            }
        }

        public override void PostHurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit, int cooldownCounter)
        {
            if (shards != null)
            {
                if (factorySoul && !coreOverheat)
                {
                    totalDamageTaken += damage;
                    if (totalDamageTaken >= 200)
                    {
                        Player.AddBuff(ModContent.BuffType<CoreOverheat>(), 3600);
                        totalDamageTaken = 0;
                    }
                }
            }
        }

        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (coreOverheat)
            {
                target.AddBuff(BuffID.OnFire3, 600);
            }
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (coreOverheat)
            {
                target.AddBuff(BuffID.OnFire3, 600);
            }
        }
    }
}
