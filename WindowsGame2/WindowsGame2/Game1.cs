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
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        int level = 1,czas=0;
        
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        bool win, gameOver;//zmienne typu bool odpowiedzialne za efekt wygrania lub przegrania
        private SpriteFont fontpause,font1,font2;//czcionka
        public Vector2 rozdzielczosc = new Vector2(1024, 600);//rozdzielczo�� okna
        int time = 0;//zmienna typu int zliczajaca czas(u�yta to spowolnienia wci�niecia klawiszy)
        string[] PStrzal=new string [4];//tablica string�w przechowujaca nazwy amunicji pokiemon�w
        Animacja over;//obiekt animacji napisu Game Over
        Start start;//obiekt menu startu gry
        Animacja winer;// obiekt animacji napisu Win
        Statek rakieta2;//wywo�anie obiektu "rakiety"
        Song backgroundMusic;//obiekt- d�wiek w tle
        Texture2D backgroundImage,w, o;//za�adowanie tekstury t�a, win i game over
        Pause pause;//obiekt menu pauzy
        List<Pokemony> Pokemon = new List<Pokemony>();//lista obiekt�w typu pokemon
        List<Asteroidy> Asreroidy = new List<Asteroidy>();//lista obiekt�w typu asteroida
        int szybkosc;//zmienna sterujaca szybko�cia poruszania sie w poziomie pokiemon�w
        int rzad = 0;//zmienna zliczajaca rz�d pokiemona (potrzebna do indeksowania tablicy nazw strza��w pokiemon�w)
        int kolumna = 10;//liczba pokiemon�w w rz�dzie
        Tlo animowaneTlo;//obiekt animowanego t�a
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
            //ucupe�nienie tabeli ze stringami nazw strza��w pokemon�w
            PStrzal[0] = "piorun";
            PStrzal[1] = "lisc";
            PStrzal[2] = "ogen";
            PStrzal[3] = "woda";
            //***
            szybkosc = 3;
            start = new Start();
            rakieta2 = new Statek();
            pause = new Pause();
            win = false;
            gameOver = false;
            over = new Animacja();
            winer = new Animacja();
            graphics.PreferredBackBufferWidth = (int)rozdzielczosc.X; //okreslenie szeroko�ci okna
            graphics.PreferredBackBufferHeight = (int)rozdzielczosc.Y; // okre�lenie wysokosci okna
            graphics.IsFullScreen = true; // czy ma by� tryb pe�noekranowy
            graphics.ApplyChanges(); // Zatwierdzenie zmiany
            animowaneTlo = new Tlo();
            base.Initialize();
        }

        protected override void LoadContent()
        {   //uzupe�nienie listy obiekt�w typu pokemon
            Pokemon.Add(new Pokemony(Content.Load<Texture2D>("pikachu"), kolumna, GraphicsDevice.Viewport.Width, 0,4,"PRAWA"));
            Pokemon.Add(new Pokemony(Content.Load<Texture2D>("bulb"), kolumna, GraphicsDevice.Viewport.Width, 1, 3, "LEWA"));
            Pokemon.Add(new Pokemony(Content.Load<Texture2D>("chal"), kolumna, GraphicsDevice.Viewport.Width, 2,2, "PRAWA"));
            Pokemon.Add(new Pokemony(Content.Load<Texture2D>("squirtle"), kolumna, GraphicsDevice.Viewport.Width, 3, 1, "LEWA"));
            //***
            fontpause = Content.Load<SpriteFont>("SpriteFont1");//za�adowanie czcionki
            font1 = Content.Load<SpriteFont>("font1");
            font2 = Content.Load<SpriteFont>("font2");
            animowaneTlo.Load(Content.Load<Texture2D>("pyl"), 1, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);//za�adowanie textury i parametr�w animowanego t�a 
            ((IStatek)rakieta2).LoadStrzal(Content.Load<Texture2D>("pokeball"), 0.1,Content.Load<SoundEffect>("Dstrzal"));//za�adowanie textury i parametr�w pocisk�w statku
            pause.Incjalizuj(Content.Load<Texture2D>("pause"), new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), fontpause);//za�adowanie textury i parametr�w pauzy 
            start.Incjalizuj(Content.Load<Texture2D>("pause"), Content.Load<Texture2D>("Logo"), Content.Load<Texture2D>("strzalka1"), Content.Load<Texture2D>("strzalka2"), Content.Load<Texture2D>("spacja"), Content.Load<Texture2D>("enter"), new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), fontpause);//za�adowanie textury i parametr�w startu
            o = Content.Load<Texture2D>("Gover");//za�adowanie textury game over
            w = Content.Load<Texture2D>("winer");//za�adowanie textury win
            //wywo�anie funkcji inicjujacej animacje dla obiekt�w over i winer
            over.Initialize(o, new Vector2((int)((GraphicsDevice.Viewport.Width - ((o.Width*1.5) / 4)) / 2), (int)((GraphicsDevice.Viewport.Height - (o.Height*1.5)) / 2)), 510, o.Height, 4, 200, Color.GhostWhite, 1.5, true);
            winer.Initialize(w, new Vector2((int)((GraphicsDevice.Viewport.Width - ((w.Width * 1.5) / 4)) / 2), (int)((GraphicsDevice.Viewport.Height - (w.Height * 1.5)) / 2)), 510, w.Height, 4, 200, Color.GhostWhite, 1.5, true);
            //***
            backgroundImage = Content.Load<Texture2D>("t�o");//za�adowanie textury t�a
            backgroundMusic = Content.Load<Song>("Mtlo");//za�adowanie d�wiku t�a
            MediaPlayer.Play(backgroundMusic);//odtwarzaie muzyki
            MediaPlayer.IsRepeating = true;//zap�tlanie muzyki w tle
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //za�adowanie strza��w
            foreach (Pokemony i in Pokemon)
            {
                ((IPokemon)i).LoadStrzal(Content.Load<Texture2D>(PStrzal[rzad]), 0.45, rzad, Content.Load<SoundEffect>("D"+PStrzal[rzad]));
                rzad += 1;
            }
            //***
            //za��dowanie obiektu statekk
            Animacja raketa = new Animacja();
            Texture2D rtexture = Content.Load<Texture2D>("raket");
            raketa.Initialize(rtexture, new Vector2((int)(GraphicsDevice.Viewport.Width / 2), GraphicsDevice.Viewport.Height - 70), 356, 331, 4, 30, Color.GhostWhite, 0.2, true);
            rakieta2.Inicjalizacja(raketa, new Vector2((int)(GraphicsDevice.Viewport.Width / 2), GraphicsDevice.Viewport.Height - 70));
            //za��dowanie obiekt�w asteroidy
            Asreroidy.Add(new Asteroidy(Content.Load<Texture2D>("asteroida"), new Vector2(200, -100), 0.25f, 0.8));
            Asreroidy.Add(new Asteroidy(Content.Load<Texture2D>("asteroida1"), new Vector2(600, -100), 0.15f, 0.5));
            Asreroidy.Add(new Asteroidy(Content.Load<Texture2D>("asteroida2"), new Vector2(-100, -100), 0.20f, 0.3));
            //za��dowanie obiektu punkty
            rakieta2.LoadPunkty(Content.Load<Texture2D>("baner"), Content.Load<SpriteFont>("Tekst"));
        }
        protected override void UnloadContent()
        {  
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            over.Update(gameTime);
            winer.Update(gameTime);
            animowaneTlo.Update();
            KeyboardState keyState = Keyboard.GetState();//wywo�anie zmiennej przechowujac� wcisniety klawisz     
            //zliczanie czasu
            if (time <= 300)
                time += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
            //mechanizm wywo��nia menu startowego
            if (!start.start)
                if (time >= 300) { 
                start.Wybor(time, keyState);
                time = start.times;}
            if (start.wyjdz)
                this.Exit();
            //**
            //mechanizm wywo��nia pauzy
            if (start.start&& !gameOver&&!win) {
            if (keyState.IsKeyDown(Keys.Q) && time >= 300)
                {
                    pause.Pauzuj(time);
                    time = 0;
                }
           if (pause.pause)
                    if (time >= 300)
                    {
                        pause.kontynuuj(time, keyState);
                        time = pause.times;
                    }
            if (pause.wyjdz) {
                    this.Reset();
            } }
            //**

            if (!pause.pause && !gameOver&&start.start)
            {
               foreach (Asteroidy a in Asreroidy)//aktualizacja po�orzenia asteroid
                    a.Update(gameTime, new Vector2(graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height),win,level);
                foreach (Pokemony i in Pokemon)//aktualizacja po�orzenia i strza�u pokemon�w
                {
                    i.Update(gameTime, szybkosc, kolumna, graphics.GraphicsDevice.Viewport.Width);
                    ((IPokemon)i).UpdateStrzal(gameTime,i.Pozycja,kolumna, graphics.GraphicsDevice.Viewport.Height,i.live);
                }
                rakieta2.sterowanie(keyState, graphics.GraphicsDevice.Viewport.Width);//sterowanie rakiet�
                //aktualizacja strza�u
                ((IStatek)rakieta2).UpdateStrzal(keyState, new Rectangle((int)rakieta2.Pozycja.X, (int)rakieta2.Pozycja.Y, rakieta2.Width, rakieta2.Height),gameTime);
                foreach (Pokemony j in Pokemon)//aktualizacja po trafieniu
                        j.live = ((IStatek)rakieta2).Traf(j.Pozycja, j.live,kolumna);
            }
            //sterowanie szybko�ci� pokiemon�w
            int licz = 0;
           foreach (Pokemony j in Pokemon)
                   for (int c = 0; c < kolumna; c++)
                        if (j.live[c] > 0)
                            licz = licz + 1;
           if (licz < ((int)(1.5 * kolumna)))
                    szybkosc=4;//koniec sterowania szybko�ci� pokiemon�w
           if (licz == 0)//wygrywanie
                 
                win = true;
           if (win&& level<10) { 
                czas += (int)gameTime.ElapsedGameTime.Milliseconds;
                
                if (czas > 5000 || keyState.IsKeyDown(Keys.Enter))
                {
                    Level();
                    time = 0;
                }

               }
            if (gameOver||(win && level ==10))
            {
                czas += (int)gameTime.ElapsedGameTime.Milliseconds;

                if (czas > 5000|| keyState.IsKeyDown(Keys.Enter))
                {
                   Reset();
                   time = 0;
                }

            }
            //przegrywanie poczatek
            foreach (Pokemony j in Pokemon)
            {
                if (!gameOver) 
                gameOver=j.GameOver((int)rakieta2.Pozycja.Y,kolumna);
                if (!gameOver)
                    gameOver = ((IPokemon)j).Traf(new Rectangle((int)rakieta2.Pozycja.X + (rakieta2.Width / 2) - 5,(int)rakieta2.Pozycja.Y, 10, rakieta2.Height-10), gameOver);
            }
         foreach (Asteroidy a in Asreroidy)
            if (!gameOver&&!win)
                 gameOver = a.Kill(new Rectangle((int)rakieta2.Pozycja.X,(int)rakieta2.Pozycja.Y, rakieta2.Width,rakieta2.Height));//przegrywanie koniec
         //***
            rakieta2.Update(gameTime);//animacja rakiety
                base.Update(gameTime);
            }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.Draw(backgroundImage, GraphicsDevice.Viewport.TitleSafeArea, Color.GhostWhite);//rysowanie t�a
            animowaneTlo.Draw(spriteBatch);//rysowanie t�a
            start.Draw(spriteBatch);//wyswietlanie menu start
            ((IStatek)rakieta2).DrawStrzal(spriteBatch);//wy�wietlenie strza�u
            foreach (Asteroidy a in Asreroidy)//rysowanie asteroid 
                a.Draw(spriteBatch);
            rakieta2.Draw(spriteBatch);//rysowanie rakiety
            foreach (Pokemony j in Pokemon)//wy�wietlenie pokemon�w i strza��w 
            {
                j.Draw(spriteBatch, GraphicsDevice.Viewport.Width, kolumna, 0);
                ((IPokemon)j).DrawStrzal(spriteBatch);
            }
            if (gameOver)//wy�wietlenie game Over
                over.Draw(spriteBatch);
            if (win && level < 10)
            {
                    spriteBatch.DrawString(font2, "LEVEL " + (level + 1), new Vector2((int)((GraphicsDevice.Viewport.Width - font2.MeasureString("LEVEL " + level).X) / 2), (int)(((GraphicsDevice.Viewport.Height - font2.MeasureString("LEVEL " + level).Y) / 2))), Color.Black);
            spriteBatch.DrawString(font1, "LEVEL " + (level + 1), new Vector2((int)((GraphicsDevice.Viewport.Width - font1.MeasureString("LEVEL " + level).X) / 2), (int)(((GraphicsDevice.Viewport.Height - font1.MeasureString("LEVEL " + level).Y) / 2))), Color.White);
            }
            if (win && level==10)//wy�wietlanie win
                winer.Draw(spriteBatch);
            pause.Draw(spriteBatch);//wy�wietlenie menu pauzy
            if(start.start)
            rakieta2.DrawPoint(spriteBatch);//wy�wietlenie punkt�w
            spriteBatch.End();
            base.Draw(gameTime);
        }
        void Reset()//funkcja restartujaca 
        {
            rzad = 0;
            foreach (Asteroidy i in Asreroidy)
                i.Reset();
            foreach (Pokemony i in Pokemon) { 
                i.Reset(kolumna);
                ((IPokemon)i).LoadStrzal(Content.Load<Texture2D>(PStrzal[rzad]), 0.45, rzad, Content.Load<SoundEffect>("D" + PStrzal[rzad]));
                rzad += 1;}
            szybkosc = 3;
            rakieta2.Reset();
            ((IStatek)rakieta2).LoadStrzal(Content.Load<Texture2D>("pokeball"), 0.1, Content.Load<SoundEffect>("Dstrzal"));
            win =gameOver=false;
            start.start = false;
            pause.wyjdz = false;
            czas = 0;
            level = 1;

        }
        void Level()//funkcja restartujaca 
        {level += 1;    
            rzad = 0;
            foreach (Asteroidy i in Asreroidy)
                i.Reset();
            foreach (Pokemony i in Pokemon)
            {
                i.Level(kolumna,level);
                ((IPokemon)i).LoadStrzal(Content.Load<Texture2D>(PStrzal[rzad]), 0.45, rzad, Content.Load<SoundEffect>("D" + PStrzal[rzad]));
                rzad += 1;
            }
            szybkosc = 3;
            ((IStatek)rakieta2).LoadStrzal(Content.Load<Texture2D>("pokeball"), 0.1, Content.Load<SoundEffect>("Dstrzal"));
            win = gameOver = false;
            pause.wyjdz = false;
            czas = 0;
            

        }
    }
}
