using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Match_3
{
    class GameOverScreen : IScreen
    {

        private Button okButton;
        private Sprite background;
        private Sprite gameOver;

        private bool init;

        public void Init()
        {
            Texture2D okButtonTexture = GameContext.Instance.ContentManager.Load<Texture2D>("okButton");

            okButton = new Button(okButtonTexture);
            okButton.Sprite.Origin = new Vector2(okButton.Pos.X / 2, okButton.Pos.Y / 2);

            int x = GameContext.Instance.Graphics.PreferredBackBufferWidth / 2 - okButton.Sprite.Texture.Width / 2;
            int y = GameContext.Instance.Graphics.PreferredBackBufferHeight / 2 - okButton.Sprite.Texture.Height / 2 + 200;

            okButton.Pos = new Vector2(x, y);

            background = new Sprite(GameContext.Instance.ContentManager.Load<Texture2D>("background"));

            gameOver = new Sprite(GameContext.Instance.ContentManager.Load<Texture2D>("gameOver"));

            x = GameContext.Instance.Graphics.PreferredBackBufferWidth / 2 - gameOver.Texture.Width / 2;
            y = GameContext.Instance.Graphics.PreferredBackBufferHeight / 2 - gameOver.Texture.Height / 2 - 100;

            gameOver.Origin = new Vector2(gameOver.Pos.X / 2, gameOver.Pos.Y / 2);

            gameOver.Pos = new Vector2(x, y);

            init = true;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (init)
            {
                background.Draw(spriteBatch);
                gameOver.Draw(spriteBatch);
                okButton.Draw(spriteBatch);
            }
        }

        public void Update(GameTime gameTime)
        {
            if (init)
            {
                if (okButton.IsClickedOnButton(Mouse.GetState().LeftButton))
                    ScreenManager.Instance.SetScreen("Menu");
            }
        }

        public void Shutdown()
        {

        }
    }
}
