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
    class Animacja
    {
       
        Texture2D spriteStrip;//tekstura animacji
        Rectangle destionRect = new Rectangle();//pozycja animacji
        Rectangle soureRect = new Rectangle();//połorzenie wyświetlanego fragnentu tekstury
        Color color;//kolor maski
        int elapsedTime;//zliczanie czasu
        int frameTime;//czas wyswietlenia jednej klatki
        int frameCount;//ilośc klatek
        int curframe;//bierzaca klatka
        public int FrameWidth;//szerokość kaltki
        public int FrameHeight;//wysokość kaltki
        public int FrameWidthS;//szerokość kaltki po przeskalowaniu
        public int FrameHeightS;//wysokość kaltki po przeskalwaniu
        double scale;//skala
        public bool Active;//aktywność 
        public bool Looping;//zapętlenie
        public Vector2 Position;//pozycja
        public void Initialize(Texture2D texture, Vector2 position, int fWidth, int fHeight, int fCount,
            int fTime, Color color, double scale, bool looping)//funkcja inicjalizujaca
        {
            Position = position;
            spriteStrip = texture;
            this.color = color;
            this.FrameWidth = fWidth;
            this.FrameHeight = fHeight;
            this.FrameWidthS = (int)(fWidth * scale);
            this.FrameHeightS = (int)(fHeight * scale);
            this.frameCount = fCount;
            this.frameTime = fTime;
            this.scale = scale;
            Looping = looping;
            elapsedTime = 0;
            curframe = 0;
            Active = true;
        }
        public void Update(GameTime gameTime)//funkcja aktualizujaca
        {
            if (Active == false)
                return;
            elapsedTime += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
            if(elapsedTime>frameTime)
            {
                curframe++;
                if(curframe==frameCount)
                {
                    curframe = 0;
                    if (Looping == false)
                        Active = false;
                 elapsedTime = 0;
                } 
            }
            destionRect = new Rectangle((int)Position.X, (int)Position.Y,FrameWidthS, FrameHeightS);
            soureRect = new Rectangle(curframe * FrameWidth, 0, FrameWidth, FrameHeight);
        }
        public void Draw(SpriteBatch sprinteBatch)//funkcaj wyświetlajaca
        {
            if (Active)
                sprinteBatch.Draw(spriteStrip, destionRect, soureRect, color);
        }
        public void Draw(SpriteBatch sprinteBatch,int kolumna)//przeciążenie funkcji wyświetlania
        {
            if (Active)
                for (int c = 0; c < kolumna; c++)
                        sprinteBatch.Draw(spriteStrip, destionRect, soureRect, color);
        }
    }
}
