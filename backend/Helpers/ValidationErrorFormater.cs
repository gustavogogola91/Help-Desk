namespace backend.Helpers
{
    public class ValidationErrorFormater
    {
        public static string FormatErrorsToString(IDictionary<string, string[]> erros)
        {
            if (erros == null || !erros.Any())
            {
                return string.Empty;
            }

            List<string> MensagensFormatadas = new List<string>();

            foreach (var entry in erros)
            {
                string propertyName = entry.Key;
                string[] errorMessages = entry.Value;

                if (errorMessages.Length == 1)
                {
                    MensagensFormatadas.Add($"{propertyName}: {errorMessages[0]}");
                }
                else
                {
                    string joinedMessages = string.Join("; ", errorMessages);
                    MensagensFormatadas.Add($"{propertyName}: {joinedMessages}");
                }
            }
            return string.Join(" | ", MensagensFormatadas);
        }
    }
}