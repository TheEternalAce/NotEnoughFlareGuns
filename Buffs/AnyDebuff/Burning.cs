using Microsoft.Xna.Framework;
using NotEnoughFlareGuns.Projectiles;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Buffs.AnyDebuff
{
    public class Burning : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Burning"); // Buff display name
            Description.SetDefault("Losing life"); // Buff description
            Main.debuff[Type] = true;  // Is it a debuff?
            Main.pvpBuff[Type] = true; // Players can give other players buffs, which are listed as pvpBuff
            Main.buffNoSave[Type] = true; // It means the buff won't save when you exit the world
            BuffID.Sets.LongerExpertDebuff[Type] = true; // If this buff is a debuff, setting this to true will make this buff last twice as long on players in expert mode
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<BurningPlayer>().playerBurning = true;
            if (player.buffTime[buffIndex] == 0)
            {
                Projectile.NewProjectile(player.GetSource_OnHit(player), player.Center, Vector2.Zero, ModContent.ProjectileType<ScorchShotExplosion>(), 20, 1f);
                SoundEngine.PlaySound(SoundID.Item20);
            }
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<BurningNPC>().npcBurning = true;
            if (npc.buffTime[buffIndex] == 0)
            {
                Projectile.NewProjectile(npc.GetSource_OnHit(npc), npc.Center, Vector2.Zero, ModContent.ProjectileType<ScorchShotExplosion>(), 20, 1f, Main.myPlayer);
                SoundEngine.PlaySound(SoundID.Item20);
            }
        }
    }

    public class BurningNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public bool npcBurning;

        public override void ResetEffects(NPC npc)
        {
            npcBurning = false;
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (npcBurning)
            {
                // These lines zero out any positive lifeRegen. This is expected for all bad life regeneration effects.
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                // lifeRegen is measured in 1/2 life per second. Therefore, this effect causes 8 life lost per second.
                npc.lifeRegen -= 16;
            }
        }
    }

    public class BurningPlayer : ModPlayer
    {
        // Flag checking when life regen debuff should be activated
        public bool playerBurning;

        public override void ResetEffects()
        {
            playerBurning = false;
        }

        // Allows you to give the player a negative life regeneration based on its state (for example, the "On Fire!" debuff makes the player take damage-over-time)
        // This is typically done by setting player.lifeRegen to 0 if it is positive, setting player.lifeRegenTime to 0, and subtracting a number from player.lifeRegen
        // The player will take damage at a rate of half the number you subtract per second
        public override void UpdateBadLifeRegen()
        {
            if (playerBurning)
            {
                // These lines zero out any positive lifeRegen. This is expected for all bad life regeneration effects
                if (Player.lifeRegen > 0)
                    Player.lifeRegen = 0;
                // Player.lifeRegenTime uses to increase the speed at which the player reaches its maximum natural life regeneration
                // So we set it to 0, and while this debuff is active, it never reaches it
                Player.lifeRegenTime = 0;
                // lifeRegen is measured in 1/2 life per second. Therefore, this effect causes 8 life lost per second
                Player.lifeRegen -= 16;
            }
        }
    }
}
