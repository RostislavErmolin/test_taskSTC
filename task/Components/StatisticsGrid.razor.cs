using Microsoft.AspNetCore.Components;
using task.Models;

namespace task.Components
{
    public partial class StatisticsGrid
    {
        /// <summary>
        /// Объект статистики для отображения
        /// </summary>
        [Parameter]
        public Statistics? Statistics { get; set; }
    }
}
