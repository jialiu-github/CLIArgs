using System;

namespace cliArgs.flag
{
    class FlagInput
    {
        public FlagInput(string trigger, string value)
        {
            Trigger = trigger;
            Value = value;
        }

        internal string Trigger {get;}
        internal string Value {get;}
        internal Guid FlagId { get; set;}
    }
}
