using Microsoft.Xna.Framework;
using NotEnoughFlareGuns.Buffs.PlayerBuff;
using NotEnoughFlareGuns.Items.Accessories.Backtanks;
using NotEnoughFlareGuns.Items.Tools;
using NotEnoughFlareGuns.NPCs.TheFactoryOnslaught;
using NotEnoughFlareGuns.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace NotEnoughFlareGuns
{
    public class FactoryPlayer : ModPlayer
    {
        Mod shards;

        public int sceneInvulnerability = 0;
        public int factoryTimer;
        public int factoryTimerMax = (int)FactoryHelper.Minutes(2);

        public bool cokeStarlight = false;
        public bool cokeStarlightActive = true;

        public bool blacklight = false;

        public int metalBacktankEquipped;
        public bool nozzleEquipped;

        bool factorySoul;
        double totalDamageTaken;
        public bool coreOverheat;

        public override void ResetEffects()
        {
            if (sceneInvulnerability > 0)
            {
                sceneInvulnerability--;
            }
            if (factoryTimer > 0)
            {
                factoryTimer--;
                if (factoryTimer == 0)
                {
                    Player.KillMe(PlayerDeathReason.ByCustomReason(Language.GetTextValue("Mods.NotEnoughFlareGuns.CustomDeathReason.Factory", Player.name)), 100000, 1);
                }
            }

            metalBacktankEquipped = BacktankTiers.None;
            nozzleEquipped = false;

            if (shards != null)
            {
                factorySoul = (bool)shards.Call("checkHasSoulCrystal", Player, "FactorySoulCrystal");
                coreOverheat = false;
            }
        }

        public override void SaveData(TagCompound tag)
        {
            tag[nameof(cokeStarlight)] = cokeStarlight;
            tag[nameof(cokeStarlightActive)] = cokeStarlightActive;
            tag[nameof(blacklight)] = blacklight;
        }

        public override void LoadData(TagCompound tag)
        {
            if (tag.ContainsKey(nameof(cokeStarlight)))
            {
                cokeStarlight = tag.GetBool(nameof(cokeStarlight));
            }
            if (tag.ContainsKey(nameof(cokeStarlightActive)))
            {
                cokeStarlightActive = tag.GetBool(nameof(cokeStarlightActive));
            }
            if (tag.ContainsKey(nameof(blacklight)))
            {
                blacklight = tag.GetBool(nameof(blacklight));
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

        public override IEnumerable<Item> AddStartingItems(bool mediumCoreDeath)
        {
            if (!mediumCoreDeath)
            {
                return new[] { new Item(ModContent.ItemType<Blacklight>()) };
            }
            return base.AddStartingItems(mediumCoreDeath);
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

        public override bool CanConsumeAmmo(Item weapon, Item ammo)
        {
            if (ammo.type == AmmoID.Gel)
            {
                switch (metalBacktankEquipped)
                {
                    case BacktankTiers.Luminite:
                        return Main.rand.NextFloat() > 0.66f;
                    case BacktankTiers.Soulstone:
                        return Main.rand.NextFloat() > 0.33f;
                    case BacktankTiers.Cobalt:
                        return Main.rand.NextFloat() > 0.16f;
                    case BacktankTiers.Copper:
                        return Main.rand.NextFloat() > 0.08f;
                }
            }
            return base.CanConsumeAmmo(weapon, ammo);
        }

        public override void ModifyShootStats(Item item, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (nozzleEquipped && item.shoot == ProjectileID.Flames && item.useAmmo == AmmoID.Gel)
            {
                velocity *= 1.3f;
            }
            base.ModifyShootStats(item, ref position, ref velocity, ref type, ref damage, ref knockback);
        }

        public override bool Shoot(Item item, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (metalBacktankEquipped > BacktankTiers.None && nozzleEquipped && item.shoot == ProjectileID.Flames
                && item.useAmmo == AmmoID.Gel)
            {
                velocity *= 1.2f;
                int numProjectiles = 2;
                float rotation = MathHelper.ToRadians(5);
                for (int i = 0; i < numProjectiles; i++)
                {
                    Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numProjectiles - 1))); // Watch out for dividing by 0 if there is only 1 projectile.
                    Projectile.NewProjectile(source, position, perturbedSpeed, type, damage, knockback, Player.whoAmI);
                }
                return false;
            }
            return base.Shoot(item, source, position, velocity, type, damage, knockback);
        }

        public override bool FreeDodge(Player.HurtInfo info)
        {
            if (sceneInvulnerability > 0)
            {
                return true;
            }
            return base.FreeDodge(info);
        }

        public override void PostHurt(Player.HurtInfo info)
        {
            if (shards != null)
            {
                if (factorySoul && !coreOverheat)
                {
                    totalDamageTaken += info.Damage;
                    if (totalDamageTaken >= 200)
                    {
                        Player.AddBuff(ModContent.BuffType<CoreOverheat>(), 3600);
                        totalDamageTaken = 0;
                    }
                }
            }
        }

        public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)
        {
            OnHitNPCGeneral(target, hit, damageDone);
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {
            OnHitNPCGeneral(target, hit, damageDone);
        }

        void OnHitNPCGeneral(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (coreOverheat)
            {
                target.AddBuff(BuffID.OnFire3, 600);
            }
            if (target.type == ModContent.NPCType<SoulstoneCore>())
            {
                if (target.life <= 0 && factoryTimer <= 0)
                {
                    factoryTimer = factoryTimerMax;
                    if (Main.masterMode || Main.getGoodWorld)
                    {
                        factoryTimer /= 2;
                    }
                    else if (Main.expertMode)
                    {
                        factoryTimer *= 3 / 4;
                    }
                }
            }
        }

        public override void OnRespawn()
        {
            //SubworldSystem.Exit();
        }

        public override void ModifyScreenPosition()
        {
            ModContent.GetInstance<CameraFocus>().UpdateScreen(this);
            Main.screenPosition = Main.screenPosition.Floor();
        }
    }
}
