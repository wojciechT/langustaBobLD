using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Media;
using System.IO;

namespace LangustaTurboExtra
{
    public class GameState
    {
        public SizeF GameArea;
        public World World;
        public int Attack;
        public int Armour;
        public int Level;
        public int Health;
        public int Treasure;
        public int Potions;
        public bool HasBrownKey;
        public bool HasGreenKey;
        public bool HasRedKey;
        public bool GameIsWon;
        
        private int _experience;
        private int _nextUpgrade;
        private Sprite _experienceSprite;
        private Sprite _attackSprite;
        private Sprite _armourSprite;
        private Sprite _healthSprite;
        private Sprite _treasureSprite;
        private Sprite _potionSprite;
        private Sprite _brownKeySprite;
        private Sprite _greenKeySprite;
        private Sprite _redKeySprite;
        private Sprite _lvlSprite;
        private Dictionary<string, Tile> _tiles = new Dictionary<string, Tile>();

        private static Font _font = new Font("Comic Sans MS", 20);
        private static Font _font2 = new Font("Comic Sans MS", 13);
        private static Brush _brush = new SolidBrush(Color.White);
        private static Brush _brush2 = new SolidBrush(Color.Gold);
        private static Random _random = new Random();



        public GameState(SizeF gameArea)
        {
            GameArea = gameArea;

            //Load in all the tile definitions
            readTileDefinitions(@"gamedata\tilelookups.csv");

            //Create the sprites for the UI
            int y = 50;
            _experienceSprite = new Sprite(this, 580, y, _tiles["her"].Bitmap, _tiles["her"].Rectangle, _tiles["her"].NumberOfFrames);
            _experienceSprite.ColorKey = Color.FromArgb(75, 75, 75);
            _healthSprite = new Sprite(this, 580, y += 74, _tiles["fd1"].Bitmap, _tiles["fd1"].Rectangle, _tiles["fd1"].NumberOfFrames);
            _healthSprite.ColorKey = Color.FromArgb(75, 75, 75);
            _attackSprite = new Sprite(this, 580, y += 74, _tiles["att"].Bitmap, _tiles["att"].Rectangle, _tiles["att"].NumberOfFrames);
            _attackSprite.ColorKey = Color.FromArgb(75, 75, 75);
            _armourSprite = new Sprite(this, 580, y += 74, _tiles["arm"].Bitmap, _tiles["arm"].Rectangle, _tiles["arm"].NumberOfFrames);
            _armourSprite.ColorKey = Color.FromArgb(75, 75, 75);
            _treasureSprite = new Sprite(this, 580, y += 74, _tiles["tr2"].Bitmap, _tiles["tr2"].Rectangle, _tiles["tr2"].NumberOfFrames);
            _treasureSprite.ColorKey = Color.FromArgb(75, 75, 75);
            _potionSprite = new Sprite(this, 580, y += 74, _tiles["pot"].Bitmap, _tiles["pot"].Rectangle, _tiles["pot"].NumberOfFrames);
            _potionSprite.ColorKey = Color.FromArgb(75, 75, 75);
            _brownKeySprite = new Sprite(this, 580, y += 74, _tiles["kbr"].Bitmap, _tiles["kbr"].Rectangle, _tiles["kbr"].NumberOfFrames);
            _brownKeySprite.ColorKey = Color.FromArgb(75, 75, 75);
            _greenKeySprite = new Sprite(this, 654, y, _tiles["kgr"].Bitmap, _tiles["kgr"].Rectangle, _tiles["kgr"].NumberOfFrames);
            _greenKeySprite.ColorKey = Color.FromArgb(75, 75, 75);
            _redKeySprite = new Sprite(this, 728, y, _tiles["kre"].Bitmap, _tiles["kre"].Rectangle, _tiles["kre"].NumberOfFrames);
            _redKeySprite.ColorKey = Color.FromArgb(75, 75, 75);
            _lvlSprite = new Sprite(this, 695, 45, _tiles["lvl"].Bitmap, _tiles["lvl"].Rectangle, _tiles["lvl"].NumberOfFrames);
            _lvlSprite.ColorKey = Color.FromArgb(75, 75, 75);
        }

