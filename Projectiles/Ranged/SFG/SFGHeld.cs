using Microsoft.Xna.Framework;
using MMZeroElements;
using NotEnoughFlareGuns.Projectiles.Ranged.Flares;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Projectiles.Ranged.SFG
{
    public class SFGHeld : ModProjectile
    {
        bool fired = false;

        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 9;
        }

        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 46;
            Projectile.aiStyle = -1;
            Projectile.tileCollide = false;
            Projectile.hide = true; //aiStyle 20 assigns heldProj
            ProjectileElements.Fire.Add(Type);
        }

        public override void AI()
        {
            ModifiedDrillAI();
            Player player = Main.player[Projectile.owner];
            bool canShoot = player.HasAmmo(player.inventory[player.selectedItem]) && !player.noItems && !player.CCed && !fired;
            player.heldProj = Projectile.whoAmI;
            if (canShoot)
            {
                player.PickAmmo(player.inventory[player.selectedItem], out int projToShoot, out float speed, out int Damage, out float KnockBack, out var usedAmmoItemId);
                IEntitySource source = player.GetSource_ItemUse_WithPotentialAmmo(player.inventory[player.selectedItem], usedAmmoItemId);
                Vector2 velocity = Projectile.velocity;
                velocity.Normalize();
                projToShoot = ModContent.ProjectileType<SmallFrickinFlare>();

                Projectile.NewProjectileDirect(source, Projectile.Center, velocity * speed, projToShoot, Damage, KnockBack, player.whoAmI);
                fired = true;
            }
            else if (fired)
            {
                UpdateVisuals();
            }
        }

        void UpdateVisuals()
        {
            if (++Projectile.frameCounter >= 15)
            {
                if (++Projectile.frame == Main.projFrames[Type])
                {
                    SoundEngine.PlaySound(SoundID.Item98);
                    Projectile.Kill();
                }
                if (Projectile.frame == 4)
                {
                    SoundEngine.PlaySound(SoundID.Item99);
                }
                Projectile.frameCounter = 0;
            }
        }

        void ModifiedDrillAI()
        {
            Projectile.timeLeft = 60;
            Vector2 vector13 = Main.player[Projectile.owner].RotatedRelativePoint(Main.player[Projectile.owner].MountedCenter);
            if (Main.myPlayer == Projectile.owner)
            {
                float num127 = Main.player[Projectile.owner].inventory[Main.player[Projectile.owner].selectedItem].shootSpeed * Projectile.scale;
                Vector2 vector14 = vector13;
                float num128 = Main.mouseX + Main.screenPosition.X - vector14.X;
                float num130 = Main.mouseY + Main.screenPosition.Y - vector14.Y;
                if (Main.player[Projectile.owner].gravDir == -1f)
                {
                    num130 = Main.screenHeight - Main.mouseY + Main.screenPosition.Y - vector14.Y;
                }
                float num131 = (float)Math.Sqrt(num128 * num128 + num130 * num130);
                num131 = (float)Math.Sqrt(num128 * num128 + num130 * num130);
                num131 = num127 / num131;
                num128 *= num131;
                num130 *= num131;
                if (num128 != Projectile.velocity.X || num130 != Projectile.velocity.Y)
                {
                    Projectile.netUpdate = true;
                }
                Projectile.velocity.X = num128;
                Projectile.velocity.Y = num130;
            }
            if (Projectile.velocity.X > 0f)
            {
                Main.player[Projectile.owner].ChangeDir(1);
            }
            else if (Projectile.velocity.X < 0f)
            {
                Main.player[Projectile.owner].ChangeDir(-1);
            }
            Projectile.spriteDirection = Projectile.direction;
            Main.player[Projectile.owner].ChangeDir(Projectile.direction);
            Main.player[Projectile.owner].heldProj = Projectile.whoAmI;
            Main.player[Projectile.owner].SetDummyItemTime(2);
            Projectile.position.X = vector13.X - Projectile.width / 2;
            Projectile.position.Y = vector13.Y - Projectile.height / 2;
            Projectile.rotation = (float)(Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.5700000524520874);
            if (Main.player[Projectile.owner].direction == 1)
            {
                Main.player[Projectile.owner].itemRotation = (float)Math.Atan2(Projectile.velocity.Y * Projectile.direction, Projectile.velocity.X * Projectile.direction);
            }
            else
            {
                Main.player[Projectile.owner].itemRotation = (float)Math.Atan2(Projectile.velocity.Y * Projectile.direction, Projectile.velocity.X * Projectile.direction);
            }
        }
    }
}