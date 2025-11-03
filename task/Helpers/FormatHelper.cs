using task.Resources;

namespace task.Helpers
{
    /// <summary>
    /// Утилиты для форматирования данных
    /// </summary>
    public static class FormatHelper
    {
        /// <summary>
        /// Форматирование байтов в читаемый формат (Bytes, KB, MB, GB)
        /// </summary>
        public static string FormatBytes(long bytes)
        {
            if (bytes == 0) return ProcessingStrings.ZeroBytes;
            const int k = 1024;
            string[] sizes = { 
                ProcessingStrings.Bytes, 
                ProcessingStrings.Kilobytes, 
                ProcessingStrings.Megabytes, 
                ProcessingStrings.Gigabytes 
            };
            int i = (int)Math.Floor(Math.Log(bytes) / Math.Log(k));
            return $"{Math.Round(bytes / Math.Pow(k, i), 2)} {sizes[i]}";
        }

        /// <summary>
        /// Форматирование скорости передачи данных (байты/секунду)
        /// </summary>
        public static string FormatSpeed(double bytesPerSecond)
        {
            return $"{FormatBytes((long)bytesPerSecond)}{ProcessingStrings.SpeedFormat}";
        }

        /// <summary>
        /// Форматирование времени из секунд в читаемый формат
        /// </summary>
        public static string FormatTime(double seconds)
        {
            if (!double.IsFinite(seconds) || seconds < 0)
                return ProcessingStrings.Unknown;

            if (seconds < 60)
            {
                return $"{Math.Round(seconds)} {ProcessingStrings.Seconds}";
            }

            if (seconds < 3600)
            {
                var minutes = (int)Math.Floor(seconds / 60);
                var secs = (int)Math.Round(seconds % 60);
                return $"{minutes} {ProcessingStrings.Minutes} {secs} {ProcessingStrings.Seconds}";
            }

            var hours = (int)Math.Floor(seconds / 3600);
            var mins = (int)Math.Floor((seconds % 3600) / 60);
            return $"{hours} {ProcessingStrings.Hours} {mins} {ProcessingStrings.Minutes}";
        }

        /// <summary>
        /// Форматирование времени обработки в миллисекундах
        /// </summary>
        public static string FormatProcessingTime(long ms)
        {
            if (ms < 1000)
            {
                return $"{ms} {ProcessingStrings.Milliseconds}";
            }

            return $"{(ms / 1000.0):F2} {ProcessingStrings.Seconds}";
        }
    }
}