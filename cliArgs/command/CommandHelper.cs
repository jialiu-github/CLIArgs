using System;
using System.Collections.Generic;
using System.Linq;
using cliArgs.flag;

namespace cliArgs.command
{
    class CommandHelper
    {
        List<Command> Commands {get;}

        internal CommandHelper(List<Command> commands)
        {
            Commands = commands;
        }

        internal Command FindCommand()
        {
            return Commands[0];
        }

        bool IsInvalidFlagInput(string input)
        {
            return !FlagHelper.FullFormInputRgx.IsMatch(input) &&
                   !FlagHelper.AbbrFormInputRgx.IsMatch(input) &&
                   !FlagHelper.CombinedAbbrFormInputRgx.IsMatch(input);
        }

        internal ArgsParsingResult FlagArgsParse(List<string> inputs)
        {
            var flagHelper = FindCommand().FlagHelper;
            if (inputs.Any(IsInvalidFlagInput))
            {
                var invalidInputsParrseError = new ParseError(ParseErrorCode.InvalidInputs, inputs.Find(IsInvalidFlagInput));
                return new ArgsParsingResult(this, false, invalidInputsParrseError);
            }

            var flagInputs = inputs.SelectMany(i =>
                {
                    if (FlagHelper.CombinedAbbrFormInputRgx.IsMatch(i))
                    {
                        return i.Skip(1).Select(c => new FlagInput(i, $"-{c}")).ToList();
                    }
                    return new List<FlagInput> {new FlagInput(i, i)};
                })
                .ToList();

            if (flagInputs.Any(i => flagHelper.FindFlag(i.Value) == null))
            {
                return new ArgsParsingResult(this, false, new ParseError
                (
                    ParseErrorCode.FreeValueNotSupported,
                    flagInputs.First(i => flagHelper.FindFlag(i.Value) == null).Trigger
                ));
            }
            flagHelper.ResetAll();
            flagInputs.ForEach(f => f.FlagId = flagHelper.FindFlag(f.Value).Id);

            if (flagInputs.GroupBy(f => f.FlagId).Any(g => g.Count() > 1))
            {
                return new ArgsParsingResult(this, false, new ParseError
                (
                    ParseErrorCode.DuplicateFlagsInArgs,
                    flagInputs.GroupBy(f => f.FlagId).First(g => g.Count() > 1).First().Trigger
                ));
            }

            flagHelper.Toggle(flagInputs.Select(f => f.FlagId).ToList());
            return new ArgsParsingResult(this);
        }

        internal string GetFlagDescription(string input)
        {
            if (IsInvalidFlagInput(input))
            {
                throw new ArgumentException();
            }
            Flag findFlag = FindCommand().FlagHelper.FindFlag(input);

            return findFlag != null ? findFlag.Description : "undefinded flag";
        }
    }
}