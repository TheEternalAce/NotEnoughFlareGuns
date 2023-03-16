using NotEnoughFlareGuns.Projectiles.Ranged.Flares;
using System;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns
{
    public partial class NotEnoughFlareGuns
    {
        public const string GetConvertFlareType = "getConvertibleFlareType";

        public override object Call(params object[] args)
        {
            // Make sure the call doesn't include anything that could potentially cause exceptions.
            if (args is null)
            {
                throw new ArgumentNullException(nameof(args), "Arguments cannot be null!");
            }
            if (args.Length == 0)
            {
                throw new ArgumentException("Arguments cannot be empty!");
            }


            // This check makes sure that the argument is a string using pattern matching.
            // Since we only need one parameter, we'll take only the first item in the array..
            if (args[0] is string content)
            {
                // ..And treat it as a command type.
                switch (content)
                {
                    default:
                        throw new ArgumentException("Unrecognized ModCall. Usable ModCalls for Shards of Atheria are as follows: " +
                            $"{GetConvertFlareType}.");
                    case GetConvertFlareType:
                        return ModContent.ProjectileType<ConvertibleFlare>();
                }
            }

            return base.Call(args);
        }
    }
}
