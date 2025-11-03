namespace task.Models;

/// <summary>
/// Модель для отслеживания прогресса обработки файла
/// </summary>
public class ProcessingProgress
{
    /// <summary>
    /// Количество обработанных байтов
    /// </summary>
    public long BytesProcessed { get; set; }

    /// <summary>
    /// Общий размер файла в байтах
    /// </summary>
    public long TotalBytes { get; set; }

    /// <summary>
    /// Процент выполнения (0-100)
    /// </summary>
    public double Percentage { get; set; }

    /// <summary>
    /// Скорость обработки в байтах/секунду
    /// </summary>
    public double Speed { get; set; }

    /// <summary>
    /// Оставшееся время в секундах (ETA)
    /// </summary>
    public double Eta { get; set; }

    /// <summary>
    /// Текущий статус обработки
    /// </summary>
    public ProcessingStatusEnum Status { get; set; }

    /// <summary>
    /// Номер текущего обрабатываемого chunk
    /// </summary>
    public int? CurrentChunk { get; set; }

    /// <summary>
    /// Общее количество chunks
    /// </summary>
    public int? TotalChunks { get; set; }
}

