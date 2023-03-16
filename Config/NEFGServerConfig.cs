using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace NotEnoughFlareGuns.Config
{
    [BackgroundColor(164, 153, 190)]
    [Label("$Mods.NotEnoughFlareGuns.Config.ServerLabel")]
    public class NEFGServerConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [Header("$Mods.NotEnoughFlareGuns.Config.SoundHeader")]
        [Label("$Mods.NotEnoughFlareGuns.Config.OnslaughtIntercom")] // A label is the text displayed next to the option. This should usually be a short description of what it does.
        [Tooltip("$Mods.NotEnoughFlareGuns.ConfigDesc.OnslaughtIntercom")] // A tooltip is a description showed when you hover your mouse over the option. It can be used as a more in-depth explanation of the option.
        [DefaultValue(true)]
        [ReloadRequired]
        public bool IntercomAudio;
    }
}
