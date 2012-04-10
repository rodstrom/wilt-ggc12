using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RunnerAlpha.Code.States
{
    class StateManager
    {
        Runner game;

        List<State> states;
        State currentState;
        int currentStateNumber = 0;

        public StateManager(Runner game)
        {
            this.game = game;

            states = new List<State>();
            states.Add(new PlayState(game, "PlayState"));
            states.Add(new HighScoreState(game, "HighScoreState"));

            currentState = SelectState("PlayState");
        }

        public State SelectState(String name)
        {
            for (int i = 0; i < states.Count; i++)
            {
                if (states[i].ID == name)
                {
                    currentStateNumber = i;
                    return states[i];
                }
            }
            return null;
        }

        public void NextState()
        {
            currentStateNumber++;
            if (currentStateNumber >= states.Count)
            {
                currentStateNumber = 0;
            }
            currentState = states[currentStateNumber];
        }

        public void Update(GameTime gameTime)
        {
            currentState.Update(gameTime);
        }

        public void Draw()
        {
            currentState.Draw();
        }
    }
}
