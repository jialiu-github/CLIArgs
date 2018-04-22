namespace cliArgs
{
    /// <summary>
    /// parse error message
    /// </summary>
    public class ParseError
    {
        /// <summary>
        /// error code
        /// </summary>
        public string Code { get; }
        /// <summary>
        /// the input which trigger the error
        /// </summary>
        public string Trigger { get; }

        /// <summary>
        /// parse error constructor
        /// </summary>
        /// <param name="code">the code of error</param>
        /// <param name="trigger">the trigger of error</param>
        public ParseError(string code, string trigger)
        {
            Code = code;
            Trigger = trigger;
        }
    }
}