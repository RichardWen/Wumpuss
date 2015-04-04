
namespace Testing_wumpus_classes
{
    class Room
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
}
