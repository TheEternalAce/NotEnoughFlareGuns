using NotEnoughFlareGuns.Items.Weapons.Melee;
using NotEnoughFlareGuns.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Globals
{
    public class NEFGlobalItem : GlobalItem
    {
        /// <summary>
        /// Allows the Merchant to sell flares if the player has any flare gun in their inventory
        /// </summary>
        public static readonly List<int> FlareGuns = new();
        public static readonly List<int> Flares = new();
        public static readonly List<int> Flamethrowers = new();

        int transformTimer = 10;

        public override bool InstancePerEntity => true;

        public override void SetDefaults(Item item)
        {
            if (item.type == ItemID.FlareGun)
            {
                item.damage = 12;
                item.DamageType = DamageClass.Ranged;
                FlareGuns.Add(item.type);
            }
            if (item.type == ItemID.Flamethrower || item.type == ItemID.ElfMelter)
            {
                Flamethrowers.Add(item.type);
            }
            if (item.type == ItemID.Grenade || item.type == ItemID.Beenade || item.type == ItemID.StickyGrenade ||
                item.type == ItemID.BouncyGrenade || item.type == ItemID.PartyGirlGrenade)
            {
                item.ammo = AmmoID.Rocket;
            }
            if (item.ammo == AmmoID.Flare && item.consumable)
            {
                Flares.Add(item.type);
            }
        }

        public override bool CanConsumeAmmo(Item weapon, Item ammo, Player player)
        {
            if (Flamethrowers.Contains(weapon.type))
            {
                return !(player.itemAnimation < weapon.useAnimation - 2);
            }
            return base.CanConsumeAmmo(weapon, ammo, player);
        }

        public override void Update(Item item, ref float gravity, ref float maxFallSpeed)
        {
            if (item.type == ItemID.EnchantedSword)
            {
                if (item.lavaWet)
                {
                    var player = item.Center.FindClosestPlayer(-1);
                    if (player.ZoneUnderworldHeight)
                    {
                        if (--transformTimer <= 0)
                        {
                            item.TurnToAir();
                            Item.NewItem(item.GetSource_FromThis(), item.Hitbox,
                                ModContent.ItemType<UntaintedSaber>());
                            SoundEngine.PlaySound(SoundID.Item74, item.Center);
                        }
                    }
                }
            }
        }
    }
}
