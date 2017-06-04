using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

/*
 * Класс описывающий предмет (часы) из игровой сетки
 */

namespace Match_3
{
    enum StaffType{ BROWN, BLUE, PURPLE, GREEN, GOLD, EMPTY};

    class Staff
    {
        //Спрайт
        public AnimatedSprite animSprite { get; set; }

        //true если на предмет нажали, false иначе
        private bool pressed = true;

        //Скорость анимации
        public float AnimSpeed { get; set; }

        //Тип предмета 
        public StaffType StaffType { get; set; }

        //Вектор для харанения индексов предмета в массиве grid[i, j].PosInGrid = Vector2(i, j)
        public Vector2 PosInGrid { get; set; }

        //Позиция к которой смещается предмет
        public Vector2 MoveDestination { get; set; }

        public Staff(Texture2D texture, int rows, int columns, Vector2 pos, Vector2 posInGrid)
        {
            PosInGrid = posInGrid;
            MoveDestination = pos;
            AnimSpeed = 0.1f;
            animSprite = new AnimatedSprite(texture, pos, rows, columns, AnimSpeed);
            animSprite.Stop();
        }

        public bool isClickOn(ButtonState buttonState)
        {
            //Обнуление после нажатия, чтобы при одном клике событие не срабатывало больше одного раза
            if (buttonState == ButtonState.Released)
                pressed = false;

            if (buttonState == ButtonState.Pressed && !pressed)
            {
                Rectangle m = new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 1, 1);
                Rectangle rect = new Rectangle((int)(animSprite.Pos.X - animSprite.Origin.X), 
                                                (int)(animSprite.Pos.Y - animSprite.Origin.Y), animSprite.Texture.Width/animSprite.Columns, animSprite.Texture.Height/animSprite.Rows);
                pressed = true;
                return rect.Intersects(m);
            }
            else
                return false;
        }

        //Перестать проигрывать анимацию и вернуться к 1 кадру
        public void Stop()
        {
            animSprite.Stop();
            animSprite.BackToFirstfame();
        }

        //Начать проигрывать анимацию
        public void PlayAnim()
        {
            animSprite.Resume();
        }

        //Двигаться к позиции MoveDestination со скоростью Speed
        public bool Move(float Speed)
        {
            if (MoveDestination.X < animSprite.Pos.X)
                animSprite.Pos = new Vector2(animSprite.Pos.X - Speed, animSprite.Pos.Y);
            if (MoveDestination.X > animSprite.Pos.X)
                animSprite.Pos = new Vector2(animSprite.Pos.X + Speed, animSprite.Pos.Y);

            if (MoveDestination.Y < animSprite.Pos.Y)
                animSprite.Pos = new Vector2(animSprite.Pos.X, animSprite.Pos.Y - Speed);
            if (MoveDestination.Y > animSprite.Pos.Y)
                animSprite.Pos = new Vector2(animSprite.Pos.X, animSprite.Pos.Y + Speed);

            if ((Math.Abs(Pos.X - MoveDestination.X) <= Speed) && (Math.Abs(Pos.Y - MoveDestination.Y) <= Speed))
            {
                Pos = MoveDestination;
                return true;
            }
            else
                return false;
        }

        //Если предмет никуда не движется
        public bool isOnNewPosition()
        {
            return Pos == MoveDestination;
        }

        public void Update(float Speed)
        {
            Move(Speed);
        }

        public void Draw(SpriteBatch spriteBacth)
        {
            if(StaffType != StaffType.EMPTY) animSprite.Draw(spriteBacth);
        }

        //Доступ к позиции предмета
        public Vector2 Pos
        {
            get
            {
                return animSprite.Pos;
            }

            set
            {
                animSprite.Pos = value;
            }
        }
    }
}
