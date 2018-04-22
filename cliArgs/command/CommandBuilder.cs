using System;
using System.Collections.Generic;
using System.Linq;
using cliArgs.flag;

namespace cliArgs.command
{
    /// <summary>
    /// command builder
    /// </summary>
    public class CommandBuilder
    {
        ArgsParserBuilder ArgsParserBuilder {get;}
        readonly List<Flag> flags = new List<Flag>();

        internal CommandBuilder(ArgsParserBuilder argsParserBuilder)
        {
            ArgsParserBuilder = argsParserBuilder;
        }

        /// <summary>
        /// add flag ptions to this command
        /// </summary>
        /// <param name="fullForm">the full form of flag</param>
        /// <param name="abbrForm">the abbrivation form of flag</param>
        /// <param name="description">the description of flag</param>
        /// <returns>return the command builder</returns>
        /// <exception cref="ArgumentException">when neither input full form nor input abbr form wwill throw arugment exception</exception>
        public CommandBuilder AddFlagOption(string fullForm, char? abbrForm, string description = null)
        {
            if (flags.Any(f => f.EqualWithAbbrForm(abbrForm) || f.EqualWithFullForm(fullForm)))
            {
                throw new ArgumentException();
            }
            flags.Add(new Flag(fullForm, abbrForm, description));
            return this;
        }

        /// <summary>
        /// then end of defind command, will create command.
        /// </summary>
        /// <returns>after create command return args parser builder</returns>
        public ArgsParserBuilder EndCommand()
        {
            ArgsParserBuilder.AddDefaultCommand(new Command(flags));
            return ArgsParserBuilder;
        }
    }
}