using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Zack
{
    public class GluttonousSnake
    {
        /// <summary>
        /// 头部位置
        /// </summary>
        Coordinate head;
        /// <summary>
        /// 记录最后一次消失的位置
        /// </summary>
        Coordinate LastPosition = new Coordinate();
        int existingAwards = 0;
        /// <summary>
        /// 蛇身长度
        /// </summary>
        int Length = 0;
        /// <summary>
        /// 地图高度
        /// </summary>
        public int Width = 30;
        /// <summary>
        /// 地图宽度
        /// </summary>
        public int Height = 30;
        /// <summary>
        /// 地图信息
        /// </summary>
        int[,] map;
        /// <summary>
        /// 前进方向，0向上, 1向下, 2向左, 3向右
        /// </summary>
        public int Direction = 0;
        /// <summary>
        /// 转向缓冲
        /// </summary>
        int tempDirection = 0;
        /// <summary>
        /// 是否暂停
        /// </summary>
        public bool isPause = false;
        /// <summary>
        /// 是否存活
        /// </summary>
        public bool isAlive = true;
        /// <summary>
        /// 是否正在转向
        /// </summary>
        public bool isTurning = false;
        /// <summary>
        /// 开始页
        /// </summary>
        public void StartPage()
        {
            Console.Clear();
            var Gluttonous = new StringBuilder();
            Gluttonous.AppendLine("   ____ _       _   _                              ");
            Gluttonous.AppendLine("  / ___| |_   _| |_| |_ ___  _ __   ___  _   _ ___ ");
            Gluttonous.AppendLine(" | |  _| | | | | __| __/ _ \\| '_ \\ / _ \\| | | / __|");
            Gluttonous.AppendLine(" | |_| | | |_| | |_| || (_) | | | | (_) | |_| \\__ \\");
            Gluttonous.AppendLine("  \\____|_|\\__,_|\\__|\\__\\___/|_| |_|\\___/ \\__,_|___/");

            var Snake = new StringBuilder();
            Snake.AppendLine("  ____              _        ");
            Snake.AppendLine(" / ___| _ __   __ _| | _____ ");
            Snake.AppendLine(" \\___ \\| '_ \\ / _` | |/ / _ \\");
            Snake.AppendLine("  ___) | | | | (_| |   <  __/");
            Snake.AppendLine(" |____/|_| |_|\\__,_|_|\\_\\___|   ");
            Console.WriteLine(Gluttonous.ToString());
            Console.WriteLine(Snake.ToString());
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("Press any key to start.");
            Console.ReadKey();
        }
        /// <summary>
        /// 初始化游戏
        /// </summary>
        public void Initialize()
        {
            Length = 4;
            Console.Title = "Score:" + (Length - 4);
            map = new int[Width, Height];
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++) { map[x, y] = 0; }
            }
            var a = Width / 2;
            var b = Height / 2;

            head = new Coordinate();
            head.x = a;
            head.y = b;

            for (int i = Length; i != 0; i--) { map[a - (i - Length), b] = i; }
            GenerateBonus();
            GenerateBonus();
        }
        /// <summary>
        /// FixedUpdate
        /// </summary>
        public void FixedUpdate()
        {
            while (isAlive)
            {
                if (isPause == false)
                {
                    if(tempDirection!= Direction)
                    {
                        tempDirection = Direction;
                        isTurning = true;
                    }
                    else { isTurning = false; }

                    Thread.Sleep(150);
                    Console.Title = "Score:" + (Length - 4);
                    MapRenderer();
                    Move();
                    
                }
            }
            GameOver();
        }
        /// <summary>
        /// 地图渲染
        /// </summary>
        void MapRenderer()
        {
            Console.Clear();
            for (int x = 0; x < Width; x++)
            {
                var sb = new StringBuilder();
                for (int y = 0; y < Height; y++)
                {
                    if (map[x, y] == -1) { sb.Append("OO"); }
                    else if (map[x, y] != 0) { sb.Append("00"); }
                    else { sb.Append("  "); }
                }
                Console.WriteLine(sb);
            }
        }
        /// <summary>
        /// 移动
        /// </summary>
        void Move()
        {
            switch (Direction)
            {
                case 0:
                    Upward();
                    break;
                case 1:
                    Downward();
                    break;
                case 2:
                    Leftward();
                    break;
                case 3:
                    Rightward();
                    break;
            }
        }
        /// <summary>
        /// 游戏结束页面
        /// </summary>
        void GameOver()
        {
            Console.Clear();
            var text = new StringBuilder();
            text.AppendLine("   _____          __  __ ______    ______      ________ _____  ");
            text.AppendLine("  / ____|   /\\   |  \\/  |  ____|  / __ \\ \\    / /  ____|  __ \\ ");
            text.AppendLine(" | |  __   /  \\  | \\  / | |__    | |  | \\ \\  / /| |__  | |__) |");
            text.AppendLine(" | | |_ | / /\\ \\ | |\\/| |  __|   | |  | |\\ \\/ / |  __| |  _  / ");
            text.AppendLine(" | |__| |/ ____ \\| |  | | |____  | |__| | \\  /  | |____| | \\ \\ ");
            text.AppendLine("  \\_____/_/    \\_\\_|  |_|______|  \\____/   \\/   |______|_|  \\_\\");
            text.AppendLine("-----------------------------------------------------------------");
            text.AppendLine();
            if(Length - 4 < 10)
            {
                text.AppendLine("-----------------         You got " + (Length - 4) + " score.      -----------------");
            }else if(Length - 4 < 100)
            {
                text.AppendLine("-----------------        You got " + (Length - 4) + " score.      -----------------");
            }else if(Length - 4  < 1000)
            {
                text.AppendLine("-----------------        You got " + (Length - 4) + " score.     -----------------");
            }else if(Length < 10000)
            {
                text.AppendLine("-----------------       You got " + (Length - 4) + " score.     -----------------");
            }
            text.AppendLine("-----------------    Press any key to resume.    ----------------");
            Console.WriteLine(text.ToString());
        }
        /// <summary>
        /// 获得奖励
        /// </summary>
        void GetBonus()
        {
            existingAwards--;
            Length++;
            map[LastPosition.x, LastPosition.y]++;
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    if (map[x,y] >= 1) { map[x, y]++; }
                }
            }
            GenerateBonus();
        }
        /// <summary>
        /// 生成奖励
        /// </summary>
        void GenerateBonus()
        {
            Random rd= new Random();
            while(existingAwards != 2)
            {
                int x = rd.Next(0,Width - 1);
                int y = rd.Next(0,Height - 1);
                if (map[x,y] !>= 0)
                {
                    map[x, y] = -1;
                    existingAwards++;
                }
            }
        }
        /// <summary>
        /// 向上
        /// </summary>
        void Upward()
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    if (map[x, y] == 1)
                    {
                        LastPosition.x = x; 
                        LastPosition.y = y;
                    }
                    if (map[x, y] > 0) { map[x, y]--; }
                }
            }

            if(head.y == 0) { head.y = Height - 1; }//触碰到地图边界
            else { head.y -= 1; }

            if (map[head.y, head.x] == -1) //前一格是否是奖励分
            {
                map[head.y, head.x] = Length;
                GetBonus();
            }
            else if (map[head.y, head.x] >= 1) { isAlive = false; }//前一个格子是否是蛇身
            else { map[head.y, head.x] = Length; }
        }
        /// <summary>
        /// 向下
        /// </summary>
        void Downward()
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    if (map[x, y] == 1)
                    {
                        LastPosition.x = x; LastPosition.y = y;
                    }
                    if (map[x, y] > 0) { map[x, y]--; }
                }
            }

            if (head.y == Height - 1) { head.y = 0; }//触碰到地图边界
            else { head.y += 1; }

            if (map[head.y, head.x] == -1) //前一格是否是奖励分
            {
                map[head.y, head.x] = Length;
                GetBonus();
            }
            else if (map[head.y, head.x] >= 1) { isAlive = false; }//前一个格子是否是蛇身
            else { map[head.y, head.x] = Length; }
        }
        /// <summary>
        /// 向左
        /// </summary>
        void Leftward()
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    if (map[x, y] == 1) { LastPosition.x = x; LastPosition.y = y; }
                    if (map[x, y] > 0) { map[x, y]--; }
                }
            }

            if (head.x == 0) { head.x = Width - 1; }//触碰到地图边界
            else { head.x -= 1; }

            if (map[head.y, head.x] == -1) //前一格是否是奖励分
            {
                map[head.y, head.x] = Length;
                GetBonus();
            }
            else if(map[head.y, head.x] >= 1) { isAlive = false; }//前一个格子是否是蛇身
            else { map[head.y, head.x] = Length; }
        }
        /// <summary>
        /// 向右
        /// </summary>
        void Rightward()
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    if (map[x, y] == 1) { LastPosition.x = x; LastPosition.y = y; }
                    if (map[x, y] > 0) { map[x, y]--; }
                }
            }

            if (head.x == Width - 1) { head.x = 0; }//触碰到地图边界
            else { head.x += 1; }

            if (map[head.y, head.x] == -1) //前一格是否是奖励分
            {
                GetBonus();
                map[head.y, head.x] = Length;
            }
            else if (map[head.y, head.x] >= 1) { isAlive = false; }//前一个格子是否是蛇身
            else { map[head.y, head.x] = Length; }
        }
        /// <summary>
        /// 暂停游戏
        /// </summary>
        public void Pause() => isPause = isPause == false ? true : false;
        /// <summary>
        /// 自定义的坐标类
        /// </summary>
        class Coordinate
        {
            public int x, y;
        }
    }
}
