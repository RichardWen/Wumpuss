using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WumpusTest_Player_Wumpus_Minion
{
    class Minion
    {
        private int position, direction, turns;
        private String state;
        private Boolean seesPlayer;
        private Random rand;

        public Minion(int startingPosition, int startingDirection)
        {
            position = startingPosition;
            direction = startingDirection;
            turns = 0;
            state = "roaming";
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
            /* seesPlayer = checkVisible(Player.getPosition()); */
            if(state.Equals("roaming"))
            {
                if(turns % 2 == 0)
                {
                    int attemptedDirection = rand.Next(0, 6);
                    /* int attemptedPosition = calculateMovement(position, attemptedDirection); */
                    /* if(attemptedPosition != -1)
                    {
                        position = attemptedPosition;
                    } */
                }

                if(seesPlayer)
                {
                    state = "hunting";
                    turns = 0;
                }
            }
            if(state.Equals("hunting"))
            {
                if(turns == 6)
                {
                    state = "roaming";
                    turns = 0;
                }
                if(seesPlayer)
                {
                    // To be implemented.
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
