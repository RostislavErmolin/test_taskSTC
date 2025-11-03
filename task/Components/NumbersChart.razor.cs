using Microsoft.AspNetCore.Components;

namespace task.Components
{
    public partial class NumbersChart
    {
        /// <summary>
        /// Максимальное количество отображаемых чисел
        /// </summary>
        private const int MaxDisplayCount = 10;

        /// <summary>
        /// Список последних чисел
        /// </summary>
        [Parameter]
        public List<int> Numbers { get; set; } = new();

        /// <summary>
        /// Всегда показываем MaxDisplayCount квадратов
        /// </summary>
        private List<int?> DisplayNumbers
        {
            get
            {
                var result = new List<int?>();
                // Добавляем пустые значения в начало если чисел меньше MaxDisplayCount
                for (int i = 0; i < MaxDisplayCount - Numbers.Count; i++)
                {
                    result.Add(null);
                }
                // Добавляем актуальные числа
                result.AddRange(Numbers.Select(n => (int?)n));
                return result.TakeLast(MaxDisplayCount).ToList();
            }
        }

        private string GetBoxClass(int? number)
        {
            return number.HasValue
                ? "mud-theme-primary"
                : "mud-theme-secondary";
        }
    }
}