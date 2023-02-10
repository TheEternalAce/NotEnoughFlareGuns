using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NotEnoughFlareGuns.InfernalUI;
using NotEnoughFlareGuns.Utilities;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace ShardsOfAtheria.UI.LoreTablet
{
    internal class StarlightButton : UIState
    {
        private bool display = false;

        private UIElement area;
        private UIImage icon;
        private Asset<Texture2D> iconImage = ModContent.Request<Texture2D>("NotEnoughFlareGuns/InfernalUI/StarlightButton");
        private UIHoverImageButton iconHover;
        private Asset<Texture2D> iconImageHover = ModContent.Request<Texture2D>("NotEnoughFlareGuns/InfernalUI/StarlightButtonHover");

        public override void OnInitialize()
        {
            area = new UIElement();
            area.Left.Set(3, 0f); // Place the tablet at the center of the screen.
            area.Top.Set(264, 0f);
            area.Width.Set(14, 0f);
            area.Height.Set(14, 0f);

            icon = new(iconImage);
            icon.Left.Set(0, 0f);
            icon.Top.Set(0, 0f);
            icon.Width.Set(14, 0f);
            icon.Height.Set(14, 0f);

            iconHover = new(iconImageHover, "Starlight: On");
            iconHover.Left.Set(-2, 0f);
            iconHover.Top.Set(-2, 0f);
            iconHover.Width.Set(18, 0f);
            iconHover.Height.Set(18, 0f);

            area.Append(icon);
            Append(area);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Main.playerInventory && Main.LocalPlayer.InfernalPlayer().cokeStarlight)
            {
                display = true;
                if (!Main.LocalPlayer.InfernalPlayer().cokeStarlightActive)
                {
                    icon.Color = Color.Gray;
                }
                else
                {
                    icon.Color = Color.White;
                }
            }
            else
            {
                display = false;
            }
            if (!display)
            {
                return;
            }
            base.Draw(spriteBatch);
        }

        public override void MouseUp(UIMouseEvent evt)
        {
            base.MouseUp(evt);
            if (display && area.ContainsPoint(evt.MousePosition))
            {
                Main.LocalPlayer.InfernalPlayer().cokeStarlightActive = !Main.LocalPlayer.InfernalPlayer().cokeStarlightActive;
                SoundEngine.PlaySound(SoundID.MenuTick);
                string on = "Off";
                if (Main.LocalPlayer.InfernalPlayer().cokeStarlightActive)
                {
                    on = "On";
                }
                iconHover.HoverText = "Starlight: " + on;
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            area.RemoveChild(iconHover);
            if (display && area.ContainsPoint(Main.MouseScreen))
            {
                Main.LocalPlayer.mouseInterface = true;
                area.Append(iconHover);
            }
        }
    }
}
