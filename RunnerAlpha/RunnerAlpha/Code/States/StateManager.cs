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
            foreach (State s in states)
            {
                if (s.ID == name)
                {
                    return s;
                }
            }
            return null;
        }

        public void Update(GameTime gameTime)
        {
            currentState.Update(gameTime);

            if (currentState.changeState)
            {
                String nextState = currentState.nextState;
                int score = -1;

                if (currentState.ID == "PlayState")
                {
                    states[0] = null;
                    states[0] = new PlayState(game, "PlayState");
                    score = currentState.outputCode;
                }

                currentState.Terminate();

                currentState = SelectState(nextState);
                currentState.inputCode = score;

            }
        }

        public void Draw()
        {
            currentState.Draw();
        }
    }
}
