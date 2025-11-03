using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using task.Models;
using task.Services;

namespace task.Pages
{

    /// <summary>
    /// Главная страница анализатора частоты символов
    /// </summary>
    public partial class Task2 : IDisposable
    {
        [Inject]
        private FileProcessorService FileProcessor { get; set; } = default!;

        /// <summary>
        /// Текущий прогресс обработки
        /// </summary>
        private ProcessingProgress? CurrentProgress { get; set; }

        /// <summary>
        /// Результат анализа
        /// </summary>
        private FrequencyResult? Result { get; set; }

        /// <summary>
        /// Флаг процесса обработки
        /// </summary>
        private bool IsProcessing { get; set; }

        /// <summary>
        /// Сообщение об ошибке
        /// </summary>
        private string? ErrorMessage { get; set; }

        protected override void OnInitialized()
        {
            // Подписываемся на событие изменения прогресса
            FileProcessor.ProgressChanged += OnProgressChanged;
        }

        /// <summary>
        /// Обработка выбранного файла
        /// </summary>
        private async Task HandleFileSelected(IBrowserFile file)
        {
            try
            {
                IsProcessing = true;
                ErrorMessage = null;
                Result = null;
                CurrentProgress = null;

                const long maxFileSize = 1024L * 1024L * 1024L;

                // Открываем stream файла
                await using var stream = file.OpenReadStream(maxFileSize);

                // Запускаем обработку
                Result = await FileProcessor.ProcessFileAsync(stream, file.Size, file.Name);

                IsProcessing = false;
            }
            catch (Exception ex)
            {
                IsProcessing = false;
                ErrorMessage = $"Ошибка при обработке файла: {ex.Message}";
            }
        }

        /// <summary>
        /// Обработчик события изменения прогресса
        /// </summary>
        private void OnProgressChanged(ProcessingProgress progress)
        {
            CurrentProgress = progress;

            // Принудительное обновление UI
            InvokeAsync(StateHasChanged);
        }

        /// <summary>
        /// Сброс состояния для нового анализа
        /// </summary>
        private void ResetAnalysis()
        {
            Result = null;
            CurrentProgress = null;
            IsProcessing = false;
            ErrorMessage = null;
        }

        /// <summary>
        /// Очистка сообщения об ошибке
        /// </summary>
        private void ClearError()
        {
            ErrorMessage = null;
        }

        public void Dispose()
        {
            // Отписываемся от события
            FileProcessor.ProgressChanged -= OnProgressChanged;
        }
    }
}