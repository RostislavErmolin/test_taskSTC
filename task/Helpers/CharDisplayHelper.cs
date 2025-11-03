using task.Resources;

namespace task.Helpers
{
    /// <summary>
    /// Вспомогательный класс для отображения символов
    /// </summary>
    public static class CharDisplayHelper
    {
        private static readonly Dictionary<string, string> CharDisplayMap = new()
        {
            { " ", ProcessingStrings.SpaceDisplay },
            { "\n", ProcessingStrings.NewLineDisplay },
            { "\r", ProcessingStrings.CarriageReturnDisplay },
            { "\t", ProcessingStrings.TabDisplay }
        };

        /// <summary>
        /// Получить отображаемое представление символа
        /// </summary>
        /// <param name="ch">Символ для отображения</param>
        /// <returns>Форматированная строка с символом</returns>
        public static string GetCharDisplay(string ch)
        {
            return CharDisplayMap.TryGetValue(ch, out var display) ? display : ch;
        }
    }
}