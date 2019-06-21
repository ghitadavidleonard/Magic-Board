using System.Windows.Controls;
using System.Windows.Threading;

namespace MagicBoard
{
    public class Player : INamable
    {
        public string Name { get; private set; }
        public Square Location { get; private set; }
        
        public Player(string name, Square location)
        {
            Name = name;
            Location = location;
        }
        
        public int GetPlayerLocation()
        {
            return Location.Index;
        }

        public void UpdateLocation(Square newLocation, int typeOfGame)
        {
            if (typeOfGame == 1)
            {
                PathGameType.main.DeleteDictionaryEntry(this.Name);
                Location = newLocation;
                PathGameType.main.LocationUpdate(newLocation.Index, this.Name);
            }
            else if(typeOfGame == 2)
            {
                CircularGameType.main.DeleteDictionaryEntry(this.Name);
                Location = newLocation;
                CircularGameType.main.LocationUpdate(newLocation.Index, this.Name);
            }
        }

        public virtual void Input()
        {

        }

    }

}
