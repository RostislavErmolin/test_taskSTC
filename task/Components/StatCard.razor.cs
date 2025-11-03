using Microsoft.AspNetCore.Components;

namespace task.Components
{
    public partial class StatCard
    {
        /// <summary>
        /// Заголовок карточки
        /// </summary>
        [Parameter]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Значение статистики
        /// </summary>
        [Parameter]
        public double? Value { get; set; }

        /// <summary>
        /// Подпись снизу
        /// </summary>
        [Parameter]
        public string Label { get; set; } = "среднее";
    }
}
