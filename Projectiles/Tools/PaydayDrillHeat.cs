using Microsoft.Xna.Framework;
using NotEnoughFlareGuns.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Projectiles.Tools
{
    public class PaydayDrillHeat : ModProjectile
    {
        public override string Texture => NEFG.BlankTexture;

        public override void SetStaticDefaults()
        {
            Projectile.AddElementElec();
        }

        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 4;
            Projectile.timeLeft = 120;
            Projectile.extraUpdates = 3;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 140;
            Projectile.ignoreWater = true;
        }

        public override void AI()
        {
            Projectile.velocity.Normalize();
            Projectile.velocity *= 4f;

            Dust d = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.Torch);
            d.velocity *= 0;
            d.fadeIn = 1.3f;
            d.noGravity = true;
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            if (!Main.mouseRight)
            {
                return false;
            }
            return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.velocity = oldVelocity;
            var player = Projectile.GetPlayerOwner();
            var item = player.HeldItem;
            int i = (int)(Projectile.Center.X / 16);
            int j = (int)(Projectile.Center.Y / 16);
            player.PickTile(i - 1, j - 1, item.pick);
            player.PickTile(i - 1, j, item.pick);
            player.PickTile(i, j - 1, item.pick);
            player.PickTile(i + 1, j + 1, item.pick);
            player.PickTile(i + 1, j, item.pick);
            player.PickTile(i, j + 1, item.pick);
            player.PickTile(i + 1, j - 1, item.pick);
            player.PickTile(i - 1, j + 1, item.pick);
            return false;
        }
    }
}
