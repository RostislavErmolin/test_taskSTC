namespace task.Resources
{
    public static class ProcessingStrings
    {
        // Статусы
        public const string Idle = "Ожидание";
        public const string Processing = "Обработка...";
        public const string Completed = "Завершено";
        public const string Error = "Ошибка";

        // Единицы измерения размера
        public const string Bytes = "Bytes";
        public const string Kilobytes = "KB";
        public const string Megabytes = "MB";
        public const string Gigabytes = "GB";

        // Единицы времени
        public const string Seconds = "сек";
        public const string Minutes = "мин";
        public const string Hours = "ч";
        public const string Milliseconds = "мс";
        
        // Форматы
        public const string SpeedFormat = "/s";
        public const string Unknown = "---";
        public const string ZeroBytes = "0 Bytes";

        // Специальные символы
        public const string SpaceDisplay = "␣ (пробел)";
        public const string NewLineDisplay = "↵ (новая строка)";
        public const string CarriageReturnDisplay = "⏎ (возврат каретки)";
        public const string TabDisplay = "⇥ (табуляция)";

         // Ключевые слова для поиска (без символов)
        public const string SpaceKeyword = "пробел";
        public const string NewLineKeyword = "строка";
        public const string TabKeyword = "табуляция";
    }
}