using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace cliArgs.flag
{
    class FlagHelper
    {
        internal static Regex FullFormInputRgx => new Regex(@"\A\--[\w-]*\z");
        internal static Regex AbbrFormInputRgx => new Regex(@"\A\-[a-zA-z]\z");
        internal static Regex CombinedAbbrFormInputRgx => new Regex(@"\A\-[a-zA-z]*\z");

        internal List<Flag> Flags { get; }

        internal FlagHelper(List<Flag> flags)
        {
            Flags = flags;
        }

        internal Flag FindFlag(string input)
        {
            if (FullFormInputRgx.IsMatch(input))
            {
                return Flags.Find(f => f.EqualWithFullForm(input.Substring(2)));
            }
            if (AbbrFormInputRgx.IsMatch(input))
            {
                return Flags.Find(f => f.EqualWithAbbrForm(input.ToCharArray()[1]));
            }
            throw new ArgumentException();
        }

        public void Toggle(List<Guid> flagIds)
        {
            Flags.Where(f => flagIds.Contains(f.Id)).ToList().ForEach(f => f.Toggle());
        }

        public void ResetAll()
        {
            Flags.ForEach(f => f.Reset());
        }
    }
}
