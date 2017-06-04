using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

/*
 * Класс изображения, обёртка над классом Texrture2D
 * */

namespace Match_3
{
    class Sprite
    {
        public Texture2D Texture { get; set; }
        public Vector2 Pos { get; set; } //Позиция спрайта на экране
        public float Angle { get; set; } //Угол поворота спрайта
        public Color Color { get; set; } //Цвет накладываемый на спрайт (по умолчанию белый, который никак не изменяет само изображение)
        public Vector2 Origin { get; set; } //Координаты центра изображения

        public Sprite(Texture2D texture)
        {
            Texture = texture;
            Pos = new Vector2(0, 0);
            Angle = 0.0f;
            Color = Color.White;
        }

        public Sprite(Texture2D texture, Vector2 pos)
        {
            Texture = texture;
            Pos = pos;
            Angle = 0.0f;
            Color = Color.White;
        }

        //Отрисовка всего изображения целиком
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(Texture, Pos, new Rectangle(0, 0, Texture.Width, Texture.Height), Color, Angle, Origin, 1, SpriteEffects.None, 1);
            spriteBatch.End();
        }

        //Отрисовка области изображения, ограниченной прямоугольником sourceRectangle
        public void Draw(SpriteBatch spriteBatch, Rectangle sourceRectangle)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(Texture, Pos, sourceRectangle, Color, Angle, Origin, 1, SpriteEffects.None, 1);
            spriteBatch.End();
        }
    }
}
