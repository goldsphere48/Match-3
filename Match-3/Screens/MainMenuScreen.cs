using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Timers;

namespace Match_3
{
    class MainMenuScreen : IScreen
    {
        private Button playButton;
        private Sprite background;

        public void Init()
        {
            Texture2D playButtonTexture = GameContext.Instance.ContentManager.Load<Texture2D>("playButton");

            playButton = new Button(playButtonTexture);
            playButton.Sprite.Origin = new Vector2(playButton.Pos.X / 2, playButton.Pos.Y / 2);

            int x = GameContext.Instance.Graphics.PreferredBackBufferWidth / 2 - playButton.Sprite.Texture.Width / 2;
            int y = GameContext.Instance.Graphics.PreferredBackBufferHeight / 2 - playButton.Sprite.Texture.Height / 2;

            playButton.Pos = new Vector2(x, y);

            background = new Sprite(GameContext.Instance.ContentManager.Load<Texture2D>("background"));
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            background.Draw(spriteBatch);
            playButton.Draw(spriteBatch);
        }

        public void Update(GameTime gameTime)
        {
            if (playButton.IsClickedOnButton(Mouse.GetState().LeftButton))
                ScreenManager.Instance.SetScreen("Game");
        }

        public void Shutdown()
        {
            
        }
    }
}
