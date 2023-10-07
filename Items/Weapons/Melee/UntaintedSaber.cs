using NotEnoughFlareGuns.Buffs.AnyDebuff;
using NotEnoughFlareGuns.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Items.Weapons.Melee
{
    public class UntaintedSaber : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
            Item.AddElementFire();
            Item.AddElementWood();
        }

        public override void SetDefaults()
        {
            // Common Properties
            Item.width = 40;
            Item.height = 40;
            Item.rare = ItemRarityID.Orange;
            Item.value = 140000;

            Item.damage = 38;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 6;
            Item.autoReuse = true;

            Item.useTime = 26;
            Item.useAnimation = 26;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
        }

        public override void ModifyHitNPC(Player player, NPC target, ref NPC.HitModifiers modifiers)
        {
            float multiplier = 1;
            if (target.HasBuff(BuffID.OnFire) || target.HasBuff(BuffID.Frostburn) ||
                target.HasBuff<BlightFire>())
            {
                multiplier *= 4;
            }
            if (target.HasBuff(BuffID.OnFire3) || target.HasBuff(BuffID.Frostburn2))
            {
                multiplier *= 5;
            }
            if (target.HasBuff(BuffID.ShadowFlame))
            {
                multiplier *= 7;
            }
            if (target.HasBuff(BuffID.CursedInferno))
            {
                multiplier *= 9;
            }
            modifiers.FinalDamage *= multiplier;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            bool applyHemorrhaging = false;
            if (target.HasBuff(BuffID.OnFire))
            {
                target.DelBuff(target.FindBuffIndex(BuffID.OnFire));
                applyHemorrhaging = true;
            }
            if (target.HasBuff<BlightFire>())
            {
                target.DelBuff(target.FindBuffIndex(ModContent.BuffType<BlightFire>()));
                applyHemorrhaging = true;
            }
            if (target.HasBuff(BuffID.OnFire3))
            {
                target.DelBuff(target.FindBuffIndex(BuffID.OnFire3));
                applyHemorrhaging = true;
            }
            if (target.HasBuff(BuffID.Frostburn))
            {
                target.DelBuff(target.FindBuffIndex(BuffID.Frostburn));
                applyHemorrhaging = true;
            }
            if (target.HasBuff(BuffID.Frostburn2))
            {
                target.DelBuff(target.FindBuffIndex(BuffID.Frostburn2));
                applyHemorrhaging = true;
            }
            if (target.HasBuff(BuffID.ShadowFlame))
            {
                target.DelBuff(target.FindBuffIndex(BuffID.ShadowFlame));
                applyHemorrhaging = true;
            }
            if (target.HasBuff(BuffID.CursedInferno))
            {
                target.DelBuff(target.FindBuffIndex(BuffID.CursedInferno));
                applyHemorrhaging = true;
            }
            if (applyHemorrhaging)
            {
                SoundEngine.PlaySound(NEFG.Lacerate);
                target.AddBuff(ModContent.BuffType<Hemorrhaging>(), 600);
            }
        }

        public override void AddRecipes()
        {
            string key = "Mods.NotEnoughFlareGuns.Condition.SacrificeEnchantedSword";
            CreateRecipe()
                .AddIngredient(ItemID.EnchantedSword)
                .AddCondition(new Condition(key, () => false))
                .Register();
        }
    }
}
