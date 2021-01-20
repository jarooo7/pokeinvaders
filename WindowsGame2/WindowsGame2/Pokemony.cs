using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame2
{
    class Pokemony:Strzal
    {
        public Texture2D Player;//textua pokemonów
        public Rectangle[] Pozycja;//pozycja 
        Rectangle[] PozPocz;//pozycja poczatkowa
        string Poczkierunek;//poczatkowykierunek
        public int[] Poczlive;//poczatkowe życie
        public int[] live;//tablica przechowujaca życie pokiemonów
        protected string kierunek;//kierunek poruszania sie pokiemonów
        protected int animuj = 0;//zliczanie czasu animacji
        protected int licznikA = 0;//licznik klatki
        protected int mnoz = -1;//przewijanie klatek
        protected bool dodaj = false;//sterowanie animasja
        protected bool opuszczanie = false;//sterowanie opuszcaniem
        
        public Pokemony()
        {
        }
        public Pokemony(Texture2D texture, int kolumny, int szerokosc, int r, int live, string kierunek)//konstrukror parametryczny uzupełniajacy dane
        {
             this.Player = texture;
            opuszczanie = false;
            this.kierunek = kierunek;
            Pozycja = new Rectangle[kolumny];
            this.live = new int[kolumny];
            PozPocz =new  Rectangle[kolumny];
            Poczlive=new int[kolumny];
            for (int c = 0; c < kolumny; c++)
            {
                if (kierunek == "LEWA")
                {
                this.Pozycja[c].X = (int)((((szerokosc * 0.4) / kolumny) + 30) * c) + szerokosc;
                this.Pozycja[c].Y = ((int)(((szerokosc * 0.4) / kolumny) + 20) * r) + 70;
                }
                if (kierunek == "PRAWA")
                {
                this.Pozycja[c].X = (int)((((szerokosc * 0.4) / kolumny) + 30) * c) - (int)(szerokosc * 0.4) - 30 * (kolumny - 1);
                this.Pozycja[c].Y = ((int)(((szerokosc * 0.4) / kolumny) + 20) * r) + 70;
                }
                this.Pozycja[c].Width = (int)((szerokosc * 0.4) / kolumny);
                this.Pozycja[c].Height = (int)(Player.Height * ((szerokosc * 0.4) / kolumny) / (Player.Width / 4));
                this.live[c] =live;
                PozPocz[c] = Pozycja[c];
                Poczlive[c] = this.live[c];
            }
            
            Poczkierunek=kierunek;
           

        }
        public  void Update(GameTime gameTime,int szybkosc,int kolumna,int pBok)//aktualizacja położenia 
        {
            for (int c = 0; c < kolumna; c++)
            {
                if (kierunek == "PRAWA")
                    Pozycja[c].X = Pozycja[c].X + szybkosc;
                if (kierunek == "LEWA")
                    Pozycja[c].X = Pozycja[c].X + -szybkosc;
            }
           for (int c = 0; c < kolumna; c++)
                {
                    if (Pozycja[c].X + Pozycja[c].Width >pBok  && kierunek == "PRAWA")
                {
                    kierunek = "LEWA";
                    opuszczanie = true;
                }
                if (Pozycja[c].X <0 && kierunek=="LEWA")
                {
                    kierunek = "PRAWA";
                    opuszczanie = true;
                }
            }
            if (opuszczanie == true)
            {
                for (int c = 0; c < kolumna; c++)
                  {
                        for (int i = 0; i < 20; i++)
                            Pozycja[c].Y = Pozycja[c].Y +1;
                  }
                opuszczanie = false;
            }
            if (licznikA < 4)
                if (animuj <= 100)
                    animuj += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
                else
                {
                    dodaj = true;
                    licznikA += 1;
                    animuj = 0;
                }
            if (licznikA == 4)
            {
                mnoz = -1;
                licznikA = 0;
            }
            if (dodaj)
            {
                mnoz += 1;
                dodaj = false;
            }
        }
        public  bool GameOver(int Rpozycja,int kolumny)
        {
            for (int c = 0; c < kolumny; c++)
                if (live[c] > 0)
                    if (Rpozycja < Pozycja[c].Y + Pozycja[c].Height)
                        return true;
             return false;
         }
        public void Draw(SpriteBatch spriteBatch, int szerokosc, int kolumna, int r)//wyświetlanie
        {
            for (int c = 0; c < kolumna; c++)
                if (live[c] > 0)
                    spriteBatch.Draw(Player, Pozycja[c], new Rectangle(50 * mnoz, 0, 50, Player.Height), Color.White);
        }

        public void Reset(int kolumny)//zresetowanie do stanu poczatkowego
        {
            kierunek =Poczkierunek;
            for (int c = 0; c < kolumny; c++) { 
                Pozycja[c] = PozPocz[c];
                live[c] =Poczlive[c];}
        }
        public void Level(int kolumny,int level)//zresetowanie do stanu poczatkowego
        {
            kierunek = Poczkierunek;
            for (int c = 0; c < kolumny; c++)
            {
                Pozycja[c] = PozPocz[c];
                if(level%2==0)
                live[c] = Poczlive[c]+(level/2)-1;
                else
                    live[c] = Poczlive[c] + ((level- 1) / 2) ;
            }
        }
    }
}
