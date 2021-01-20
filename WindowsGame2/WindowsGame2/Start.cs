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
    class Start
    {   //zmienne sterujacae kkalwiszmi
        public bool start;
        bool wybor;
        public bool wyjdz;
        Texture2D texture, strzalka1, strzalka2, spacja, enter,logo;//tekstury klawiszy instukcji i loga
        Rectangle klawisz;//pozycja klawisa 1
        Rectangle klawisz2;//pozycja klawisza2
        //wyświetlane keksty klawiszy
        string Scontinue = "Start";
        string Sexit = "WYJDZ";
        SpriteFont font;//czcionka
        public int times;//zliczanie czasu
        Vector2 roz;//rozdzielczość
        //kolory masek klawiszy
        Color color1;
        Color color2;
        //funkcja uzupełniajaca dane 
        public void Incjalizuj(Texture2D texture, Texture2D logo, Texture2D strzalka1, Texture2D strzalka2, Texture2D spacja, Texture2D enter, Vector2 rozdzielczosc, SpriteFont font)
        {
            this.roz = rozdzielczosc;
            this.color2 = Color.Black;
            this.color1 = Color.Blue;
            wyjdz = false;
            start = false;
            wybor = true;
            this.font = font;
            this.texture = texture;
            this.klawisz.Width = texture.Width - 50;
            this.klawisz.Height = 70;
            this.klawisz.X = (int)((rozdzielczosc.X / 2) - (klawisz.Width / 2));
            this.klawisz.Y = (int)((rozdzielczosc.Y / 2) - (klawisz.Height / 2));
            this.klawisz2.Width = texture.Width - 50;
            this.klawisz2.Height = 70;
            this.klawisz2.X = (int)((rozdzielczosc.X / 2) - (klawisz2.Width / 2));
            this.klawisz2.Y = (int)((rozdzielczosc.Y / 2) - (klawisz2.Height / 2))+80;
            this.strzalka1 = strzalka1;
            this.strzalka2 = strzalka2;
            this.spacja = spacja;
            this.enter = enter;
            this.logo = logo;
        }

        public void Wybor(int time, KeyboardState key)//wybor klawisza
        {
            if (!start) { 
            if (key.IsKeyDown(Keys.Down) || key.IsKeyDown(Keys.Up))
            {
                if (wybor && time >= 300)
                {
                    wybor= false;
                    time = 0;
                    color2 = Color.Blue;
                    color1 = Color.Black;
                }
                if (wybor == false && time >= 300)
                {
                    wybor = true;
                    time = 0;
                    color2 = Color.Black;
                    color1 = Color.Blue;
                }
            }
            if (key.IsKeyDown(Keys.Enter) && time >= 300)
            {
                if (!wybor)
                    wyjdz = true;
                    
                if (wybor)
                {
                    start = true;
                    time = 0;
                }
            }
            times = time;}
        }
        public void Draw(SpriteBatch sprinteBatch)//wyswietlanie
        {
            if (!start)
            {
                sprinteBatch.Draw(texture, klawisz, color1);
                sprinteBatch.Draw(texture, klawisz2, color2);
                sprinteBatch.DrawString(font, Scontinue, new Vector2((int)((roz.X - font.MeasureString(Scontinue).X) / 2), (int)((klawisz.Y + klawisz.Height / 2 - font.MeasureString(Sexit).Y / 2))), Color.White);
                sprinteBatch.DrawString(font, Sexit, new Vector2((int)((roz.X - font.MeasureString(Sexit).X) / 2), (int)((klawisz2.Y + klawisz2.Height / 2 - font.MeasureString(Sexit).Y / 2))), Color.White);
                sprinteBatch.Draw(strzalka1, new Rectangle(20, (int)(roz.Y - ((enter.Height/2 ) + 20)* 4),(int)(strzalka1.Width/2), (int)(strzalka1.Height / 2)), Color.White);
                sprinteBatch.Draw(strzalka2, new Rectangle(20, (int)(roz.Y- ((enter.Height/2 ) + 20)* 3), (int)(strzalka2.Width / 2), (int)(strzalka2.Height / 2)), Color.White);
                sprinteBatch.Draw(spacja, new Rectangle(20, (int)(roz.Y - ((enter.Height/2 ) + 20)* 2), (int)(spacja.Width / 2), (int)(spacja.Height / 2)), Color.White);
                sprinteBatch.Draw(enter, new Rectangle(20, (int)(roz.Y - (enter.Height /2) - 20), (int)(enter.Width / 2), (int)(enter.Height / 2)), Color.White);
                sprinteBatch.Draw(logo, new Vector2((int)((roz.X-logo.Width)/2), 30), Color.White);
                sprinteBatch.DrawString(font,"Q      PAUZA", new Vector2(20, (int)(roz.Y - ((enter.Height/2) + 20) * 5)), Color.White);
                sprinteBatch.DrawString(font, "ZATWIERDZANIE", new Vector2(20+ enter.Width, (int)(roz.Y - (enter.Height / 2) - 20)), Color.White);
                sprinteBatch.DrawString(font, "STRZAL", new Vector2(20 + spacja.Width, (int)(roz.Y - ((enter.Height / 2) + 20) * 2)), Color.White);
                sprinteBatch.DrawString(font, "WYBOR", new Vector2(20 +strzalka1.Width, (int)(roz.Y - ((enter.Height / 2) + 20) * 3)), Color.White);
                sprinteBatch.DrawString(font, "PORUSZANIE STATKIEM", new Vector2(20 + strzalka2.Width, (int)(roz.Y - ((enter.Height / 2) + 20) * 4)), Color.White);

            }
        }
    }
}