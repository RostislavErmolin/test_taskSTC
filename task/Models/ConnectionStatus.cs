namespace task.Models
{
    /// <summary>
    /// Статус подключения к API
    /// </summary>
    public enum ConnectionStatus
    {
        /// <summary>
        /// Ожидание
        /// </summary>
        Idle,

        /// <summary>
        /// Подключено
        /// </summary>
        Connected,

        /// <summary>
        /// Подключение...
        /// </summary>
        Connecting,

        /// <summary>
        /// Ошибка подключения
        /// </summary>
        Error,

        /// <summary>
        /// Приостановлено
        /// </summary>
        Paused
    }
}