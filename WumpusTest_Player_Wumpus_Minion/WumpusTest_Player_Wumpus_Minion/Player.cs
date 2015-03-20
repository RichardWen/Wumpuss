using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WumpusTest_Player_Wumpus_Minion
{
    class Player
    {
        private int  position, direction, arrows, coins, turns, score;

        public Player(int startingPosition, int startingDirection, int startingArrows, int startingCoins)
        {
            position = startingPosition;
            direction = startingDirection;
            arrows = startingArrows;
            coins = startingCoins;
            turns = 0;
            score = 0;
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

        public void changeCoins(int newCoins)
        {
            coins += newCoins;
        }

        public void changeArrows(int newArrows)
        {
            arrows += newArrows;
        }

        public void addTurn()
        {
            turns++;
        }

        public int updateScore()
        {
            score = 100 - turns + coins + (10 * arrows);
            return score;
        }

        public Boolean checkVisible(int checkPosition)
        {
            // To be implemented.
            return true;
        }
    }
}
