using Microsoft.Xna.Framework;
using MMZeroElements.Utilities;
using NotEnoughFlareGuns.Utilities;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Projectiles.Magic
{
	public class FlameJavelin : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			Projectile.AddFire();
			Main.projFrames[Type] = 8;
			ProjectileID.Sets.TrailCacheLength[Type] = 30;
			ProjectileID.Sets.TrailingMode[Type] = 3;
		}

		public override void SetDefaults()
		{
			Projectile.width = Projectile.height = 14;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.timeLeft = 240;
			Projectile.alpha = 255;
			Projectile.penetrate = 3;
			Projectile.tileCollide = false;
		}

		public override void AI()
		{
			Projectile.ai[1]++;
			if (Projectile.ai[1] == 1)
			{
				for (var i = 0; i < 10; i++)
				{
					Dust dust = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.RedTorch, 0, 0, 0, new Color(255, 255, 255),
						1.575f);
					dust.noGravity = true;
					dust.fadeIn = 1.5f;
					dust.noLight = true;
					dust.velocity = new Vector2(2, 2).RotatedBy(MathHelper.ToRadians(36 * i));
				}
				Projectile.frame = Main.rand.Next(Main.projFrames[Type]);
				SoundEngine.PlaySound(SoundID.Item20, Projectile.position);

				if (Main.myPlayer == Main.player[Projectile.owner].whoAmI)
				{
					Vector2 shootVel = Main.MouseWorld - Projectile.Center;
					if (shootVel == Vector2.Zero)
					{
						shootVel = new Vector2(0f, 1f);
					}
					shootVel.Normalize();
					shootVel *= 5;
					Projectile.velocity = shootVel.RotatedByRandom(MathHelper.ToRadians(20));

					Projectile.netUpdate = true;
				}
			}
			else
			{
				if (Projectile.alpha > 0)
				{
					Projectile.alpha -= 36;
				}
				Projectile.tileCollide = true;
				Dust d = Dust.NewDustDirect(Projectile.Center - new Vector2(4, 4), 9, 9, DustID.Torch, Scale: 1.25f);
				d.velocity = -Projectile.velocity / 5;
				d.noLight = true;
				d.noGravity = true;

				if (Projectile.ai[1] < 30 && Main.myPlayer == Main.player[Projectile.owner].whoAmI)
				{
					Vector2 move = Main.MouseWorld - Projectile.Center;
					AdjustMagnitude(ref move);
					Projectile.velocity = (20 * Projectile.velocity + move);
					AdjustMagnitude(ref Projectile.velocity);
					Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90);

					Projectile.netUpdate = true;
					Projectile.tileCollide = false;
				}
				if (Projectile.ai[1] > 20)
				{
					Projectile.penetrate = 1;
				}
			}
		}

		public override void Kill(int timeLeft)
		{
			SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
			for (var i = 0; i < 11; i++)
			{
				Dust d = Dust.NewDustDirect(Projectile.Center - new Vector2(9, 9), 19, 19, DustID.Torch, Scale: 1.25f);
				d.velocity = (Projectile.velocity / (1.5f * d.scale)).RotatedByRandom(MathHelper.ToRadians(8));
				d.noLight = true;
				d.noGravity = true;
			}
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(BuffID.OnFire, 600);
		}

		private void AdjustMagnitude(ref Vector2 vector)
		{
			float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
			if (magnitude > 6f)
			{
				vector *= 10f / magnitude;
			}
		}

		public override bool PreDraw(ref Color lightColor)
		{
			if (Projectile.alpha <= 0)
			{
				Color color = new(227, 182, 245, 80);
				Projectile.DrawPrimsAfterImage(color);
			}
			return false;
		}
	}
}
