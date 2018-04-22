namespace cliArgs.command
{
    /// <summary>
    /// the metadata of option symbol
    /// </summary>
    public interface IOptionSymbolMetadata
    {
        /// <summary>
        /// the abbreviation form of option
        /// </summary>
        char? Abbreviation { get;}
        /// <summary>
        /// the full form of option
        /// </summary>
        string FullForm { get;}
    }
}