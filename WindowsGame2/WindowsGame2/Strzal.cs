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
   public interface IStatek
    {
        void UpdateStrzal(KeyboardState key, Rectangle Pozycja, GameTime time);
        int[] Traf(Rectangle[] pozycjaO, int[] live,int kolumna);
        void DrawStrzal(SpriteBatch spriteBatch);
        void LoadStrzal(Texture2D pocisk, double skala, SoundEffect dzwiek);
    }
   public interface IPokemon
    {
        void UpdateStrzal(GameTime time, Rectangle[] Pozycja,int kolumna, int wysokosc,int[]live);
        bool Traf(Rectangle pozycjaO, bool GameOver);
        void LoadStrzal(Texture2D pocisk, double skala, int rzad, SoundEffect dzwiek);
        void DrawStrzal(SpriteBatch spriteBatch);
    }
    class Strzal:Punkty,IStatek,IPokemon
    {
        Texture2D pocisk;//tekstuara
        Rectangle pozycja;//pozycja
        int czas=300;//zliczanieczasu
        double skala;//skala
        bool strzal;//czy wystrzelono
        Random Los=new Random();//zmienanlosowa
        int los;//zmienna przechowywujaca los
        int c;//która kolumna
        int j = 0;//licznik 
        int rzad;//który rząd
        int Ltime=0;//zliczanie czau (do losu)
        Rectangle[] Spozycja= new Rectangle[20];//pozycja strzału
        bool[] Sstrzal=new bool[20];//aktywność strzału

        SoundEffect dzwiek;
      void IStatek.LoadStrzal(Texture2D pocisk,double skala, SoundEffect dzwiek)
        {   this.skala = skala;
            this.pocisk = pocisk;
            this.dzwiek = dzwiek;
            for (int i = 0; i < 20; i++)
            { 
            this.Spozycja[i].X = 0;
            this.Spozycja[i].Y = 0;
            this.Spozycja[i].Width = (int)(pocisk.Width*skala);
            this.Spozycja[i].Height = (int)(pocisk.Height * skala);
            Sstrzal[i] = false;
           
            }
        }
        void IPokemon.LoadStrzal(Texture2D pocisk, double skala,int rzad, SoundEffect dzwiek)
        {
            this.skala = skala;
            this.pocisk = pocisk;
            this.pozycja.X = 0;
            this.pozycja.Y = 0;
            this.pozycja.Width = (int)(pocisk.Width * skala);
            this.pozycja.Height = (int)(pocisk.Height * skala);
            strzal = false;
            this.rzad = rzad;
            this.dzwiek = dzwiek;
        }
        void IStatek.UpdateStrzal(KeyboardState key,Rectangle Pozycja, GameTime time)
        {   if (czas>0)
            czas -=(int) time.ElapsedGameTime.Milliseconds;
            if(czas<0)
            if (!Sstrzal[j])
            if (key.IsKeyDown(Keys.Space))
            {
                Spozycja[j].X = (int)Pozycja.X + (int)((Pozycja.Width/ 2)-(int)((pocisk.Width / 2) * skala)) ;
                Spozycja[j].Y =(int) (Pozycja.Y - (pocisk.Height*skala) );
                Sstrzal[j] = true;
                czas = 200;
                dzwiek.Play();
            }
            for (int i = 0; i < 20; i++) { 
                Spozycja[i].Y = Spozycja[i].Y - 5;
            if(Spozycja[i].Y<0)
                Sstrzal[i] = false;}
             if (Sstrzal[j])

            j += 1;
            if (j >= 20)
                j = 0;
        }
     
        int[] IStatek.Traf(Rectangle[] pozycjaO,int[] live,int kolumna)
        {
            for (int i = 0; i < 20; i++)
                for (int c = 0; c < kolumna; c++)
                if (live[c]> 0)
                if (Spozycja[i].Intersects(pozycjaO[c]))
                {
                    live[c]= live[c]-1;
                    Sstrzal [i]= false;
                    Spozycja[i].X = Spozycja[i].Y = 0;
                    point += (int)((900 - pozycjaO[c].Y)/4);
                }
           
            return live;
        }
        bool IPokemon.Traf(Rectangle pozycjaO,bool GameOver)
        {
            if (pozycja.Intersects(pozycjaO))
                return true;

                return GameOver;
        }
        void IPokemon.UpdateStrzal(GameTime time, Rectangle[] Pozycja,int kolumna,int wysokosc,int[] live)
        {
            Ltime+=(int)time.ElapsedGameTime.TotalMilliseconds+rzad;
            los=Los.Next(1,600);
            if (Ltime == los&&!strzal)
            {
                c = Los.Next(0, kolumna-1);
                if (live[c] > 0) { 
                pozycja.X = (int)Pozycja[c].X + (int)((Pozycja[c].Width / 2) - (int)((pocisk.Width / 2) * skala));
                pozycja.Y = (int)(Pozycja[c].Y +Pozycja[c].Height);
                strzal = true;
               dzwiek.Play();}
            }
            if (strzal)
                pozycja.Y = pozycja.Y + 2;
            if (pozycja.Y > wysokosc)
                strzal = false;
            if (Ltime > 300) 
            Ltime = 0;
        }
        void IPokemon.DrawStrzal(SpriteBatch spriteBatch)
        {
            if (strzal)
                spriteBatch.Draw(pocisk, pozycja, Color.GhostWhite);
        }
         void IStatek.DrawStrzal(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < 20; i++)
            {
                    if(Sstrzal[i])
                    spriteBatch.Draw(pocisk, Spozycja[i], Color.GhostWhite);
            }
        }
    }
}
