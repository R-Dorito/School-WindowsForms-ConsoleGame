using System;
using System.Collections.Generic;
using System.Text;

namespace Iteration
{
    public class Command_Processor: Command
    {
        private Command _myCommand;
        public Command_Processor(): base(new string[] { "command_processor" })
        {

        }

        public override string Execute(Player p, string[] text)
        {

            for (int i = 0; i < text.Length; i++)
            {
                text[i] = text[i].ToLower();
            }

            switch (text[0])
            {

                case ("look"):
                    _myCommand = new CommandLook();
                    break;
                case ("move"):
                    _myCommand = new CommandMove();
                    break;
                case ("go"):
                    _myCommand = new CommandMove();
                    break;
                case ("head"):
                    _myCommand = new CommandMove();
                    break;
                case ("leave"):
                    _myCommand = new CommandMove();
                    break;
                case ("put"):
                    _myCommand = new CommandPut();
                    break;
                case ("drop"):
                    _myCommand = new CommandPut();
                    break;
                case ("take"):
                    _myCommand = new CommandTake();
                    break;
                case ("pickup"):
                    _myCommand = new CommandTake();
                    break;
                default:
                    return $"I don't understand the command";
            }
            return _myCommand.Execute(p, text);
        }
    }
}
