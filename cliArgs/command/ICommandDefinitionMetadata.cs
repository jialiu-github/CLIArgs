using System.Collections.Generic;

namespace cliArgs.command
{
    /// <summary>
    /// the definition of command
    /// </summary>
    public interface ICommandDefinitionMetadata
    {
        /// <summary>
        /// the symbol of command which should be unique
        /// </summary>
        string Symbol { get; }

        /// <summary>
        /// the description of command which can give help for users
        /// </summary>
        string Description { get;}

        /// <summary>
        /// get information of all regisetered options in the command
        /// </summary>
        /// <returns>all regisetered options information</returns>
        IEnumerable<IOptionDefinitionMetadata> GetRegisteredOptionsMetadata();
    }
}