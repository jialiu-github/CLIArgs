using System;
using System.Collections.Generic;
using cliArgs.command;
using cliArgs.flag;

namespace cliArgs
{
    /// <summary>
    /// args parsing result
    /// </summary>
    public class ArgsParsingResult
    {
        FlagHelper FlagHelper { get; }
        CommandHelper CommandHelper { get;}
        /// <summary>
        /// is parse success
        /// </summary>
        public bool IsSuccess { get;}
        /// <summary>
        /// parse error message
        /// </summary>
        public ParseError Error { get;}

        internal ArgsParsingResult(CommandHelper commandHelper, bool isSuccess = true, ParseError error = null)
        {
            FlagHelper = commandHelper.FindCommand().FlagHelper;
            CommandHelper = commandHelper;
            IsSuccess = isSuccess;
            Error = error;
        }

        /// <summary>
        /// the command which be parsed. When parse faild it will be null
        /// </summary>
        public ICommandDefinitionMetadata Command => IsSuccess ? CommandHelper.FindCommand() : null;

        /// <summary>
        /// get parse value
        /// </summary>
        /// <param name="input">should be the full form or abbrivation form of the flag which you want to get the value</param>
        /// <returns>return the flag value</returns>
        /// <exception cref="InvalidOperationException">if parse faild will throw exception</exception>
        /// <exception cref="ArgumentException">if input can't match any flags will throw exception</exception>
        public bool GetFlagValue(string input)
        {
            if (!IsSuccess)
            {
                throw new InvalidOperationException();
            }

            if (FlagHelper.FindFlag(input) == null)
            {
                throw new ArgumentException();
            }
            return FlagHelper.FindFlag(input).Value;
        }


    }
}