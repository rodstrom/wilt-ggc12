using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using RunnerAlpha.Code.Entities;

/* Kollisionen fungerar som sådan:
 * 
 * Alla entiteter har två eller flera texturer.
 * En är den som visas, den andra är texturen som används för kollisionshanteringen.
 * 
 * Två entiteter tas in via BasicCheck, i detta fall är det en Player och en Platform.
 * Skär dess SourceRectangles varandra kallas AdvancedCheck automatiskt.
 * I AdvancedCheck skapas en ny rektangel som motsvarar den nya gemensamma ytan.
 * Inom denna rektangel jämförs alla pixlar, och är båda pixlar på samma koordinat röda..
 * BINGO! Kollision!
 * 
 * Beräkningen av vilken sida kollisionen ägde rum görs via beräkning av en vinkel.
 * Vinkeln är mellan den gemensamma rektangelns och plattformens centrum
*/

namespace RunnerAlpha.Code.Physics
{
    public enum Side
    {
        None = 0x00,
        Top = 0x01,
        Bottom = 0x02,
        Left = 0x04,
        Right = 0x08,
    };

    public class Collision
    {
        public Collision()
        {
        }

        public bool BasicCheck(Entity entity1, Entity entity2)
        {
            if (entity1.SourceRectangle.Intersects(entity2.SourceRectangle))
            {
                return AdvancedCheck(entity1, entity2);
            }
            return false;
        }

        public bool AdvancedCheck(Entity entity1, Entity entity2)
        {
            Rectangle midRect = Rectangle.Intersect(entity1.SourceRectangle, entity2.SourceRectangle);

            for (int x = 0; x < midRect.Width; x++)
            {
                for (int y = 0; y < midRect.Height; y++)
                {
                    Point pos1 = new Point(midRect.Location.X - entity1.CollisionRectangle.Location.X,
                        midRect.Location.Y - entity1.CollisionRectangle.Location.Y);
                    Point pos2 = new Point(midRect.Location.X - entity2.CollisionRectangle.Location.X,
                        midRect.Location.Y - entity2.CollisionRectangle.Location.Y);

                    if (entity1.ColorData[x + pos1.X, y + pos1.Y].R > 200 &&
                        entity2.ColorData[x + pos2.X, y + pos2.Y].R > 200)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public Side GetSidesCollided(Entity entity1, Entity entity2)
        {
            Rectangle midRect = Rectangle.Intersect(entity1.SourceRectangle, entity2.SourceRectangle);

            float R = MathHelper.ToDegrees((float)Math.Atan2(entity2.CollisionRectangle.Bottom - midRect.Center.Y, 
               entity2.CollisionRectangle.Center.X - midRect.Center.X));

            Vector2 K1 = new Vector2(entity2.SourceRectangle.Center.X, entity2.SourceRectangle.Bottom);
            Vector2 K2 = new Vector2(entity2.SourceRectangle.Left, entity2.SourceRectangle.Top);
            Vector2 K3 = new Vector2(entity2.SourceRectangle.Right, entity2.SourceRectangle.Top);

            float R1 = MathHelper.ToDegrees((float)Math.Atan2(K1.Y - K2.Y, K1.X - K2.X));
            float R2 = MathHelper.ToDegrees((float)Math.Atan2(K1.Y - K3.Y, K1.X - K3.X));
            
            Side returnVal = Side.None;

            if (R > R1 && R < R2)
            {
                returnVal = (returnVal | Side.Top);
            }

            //Left
            if (R < R1 && R > R2)
            {
                returnVal = (returnVal | Side.Left);
            }

            ////Right
            //if (R < 135 && R > 45)
            //{
            //    returnVal = (returnVal | Side.Right);
            //}

            ////Bottom
            //if (R < 225 && R > 135)
            //{
            //    returnVal = (returnVal | Side.Bottom);
            //}

            return returnVal;
        } 
    }
}
