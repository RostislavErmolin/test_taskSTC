namespace task.Models
{
    /// <summary>
    /// Ответ от API с случайным числом
    /// </summary>
    public class RandomNumberResponse
    {
        /// <summary>
        /// Случайное число
        /// </summary>
        public int RandomNumber { get; set; }

        /// <summary>
        /// Временная метка генерации числа
        /// </summary>
        public string Timestamp { get; set; } = string.Empty;
    }
}