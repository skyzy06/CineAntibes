namespace CineAntibes.Utils
{
    public class StringTools
    {
        /// <summary>
        /// Remove unsupported characters and replace them by an equivalent supported
        /// </summary>
        /// <returns>The sanitized input</returns>
        /// <param name="input">The string to sanitize</param>
        public static string Sanitize(string input)
        {
            /*
             * \u0092 -> '
             * \u0096 -> -
             * \u0085 -> ...
             * \u009c -> œ
             */
            return input.Replace('\u0092', '\u0027')
                        .Replace('\u0096', '\u2013')
                        .Replace('\u0085', '\u2026')
                        .Replace('\u009c', '\u0153')
                        .Replace('\u00a0', ' ')
                        .Replace('\n', ' ');
        }
    }
}
