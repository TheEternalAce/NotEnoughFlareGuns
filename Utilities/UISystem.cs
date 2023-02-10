using Microsoft.Xna.Framework;
using ShardsOfAtheria.UI.LoreTablet;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace NotEnoughFlareGuns.Utilities
{
    public class UISystem : ModSystem
    {
        private StarlightButton starlightButton;
        private UserInterface starlightButtonState;

        public override void Load()
        {
            starlightButton = new();
            starlightButton.Activate();
            starlightButtonState = new();
            starlightButtonState.SetState(starlightButton);
        }

        public override void UpdateUI(GameTime gameTime)
        {
            starlightButtonState?.Update(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "YourMod: A Description",
                    delegate
                    {
                        starlightButtonState.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}
