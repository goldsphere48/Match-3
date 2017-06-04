using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Match_3
{
    public class MainGameClass : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private GameTime gameTime;

        public MainGameClass()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 692;
            graphics.PreferredBackBufferHeight = 734;

            Content.RootDirectory = "Content";

            ScreenManager.Instance.Add("Menu", new MainMenuScreen());
            ScreenManager.Instance.Add("Game", new GameScreen());
            ScreenManager.Instance.Add("GameOver", new GameOverScreen());

            gameTime = new GameTime();
        }

        protected override void Initialize()
        {
            this.IsMouseVisible = true;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            GameContext.Instance.ContentManager = Content;
            GameContext.Instance.Graphics = graphics;
            GameContext.Instance.GraphicsDevice = GraphicsDevice;

            ScreenManager.Instance.SetScreen("Menu");
        }

        protected override void UnloadContent()
        {
            ScreenManager.Instance.Del("Menu");
            ScreenManager.Instance.Del("Game");
            ScreenManager.Instance.Del("GameOver");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            ScreenManager.Instance.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            ScreenManager.Instance.Draw(gameTime, spriteBatch);

            base.Draw(gameTime);
        }
    }
}
