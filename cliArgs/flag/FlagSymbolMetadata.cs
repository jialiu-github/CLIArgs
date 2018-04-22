using cliArgs.command;

namespace cliArgs.flag
{
    class FlagSymbolMetadata : IOptionSymbolMetadata
    {
        public FlagSymbolMetadata(string fullForm, char? abbrForm)
        {
            Abbreviation = abbrForm;
            FullForm = fullForm;
        }

        public char? Abbreviation { get; }
        public string FullForm { get; }
    }
}