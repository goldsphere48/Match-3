
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Timers;

namespace Match_3
{
    class GameScreen : IScreen
    {
        private Sprite background;
        private SpriteFont font;
        private Timer timer;

        private GameGrid grid;

        private int timeLeft;

        public void Init()
        {
            background = new Sprite(GameContext.Instance.ContentManager.Load<Texture2D>("gameBackground"));

            font = GameContext.Instance.ContentManager.Load<SpriteFont>("Font");

            grid = new GameGrid(8, 8, new Vector2(28, 72));

            //Заводим таймер на 60 секунд
            timeLeft = 60;

            timer = new Timer(1000);
            timer.Elapsed += TimerTick;
            timer.Start();
        }

        //Срабатывает каждую секунду
        public void TimerTick(object sender, EventArgs e)
        {
            if (timeLeft > 0)
            {
                timeLeft--;
            }
            else
            {
                //если время игры вышло, перейти к Game over экрану
                timer.Stop();
                ScreenManager.Instance.SetScreen("GameOver");
            }

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            GameContext.Instance.GraphicsDevice.Clear(new Color(255, 202, 85));

            grid.Draw(spriteBatch);
            background.Draw(spriteBatch);

            spriteBatch.Begin();

            spriteBatch.DrawString(font, "Score: " + grid.Score, new Vector2(110, 10), Color.Black);
            spriteBatch.DrawString(font, "Timer: " + timeLeft, new Vector2(420, 10), Color.Black);

            spriteBatch.End();
        }

        public void Update(GameTime gameTime)
        {
            grid.Update();
        }

        public void Shutdown()
        {

        }
    }
}
