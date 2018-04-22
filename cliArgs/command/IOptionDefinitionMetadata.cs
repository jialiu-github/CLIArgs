namespace cliArgs.command
{
    /// <summary>
    /// the definition of option
    /// </summary>
    public interface IOptionDefinitionMetadata
    {
        /// <summary>
        /// the metadate of this option
        /// </summary>
        IOptionSymbolMetadata SymbolMetadata { get;}
        /// <summary>
        /// the description of option which can give help for users
        /// </summary>
        string Description { get;}

    }
}