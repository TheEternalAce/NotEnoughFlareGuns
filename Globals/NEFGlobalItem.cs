using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Globals
{
    public class NEFGlobalItem : GlobalItem
    {
        /// <summary>
        /// Allows the Merchant to sell flares if the player has any flare gun in their inventory
        /// </summary>
        public static List<int> FlareGuns = new();
        public static List<int> Flares = new();
        public static List<int> Flamethrowers = new();
        public static List<int> Launchers = new();

        public override void SetDefaults(Item item)
        {
            if (item.type == ItemID.FlareGun)
            {
                item.damage = 12;
                item.DamageType = DamageClass.Ranged;
                FlareGuns.Add(item.type);
            }
            if (item.type == ItemID.Flamethrower || item.type == ItemID.EldMelter)
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
    }
}
