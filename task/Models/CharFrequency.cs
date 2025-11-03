namespace task.Models;

/// <summary>
/// Модель для отображения частоты встречаемости одного символа
/// Используется для построения диаграммы
/// </summary>
public class CharFrequency
{
    /// <summary>
    /// Символ (может быть с описанием для спецсимволов, например "␣ (пробел)")
    /// </summary>
    public string Char { get; set; } = string.Empty;

    /// <summary>
    /// Количество повторений символа в тексте
    /// </summary>
    public int Count { get; set; }

    /// <summary>
    /// Процент от общего количества символов
    /// </summary>
    public double Percentage { get; set; }
}