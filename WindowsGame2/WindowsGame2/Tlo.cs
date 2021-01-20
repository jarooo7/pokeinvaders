using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace WindowsGame2
{
    class Tlo
    {
        Texture2D texturatlo;//textura
        int speed;//szybkość
        int wysokosc;//wysokość okna
        Rectangle pozycja1,pozycja2;//pozycje
        public void Load(Texture2D texturatlo, int speed,int szerokosc, int wysokosc)
        {
            this.texturatlo= texturatlo;
            this.pozycja1 = new Rectangle(0,0, szerokosc, texturatlo.Height);
            this.pozycja2 = new Rectangle(0,-texturatlo.Height+100,szerokosc, texturatlo.Height);
            this.speed = speed;
            this.wysokosc = wysokosc;
        }
        public void Update()
        {
            pozycja1.Y += speed;
            pozycja2.Y += speed;
            if (pozycja1.Y > wysokosc)
                pozycja1.Y = -texturatlo.Height+100;
            if (pozycja2.Y > wysokosc)
                pozycja2.Y = -texturatlo.Height+100;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
                spriteBatch.Draw(texturatlo,pozycja1, Color.GhostWhite);
                spriteBatch.Draw(texturatlo, pozycja2, Color.GhostWhite);
        }
    }
}
