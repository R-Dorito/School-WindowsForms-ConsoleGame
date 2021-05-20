using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Iteration
{
    public class MapLoader
    {
        private const string FILENAME = "Map.txt";

        public Dictionary<string, dynamic> GameList { get; }


        private Player _player;

        public MapLoader(Player player)
        {
            GameList = new Dictionary<string, dynamic>();

            _player = player;
            LoadMap();
        }

        public string ListAllGameItems()
        {
            string allItems = "Game Items:\n";
            foreach (KeyValuePair<string, dynamic> key in GameList)
            {
                allItems += $"{key.Value.Name}: {key.Value.FullDescription}\n";
            }
            return allItems;
        }

        private void LoadMap()
        {
            StreamReader reader = new StreamReader(FILENAME);
            try
            {
                reader = new StreamReader(FILENAME);
                string input;
                while ((input = reader.ReadLine()) != "end")
                {

                    string[] readData = input.Split('|');
                    switch (readData[0])
                    {
                        case "Location":
                            Locations location = new Locations(readData[1], readData[2]);
                            GameList.Add(readData[1], location);
                            break;
                        case "Item":
                            string[] _readItemIds = readData[1].Split(',');

                            Item item = new Item(_readItemIds, readData[2], readData[3]);
                            GameList.Add(_readItemIds[0], item);
                            break;

                        case "Bag":
                            string[] _readBagIds = readData[1].Split(',');
                            Bag bag = new Bag(_readBagIds, readData[2], readData[3]);
                            GameList.Add(_readBagIds[0], bag);
                            break;

                        case "Path":
                            string[] _readPathIds = readData[1].Split(',');
                            Locations destination = GetDictonaryItem(readData[4]);
                            if (destination != null)
                            {
                                Path path = new Path(_readPathIds, readData[2], readData[3], destination);
                                //path to this location.
                                GameList.Add(readData[2], path); // key is the path name
                            }
                            break;

                        case "Command": // commands should come last in the file reading.
                            switch (readData[1])
                            {
                                case "Add":
                                    dynamic container = GetDictonaryItem(readData[2]); //may return null
                                    Item itemToBePut = GetDictonaryItem(readData[3]);

                                    if(container == null || itemToBePut == null)
                                    {
                                        break;
                                    }
                                    container.Inventory.Put(itemToBePut);
                                    break;

                                case "Path":
                                    Path pathID = GetDictonaryItem(readData[2]); //getLocation
                                    Locations startLoc = GetDictonaryItem(readData[3]);
                                    if (pathID == null || startLoc == null)
                                    {
                                        break;
                                    }
                                    startLoc.MakePathConnection(pathID);
                                    break;
                            }
                            break;
                    }
                }
            }
            catch
            {
                Console.WriteLine("Error in file read");
                reader.Close();

            }
            finally
            {
                reader.Close();
            }
        }
        private dynamic GetDictonaryItem(string ContainerKey)
        {
            if (ContainerKey == "Player") 
                return _player;
            try
            {
                dynamic container = GameList[ContainerKey];
                return container;
            }
            catch
            {
                return null;
            }
        }
    }
}

/*data is saved like this:
Location|Dining Room|a special place to eat
Item|shovel|a shovel| a lovely shovel
Path|North|a long path|a long path to somewhere|destination

Location|name|description
Used like:
List<Location> _location;...
_locations.add(new Location(input[1],input[2]));
*/

/*
LoadCommands:
player.Inventory.Put(itemTest1);
player.MyLocation = loc1;
loc1.MakePathConnection(path1);

Command|Add|container|item
Command|Inventory|player|shovel
Command|Inventory|bag|shovel

Command|Path|path name|starting point
findObject(input[1]).MakePathConnection(findObject(input[2]));
*/


// return item type? request location
// Command|add|roomName|book
// Command|add|Player|book
// Command|add|bagName|book
// [0]   |[1]|[2]|[3]|[4]
// [2] = Item type
// [3] = Item name/Id
// room.Inventory.Put(book);
// player.Inventory.Put(book);
// bag.Inventory.Put(book);

