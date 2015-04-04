using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing_wumpus_classes
{
    class MapRewrite
    {
        int count, rows, columns, doorsPerRoom;
        Room[,] rooms;
        public MapRewrite(int height, int length, int doors)
        {
            count = length * height;
            rows = height;
            columns = length;
            rooms = new Room[rows, columns];
            doorsPerRoom = doors;
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
                int targetNum = rand.Next(1, doorsPerRoom + 1);  //Picks how many doors it wants the room to have
                int rowNum = order[i] / columns;
                int colNum = order[i] - rowNum * columns;
                int tries = 0;
                while (rooms[rowNum, colNum].getDoors() < targetNum || rooms[rowNum,colNum].getDoors() == 0)
                {
                    int randNum = rand.Next(0, 6);
                    int adjNum = rooms[rowNum, colNum].getAdj(randNum);     //Picks a random direction and gets that room
                    int adjCol = adjNum % columns;
                    int adjRow = adjNum / columns;
                    tries++;
                    if (tries > 3 && rooms[rowNum, colNum].getDoors() > 0)  //Checks if the room has too many doors
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
                    if (rooms[r,c].getDoors() < 1 || rooms[r,c].getDoors() > doorsPerRoom)
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
                    if (rooms[roomRow,roomCol].checkDoor(dir))
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
                    while ((rooms[adjRow, adjCol].getDoors() >= doorsPerRoom && tries < 5) || !rooms[adjRow,adjCol].isAccess())
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

        public Room calculateMovement(int roomNum, int direction)
        {
            int roomRow = roomNum / columns;
            int roomCol = roomNum % columns;
            if (rooms[roomRow, roomCol].checkDoor(direction))
            {
                int adjNum = rooms[roomRow, roomCol].getAdj(direction);
                int adjRow = adjNum / columns;
                int adjCol = adjNum % columns;
                return rooms[adjRow, adjCol];
            }
            else
            {
                Room nullRoom = new Room(false);
                return nullRoom;
            }
        }
    }
}
