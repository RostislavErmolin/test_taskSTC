namespace task.Helpers
{
    /// <summary>
    /// Регулярные выражения для фильтрации символов
    /// </summary>
    public static class RegexPatterns
    {
        /// <summary>
        /// Паттерн для букв (латиница и кириллица)
        /// </summary>
        public const string Letters = @"[a-zA-Zа-яА-ЯёЁ]";

        /// <summary>
        /// Паттерн для цифр
        /// </summary>
        public const string Digits = @"\d";

        /// <summary>
        /// Паттерн для знаков препинания
        /// </summary>
        public const string Punctuation = @"[.,;:!?'""«»—–-]";
    }
}