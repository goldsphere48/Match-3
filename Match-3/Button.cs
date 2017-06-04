using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

/*
 *Класс реализует работу кнопки 
 */

namespace Match_3
{
    class Button
    {
        // Спрайт кнопки
        public Sprite Sprite { get; set; }

        //Нажата ли была кнопка
        private bool pressed = false;

        public Button(Texture2D texture)
        {
            Sprite = new Sprite(texture);
        }

        public Button(Texture2D texture, Vector2 pos)
        {
            Sprite = new Sprite(texture);
            Sprite.Pos = pos;
        }

        //Проверка нажата ли кнопка передаваемая через параметр
        public bool IsClickedOnButton(ButtonState buttonState)
        {
            //Обнуление после нажатия, чтобы при одном клике событие не срабатывало больше одного раза
            if (buttonState == ButtonState.Released)
                pressed = false;

            if (buttonState == ButtonState.Pressed && !pressed)
            {
                Rectangle m = new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 1, 1);
                Rectangle rect = new Rectangle((int)(Sprite.Pos.X - Sprite.Origin.X), (int)(Sprite.Pos.Y - Sprite.Origin.Y), Sprite.Texture.Width, Sprite.Texture.Height);
                pressed = true;
                return rect.Intersects(m);
            }
            else
                return false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch);
        }

        public Vector2 Pos
        {
            get
            {
                return Sprite.Pos;
            }

            set
            {
                Sprite.Pos = value;
            }
        }
    }
}
