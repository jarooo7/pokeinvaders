using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame2
{
    class Asteroidy
    {
        Texture2D asteroida;//tekstura asteroidy
        float przesunX, przesunY;//skala przesuniecia
        public Rectangle pozycjaA;//pozycja
        Vector2 PozPocz;//pozycja początkowa
        bool Wpolu;//zmienna typu bool określajaca czy asteroida znajduje sie na ekranie
        float speed;//zmienna przechowywujaca skale poczatkowa
        public Asteroidy(Texture2D asteroida, Vector2 pozycja, float speed, double scala)//kostruktor parametryczny uzupełniajacy obiekt asteroidy
        {
            this.asteroida = asteroida;
            this.pozycjaA = new Rectangle((int)pozycja.X, (int)pozycja.Y, (int)(asteroida.Width * scala), (int)(asteroida.Height * scala));
            this.przesunX = przesunY = speed;
            this.speed = speed;
            this.PozPocz = pozycja;
            Wpolu = false;
        }
        public void Update(GameTime gameTime,Vector2 roz,bool win,int level)//funkcja aktalizujaca połorzenie asteroid
        {
            if(level%2==0)
            { 
            pozycjaA.X += (int)(gameTime.ElapsedGameTime.TotalMilliseconds * przesunX);
            pozycjaA.Y += (int)(gameTime.ElapsedGameTime.TotalMilliseconds * przesunY);
            if ((pozycjaA.X> 0 && pozycjaA.X + pozycjaA.Width < roz.X) && (pozycjaA.Y >0 && pozycjaA.Y + pozycjaA.Height < roz.Y))
                Wpolu = true;
            if (Wpolu) { 
            if ((pozycjaA.X < 0 || pozycjaA.X + pozycjaA.Width > roz.X)&&!win)
                przesunX = -przesunX;
            if ((pozycjaA.Y < 0 || pozycjaA.Y + pozycjaA.Height > roz.Y)&&!win)
                przesunY = -przesunY;
            }}
        }
        public void Draw (SpriteBatch spriteBatch)//funkcja rysujaca
        {
            spriteBatch.Draw(asteroida, pozycjaA, Color.GhostWhite);
        }
        public void Reset()//funkcaj restartujaca
        {
            Wpolu = false;
            pozycjaA.X = (int)PozPocz.X;
            pozycjaA.Y = (int)PozPocz.Y;
            przesunX = przesunY = speed;

        }
        public bool Kill(Rectangle pozycja)//funkcaj zwracajaca wartoś typu bool false jezeli nie dotknie statku tru jeżeli dotknie
        {
            if (pozycjaA.Intersects(new Rectangle(pozycja.X + (pozycja.Width / 2) - 5, pozycja.Y, 10, pozycja.Height - 10)))
                return true;
            return false;
        }
    }
}
