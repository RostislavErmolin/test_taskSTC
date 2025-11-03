namespace task.Models
{
    /// <summary>
    /// Статусы процесса обработки файла
    /// </summary>
    public enum ProcessingStatusEnum
    {
        /// <summary>
        /// Ожидание начала обработки
        /// </summary>
        Idle,

        /// <summary>
        /// Файл обрабатывается
        /// </summary>
        Processing,

        /// <summary>
        /// Обработка завершена успешно
        /// </summary>
        Completed,

        /// <summary>
        /// Произошла ошибка
        /// </summary>
        Error
    }
}
