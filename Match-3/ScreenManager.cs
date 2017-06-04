using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

/*
Класс-синглтон реализует управление экранами игры 
*/

namespace Match_3
{
    class ScreenManager
    {
        public ScreenManager()
        {
            lstScreens = new Dictionary<string, IScreen>();
        }

        public static ScreenManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new ScreenManager();
                return instance;
            }
        }

        //Если экрана с именем sScreenName ещё нет, добавлем его
        public void Add(string screenName, IScreen screen)
        {
            if (!Contains(screenName))
            {
                lstScreens.Add(screenName, screen);
            }
        }

        //Удаление экрана с именем sScreenNAme
        public void Del(string screenName)
        {
            if (Contains(screenName))
            {
                lstScreens.Remove(screenName);
            }
        }

        //Получение экрана с именем sScreenName 
        public IScreen Get(string screenName)
        {
            IScreen screen;
            lstScreens.TryGetValue(screenName, out screen);
            return screen;
        }

        //Если экран с именем sScreenName существует, вернуть true, иначе false
        public bool Contains(string screenName)
        {
            return lstScreens.ContainsKey(screenName);
        }

        //Устанвоить экран с именем sScreenName как текущий.
        public void SetScreen(string screenName)
        {
            if (Contains(screenName))
            {
                if(activeScreen != null) activeScreen.Shutdown(); //Если уже есть установленный экран, закрыть его
                lstScreens.TryGetValue(screenName, out activeScreen);
                activeScreen.Init();
            }
        }

        //Отрисовать текущий экран
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (activeScreen != null)
                activeScreen.Draw(gameTime, spriteBatch);
        }

        //Вызвать метод update у текущего экрана
        public void Update(GameTime gameTime)
        {
            if (activeScreen != null)
                activeScreen.Update(gameTime);
        }

        private static ScreenManager instance;

        //Коллекция экранов. Связывает объекты экрана с их именами
        private Dictionary<string, IScreen> lstScreens;

        //Текущий экран
        private IScreen activeScreen;

    }
}
