using System;
using System.Collections.Generic;
using System.Linq;
using cliArgs.command;

namespace cliArgs
{
    /// <summary>
    /// arguments parser
    /// </summary>
    public class ArgsParser
    {
        CommandHelper CommandHelper { get; }

        internal ArgsParser(List<Command> commands)
        {
            CommandHelper = new CommandHelper(commands);
        }

        /// <summary>
        /// parse inputs
        /// </summary>
        /// <param name="inputs">arguments list, should be array of string</param>
        /// <returns>return the parse result</returns>
        /// <exception cref="ArgumentException">when input any null item will throw exception</exception>
        public ArgsParsingResult Parse(string[] inputs)
        {
            if (inputs.Any(i => i == null))
            {
                throw new ArgumentException();
            }
            var inputWithoutEmptyString = inputs.Where(i => i != string.Empty).ToList();
            return CommandArgsParse(inputWithoutEmptyString);
        }

        /// <summary>
        /// get flag's description
        /// </summary>
        /// <param name="input"> should a flag's full or abbrivation form</param>
        /// <returns>return description</returns>
        public string Help(string input)
        {
            return CommandHelper.GetFlagDescription(input);
        }

        ArgsParsingResult CommandArgsParse(List<string> inputWithoutEmptyString)
        {
            return CommandHelper.FlagArgsParse(inputWithoutEmptyString);
        }

    }
}