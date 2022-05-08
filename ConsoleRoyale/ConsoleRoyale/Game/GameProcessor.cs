using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRoyale
{
    public class GameProcessor : IGameProcessor
    {
        private ICollection<string> _commands;

        public GameProcessor(ICollection<string> commands)
        {
            _commands = commands;
        }

        public void ProcessCommand(string command)
        {
            var parsedCommand = command.Split(' ');

            if (parsedCommand.Length != 2)
            {
                throw new InvalidOperationException($"The given command: {command} is wrong.");
            }

            var cmd = parsedCommand[0];
            var value = decimal.Parse(parsedCommand[1]);
        }
    }
}
