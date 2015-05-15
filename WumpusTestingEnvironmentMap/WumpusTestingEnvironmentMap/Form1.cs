using System;               
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WumpusTestingEnvironmentMap
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {

            Map mapInstance = new Map(5, 5, 6, 6, 100);
            mapInstance.generate();
            mapInstance.addDoors();
            Room startingRoom = mapInstance.getMap()[2, 2];
            Player playerInstance = new Player(startingRoom, 3, 0, 0, mapInstance);
         
            switch (keyData)
            {
                case Keys.Space:
                    Start(mapInstance, startingRoom, playerInstance);
                    return true;
                case Keys.Up:
                    playerMove(playerInstance, mapInstance, 0, true);
                    return true;
                case Keys.Right:
                    playerMove(playerInstance, mapInstance, 1, false);
                    return true;
                case Keys.Left:
                    playerMove(playerInstance, mapInstance, -1, false);
                    return true;
            }
            return false;
        }
        private void Start(Map mapInstance, Room startingRoom, Player playerInstance)
        {   
            System.Drawing.SolidBrush myBrushBlue = new System.Drawing.SolidBrush(System.Drawing.Color.Blue);
            System.Drawing.SolidBrush myBrushRed = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
            System.Drawing.Graphics formGraphics;
            formGraphics = this.CreateGraphics();
            for (int i = 0; i < mapInstance.getMap().GetLength(0); i++)
            {
                for (int j = 0; j < mapInstance.getMap().GetLength(1); j++)
                {
                    if (i % 2 == 0)
                    {
                        if (playerInstance.getPosition().Equals(mapInstance.getMap()[i, j]))
                        {
                            formGraphics.FillRectangle(myBrushBlue, new Rectangle(i * 40 + 2, j * 40 + 2, 34, 34));
                
                        }
                        else
                        {
                            formGraphics.FillRectangle(myBrushRed, new Rectangle(i * 40 + 2, j * 40 + 2, 34, 34));

                        }
                    }
                    else
                    {
                        if (playerInstance.getPosition().Equals(mapInstance.getMap()[i, j]))
                        {
                            formGraphics.FillRectangle(myBrushBlue, new Rectangle(i * 40 + 2, j * 40 + 22, 34, 34));
                        }
                        else
                        {
                            formGraphics.FillRectangle(myBrushRed, new Rectangle(i * 40 + 2, j * 40 + 22, 34, 34));

                        }
                    }
                }
            }
        }
        private void playerMove(Player playerInstance, Map mapInstance, int directionChange, bool isMoving)
        {
            playerInstance.setDirection(playerInstance.getDirection() + directionChange);
            if (isMoving == true)
            {
                playerInstance.moveInDirection(playerInstance.getDirection());
                Start(mapInstance, playerInstance.getPosition(), playerInstance);
            }
        }
    
        }
    }
    public class Player
    {
        private int direction, arrows, coins, turns, score;
        private Map mapInstance;
        private Room position;
        private Room[] visibleRooms;

        public Player(Room startingPosition, int startingDirection, int startingArrows, int startingCoins, Map startingMapInstance)
        {
            // Initializes the starting attributes of Player
            position = startingPosition;
            direction = startingDirection;
            arrows = startingArrows;
            coins = startingCoins;
            mapInstance = startingMapInstance;
            turns = 0;
            score = 0;
            visibleRooms = new Room[12];
        }

        public void moveInDirection(int moveDirection)
        {
            // Call method in Map object to see if moving that direction is possible:
            Room newPosition = mapInstance.calculateMovement(position, moveDirection);
            Console.WriteLine("Room:" + newPosition.number);
            Console.WriteLine("Dir:" + moveDirection);

            // If possible, update the class:
            if (!newPosition.nullRoom)
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

        public Boolean checkVisible(Room checkPosition)
        {
            for (int i = 0; i < 12; i++)
            {
                if (visibleRooms[i] == checkPosition)
                    return true;
            }
            return false;
        }

        public int getDirection()
        {
            return direction;
        }
        public void setDirection(int newDirection)
        {
            direction = newDirection;
        }
        public Room getPosition()
        {
            return position;
        }

        private void updateVisibleRooms()
        {
            Room currentPosition = position;
            int direction1 = direction - 1;
            int direction2 = direction;
            int direction3 = direction + 1;

            if (direction1 < 0)
                direction1 = 5;
            if (direction3 > 5)
                direction3 = 0;

            currentPosition = mapInstance.calculateMovement(position, direction1);
            for (int i = 0; i < 4; i++)
            {
                visibleRooms[i] = currentPosition;
                if (currentPosition != null)
                    currentPosition = mapInstance.calculateMovement(currentPosition, direction1);
            }

            currentPosition = mapInstance.calculateMovement(position, direction2);
            for (int i = 0; i < 4; i++)
            {
                visibleRooms[i + 4] = currentPosition;
                if (currentPosition != null)
                    currentPosition = mapInstance.calculateMovement(currentPosition, direction2);
            }

            currentPosition = mapInstance.calculateMovement(position, direction3);
            for (int i = 0; i < 4; i++)
            {
                visibleRooms[i + 8] = currentPosition;
                if (currentPosition != null)
                    currentPosition = mapInstance.calculateMovement(currentPosition, direction3);
            }
        }
    }

    public class Room
    {
        public int number;
        private int aboveC, aboveR, aboveL, belowC, belowR, belowL;
        private int doors;
        private bool isAccessible = false;
        public bool nullRoom = false;
        private bool[] direction = new bool[6];
        public Room(int num)
        {
            number = num;
            doors = 0;
        }

        public Room(bool isNull)
        {
            nullRoom = isNull;
        }

        public void setAdj(int door, int adj)
        {
            if (door == 0)
            {
                aboveR = adj;
            }
            else if (door == 1)
            {
                belowR = adj;
            }
            else if (door == 2)
            {
                belowC = adj;
            }
            else if (door == 3)
            {
                belowL = adj;
            }
            else if (door == 4)
            {
                aboveL = adj;
            }
            else if (door == 5)
            {
                aboveC = adj;
            }
        }

        public int getAdj(int direction)
        {
            if (direction == 0)
            {
                return aboveR;
            }
            else if (direction == 1)
            {
                return belowR;
            }
            else if (direction == 2)
            {
                return belowC;
            }
            else if (direction == 3)
            {
                return belowL;
            }
            else if (direction == 4)
            {
                return aboveL;
            }
            else
            {
                return aboveC;
            }
        }
        public int getRoomnumber()
        {
            return number;
        }
        public string getDoors(string text)
        {
            return number + "\n" + "Room " + number + " has " + doors + " door(s)" + "\n" + "It is adjacent to rooms " + aboveR + ", " + belowR + ", " + belowC + ", " + belowL + ", " + aboveL + ", and " + aboveC + "\n" + "Accessibility = " + this.isAccess() + "\n";
        }

        public string listDoors()
        {
            string doorList = "";
            for (int i = 0; i < 6; i++)
            {
                if (direction[i])
                {
                    doorList += this.getAdj(i) + "  ";
                }
            }
            return doorList;
        }

        public int getDoors()
        {
            return doors;
        }

        public void addDoor(int dir)
        {
            direction[dir] = true;
            doors++;
        }

        public bool isAccess()
        {
            return isAccessible;
        }

        public void setAccess(bool acc)
        {
            isAccessible = acc;
        }

        public bool checkDoor(int dir)
        {
            return direction[dir];
        }


    }
       public class Map
    {
        int count, rows, columns, doorsPerRoom, doorsMinPerRoom, maxTries;
        Room[,] rooms;
        public Map(int height, int length, int doorsMin, int doorsMax, int tries)
        {
            count = length * height;
            rows = height;
            columns = length;
            rooms = new Room[rows, columns];
            doorsPerRoom = doorsMax;
            doorsMinPerRoom = doorsMin;
            maxTries = tries;
        }

        //======================================================================================================================
        //
        //  Generate Method
        //
        //======================================================================================================================

        public void generate()
        {
            //
            //
            //  A method that creates the 2d array of rooms, and sets the values for adjacent rooms for all of them
            //
            //
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    int roomNum = columns * r + c;
                    rooms[r, c] = new Room(roomNum);
                    int adjNum = roomNum;
                    adjNum -= columns;
                    if (adjNum < 0)         //gets the number of the room 1 row up, if it is negative it adds the total number to get the last row
                    {
                        adjNum += count;
                    }
                    rooms[r, c].setAdj(5, adjNum);  //Sets that num as the room above the current one

                    if (c % 2 != 0)         //If it is in an odd row, with room 0 being row 0 col 0, it moves a row down before continuing, to adjust for the alternating heights pattern
                    {
                        adjNum += columns;
                    }
                    if (c == columns - 1)   // If it is in the last column, it subtracts the goes up a column to adjust for wrap-around weirdness
                    {
                        adjNum -= columns;
                    }
                    adjNum++;               //
                    if (adjNum < 0)         //
                    {                       //
                        adjNum += count;    //
                    }                       //      Misc adjustments
                    if (adjNum >= count)    //
                    {                       //
                        adjNum -= count;    //
                    }
                    rooms[r, c].setAdj(0, adjNum); //Sets that num as the room above and to the right of the current one

                    adjNum += columns;      // Goes down a column
                    if (adjNum >= count)
                    {
                        adjNum -= count;
                    }
                    rooms[r, c].setAdj(1, adjNum); //Sets that num as the room below and to the right of the current one

                    adjNum = roomNum;
                    adjNum += columns;
                    if (adjNum >= count)
                    {
                        adjNum -= count;
                    }
                    rooms[r, c].setAdj(2, adjNum);

                    if (c % 2 == 0 && c != 0)
                    {
                        adjNum -= columns;
                    }
                    adjNum--;
                    if (adjNum < 0)
                    {
                        adjNum += count;
                    }
                    rooms[r, c].setAdj(3, adjNum);

                    adjNum -= columns;
                    if (adjNum < 0)
                    {
                        adjNum += count;
                    }
                    rooms[r, c].setAdj(4, adjNum);
                }
            }
        }

        //======================================================================================================================
        //
        //  Scramble Method
        //
        //======================================================================================================================

        private int[] scramble()
        {
            //
            //  A method that creates a list of room numbers in a random order
            //      Doing so prevents "grouping" where the doors cluster together
            //      Also adds more randomness to the map generation
            //
            Random rand = new Random();
            int[] order = new int[rows * columns];
            List<int> numbers = new List<int>();
            for (int i = 0; i < rows * columns; i++)
            {
                numbers.Add(i);
            }
            int remaining = order.Length;

            for (int i = 0; i < rows * columns; i++)
            {

                int pos = rand.Next(0, remaining);
                order[i] = numbers[pos];
                numbers.RemoveAt(pos);
                remaining--;
            }
            return order;
        }

        //======================================================================================================================
        //
        //  Add Doors Method
        //
        //======================================================================================================================

        public void addDoors()
        {
            //
            //
            //  This is the true "map generation", it uses the array from scramble to go through each room and add doors
            //  until said room has an acceptable number of doors (At least 1, maximum is modifiable in the constructor)
            //
            //
            int[] order = scramble();
            Random rand = new Random();
            for (int i = 0; i < order.Length; i++)
            {
                int targetNum = rand.Next(doorsMinPerRoom, doorsPerRoom + 1);  //Picks how many doors it wants the room to have
                int rowNum = order[i] / columns;
                int colNum = order[i] - rowNum * columns;
                int tries = 0;
                while (rooms[rowNum, colNum].getDoors() < targetNum || rooms[rowNum, colNum].getDoors() == 0)
                {
                    int randNum = rand.Next(0, 6);
                    int adjNum = rooms[rowNum, colNum].getAdj(randNum);     //Picks a random direction and gets that room
                    int adjCol = adjNum % columns;
                    int adjRow = adjNum / columns;
                    tries++;
                    if (tries > maxTries && rooms[rowNum, colNum].getDoors() > 0)  //Checks if the room has too many doors
                    {
                        targetNum = 0;                                      //If so, and it has tried 3 times, exits loop
                    }
                    if (rooms[adjRow, adjCol].getDoors() < doorsPerRoom)    //If not adds a door to BOTH rooms
                    {
                        rooms[rowNum, colNum].addDoor(randNum);
                        if (randNum < 3)
                        {
                            randNum += 6;
                        }
                        rooms[adjRow, adjCol].addDoor(randNum - 3);
                    }
                }
            }
            //for (int i = 0; i < order.Length; i++)
            //{
            //    Console.Out.WriteLine(order[i]);      For debugging purposes
            //}
        }
        public Room[,] getMap()
        {
            return rooms;
        }

        public void print()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Console.Out.WriteLine(rooms[i, j].getDoors("text"));
                }

            }
        }

        public bool checkDoorsPerRoom()
        {
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    if (rooms[r, c].getDoors() < 1 || rooms[r, c].getDoors() > doorsPerRoom)
                    {
                        return false;
                    }
                }
            }
            return true;
        }



        public void checkAccessibility(int startingRoom)
        {
            int roomRow = startingRoom / columns;
            int roomCol = startingRoom % columns;
            if (rooms[roomRow, roomCol].isAccess())
            {

            }
            else
            {
                rooms[roomRow, roomCol].setAccess(true);
                for (int dir = 0; dir < 6; dir++)
                {
                    if (rooms[roomRow, roomCol].checkDoor(dir))
                    {
                        checkAccessibility(rooms[roomRow, roomCol].getAdj(dir));
                    }
                }
            }

        }

        public void unaccess()
        {
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    rooms[r, c].setAccess(false);
                }
            }

        }


        public void fixDoors()
        {
            Random rand = new Random();
            int roomRow, roomCol;
            for (int room = 0; room < rows * columns; room++)
            {
                roomRow = room / columns;
                roomCol = room % columns;
                if (!rooms[roomRow, roomCol].isAccess())
                {
                    int dir = rand.Next(0, 6);
                    int adjNum = rooms[roomRow, roomCol].getAdj(dir);
                    int adjRow = adjNum / columns;
                    int adjCol = adjNum % columns;
                    int tries = 0;
                    while ((rooms[adjRow, adjCol].getDoors() >= doorsPerRoom && tries < 5) || !rooms[adjRow, adjCol].isAccess())
                    {
                        dir = rand.Next(0, 6);
                        adjNum = rooms[roomRow, roomCol].getAdj(dir);
                        adjRow = adjNum / columns;
                        adjCol = adjNum % columns;
                        tries++;
                    }
                    rooms[roomRow, roomCol].addDoor(dir);
                    dir -= 3;
                    if (dir < 0)
                    {
                        dir += 6;
                    }
                    rooms[adjRow, adjCol].addDoor(dir);

                }
            }
            this.unaccess();
        }

        public Room calculateMovement(Room room, int direction)
        {
            int roomRow = room.getRoomnumber() / columns;
            int roomCol = room.getRoomnumber() % columns;
            if (rooms[roomRow, roomCol].checkDoor(direction))
            {
                int adjNum = rooms[roomRow, roomCol].getAdj(direction);
                int adjRow = adjNum / columns;
                int adjCol = adjNum % columns;
                return rooms[adjRow, adjCol];
            }
            else
            {
                Room nullRoom = new Room(true);
                return nullRoom;
            }
        }
    }



