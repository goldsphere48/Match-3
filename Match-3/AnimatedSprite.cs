using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

/*
 * Класс для работы с анимированными изображения
 */

namespace Match_3
{
    class AnimatedSprite : Sprite
    {
        public int Rows { get; set; } //Кол-во рядов с кадрами
        public int Columns { get; set; } //Кол-во колонок с кадрами
        private float currentFrame; //Текущий отрисовываемый кадр
        private int totalFrames; //Общее кол-во кадров
        private float animSpeed;

        private bool isAnimationPlayed;

        public AnimatedSprite(Texture2D texture, int rows, int columns, float animSpeed) : base(texture)
        {
            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            totalFrames = rows * columns;
            isAnimationPlayed = true;
            this.animSpeed = animSpeed;
        }

        public AnimatedSprite(Texture2D texture, Vector2 pos, int rows, int columns, float animSpeed) : base(texture, pos)
        {
            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            totalFrames = rows * columns;
            isAnimationPlayed = true;
            this.animSpeed = animSpeed;
        }

        //Остановка анимации
        public void Stop()
        {
            isAnimationPlayed = false;
        }

        //Возобновление анимации
        public void Resume()
        {
            isAnimationPlayed = true;
        }

        //Установка первого кадра как текущего
        public void BackToFirstfame()
        {
            currentFrame = 0;
        }

        //Вычисление отрисовываемого кадра
        private void Update()
        {
            if (isAnimationPlayed)
            {
                currentFrame+=animSpeed;
                if (currentFrame >= totalFrames)
                    currentFrame = 0;
            }
        }

        //Отрисовка текущего кадра
        public override void Draw(SpriteBatch spriteBatch)
        {
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            int row = (int)((float)currentFrame / (float)Columns);
            int column = (int)currentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(width*column, height*row, width, height);

            Draw(spriteBatch, sourceRectangle);

            Update();
        }

    }
}
