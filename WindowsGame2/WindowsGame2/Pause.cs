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
    class Pause
    {   //zmienne typu bool sterujace klawiszami
        public bool pause;
        public bool dalej;
        public bool wyjdz;
        Texture2D texture;//textura tłą okenek i klawiszy
        Rectangle pozycja;//pozycja okna
        Rectangle klawisz;//pozycja klawisza1
        Rectangle klawisz2;//pozycja klawisza 2
        //wyświetlane teksty w kalwiszach
        string Spause = "PAUZA";
        string Scontinue = "KONTYNUUJ";
        string Sexit = "MENU";
        SpriteFont font;//czcionka
        public int times;//zliczanie czasu
        Vector2 roz;//rozdzielczość okna gry
        Color color1;//kolor maski klawisza1
        Color color2;//kolor maski klawisza2
        public void Incjalizuj(Texture2D texture,Vector2 rozdzielczosc, SpriteFont font)//załadowanie danych
        {
            this.roz = rozdzielczosc;
            this.color2 = Color.Black;
            this.color1 = Color.Blue;
            wyjdz = false;
            pause = false;
            dalej= true;
            this.font = font;
            this.texture = texture;
            this.pozycja.Width = texture.Width;
            this.pozycja.Height = texture.Height;
            this.pozycja.X =(int)((rozdzielczosc.X/2)-(pozycja.Width / 2));
            this.pozycja.Y = (int)((rozdzielczosc.Y / 2) - (pozycja.Height / 2));
            this.klawisz.Width = texture.Width-50;
            this.klawisz.Height = 70;
            this.klawisz.X = (int)((rozdzielczosc.X / 2) - (klawisz.Width / 2));
            this.klawisz.Y = (int)((rozdzielczosc.Y / 2) - (klawisz.Height / 2));
            this.klawisz2.Width = texture.Width - 50;
            this.klawisz2.Height = 70;
            this.klawisz2.X = (int)((rozdzielczosc.X / 2) - (klawisz2.Width / 2));
            this.klawisz2.Y = (int)((rozdzielczosc.Y / 2) - (klawisz2.Height / 2))+80;
        }
        public void Pauzuj(int time)//właczenie pauzy
        {    
            if (pause == false && time >= 300) 
                {
                    pause = true;
                    time = 0;
            }
            if (pause&& time>=300)
                {
                    pause = false;
                    time = 0;
            }
        }
        public void kontynuuj(int time, KeyboardState key)//funkcja wyboru klawusz kontynuuj lub menu
        {
            if (key.IsKeyDown(Keys.Down) || key.IsKeyDown(Keys.Up)) {
                if (dalej && time >= 300)
                {
                    dalej = false;
                    time = 0;
                    color1 = Color.Black;
                    color2 = Color.Blue;
                    
                }
                if (dalej == false && time >= 300)
                {
                    dalej = true;
                    time = 0;
                    color1 = Color.Blue;
                    color2 = Color.Black;
                    
                } }
            if (key.IsKeyDown(Keys.Enter)){
                if (!dalej){
                    wyjdz = true;
                    pause = false;
                   
                    time = 0;
                }
                if(dalej)   {
                    pause = false;
                    time = 0;
            }
            }
            times = time;
        }
        public void Draw(SpriteBatch sprinteBatch)//wyświtlanie obiektów
        {
            if (pause) {
            sprinteBatch.Draw(texture, pozycja, Color.White);
            sprinteBatch.Draw(texture, klawisz, color1);
            sprinteBatch.Draw(texture, klawisz2, color2);
                sprinteBatch.DrawString(font, Spause, new Vector2((int)((roz.X - font.MeasureString(Spause).X)/2),(int)((( roz.Y - font.MeasureString(Spause).Y)/2) - 80)), Color.White);
                sprinteBatch.DrawString(font, Scontinue, new Vector2((int)((roz.X - font.MeasureString(Scontinue).X) / 2), (int)((klawisz.Y + klawisz.Height / 2 - font.MeasureString(Sexit).Y / 2))), Color.White);
                sprinteBatch.DrawString(font, Sexit, new Vector2((int)((roz.X - font.MeasureString(Sexit).X) / 2), (int)((klawisz2.Y +klawisz2.Height/2- font.MeasureString(Sexit).Y / 2))), Color.White);
            }
        }
    }
}
