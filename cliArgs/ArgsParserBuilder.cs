using System;
using System.Collections.Generic;
using cliArgs.command;
using cliArgs.flag;

namespace cliArgs
{
    /// <summary>
    /// arguments parser builder
    /// </summary>
    public class ArgsParserBuilder
    {
        readonly List<Flag> flags = new List<Flag>();
        readonly List<Command> commands = new List<Command>();

        internal void AddDefaultCommand(Command command)
        {
            if (commands.Count != 0)
            {
                throw new InvalidOperationException();
            }
            commands.Add(command);
        }

        /// <summary>
        /// build arguments parser
        /// </summary>
        /// <returns>return an arguments parser</returns>
        public ArgsParser Build()
        {
            return new ArgsParser(commands);
        }

        /// <summary>
        /// begin definde default command
        /// </summary>
        /// <returns>return the command builder</returns>
        public CommandBuilder BeginDefaultCommand()
        {
            return new CommandBuilder(this);
        }
    }
}
