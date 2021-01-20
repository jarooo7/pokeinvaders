using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace WindowsGame2
{
    class Punkty
    {   Rectangle pozycja;//pozycja baneru
        Texture2D textura;//tekstura baneru punktów
        Vector2 pozycjaText;//pozycja licznika 
        SpriteFont font;//czcionka
        protected int point;//punkty
        public void LoadPunkty(Texture2D textura, SpriteFont font)//funkcja uzupełniajaca dane
        {
            this.textura= textura;
            this.pozycja = new Rectangle(0, 0, textura.Width, 70);
            this.font = font;
            pozycjaText = new Vector2(10, 10);
            point = 0;
        }
        public void DrawPoint(SpriteBatch spriteBatch)//funkcja wyświetlajaca
        {
            spriteBatch.Draw(textura, pozycja, Color.GhostWhite);
            spriteBatch.DrawString(font,"Punkty : "+point, pozycjaText, Color.GhostWhite);
        }
    }
}
