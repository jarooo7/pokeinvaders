using System;
using System.Collections.Generic;
using System.Linq;


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace WindowsGame2
{
    class Statek:Strzal
    {

        public Animacja Player;//textura
        public bool Aktywnosc;//aktywnosc
     
        public Vector2 Pozycja;//pozycja
        public int Width
        {
            get { return Player.FrameWidthS; }
        }
        public int Height
        {
            get { return Player.FrameHeightS; }
        }
        public void Inicjalizacja(Animacja texture, Vector2 pozycja)
        {
            this.Player = texture;
            this.Pozycja = pozycja;
            Aktywnosc = true;
            
        }
        public void Update(GameTime time)
        {
            Player.Update(time);
            Player.Position= Pozycja;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            Player.Draw(spriteBatch);
        }
        public void sterowanie(KeyboardState key,int pbok)
        {
            if (key.IsKeyDown(Keys.Left) && Pozycja.X > 0)
                Pozycja.X = Pozycja.X - 10;
            if (key.IsKeyDown(Keys.Right) && Pozycja.X < pbok - Width )
                Pozycja.X = Pozycja.X + 10;
        }
        public void Reset()
        {
            point = 0;
        }
    }
}