        //z ka¿dym poziomem rosn¹ ci statsy
        public int Experience
        {
            get
            {
                return _experience;
            }
            
            set
            {
                _experience = value;
                //jak masz wystaraczj¹co exp to masz nowy level
                if (_experience > _nextUpgrade)
                {
                    Attack++;
                    Armour++;
                    _nextUpgrade = _nextUpgrade + 20 * Level;
                    Level++; 
                } 
              
            }
        }

        public void Draw(Graphics graphics)
        {
            World.Draw(graphics);

            //Draw the HUD
            _experienceSprite.Draw(graphics);
            _lvlSprite.Draw(graphics);
            _healthSprite.Draw(graphics);
            _attackSprite.Draw(graphics);
            _armourSprite.Draw(graphics);
            _treasureSprite.Draw(graphics);
            _potionSprite.Draw(graphics);
            if (HasBrownKey) _brownKeySprite.Draw(graphics);
            if (HasGreenKey) _greenKeySprite.Draw(graphics);
            if (HasRedKey) _redKeySprite.Draw(graphics);
            int y = 62;
            graphics.DrawString(Experience.ToString(), _font, _brush, 650, y);
            graphics.DrawString(Health.ToString(), _font, _brush, 650, y += 74);
            graphics.DrawString(Attack.ToString(), _font, _brush, 650, y += 74);
            graphics.DrawString(Armour.ToString(), _font, _brush, 650, y += 74);
            graphics.DrawString(Treasure.ToString(), _font, _brush, 650, y += 74);
            graphics.DrawString(Potions.ToString(), _font, _brush, 650, y += 74);
            graphics.DrawString(Level.ToString(), _font, _brush2, 755, 60);
            
            //jesli game over to wyswietl
            if (Health == 0)
            {
                graphics.DrawString("Wpadles w wir Wszechogarniajacej Odchlani!", _font, _brush, 40, 250);
                graphics.DrawString("Wcisnij 's' aby sprobowac ponownie swoich si³!", _font, _brush, 40, 300);
            }
            //jesli zwyciestwo to wyswietl
            if (GameIsWon)
            {
                graphics.DrawString("Hejka! Jestem Francis Drake, najbardziej fajny z piratów!", _font2, _brush2, 40, 100);
                graphics.DrawString("Zbli¿asz siê do koñca wszechœwiata, ale to nie koniec podró¿y!", _font2, _brush2, 40, 150);
                graphics.DrawString("By ocaliæ Sprawiedliwoœæ musisz pokonaæ Jamesa Cooka,", _font2, _brush2, 40, 200);
                graphics.DrawString("który jest królewskim ³ajdakiem pod pirack¹ bander¹!", _font2, _brush2, 40, 250);
                graphics.DrawString("Twój wynik (skarby) to:", _font2, _brush2, 40, 300);
                graphics.DrawString(Treasure.ToString(), _font, _brush2, 260, 290);
            }
            
        }

        public void Update(double gameTime, double elapsedTime)
        {
            World.Update(gameTime, elapsedTime);
        }


        public void Initialize()
        {
            Sounds.Start();

            //Create all the main gameobjects
            World = new World(this, _tiles, @"gamedata\map.txt");

            //reset stanu gry
            Attack = 1;
            Potions = 6;
            Armour = 1;
            Experience = 0;
            Level = 1;
            _nextUpgrade = 20;
            Health = 100;
            Treasure = 0;
            GameIsWon = false;
        }

        //Each line contains a comma delimited tile definition that the tile constructor understands
        private void readTileDefinitions(string tileDescriptionFile)
        {
            using (StreamReader stream = new StreamReader(tileDescriptionFile))
            {
                string line;
                while ((line = stream.ReadLine()) != null)
                {
                    //separate out the elements of the 
                    string[] elements = line.Split(',');

                    //And make the tile.
                    Tile tile = new Tile(elements);
                    _tiles.Add(tile.Shortcut, tile);
                }
            }
        }
        public void KeyDown(Keys keys)
        {
            //If the game is not over then allow the user to play
            if (Health > 0 && !GameIsWon)
            {
                World.KeyDown(keys);
            }
            else
            {
                //jesli koniec gry to s uruchamia od nowa 
                if (keys == Keys.S)
                {
                    Initialize();
                }
            }
        }
    }
}
