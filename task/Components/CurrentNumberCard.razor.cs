using Microsoft.AspNetCore.Components;

namespace task.Components
{
    public partial class CurrentNumberCard
    {
        /// <summary>
        /// Текущее значение числа
        /// </summary>
        [Parameter]
        public int? Value { get; set; }
    }
}
