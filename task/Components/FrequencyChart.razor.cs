using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Text.RegularExpressions;
using task.Models;
using task.Helpers;
using task.Resources;

namespace task.Components
{
    /// <summary>
    /// Компонент для отображения диаграммы частоты символов
    /// </summary>
    public partial class FrequencyChart : IAsyncDisposable
    {
        [Inject]
        private IJSRuntime JSRuntime { get; set; } = default!;

        /// <summary>
        /// Результат анализа частоты символов
        /// </summary>
        [Parameter]
        public FrequencyResult? Result { get; set; }

        /// <summary>
        /// Список частот символов для отображения
        /// </summary>
        private List<CharFrequency> charFrequencies = new();

        /// <summary>
        /// Текущий режим фильтрации
        /// </summary>
        private FilterMode currentFilter = FilterMode.All;

        /// <summary>
        /// Текущий режим сортировки
        /// </summary>
        private SortMode currentSort = SortMode.Frequency;

        /// <summary>
        /// Количество отображаемых элементов
        /// </summary>
        private int topCount = 50;

        /// <summary>
        /// Флаг первой отрисовки
        /// </summary>
        private bool isFirstRender = true;

        protected override void OnParametersSet()
        {
            if (Result != null)
            {
                ProcessResult();
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                isFirstRender = false;
            }
            if (Result != null && !isFirstRender)
            {
                await UpdateChart();
            }
        }

        /// <summary>
        /// Обработка результата 
        /// </summary>
        private void ProcessResult()
        {
            if (Result == null) return;

            var totalChars = Result.TotalChars;
            charFrequencies = Result.FrequencyMap
                .Select(kvp => new CharFrequency
                {
                    Char = CharDisplayHelper.GetCharDisplay(kvp.Key),
                    Count = kvp.Value,
                    Percentage = (double)kvp.Value / totalChars * 100
                })
                .OrderByDescending(x => x.Count)
                .ToList();
        }

        /// <summary>
        /// Получить отфильтрованные данные
        /// </summary>
        private List<CharFrequency> GetFilteredData()
        {
            var filtered = charFrequencies.AsEnumerable();

            // Применяем фильтр
            filtered = currentFilter switch
            {
                FilterMode.Letters => filtered.Where(x => Regex.IsMatch(x.Char, RegexPatterns.Letters)),
                FilterMode.Digits => filtered.Where(x => Regex.IsMatch(x.Char, RegexPatterns.Digits)),
                FilterMode.Punctuation => filtered.Where(x => Regex.IsMatch(x.Char, RegexPatterns.Punctuation)),
                FilterMode.Whitespace => filtered.Where(x => 
                    x.Char.Contains(ProcessingStrings.SpaceKeyword) || 
                    x.Char.Contains(ProcessingStrings.NewLineKeyword) || 
                    x.Char.Contains(ProcessingStrings.TabKeyword)),
                    _ => filtered
            };

            
            filtered = currentSort switch
            {
                SortMode.Alphabet => filtered.OrderBy(x => x.Char),
                _ => filtered.OrderByDescending(x => x.Count)
            };

            
            return filtered.Take(topCount).ToList();
        }

        /// <summary>
        /// Обновление диаграммы через JSInterop
        /// </summary>
        private async Task UpdateChart()
        {
            var data = GetFilteredData();
            var labels = data.Select(x => x.Char).ToArray();
            var values = data.Select(x => x.Count).ToArray();

            await JSRuntime.InvokeVoidAsync("chartHelper.updateChart", labels, values);
        }

        /// <summary>
        /// Изменить режим фильтрации
        /// </summary>
        /// <param name="mode">Новый режим фильтрации</param>
        private async Task ChangeFilter(FilterMode mode)
        {
            currentFilter = mode;
            await UpdateChart();
        }

        /// <summary>
        /// Изменить режим сортировки
        /// </summary>
        /// <param name="mode">Новый режим сортировки</param>
        private async Task ChangeSortMode(SortMode mode)
        {
            currentSort = mode;
            await UpdateChart();
        }

        /// <summary>
        /// Изменить количество отображаемых элементов
        /// </summary>
        /// <param name="count">Количество элементов</param>
        private async Task ChangeTopCount(int count)
        {
            topCount = count;
            await UpdateChart();
        }

        /// <summary>
        /// Форматирование размера файла
        /// </summary>
        /// <param name="bytes">Размер в байтах</param>
        /// <returns>Отформатированная строка</returns>
        private string FormatFileSize(long bytes) => FormatHelper.FormatBytes(bytes);

        /// <summary>
        /// Форматирование времени обработки
        /// </summary>
        /// <param name="ms">Время в миллисекундах</param>
        /// <returns>Отформатированная строка</returns>
        private string FormatProcessingTime(long ms) => FormatHelper.FormatProcessingTime(ms);

        public async ValueTask DisposeAsync()
        {
            try
            {
                await JSRuntime.InvokeVoidAsync("chartHelper.destroyChart");
            }
            catch
            {
                // Игнорируем ошибки при dispose
            }
        }
    }

    /// <summary>
    /// Режимы фильтрации символов
    /// </summary>
    public enum FilterMode
    {
        All,
        Letters,
        Digits,
        Punctuation,
        Whitespace
    }

    /// <summary>
    /// Режимы сортировки
    /// </summary>
    public enum SortMode
    {
        Frequency,
        Alphabet
    }
}