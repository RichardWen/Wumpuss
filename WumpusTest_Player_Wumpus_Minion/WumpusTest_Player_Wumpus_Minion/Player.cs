using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WumpusTest_Player_Wumpus_Minion
{
    class Player
    {
        private int position, direction, arrows, coins, turns, score;
        private Map mapInstance;
        private int[] visibleRooms;

        public Player(int startingPosition, int startingDirection, int startingArrows, int startingCoins, int startingMapInstance)
        {
            // Initializes the starting attributes of Player
            position = startingPosition;
            direction = startingDirection;
            arrows = startingArrows;
            coins = startingCoins;
            mapInstance = startingMapInstance;
            turns = 0;
            score = 0;
            visibleRooms = new int[12];
        }

        public void moveInDirection(int moveDirection)
        {
            // Call method in Map object to see if moving that direction is possible:
            int newPosition = mapInstance.calculateMovement(position, moveDirection);

            // If possible, update the class:
            if(newPosition != -1)
             {
                 position = newPosition;
                 direction = moveDirection;
             }
             
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
            for(int i = 0; i < 12; i++)
            {
                if (visibleRooms[i] == checkPosition)
                    return true;
            }
            return false;
        }

        private void updateVisibleRooms()
        {
            int currentPosition = position;
            int direction1 = direction - 1;
            int direction2 = direction;
            int direction3 = direction + 1;

            if (direction1 < 0)
                direction1 = 5;
            if (direction3 > 5)
                direction3 = 0;

            currentPosition = mapInstance.calculateMovement(position, direction1);
            for(int i = 0; i < 4; i++)
            {
                visibleRooms[i] = currentPosition;
                if (currentPosition != -1)
                    currentPosition = mapInstance.calculateMovement(currentPosition, direction1);
            }

            currentPosition = mapInstance.calculateMovement(position, direction2);
            for (int i = 0; i < 4; i++)
            {
                visibleRooms[i+4] = currentPosition;
                if (currentPosition != -1)
                    currentPosition = mapInstance.calculateMovement(currentPosition, direction2);
            }

            currentPosition = mapInstance.calculateMovement(position, direction3);
            for (int i = 0; i < 4; i++)
            {
                visibleRooms[i+8] = currentPosition;
                if (currentPosition != -1)
                    currentPosition = mapInstance.calculateMovement(currentPosition, direction3);
            }
        }
    }
}
