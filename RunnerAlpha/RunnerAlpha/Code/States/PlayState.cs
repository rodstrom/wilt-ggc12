using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RunnerAlpha.Code.Graphics;
using RunnerAlpha.Code.Entities;
using RunnerAlpha.Code.Time;
using RunnerAlpha.Code.Input;

namespace RunnerAlpha.Code.States
{
    class PlayState : State
    {
        EntityManager entityManager;

        Timer timer = null;
        
        Texture2D background;

        int score = 0;

        bool pause = false;
        int pauseSelectedItem = 0;

        public PlayState(Runner game, String id)
            : base(game, id)
        {
            entityManager = new EntityManager(game, spriteBatch);

            nextState = "HighScoreState";

            timer = new Timer(game);
            timer.StartTimer();

            background = game.Content.Load<Texture2D>(@"Graphics\Background");
        }

        public override void Terminate()
        {
            entityManager.Terminate();
            timer.ResetTimer();
            score = 0;
            changeState = false;
        }

        public override void Update(GameTime gameTime)
        {
            inputManager.Update();

            if (inputManager.Pause)
            {
                pause = !pause;
            }

            if (entityManager.player.win)
            {
                outputCode = score;
                changeState = true;
            }
            else if (entityManager.player.lose)
            {
                outputCode = -1;
                changeState = true;
            }

            if (!pause)
            {
                timer.Update(gameTime);
                score = timer.ToInteger("s_total");

                entityManager.Update(gameTime);
            }
            else
            {
                SelectionScreen();
            }
        }

        private void SelectionScreen()
        {
            if (inputManager.LeftOnce || inputManager.RightOnce)
            {
                if (pauseSelectedItem == 0)
                {
                    pauseSelectedItem = 1;
                }
                else
                {
                    pauseSelectedItem = 0;
                }
            }
            if (inputManager.Select)
            {
                if (pauseSelectedItem == 0)
                {
                    pause = false;
                }
                else
                {
                    changeState = true;
                }
            }
        }

        public override void Draw()
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, null, null, null, null, Resolution.getTransformationMatrix());

            spriteBatch.Draw(background, new Vector2(0, 0), Color.White);
            int time = (int)timer.MainEvent.currentTime;
            spriteBatch.DrawString(font, (time / 1000).ToString(), new Vector2(100, 100), Color.Red);

            entityManager.Draw();

            if (pause)
            {
                drawPauseDialog();
            }

            spriteBatch.End();
        }

        private void drawPauseDialog()
        {
            Texture2D dialog = new Texture2D(game.graphics.GraphicsDevice, 500, 220); 
            Color[] data = new Color[500 * 220];
            for (int i = 0; i < data.Length; ++i) data[i] = Color.Black;
            dialog.SetData(data);
            Vector2 pos = new Vector2(Runner.WIDTH / 2 - 250, Runner.HEIGHT / 2 - 110);
            spriteBatch.Draw(dialog, pos, Color.White);

            Texture2D selection = new Texture2D(game.graphics.GraphicsDevice, 200, 80);
            data = new Color[200 * 80];
            for (int i = 0; i < data.Length; ++i) data[i] = Color.Gray;
            selection.SetData(data);
            pos = new Vector2(Runner.WIDTH / 2 - 220 + 240 * pauseSelectedItem, Runner.HEIGHT / 2 + 5);
            spriteBatch.Draw(selection, pos, Color.White);

            String dialogText = "Giving up?";
            Vector2 origin = font.MeasureString(dialogText);
            origin.X /= 2;
            pos = new Vector2(Runner.WIDTH / 2, Runner.HEIGHT / 2 - 20);
            spriteBatch.DrawString(font, dialogText, pos, Color.White, 0f, origin, 1f, SpriteEffects.None, 1f);

            dialogText = "Fuck no!";
            origin = font.MeasureString(dialogText) / 2;
            pos = new Vector2(Runner.WIDTH / 2 - 125, Runner.HEIGHT / 2 + 45);
            spriteBatch.DrawString(font, dialogText, pos, Color.White, 0f, origin, 1f, SpriteEffects.None, 1f);

            dialogText = "Yeah...";
            origin = font.MeasureString(dialogText) / 2;
            pos = new Vector2(Runner.WIDTH / 2 + 125, Runner.HEIGHT / 2 + 45);
            spriteBatch.DrawString(font, dialogText, pos, Color.White, 0f, origin, 1f, SpriteEffects.None, 1f);
        }
    }
}
