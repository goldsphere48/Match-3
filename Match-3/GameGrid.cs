using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Match_3
{
    class GameGrid
    {
        //Игровая сетка из предметов
        private Staff[,] grid;

        //Выбранные предметы
        private Staff[] selectedStaff = new Staff[2];

        //Векторы в которые запоминаются позиции двух выбранных предметов перед перестановкой их местами
        private Vector2[] oldPos = new Vector2[2];

        //Размеры игровой сетки
        private int width;
        private int height;

        //Если необходимо сдвинуть вниз верхнии предметы
        private bool isDroped = false;

        private bool isSwapedBack = false;

        //Размер текстуру одного предмета (ширина = длинна)
        private int staffSize;

        //Кол-во типов предметов
        private int staffTypeQuantity = 5;

        //Позиция на экране начиная с которой отрисовывется игровая сетка
        private Vector2 Pos;

        
        private Vector2[] posInGrid;

        //Кол-во выбранных предметов
        private int count = 0;

        //Скорость падения предметов
        private float moveSpeed = 3;

        //Массив текстур для каждого типа объектов
        private  Texture2D[] staffTextures;

        private Random rnd = new Random();

        //Очки
        public int Score { get; set; }

        public GameGrid(int w, int h, Vector2 pos)
        {
            width = w;
            height = h;
            Pos = pos;

            staffTextures = new Texture2D[staffTypeQuantity];

            //Подгружаем текстуру из Content Manager
            for (int i = 0; i < staffTypeQuantity; ++i)
                staffTextures[i] = GameContext.Instance.ContentManager.Load<Texture2D>("staff" + (i + 1));

            staffSize = staffTextures[0].Height;

            posInGrid = new Vector2[2];
            posInGrid[0] = new Vector2();
            posInGrid[1] = new Vector2();

            //Генерирвоатьт поле до тех пор пока не будет сгенерированно поле хотябы с 1 доступным ходом и без изначально собранных линий
            while (true)
            {
                SetUpGrid(w, h);

                if (LookForMatches().Count != 0)
                    continue;

                if (!LookForPossible())
                    continue;

                break;
            }
        }

        //Генерация сетки
        private void SetUpGrid(int w, int h)
        {

            grid = new Staff[w, h];

            for (int i = 0; i < w; ++i)
                for (int j = 0; j < h; ++j)
                {
                    int index = rnd.Next(0, staffTypeQuantity);
                    grid[i, j] = new Staff(staffTextures[index], 1, 4, new Vector2(Pos.X + i * 80, Pos.Y + j * 80), new Vector2(i, j));
                    grid[i, j].StaffType = (StaffType)index;
                }
                    
        }

        //Возвращает список предметов, находящихся в линиях, которые нужно удалить
        private List<Staff> LookForMatches()
        {
            List<Staff> matches = GetMatchHoriz();
            matches.AddRange(GetMatchVert());

            return matches;
        }

        //Возвращает true если доступен ходябы один ход, false иначе
        private bool LookForPossible()
        {
            return LookForHorizPossible() || LookForVertPossible();
        }

        //Возвращает true если доступен ходябы один ход по горизонтали, false иначе
        private bool LookForHorizPossible()
        {
            //кол-во подряд идущих предметов одного типа
            int count = 1;
            for (int i = 0; i < height; ++i)
            {
                for (int j = 0; j < width - 3; ++j)
                {
                    if (count == 1)
                    {
                        if (grid[j, i].StaffType == grid[j + 1, i].StaffType)
                            count++;
                    }
                    else //Если найдено 2 подряд идущих предметов одного типа, проверить предмет через один, есл он того же типа - ход найден 
                    {
                        if (grid[j, i].StaffType == grid[j + 2, i].StaffType)
                            return true;
                        else count = 1;
                    }
                }

                count = 1;
            }

            return false;
        }

        //Возвращает true если доступен ходябы один ход по вертикали, false иначе
        private bool LookForVertPossible()
        {
            //кол-во подряд идущих предметов одного типа
            int count = 1;
            for (int i = 0; i < width; ++i)
            {
                for (int j = 0; j < height - 3; ++j)
                {
                    if (count == 1)
                    {
                        if (grid[i, j].StaffType == grid[i, j + 1].StaffType)
                            count++;
                    }
                    else //Если найдено 2 подряд идущих предметов одного типа, проверить предмет через один, есл он того же типа - ход найден 
                    {
                        if (grid[i, j].StaffType == grid[i, j + 2].StaffType)
                            return true;
                        else count = 1;
                    }
                }

                count = 1;
            }

            return false;
        }

        //Возвращает список горизонтальных собранных линий
        private List<Staff> GetMatchHoriz()
        {
            List<Staff> matches = new List<Staff>();

            //Идём по рядам слева на право. Count считает кол-во подряд идущих предметов одинакого типа
            int count = 1;
            for (int i = 0; i < height; ++i)
            {
                int j;
                for (j = 0; j < width - 1; ++j)
                {
                    //Если тип следующего предмета равен типу текущего увлечиваем count
                    if ((grid[j, i].StaffType != StaffType.EMPTY) && (grid[j, i].StaffType == grid[j + 1, i].StaffType))
                        count++;
                    else //Если тип следующего предмета не равен типу текущего и count >= 3, то мы нашли линию под удаление. Заносим её элементы в список
                    {
                        if (count >= 3)
                        {
                            for (int k = j; k > j - count; --k)
                                matches.Add(grid[k, i]);
                        }

                        count = 1;
                    }
                }

                if (count >= 3)
                {
                    for (int k = j; k > j - count; --k)
                        matches.Add(grid[k, i]);
                }

                count = 1;
            }
            return matches;
        }

        //Возвращает список вертикальных собранных линий
        private List<Staff> GetMatchVert()
        {
            List<Staff> matches = new List<Staff>();

            int count = 1;
            for (int i = 0; i < width; ++i)
            {
                int j;
                for (j = 0; j < height - 1; ++j)
                {
                    if ((grid[i, j].StaffType != StaffType.EMPTY) && grid[i, j].StaffType == grid[i, j + 1].StaffType)
                        count++;
                    else
                    {
                        if (count >= 3)
                        {
                            for (int k = j; k > j - count; --k)
                                matches.Add(grid[i, k]);
                        }

                        count = 1;
                    }
                }

                if (count >= 3)
                {
                    for (int k = j; k > j - count; --k)
                        matches.Add(grid[i, k]);
                }

                count = 1;
            }
            return matches;
        }

        //Функция выбора предмета
        private void SelectStaff()
        {
            for (int i = 0; i < width; ++i)
            {
                for (int j = 0; j < height; ++j)
                {
                    //Если выбранных предметов строго меньше 2, выбираем новый на который кликнули и запускаем анимацию
                    if (grid[i, j].isClickOn(Mouse.GetState().LeftButton) && count < 2)
                    {
                        selectedStaff[count] = grid[i, j];
                        selectedStaff[count].PlayAnim();
                        oldPos[count] = selectedStaff[count].Pos;
                        posInGrid[count] = new Vector2();
                        posInGrid[count].X = i;
                        posInGrid[count].Y = j;
                        count++;
                    }
                }
            }
        }

        //Подсчёт расстояние между выбранными предметами
        private double GetDistanceBetweenSelectedStaffs()
        {
            return Math.Sqrt((selectedStaff[0].Pos.X - selectedStaff[1].Pos.X) * (selectedStaff[0].Pos.X - selectedStaff[1].Pos.X) +
                                            (selectedStaff[0].Pos.Y - selectedStaff[1].Pos.Y) * (selectedStaff[0].Pos.Y - selectedStaff[1].Pos.Y));
        }

        //Снять выделение с выбраных предметов
        private void UnselectStaffs()
        {
            selectedStaff[0].Stop();
            selectedStaff[1].Stop();
            count = 0;
        }

        //Поменять местами выбранные предметы
        private void SwapSelectedStaffs()
        {
            double distance = GetDistanceBetweenSelectedStaffs();

            //Если выбранные предметы - соседи по горизонтали или вертикали
            if (distance <= staffSize)
            {
                //Установить позицию для движения каждого, так чтобы первый выделенный предмет встал на место второго и наоборот
                selectedStaff[0].MoveDestination = oldPos[1];
                selectedStaff[1].MoveDestination = oldPos[0];

                bool b1 = selectedStaff[0].isOnNewPosition();
                bool b2 = selectedStaff[1].isOnNewPosition();
                if (b1 && b2) //Если они встали на новые места
                {
                    //Поменять их местами в игровой сетке
                    grid[(int)posInGrid[0].X, (int)posInGrid[0].Y] = selectedStaff[1];
                    grid[(int)posInGrid[1].X, (int)posInGrid[1].Y] = selectedStaff[0];

                    grid[(int)posInGrid[0].X, (int)posInGrid[0].Y].PosInGrid = posInGrid[0];
                    grid[(int)posInGrid[1].X, (int)posInGrid[1].Y].PosInGrid = posInGrid[1];

                    //Проверяем появились ли собранные линии после перестановки
                    //Если не удаётся найти линии
                    bool matchesWasFound = FindAndRemovemathces();
                    if (!isSwapedBack && !matchesWasFound)
                    {
                        //Меняем их местами обратно
                        Staff tmp = selectedStaff[0];
                        selectedStaff[0] = selectedStaff[1];
                        selectedStaff[1] = tmp;
                        isSwapedBack = true;
                    }
                    else
                    {
                        //Если линии найдены дать сигнал запуска функции падения предметов и снять выделения с выбранных
                        if (matchesWasFound) 
                            isDroped = true;
                        UnselectStaffs();
                        if (isSwapedBack)
                            isSwapedBack = false;
                    }
                }
            }
            else //если предметы не соседние - снять выделения
            {
                UnselectStaffs();
            }
        }

        //Поиск и удаление линий
        private bool FindAndRemovemathces()
        {
            //получаем список предметов на удаление
            List<Staff> matches = LookForMatches();

            //если он пуст - выходим
            if (matches.Count == 0)
                return false;

            //Удаляем предмета путём присваивания их типам типа EMPTY и увеличиваем кол-во очков за каждый удалённый
            for (int i = 0; i < matches.Count; ++i)
            {
                matches[i].StaffType = StaffType.EMPTY;
                Score++;
            }

            //Все предмета выше удалённых снижаем на 1 позицию вниз, таким образом, что удалённые предмета поднимаются на самый верх
            for (int i = 0; i < width; ++i)
                for (int j = 0; j < height - 1; ++j)
                    if (grid[i, j + 1].StaffType == StaffType.EMPTY)
                    {
                        for (int k = j; k >= 0; --k)
                        {
                            //если ниже находится удалённый предмет меняемся с ним местами
                            grid[i, k].MoveDestination = new Vector2(grid[i, k].MoveDestination.X, grid[i, k].MoveDestination.Y + staffSize);
                            Staff tmp = grid[i, k];
                            grid[i, k] = grid[i, k + 1];
                            grid[i, k + 1] = tmp;

                            //Позиции в сетке должны сохранятся, поэтому меняем их обратно
                            Vector2 PosInGrid = new Vector2(grid[i, k].PosInGrid.X, grid[i, k].PosInGrid.Y);
                            grid[i, k].PosInGrid = grid[i, k + 1].PosInGrid;
                            grid[i, k + 1].PosInGrid = PosInGrid;

                            //Указываем новую позицию, чтобы предметы "упали" на неё
                            grid[i, k].MoveDestination = new Vector2(grid[i, k].PosInGrid.X * staffSize + Pos.X, grid[i, k].PosInGrid.Y * staffSize + Pos.Y);
                            grid[i, k+1].MoveDestination = new Vector2(grid[i, k + 1].PosInGrid.X * staffSize + Pos.X, grid[i, k + 1].PosInGrid.Y * staffSize + Pos.Y);
                        }
                    }

                    return true;
        }

        // Функция повторной генерации удалённых предметов
        private void DropStaffs()
        {
            List<Staff> lst = new List<Staff>();
            //Получаем список уже удалённых предметов
            for (int j = 0; j < height; ++j)
            {
                int i = 0;
                for (; i < width; ++i)
                {
                    if (grid[i, j].StaffType == StaffType.EMPTY)
                    {
                        lst.Add(grid[i, j]);
                    }
                }
            }

            //Находим y координату самого нижнего предмета

            int maxY = 0;

            foreach(Staff s in lst)
            {
                if (s.Pos.Y > maxY)
                    maxY = (int)s.Pos.Y;
            }

            //Вычисляем новые позиции и типы предметов

            foreach (Staff s in lst)
            {
                s.Pos = new Vector2(s.Pos.X, s.Pos.Y - maxY);
                s.Pos = new Vector2(s.Pos.X, -(s.Pos.Y + maxY) + Pos.Y - staffSize);
                int index = rnd.Next(staffTypeQuantity);
                s.StaffType = (StaffType)index;
                s.animSprite.Texture = staffTextures[(int)s.StaffType];
            }

            //Делаем так, чтобы нижний элемент занимал самое нижнее положени над сеткой

            maxY = -10000;

            foreach (Staff s in lst)
            {
                if (s.Pos.Y > maxY)
                    maxY = (int)s.Pos.Y;
            }

            foreach (Staff s in lst)
            {
                s.Pos = new Vector2(s.Pos.X, s.Pos.Y - maxY);
            }
        }

        //Основной игровой икл
        public void Update()
        {
            //Проверяем нет ли падающих объектов
            bool allStaffsFaled = true;
            for (int i = 0; i < width; ++i)
                for (int j = 0; j < height; ++j)
                { 
                    grid[i, j].Update(moveSpeed*2*1.8f);
                    allStaffsFaled = allStaffsFaled && grid[i, j].isOnNewPosition();
                }

            //если нет
            if (allStaffsFaled) 
            {
                //проверяем на наличие линии, если они есть, "роняем" новые предметы
                if(FindAndRemovemathces())
                    isDroped = true;

                //Проверяем нажатие на предметы
                SelectStaff();

                //если выбрано 2 предмета - пытаемся поменять их местами
                if (count == 2)
                {
                    SwapSelectedStaffs();
                }
            }

            if (isDroped)
            {
                DropStaffs();
                isDroped = false;
            }
        }

        //Отрисовка игрового поля
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < width; ++i)
                for (int j = 0; j < height; ++j)
                    grid[i, j].Draw(spriteBatch);
        }


    }
}
