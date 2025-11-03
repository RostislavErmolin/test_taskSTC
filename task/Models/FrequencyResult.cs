namespace task.Models;

/// <summary>
/// Результат анализа частоты символов в файле
/// </summary>
public class FrequencyResult
{
    /// <summary>
    /// Словарь с частотой встречаемости каждого символа
    /// Ключ - символ, Значение - количество повторений
    /// </summary>
    public Dictionary<string, int> FrequencyMap { get; set; } = new();

    /// <summary>
    /// Общее количество проанализированных символов
    /// </summary>
    public int TotalChars { get; set; }

    /// <summary>
    /// Количество уникальных символов
    /// </summary>
    public int UniqueChars { get; set; }

    /// <summary>
    /// Время обработки файла в миллисекундах
    /// </summary>
    public long ProcessingTime { get; set; }

    /// <summary>
    /// Имя обработанного файла
    /// </summary>
    public string FileName { get; set; } = string.Empty;

    /// <summary>
    /// Размер файла в байтах
    /// </summary>
    public long FileSize { get; set; }
}