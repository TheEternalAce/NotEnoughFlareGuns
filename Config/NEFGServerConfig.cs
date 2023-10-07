using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace NotEnoughFlareGuns.Config
{
    [BackgroundColor(164, 153, 190)]
    public class NEFGServerConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [Header("SoundHeader")]
        [DefaultValue(true)]
        [ReloadRequired]
        public bool IntercomAudio;
    }
}
