using NotEnoughFlareGuns.Buffs.PlayerBuff;
using NotEnoughFlareGuns.Items;
using NotEnoughFlareGuns.Utilities;
using SubworldLibrary;
using Terraria;
using Terraria.DataStructures;
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
        public int sceneInvulnerability = 0;

        public override void ResetEffects()
        {
            if (shards != null)
            {
                factorySoul = (bool)shards.Call("checkHasSoulCrystal", Player, ModContent.ItemType<FactorySoulCrystal>());
            }
            coreOverheat = false;
            if (sceneInvulnerability > 0)
            {
                sceneInvulnerability--;
            }
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

        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource, ref int cooldownCounter)
        {
            if (sceneInvulnerability > 0)
            {
                return false;
            }
            return base.PreHurt(pvp, quiet, ref damage, ref hitDirection, ref crit, ref customDamage, ref playSound, ref genGore, ref damageSource, ref cooldownCounter);
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

        public override void OnRespawn(Player player)
        {
            SubworldSystem.Exit();
        }

        public override void ModifyScreenPosition()
        {
            ModContent.GetInstance<CameraFocus>().UpdateScreen(this);
            Main.screenPosition = Main.screenPosition.Floor();
        }
    }
}
