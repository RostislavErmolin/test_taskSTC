namespace task.Models
{
    /// <summary>
    /// Статистика по случайным числам
    /// </summary>
    public class Statistics
    {
        /// <summary>
        /// Текущее число
        /// </summary>
        public int? Current { get; set; }

        /// <summary>
        /// Среднее значение за последние 10 секунд
        /// </summary>
        public double? Avg10Seconds { get; set; }

        /// <summary>
        /// Среднее значение за последнюю минуту
        /// </summary>
        public double? Avg1Minute { get; set; }

        /// <summary>
        /// Среднее значение за всё время работы приложения
        /// </summary>
        public double? AvgAllTime { get; set; }

        /// <summary>
        /// Общее количество полученных чисел
        /// </summary>
        public int TotalCount { get; set; }
    }
}