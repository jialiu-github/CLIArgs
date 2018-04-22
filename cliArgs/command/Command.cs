using System.Collections.Generic;
using cliArgs.flag;

namespace cliArgs.command
{
    class Command : ICommandDefinitionMetadata
    {
        internal FlagHelper FlagHelper { get;}
        public string Symbol { get; }

        public string Description { get; }

        public IEnumerable<IOptionDefinitionMetadata> GetRegisteredOptionsMetadata()
        {
            return FlagHelper.Flags;
        }

        internal Command(List<Flag> flags)
        {
            Symbol = null;
            Description = string.Empty;
            FlagHelper = new FlagHelper(flags);
        }

    }
}