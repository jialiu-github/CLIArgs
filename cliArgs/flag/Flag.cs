using System;
using System.Text.RegularExpressions;
using cliArgs.command;

namespace cliArgs.flag
{
    class Flag : IOptionDefinitionMetadata
    {
        readonly Regex invalidFullFormRgx = new Regex(@"\A-|[^\w-]");

        public IOptionSymbolMetadata SymbolMetadata { get; }

        public string Description { get; }

        internal bool Value;

        internal readonly Guid Id;

        internal Flag(string fullForm, char? abbrForm, string description)
        {
            if (fullForm == null && abbrForm == null)
            {
                throw new ArgumentException();
            }

            if (fullForm == "" && abbrForm.HasValue)
            {
                throw new ArgumentException();
            }

            if (abbrForm != null && !char.IsLetter(abbrForm.Value))
            {
                throw new ArgumentException();
            }
            if (fullForm != null &&  invalidFullFormRgx.Matches(fullForm).Count != 0)
            {
                throw new ArgumentException();
            }
            Id = Guid.NewGuid();
            SymbolMetadata = new FlagSymbolMetadata(fullForm, abbrForm);
            Description = description ?? string.Empty;
            Value = false;
        }

        internal void Toggle()
        {
            Value = !Value;
        }

        public void Reset()
        {
            Value = false;
        }

        public bool EqualWithFullForm(string fullForm)
        {
            return string.Equals(SymbolMetadata.FullForm, fullForm, StringComparison.OrdinalIgnoreCase);
        }

        public bool EqualWithAbbrForm(char? abbrForm)
        {
            return string.Equals(SymbolMetadata.Abbreviation.ToString(), abbrForm.ToString(), StringComparison.OrdinalIgnoreCase);
        }
    }
}