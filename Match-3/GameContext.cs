using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

/*
 * Класс-синглтон для удобного доступа к необходим модулям движка, без предачи их в качестве параметров
 */

namespace Match_3
{
    class GameContext
    {
        private static GameContext instance;

        public ContentManager ContentManager { get; set; }

        public GraphicsDeviceManager Graphics { get; set; }

        public GraphicsDevice GraphicsDevice { get; set; }

        public static GameContext Instance
        {
            get
            {
                if (instance == null)
                    instance = new GameContext();
                return instance;
            }
        }

    }
}
