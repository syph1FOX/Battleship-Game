using kursach2WinForms.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace kursach2WinForms
{
    public partial class FormGame : Form
    {
        public static readonly string recPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\records.txt";
        const int mapSize = 10;
        const int cellSize = 30;
        const string alphabet = "АБВГДЕЖЗИК";
        static int[,] p1Map = new int[mapSize, mapSize];
        static int[,] p2Map;
        int[] ships = new int[4] {4,3,2,1};
        int fShips = 20, eShips = 20, shots = 0, prevX, prevY, tempX, tempY, hp = -1, prevDir = 0;
        bool prevHit = false;
        bool[] shipClick = new bool[4] { false, false, false, false };
        internal static bool down = true, gameStart = false;
        Button ship1, ship2, ship3, ship4;
        Label mod, score, turn;

        public FormGame()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            this.Width = mapSize * 2 * cellSize + cellSize * 5;
            this.Height = mapSize * 2 * cellSize;
            CreateMap();
        }

        private void CreateMap()
        {
            p2Map = SetShips();
            for(int i = 0; i < mapSize; i++)
                for(int j = 0; j < mapSize; j++)
                {
                    Button button = new Button();
                    button.Location = new Point(j * cellSize, i * cellSize);
                    button.Size = new Size(cellSize, cellSize);
                    button.Cursor = Cursors.Hand;
                    button.Click += Button_Click;
                    button.KeyDown += Button_KeyDown; 
                    button.Name = i.ToString() + j.ToString();
                    button.BackColor = Color.White;
                    if(i == 0 || j == 0)
                    {
                        p1Map[i, j] = -1;
                        button.BackColor = Color.Gray;
                        if(j == 0 && i > 0)
                            button.Text = i.ToString();
                        if (i == 0 && j > 0)
                            button.Text = alphabet[j - 1].ToString();
                        
                    }
                    Controls.Add(button);
                }
            for (int i = 0; i < mapSize; i++)
                for (int j = 0; j < mapSize; j++)
                {
                    Button button = new Button();
                    button.Location = new Point(350 + j * cellSize, i * cellSize);
                    button.Size = new Size(cellSize, cellSize);
                    button.Cursor = Cursors.Hand;
                    button.Name = "x" + i.ToString() + j.ToString();
                    button.BackColor = Color.WhiteSmoke;
                    button.Click += ButtonEnemy_Click;
                    if (i == 0 || j == 0)
                    {
                        button.BackColor = Color.Gray;
                        if (j == 0 && i > 0)
                            button.Text = i.ToString();
                        if (i == 0 && j > 0)
                            button.Text = alphabet[j - 1].ToString();

                    }
                    /*else if (p2Map[i, j] >= 1)
                    {
                        button.BackColor = Color.Blue;
                        button.Text = p2Map[i, j].ToString();
                    }*/
                    Controls.Add(button);
                }
            
            ship1 = new Button();
            ship1.Name = "ship1";
            ship1.Location = new Point(mapSize * cellSize / 2 - 150, mapSize * cellSize + 100);
            ship1.Text = "1";
            ship1.Size = new Size(40, 30);
            ship1.ForeColor = Color.Red;
            ship1.BackgroundImage = Image.FromFile(Directory.GetCurrentDirectory() + "\\1p_ship.jpg");
            ship1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            ship1.Cursor = Cursors.Hand;
            ship1.Click += Ship1_Click;

            ship2 = new Button();
            ship2.Location = new Point(mapSize * cellSize / 2 - 100, mapSize * cellSize + 100);
            ship2.Name = "ship2";
            ship2.Text = "2";
            ship2.Size = new Size(50, 30);
            ship2.ForeColor = Color.Red;
            ship2.BackgroundImage = Image.FromFile(Directory.GetCurrentDirectory() + "\\2p_ship.jpg");
            ship2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            ship2.Cursor = Cursors.Hand;
            ship2.Click += Ship2_Click;

            ship3 = new Button();
            ship3.Location = new Point(mapSize * cellSize / 2 - 30, mapSize * cellSize + 100);
            ship3.Name = "ship3";
            ship3.Text = "3";
            ship3.Size = new Size(70, 35);
            ship3.ForeColor = Color.Red;
            ship3.BackgroundImage = Image.FromFile(Directory.GetCurrentDirectory() + "\\3p_ship.jpg");
            ship3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            ship3.Cursor = Cursors.Hand;
            ship3.Click += Ship3_Click;

            ship4 = new Button();
            ship4.Location = new Point(mapSize * cellSize / 2 + 50, mapSize * cellSize + 100);
            ship4.Name = "ship4";
            ship4.Text = "4";
            ship4.Size = new Size(100, 50);
            ship4.ForeColor = Color.Red;
            ship4.BackgroundImage = Image.FromFile(Directory.GetCurrentDirectory() + "\\4p_ship.jpg");
            ship4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            ship4.Cursor = Cursors.Hand;
            ship4.Click += Ship4_Click;

            Label map1 = new Label();
            map1.Location = new Point(mapSize * cellSize / 2, mapSize * cellSize + 10);
            map1.BackColor = Color.Transparent;
            map1.ForeColor = Color.Red;
            map1.Text = "Игрок1";

            Label map2 = new Label();
            map2.Location = new Point(350 + (mapSize * cellSize) / 2, mapSize * cellSize + 10);
            map2.BackColor = Color.Transparent;
            map2.ForeColor = Color.Red;
            map2.Text = "Игрок2";

            mod = new Label();
            mod.Location = new Point(150 + (mapSize * cellSize) / 2, mapSize * cellSize - 30 );
            mod.BackColor = Color.Transparent;
            mod.ForeColor = Color.LimeGreen;
            mod.Font = new Font("TimesNewRoman", 9);
            mod.AutoSize = true;
            mod.Name = "mod";
            mod.Text = "Сверху\nвниз";
            mod.Click += Mod_Click;

            turn = new Label();
            turn.Location = new Point(150 + (mapSize * cellSize) / 2, mapSize * cellSize + 10);
            turn.BackColor = Color.Transparent;
            turn.Font = new Font("TimesNewRoman", 9);
            turn.AutoSize = true;
            turn.Name = "turn";

            score = new Label();
            score.Location = new Point(155 + (mapSize * cellSize) / 2, 30);
            score.BackColor = Color.Transparent;
            score.ForeColor = Color.DarkOrange;
            score.Font = new Font("TimesNewRoman", 18, FontStyle.Bold);
            score.Name = "score";
            score.Text = eShips.ToString();

            Controls.Add(ship1);
            Controls.Add(ship2);
            Controls.Add(ship3);
            Controls.Add(ship4);
            Controls.Add(map1);
            Controls.Add(map2);
            Controls.Add(turn);
            Controls.Add(mod);
            Controls.Add(score);
        }

        private void Button_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.R)
            {
                down = !down;
                if (down)
                    mod.Text = "Сверху\nвниз";
                else
                    mod.Text = "Справа\nналево";
            }
        }

        private void Mod_Click(object sender, EventArgs e)
        {
            down = !down;
            if (down)
                mod.Text = "Сверху\nвниз";
            else
                mod.Text = "Справа\nналево";
        }


        private void Button_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            if (b.Location.X == 0 || b.Location.Y == 0)
            {
                for(int i = 0;i < shipClick.Length;i++)
                {
                    shipClick[i] = false;
                }
                return;
            }
            Button Cell2, Cell3, Cell4;
            int cords = 0;
            if(shipClick.Any(x => x))
            {
                if (shipClick[0])
                {
                    if (CanSet(p1Map, b.Location.X / cellSize, b.Location.Y / cellSize, 1, -1))
                    {
                        b.BackColor = Color.Red;
                        b.Text = "1";
                        b.ForeColor = Color.Blue;
                        p1Map[b.Location.Y / cellSize, b.Location.X / cellSize] = 1;
                        ships[0]--;
                        shipClick[0] = false;
                        if (ships[0] < 1)
                            Controls.Remove(ship1);
                    }
                }
                else if (shipClick[1])
                {
                    if (CanSet(p1Map, b.Location.X / cellSize, b.Location.Y / cellSize, 2, down ? 2 : 1))
                    {
                        switch (down)
                        {
                            case true:
                                cords = int.Parse(b.Name);
                                if (cords > 89)
                                    return;
                                cords += 10;
                                Cell2 = (Button)Controls[(string)cords.ToString()];
                                break;
                            case false:
                                cords = int.Parse(b.Name);
                                if (cords > 99)
                                    goto case default;
                                if (cords % 10 > 8)
                                    return;
                                cords += 1;
                                Cell2 = (Button)Controls[(string)cords.ToString()];
                                break;
                            default:
                                return;
                        }
                        b.BackColor = Color.Red;
                        b.Text = "2";
                        b.ForeColor = Color.Blue;
                        p1Map[b.Location.Y / cellSize, b.Location.X / cellSize] = 1;
                        ships[1]--;
                        shipClick[1] = false;
                        if (ships[1] < 1)
                            Controls.Remove(ship2);
                        Cell2.BackColor = Color.Red;
                        Cell2.Text = "2";
                        p1Map[Cell2.Location.Y / cellSize, Cell2.Location.X / cellSize] = 1;
                        
                    }
                    else
                    {
                        shipClick[1] = false;
                    }
                }
                else if (shipClick[2])
                { 
                    if (CanSet(p1Map, b.Location.X / cellSize, b.Location.Y / cellSize, 3, down ? 2 : 1))
                    {
                        switch (down)
                        {
                            case true:
                                cords = int.Parse(b.Name);
                                if (cords > 89)
                                    return;
                                cords += 10;
                                Cell2 = (Button)Controls[(string)cords.ToString()];
                                if (cords > 89)
                                    return;
                                cords += 10;
                                Cell3 = (Button)Controls[(string)cords.ToString()];
                                break;
                            case false:
                                cords = int.Parse(b.Name);
                                if (cords > 99)
                                    goto case default;
                                if (cords % 10 > 8)
                                    return;
                                cords += 1;
                                Cell2 = (Button)Controls[(string)cords.ToString()];
                                if (cords % 10 > 8)
                                    return;
                                cords += 1;
                                Cell3 = (Button)Controls[(string)cords.ToString()];
                                break;
                            default:
                                return;
                        }
                        b.BackColor = Color.Red;
                        b.Text = "3";
                        b.ForeColor = Color.Blue;
                        p1Map[b.Location.Y / cellSize, b.Location.X / cellSize] = 1;
                        ships[2]--;
                        shipClick[2] = false;
                        if (ships[2] < 1)
                            Controls.Remove(ship3);
                        Cell2.BackColor = Color.Red;
                        Cell2.Text = "3";
                        p1Map[Cell2.Location.Y / cellSize, Cell2.Location.X / cellSize] = 1;
                        Cell3.BackColor = Color.Red;
                        Cell3.Text = "3";
                        p1Map[Cell3.Location.Y / cellSize, Cell3.Location.X / cellSize] = 1;

                    }
                    else
                    {
                        shipClick[2] = false;
                    }
                }
                else if (shipClick[3])
                {
                    if (CanSet(p1Map, b.Location.X / cellSize, b.Location.Y / cellSize, 4, down ? 2 : 1))
                    {
                        switch (down)
                        {
                            case true:
                                cords = int.Parse(b.Name);
                                if (cords > 89)
                                    return;
                                cords += 10;
                                Cell2 = (Button)Controls[(string)cords.ToString()];
                                if (cords > 89)
                                    return;
                                cords += 10;
                                Cell3 = (Button)Controls[(string)cords.ToString()];
                                if (cords > 89)
                                    return;
                                cords += 10;
                                Cell4 = (Button)Controls[(string)cords.ToString()];
                                break;
                            case false:
                                cords = int.Parse(b.Name);
                                if (cords > 99)
                                    goto case default;
                                if (cords % 10 > 8)
                                    return;
                                cords += 1;
                                Cell2 = (Button)Controls[(string)cords.ToString()];
                                if (cords % 10 > 8)
                                    return;
                                cords += 1;
                                Cell3 = (Button)Controls[(string)cords.ToString()];
                                if (cords % 10 > 8)
                                    return;
                                cords += 1;
                                Cell4 = (Button)Controls[(string)cords.ToString()];
                                break;
                            default:
                                return;
                        }
                        b.BackColor = Color.Red;
                        b.Text = "4";
                        b.ForeColor = Color.Blue;
                        p1Map[b.Location.Y / cellSize, b.Location.X / cellSize] = 1;
                        ships[3]--;
                        shipClick[3] = false;
                        if (ships[3] < 1)
                            Controls.Remove(ship4);
                        Cell2.BackColor = Color.Red;
                        Cell2.Text = "4";
                        p1Map[Cell2.Location.Y / cellSize, Cell2.Location.X / cellSize] = 1;
                        Cell3.BackColor = Color.Red;
                        Cell3.Text = "4";
                        p1Map[Cell3.Location.Y / cellSize, Cell3.Location.X / cellSize] = 1;
                        Cell4.BackColor = Color.Red;
                        Cell4.Text = "4";
                        p1Map[Cell4.Location.Y / cellSize, Cell4.Location.X / cellSize] = 1;

                    }
                    else
                    {
                        shipClick[3] = false;
                    }
                }
            }
            if(ships.Sum() < 1)
            {
                gameStart = true;
                Controls.Remove(mod);
                turn.Text = "Ваш ход";
                turn.ForeColor = Color.DarkSeaGreen;
            }
        }


        private void Ship4_Click(object sender, EventArgs e)
        {
            if (shipClick.Any(x => x))
            {
                for (int i = 0; i < shipClick.Length; i++)
                    shipClick[i] = false;
            }
            shipClick[3] = !shipClick[3];
        }

        private void Ship3_Click(object sender, EventArgs e)
        {
            if (shipClick.Any(x => x))
            {
                for (int i = 0; i < shipClick.Length; i++)
                    shipClick[i] = false;
            }
            shipClick[2] = !shipClick[2];
        }

        private void Ship2_Click(object sender, EventArgs e)
        {
            if (shipClick.Any(x => x))
            {
                for (int i = 0 ; i < shipClick.Length; i++)
                    shipClick[i] = false;
            }
            shipClick[1] = !shipClick[1];
        }

        private void Ship1_Click(object sender, EventArgs e)
        {
            if (shipClick.Any(x => x))
            {
                for (int i = 0; i < shipClick.Length; i++)
                    shipClick[i] = false;
            }
            shipClick[0] = !shipClick[0];
        }


        private void ButtonEnemy_Click(object sender, EventArgs e)
        {
            bool ifHit = false;
            if (gameStart)
            {
                Button hit = sender as Button;
                int result = Hit(p2Map, (hit));
                shots++;
                if (result == 1)
                { 
                    hit.BackColor = Color.Pink;

                    eShips--;

                    score.Text = eShips.ToString();
                }
                else if(result == 0)
                {
                    hit.BackColor = Color.Yellow;
                    turn.Text = "Ход оппонента";
                    turn.ForeColor = Color.DarkRed;

                    do
                    {
                        ifHit = Shoot();
                        if(ifHit)
                            Wait(1.5);
                    } while (ifHit);
                    turn.Text = "Ваш ход";
                    turn.ForeColor = Color.DarkSeaGreen;
                    if (fShips < 1)
                    {
                        MessageBox.Show($"Вы проиграли( Кол-во выстрелов : {shots.ToString()}");
                        Close();
                    }
                }
                else 
                {
                    turn.Text = "Ход оппонента";
                    turn.ForeColor = Color.DarkRed;
                    do
                    {
                        ifHit = Shoot();
                        if (ifHit)
                            Wait(1.5);
                    } while (ifHit);
                    turn.Text = "Ваш ход";
                    turn.ForeColor = Color.DarkSeaGreen;
                    if (fShips < 1)
                    {
                        MessageBox.Show($"Вы проиграли( Кол-во выстрелов : {shots.ToString()}");
                        Close();
                    }
                }
                if (eShips < 1)
                {
                    MessageBox.Show($"Вы победили! Кол-во выстрелов : {shots.ToString()}");
                    WriteRecord();
                    Close();
                }
            }
        }

        private int Hit(int[,] map, Button pressed)
        {
            int hit = 0;

            if(map[pressed.Location.Y / cellSize, (pressed.Location.X - 350) / cellSize] == -1)
            {
                hit = -1;
            }
            else if (map[pressed.Location.Y / cellSize, (pressed.Location.X - 350) / cellSize] >= 1)
            {
                hit = 1;
                map[pressed.Location.Y / cellSize, (pressed.Location.X - 350) / cellSize] = -1;
            }

            return hit;
        }

        private int[,] SetShips()
        {
            int[,] map = new int[mapSize, mapSize];

            int x, y, dir, amount;

            Random rand = new Random();

            amount = 1;
            while (amount > 0)
            {
                x = rand.Next(1, 10);
                y = rand.Next(1, 10);
                dir = rand.Next(0, 4);
                if (CanSet(map, x, y, 4, dir))
                {
                    switch (dir)
                    {
                        case 0:
                            for (int i = y; i > y - 4; i--)
                            {
                                map[i, x] = 1;
                                map[i, x] = 4;
                            }
                            break;
                        case 1:
                            for (int i = x; i < x + 4; i++)
                            {
                                map[y, i] = 1;
                                map[y, i] = 4;
                            }
                            break;
                        case 2:
                            for (int i = y; i < y + 4; i++)
                            {
                                map[i, x] = 1;
                                map[i, x] = 4;
                            }
                            break;
                        case 3:
                            for (int i = x; i > x - 4; i--)
                            {
                                map[y, i] = 1;
                                map[y, i] = 4;
                            }
                            break;
                    }
                    amount--;
                }
            }

            amount = 2;
            while (amount > 0)
            {
                x = rand.Next(1, 10);
                y = rand.Next(1, 10);
                dir = rand.Next(0, 4);
                if (CanSet(map, x, y, 3, dir))
                {
                    switch (dir)
                    {
                        case 0:
                            for (int i = y; i > y - 3; i--)
                            {
                                map[i, x] = 1;
                                map[i, x] = 3;
                            }
                            break;
                        case 1:
                            for (int i = x; i < x + 3; i++)
                            {
                                map[y, i] = 1;
                                map[y, i] = 3;
                            }
                            break;
                        case 2:
                            for (int i = y; i < y + 3; i++)
                            {
                                map[i, x] = 1;
                                map[i, x] = 3;
                            }
                            break;
                        case 3:
                            for (int i = x; i > x - 3; i--)
                            {
                                map[y, i] = 1;
                                map[y, i] = 3;
                            }
                            break;
                    }

                    amount--;

                }
            }


            amount = 3;
            while (amount > 0)
            {
                x = rand.Next(1, 10);
                y = rand.Next(1, 10);
                dir = rand.Next(0, 4);
                if (CanSet(map, x, y, 2, dir))
                {
                    switch (dir)
                    {
                        case 0:
                            for (int i = y; i > y - 2; i--)
                            {
                                map[i, x] = 1;
                                map[i, x] = 2;
                            }
                            break;
                        case 1:
                            for (int i = x; i < x + 2; i++)
                            {
                                map[y, i] = 1;
                                map[y, i] = 2;
                            }
                            break;
                        case 2:
                            for (int i = y; i < y + 2; i++)
                            {
                                map[i, x] = 1;
                                map[i, x] = 2;
                            }
                            break;
                        case 3:
                            for (int i = x; i > x - 2; i--)
                            {
                                map[y, i] = 1;
                                map[y, i] = 2;
                            }
                            break;
                    }
                    amount--;

                }
            }
            amount = 4;
            while (amount > 0)
            {
                x = rand.Next(1, 10);
                y = rand.Next(1, 10);

                if (CanSet(map, x, y, 1, -1))
                {
                    map[y, x] = 1;
                    amount--;
                }

            }
            return map;
        }
            

        private bool CanSet(in int[,] map, in int x, in int y, in int length, in int dir)
        {
            int i, j;

            if (length > 1)
            {
                switch (dir)
                {
                    case 0:
                        switch(length)
                        {
                            case 2:
                                if (y < 2)
                                    return false;
                                for (j = x - 1; j < x + 2; j++)
                                {
                                    if (j >= mapSize || j <= 0)
                                        continue;
                                    for (i = y + 1; i > y - 3; i--)
                                    {
                                        if (i >= mapSize || i <= 0)
                                            continue;
                                        if (map[i, j] >= 1)
                                            return false;
                                    }
                                }
                                break;
                            case 3:
                                if (y < 3)
                                    return false;
                                for (j = x - 1; j < x + 2; j++)
                                {
                                    if (j >= mapSize || j <= 0)
                                        continue;
                                    for (i = y + 1; i > y - 4; i--)
                                    {
                                        if (i >= mapSize || i < 0)
                                            continue;
                                        if (map[i, j] >= 1)
                                            return false;
                                    }
                                }
                                break;
                            case 4:
                                if (y < 4)
                                    return false;
                                for (j = x - 1; j < x + 2; j++)
                                {
                                    if (j >= mapSize || j <= 0)
                                        continue;
                                    for (i = y + 1; i > y - 5; i--)
                                    {
                                        if (i >= mapSize || i <= 0)
                                            continue;
                                        if (map[i, j] >= 1)
                                            return false;
                                    }
                                }
                                break;
                        }
                        break;
                    case 1:
                        switch (length)
                        {
                            case 2:
                                if (x > 8)
                                    return false;
                                for (j = y - 1; j < y + 2; j++)
                                {
                                    if (j >= mapSize || j <= 0)
                                        continue;
                                    for (i = x - 1; i < x + 3; i++)
                                    {
                                        if (i >= mapSize || i <= 0)
                                            continue;
                                        if (map[j, i] >= 1)
                                            return false;
                                    }
                                }
                                break;
                            case 3:
                                if (x > 7)
                                    return false;
                                for (j = y - 1; j < y + 2; j++)
                                {
                                    if (j >= mapSize || j <= 0)
                                        continue;
                                    for (i = x - 1; i < x + 4; i++)
                                    {
                                        if (i >= mapSize || i <= 0)
                                            continue;
                                        if (map[j, i] >= 1)
                                            return false;
                                    }
                                }
                                break;
                            case 4:
                                if (x > 6)
                                    return false;
                                for (j = y - 1; j < y + 2; j++)
                                {
                                    if (j >= mapSize || j <= 0)
                                        continue;
                                    for (i = x - 1; i < x + 5; i++)
                                    {
                                        if (i >= mapSize || i <= 0)
                                            continue;
                                        if (map[j, i] >= 1)
                                            return false;
                                    }
                                }
                                break;
                        }
                        break;
                    case 2:
                        switch (length)
                        {
                            case 2:
                                if (y > 8)
                                    return false;
                                for (j = x - 1; j < x + 2; j++)
                                {
                                    if (j >= mapSize || j <= 0)
                                        continue;
                                    for (i = y - 1; i < y + 3; i++)
                                    {
                                        if (i >= mapSize || i <= 0)
                                            continue;
                                        if (map[i, j] >= 1)
                                            return false;
                                    }
                                }
                                break;
                            case 3:
                                if (y > 7)
                                    return false;
                                for (j = x - 1; j < x + 2; j++)
                                {
                                    if (j >= mapSize || j <= 0)
                                        continue;
                                    for (i = y - 1; i < y + 4; i++)
                                    {
                                        if (i >= mapSize || i <= 0)
                                            continue;
                                        if (map[i, j] >= 1)
                                            return false;
                                    }
                                }
                                break;
                            case 4:
                                if (y > 6)
                                    return false;
                                for (j = x - 1; j < x + 2; j++)
                                {
                                    if (j >= mapSize || j <= 0)
                                        continue;
                                    for (i = y - 1; i < y + 5; i++)
                                    {
                                        if (i >= mapSize || i <= 0)
                                            continue;
                                        if (map[i, j] >= 1)
                                            return false;
                                    }
                                }
                                break;
                        }
                        break;
                    case 3:
                        switch (length)
                        {
                            case 2:
                                if (x < 2)
                                    return false;
                                for (j = y - 1; j < y + 2; j++)
                                {
                                    if (j >= mapSize || j <= 0)
                                        continue;
                                    for (i = x + 1; i > x - 3; i--)
                                    {
                                        if (i >= mapSize || i <= 0)
                                            continue;
                                        if (map[j, i] >= 1)
                                            return false;
                                    }
                                }
                                break;
                            case 3:
                                if (x < 3)
                                    return false;
                                for (j = y - 1; j < y + 2; j++)
                                {
                                    if (j >= mapSize || j <= 0)
                                        continue;
                                    for (i = x + 1; i > x - 4; i--)
                                    {
                                        if (i >= mapSize || i <= 0)
                                            continue;
                                        if (map[j, i] >= 1)
                                            return false;
                                    }
                                }
                                break;
                            case 4:
                                if (x < 4)
                                    return false;
                                for (j = y - 1; j < y + 2; j++)
                                {
                                    if (j >= mapSize || j <= 0)
                                        continue;
                                    for (i = x + 1; i > x - 5; i--)
                                    {
                                        if (i >= mapSize || i <= 0)
                                            continue;
                                        if (map[j, i] >= 1)
                                            return false;
                                    }
                                }
                                break;
                        }
                        break;
                }
            }
            else
            {
                for (j = y - 1; j < y + 2; j++)
                {
                    if (j >= mapSize || j <= 0)
                        continue;
                    for (i = x - 1; i < x + 2; i++)
                    {
                        if (i >= mapSize || i <= 0)
                            continue;
                        if (map[j, i] >= 1)
                            return false;
                    }
                }
            }

            return true;
        }


        private bool Shoot()
        {
            int x, y;
            bool hit = false;
            string index;
            Button s;
            switch (prevHit)
            {
                case false:
                    Random rand = new Random();
                    do
                    {
                        x = rand.Next(1, 10);
                        y = rand.Next(1, 10);
                    } while (p1Map[y, x] == -1);
                    index = y.ToString() + x.ToString();
                    s = (Button)Controls[index];
                    if (p1Map[y, x] >= 1)
                    {
                        p1Map[y, x] = -1;
                        fShips--;
                        s.BackColor = Color.Purple;
                        prevX = x;
                        prevY = y;
                        hit = true;
                        prevHit = true;
                    }
                    else
                    {
                        p1Map[y, x] = -1;
                        s.BackColor = Color.Yellow;
                        hit = false;
                    }
                    break;
                case true:

                    if (hp == -1)
                    {
                        tempX = prevX;
                        tempY = prevY;
                        index = tempY.ToString() + tempX.ToString();
                        prevDir = 0;
                        s = (Button)Controls[index];
                        hp = int.Parse(s.Text) - 1;
                    }
                    if (hp == 0)
                    {
                        prevHit = false;
                        hp = -1;
                        prevDir = 0;
                        goto case false;
                    }
                    else if(hp > 0)
                    {
                        switch (prevDir)
                        {
                            case 0:
                                tempY--;
                                if (tempY <= 0)
                                {
                                    prevDir++;
                                    goto case 1;
                                }
                                index = tempY.ToString() + tempX.ToString();
                                s = (Button)Controls[index];
                                if (p1Map[tempY, tempX] != -1)
                                {
                                    if (p1Map[tempY, tempX] >= 1)
                                    {
                                        p1Map[tempY, tempX] = -1;
                                        fShips--;
                                        hp--;
                                        s.BackColor = Color.Purple;
                                        hit = true;
                                    }
                                    else
                                    {
                                        p1Map[tempY, tempX] = -1;
                                        s.BackColor = Color.Yellow;
                                        prevDir++;
                                        hit = false;
                                    }
                                }
                                else
                                {
                                    prevDir++;
                                    goto case 1;
                                }
                                break;
                            case 1:
                                tempY = prevY;
                                tempX++;
                                if (tempX >= mapSize)
                                {
                                    prevDir++;
                                    goto case 2;
                                }
                                index = tempY.ToString() + tempX.ToString();
                                s = (Button)Controls[index];
                                if (p1Map[tempY, tempX] != -1)
                                {
                                    if (p1Map[tempY, tempX] >= 1)
                                    {
                                        p1Map[tempY, tempX] = -1;
                                        fShips--;
                                        hp--;
                                        s.BackColor = Color.Purple;
                                        hit = true;
                                    }
                                    else
                                    {
                                        p1Map[tempY, tempX] = -1;
                                        s.BackColor = Color.Yellow;
                                        prevDir++;
                                        hit = false;
                                    }
                                }
                                else
                                {
                                    prevDir++;
                                    goto case 2;
                                }
                                break;
                            case 2:
                                tempX = prevX;
                                tempY++;
                                if (tempY >= mapSize)
                                {
                                    prevDir++;
                                    goto case 3;
                                }
                                index = tempY.ToString() + tempX.ToString();
                                s = (Button)Controls[index];
                                if (p1Map[tempY, tempX] != -1)
                                {
                                    if (p1Map[tempY, tempX] >= 1)
                                    {
                                        p1Map[tempY, tempX] = -1;
                                        fShips--;
                                        hp--;
                                        s.BackColor = Color.Purple;
                                        hit = true;
                                    }
                                    else
                                    {
                                        p1Map[tempY, tempX] = -1;
                                        s.BackColor = Color.Yellow;
                                        prevDir++;
                                        hit = false;
                                    }
                                }
                                else
                                {
                                    prevDir++;
                                    goto case 3;
                                }
                                break;
                            case 3:
                                tempY = prevY;
                                tempX--;
                                if (tempX <= 0)
                                {
                                    prevHit = false;
                                    hp = -1;
                                    prevDir = 0;
                                    hit = false;
                                }
                                index = tempY.ToString() + tempX.ToString();
                                s = (Button)Controls[index];
                                if (p1Map[tempY, tempX] != -1)
                                {
                                    if (p1Map[tempY, tempX] >= 1)
                                    {
                                        p1Map[tempY, tempX] = -1;
                                        fShips--;
                                        hp--;
                                        s.BackColor = Color.Purple;
                                        hit = true;
                                    }
                                    else
                                    {
                                        p1Map[tempY, tempX] = -1;
                                        s.BackColor = Color.Yellow;
                                        prevHit = false;
                                        hit = false;
                                        hp = -1;
                                    }
                                }
                                else
                                {
                                    prevHit = false;
                                    hp = -1;
                                    prevDir = 0;
                                    hit = false;
                                }
                                break;
                        }

                    }
                break;
            }
            return hit;
        }

        private void Wait(double seconds)
        {
            int ticks = System.Environment.TickCount + (int)Math.Round(seconds * 1000.0);
            while (System.Environment.TickCount < ticks)
            {
                Application.DoEvents();
            }
        }


        private void WriteRecord()
        {

            StreamReader Rstream = new StreamReader(recPath);

            bool isChanged = false;

            string textFromFile = Rstream.ReadToEnd();

            Rstream.Close();

            textFromFile = textFromFile.Trim('\0');

            string[] temp = textFromFile.Split(new char[] { ' ', '\n' });

            List<string> words = new List<string>();

            foreach(var word in temp)
            {
                words.Add(word);
            }

            for(int i = 0; i < words.Count; i++)
            {
                if (words[i] == " " || words[i] == "")
                {
                    words.Remove(words[i]);
                }
            }

            int length = int.Parse(words[0]);

            int[] points = new int[words.Count / 2];
            int ind = -1;

            for (int i = 1, j = 0; i < words.Count && j < words.Count / 2; i += 2, j++)
            {
                points[j] = int.Parse(words[i]);
            }
            for (int i = 0; i < words.Count / 2; i++)
            {
                if (points[i] > shots)
                {
                    ind = i;
                    break;
                }
            }

            if (length >= 5)
            {
                if (ind > -1)
                {
                    int offset = ind * 2;
                    for(int i = words.Count - 1; i > offset; i -= 2)
                    {
                        words[i] = words[i - 2];
                        words[i - 1] = words[i - 3];
                    }
                    words[offset] = shots.ToString();
                    words[offset + 1] = FormMenu.nickname;

                    textFromFile = length.ToString() + '\n';

                    foreach (var word in words)
                    {
                        textFromFile += word + ' ';
                    }
                    isChanged = true;
                }
            }
            else
            {
                length++;

                textFromFile = length.ToString() + '\n';

                if (ind > -1)
                {
                    for (int i = 1; i < ind * 2; i += 2)
                    {
                        textFromFile += words[i] + ' ' + words[i + 1] + '\n';
                    }
                    textFromFile += shots.ToString() + ' ';
                    textFromFile += FormMenu.nickname + '\n';

                    for (int i = ind * 2 + 1; i < words.Count; i += 2)
                    {
                        textFromFile += words[i] + ' ' + words[i + 1] + '\n';
                    }
                }
                else
                {
                    for (int i = 1; i < words.Count; i += 2)
                    {
                        textFromFile += words[i] + ' ' + words[i + 1] + '\n';
                    }
                    textFromFile += shots.ToString() + ' ';
                    textFromFile += FormMenu.nickname + ' ';
                }

                isChanged = true;

            }
            if(isChanged)
            {
                StreamWriter Wstream = new StreamWriter(recPath);

                Wstream.Write(textFromFile);
                Wstream.Close();
            }
        }
    }
}
