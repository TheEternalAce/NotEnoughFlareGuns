using Microsoft.Xna.Framework;
using NotEnoughFlareGuns.Projectiles.Melee;
using Terraria;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Buffs.AnyDebuff
{
    public class Hemorrhaging : ModBuff
    {
        int sprayTimer;

        public override string Texture => NEFG.Debuff;

        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;  // Is it a debuff?
            Main.pvpBuff[Type] = true; // Players can give other players buffs, which are listed as pvpBuff
            Main.buffNoSave[Type] = true; // It means the buff won't save when you exit the world
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            if (++sprayTimer >= 10)
            {
                Vector2 vector = Vector2.One.RotateRandom(MathHelper.TwoPi);
                vector *= 8 * Main.rand.NextFloat(0.33f, 1f);
                Projectile.NewProjectile(npc.GetSource_FromThis(), npc.Center, vector,
                    ModContent.ProjectileType<BloodSpray>(), 34, 2, Main.myPlayer,
                    npc.whoAmI);
                sprayTimer = 0;
            }
        }
    }
}
