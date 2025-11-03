using Microsoft.AspNetCore.Components;
using task.Models;

namespace task.Components
{
    public partial class Controls
    {
        /// <summary>
        /// Статус подключения
        /// </summary>
        [Parameter]
        public ConnectionStatus Status { get; set; }

        /// <summary>
        /// Событие нажатия на кнопку старт/пауза
        /// </summary>
        [Parameter]
        public EventCallback OnPlayPause { get; set; }

        /// <summary>
        /// Событие нажатия на кнопку сброса
        /// </summary>
        [Parameter]
        public EventCallback OnReset { get; set; }

        /// <summary>
        /// Проверка, запущен ли опрос
        /// </summary>
        private bool IsRunning => Status == ConnectionStatus.Connected || Status == ConnectionStatus.Connecting;

        private async Task HandlePlayPause()
        {
            await OnPlayPause.InvokeAsync();
        }

        private async Task HandleReset()
        {
            await OnReset.InvokeAsync();
        }
    }
}
