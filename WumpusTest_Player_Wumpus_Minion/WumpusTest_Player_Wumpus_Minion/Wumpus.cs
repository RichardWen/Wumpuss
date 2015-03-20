using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WumpusTest_Player_Wumpus_Minion
{
    class Wumpus
    {
        private int position, direction, turns;
        private String state;
        private Boolean seesPlayer;
        private Random rand;

        public Wumpus(int startingPosition, int startingDirection)
        {
            position = startingPosition;
            direction = startingDirection;
            turns = 0;
            state = "asleep";
        }

        public void moveInDirection(int moveDirection)
        {
            // Call method in Map object to see if moving that direction is possible:

            /* int newPosition = _Map.calculateMovement(position, moveDirection); */

            // If possible, update the class:

            /* if(newPosition != -1)
             * {
             *      position = newPosition;
             *      direction = moveDirection;
             * }
             */
        }

        public void addTurn()
        {
            turns++;
        }

        public void updateState()
        {
            rand = new Random();
            if(state.Equals("asleep") && turns > 5)
            {
                if(rand.Next(0, 4) == 0 || turns == 10)
                {
                    state = "awake";
                    turns = 0;
                }
            }
            else
            {
                if(turns == 4)
                {
                    state = "asleep";
                    turns = 0;
                }
                else
                {
                    int attemptedDirection = rand.Next(0, 6);
                    /* int attemptedPosition = calculateMovement(position, attemptedDirection); */
                    /* if(attemptedPosition != -1)
                    {
                        position = attemptedPosition;
                    } */
                }
            }
        }

        public Boolean checkVisible(int checkPosition)
        {
            // To be implemented.
            return true;
        }
    }
}
