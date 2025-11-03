using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using task.Helpers;

namespace task.Components
{
    /// <summary>
    /// Компонент для загрузки файла с поддержкой Drag&Drop
    /// </summary>
    public partial class FileUploadCard
    {
        /// <summary>
        /// Событие, вызываемое при выборе файла для анализа
        /// </summary>
        [Parameter]
        public EventCallback<IBrowserFile> OnFileSelected { get; set; }

        /// <summary>
        /// Выбранный файл
        /// </summary>
        private IBrowserFile? SelectedFile { get; set; }

        /// <summary>
        /// Обработка выбора файла через input
        /// </summary>
        /// <param name="e">Аргументы события изменения файла</param>
        private void HandleFileSelected(InputFileChangeEventArgs e)
        {
            SelectedFile = e.File;
        }

        /// <summary>
        /// Начать анализ выбранного файла
        /// </summary>
        private async Task StartAnalysis()
        {
            if (SelectedFile != null)
            {
                await OnFileSelected.InvokeAsync(SelectedFile);
            }
        }

        /// <summary>
        /// Очистить выбранный файл
        /// </summary>
        private void ClearFile()
        {
            SelectedFile = null;
        }

        /// <summary>
        /// Форматирование размера файла в читаемый вид
        /// </summary>
        /// <param name="bytes">Размер в байтах</param>
        private string FormatFileSize(long bytes) => FormatHelper.FormatBytes(bytes);
    }
}