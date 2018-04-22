namespace cliArgs
{
    /// <summary>
    /// parse error code
    /// </summary>
    public class ParseErrorCode
    {
        /// <summary>
        /// invalid inputs code
        /// </summary>
        public static string InvalidInputs = "invalid inputs";

        /// <summary>
        /// can not input duplicate flag
        /// </summary>
        public static string DuplicateFlagsInArgs = "can not input duplicate flag";

        /// <summary>
        /// undefinded arguments
        /// </summary>
        public static string FreeValueNotSupported = "Free Value Not Supported";
    }
}