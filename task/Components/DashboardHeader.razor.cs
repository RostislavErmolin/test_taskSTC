using Microsoft.AspNetCore.Components;
using MudBlazor;
using task.Models;
using task.Resources;

namespace task.Components
{
    public partial class DashboardHeader
    {
        /// <summary>
        /// Статус подключения
        /// </summary>
        [Parameter]
        public ConnectionStatus Status { get; set; }

        private static readonly Dictionary<ConnectionStatus, Color> StatusColors = new()
        {
            { ConnectionStatus.Connected, Color.Success },
            { ConnectionStatus.Connecting, Color.Warning },
            { ConnectionStatus.Error, Color.Error },
            { ConnectionStatus.Paused, Color.Default }
        };

        private static readonly Dictionary<ConnectionStatus, string> StatusIcons = new()
        {
            { ConnectionStatus.Connected, Icons.Material.Filled.CheckCircle },
            { ConnectionStatus.Connecting, Icons.Material.Filled.Sync },
            { ConnectionStatus.Error, Icons.Material.Filled.Error },
            { ConnectionStatus.Paused, Icons.Material.Filled.Pause }
        };

        private static readonly Dictionary<ConnectionStatus, string> StatusTexts = new()
        {
            { ConnectionStatus.Connected, StatusStrings.Connected },
            { ConnectionStatus.Connecting, StatusStrings.Connecting },
            { ConnectionStatus.Error, StatusStrings.Error },
            { ConnectionStatus.Paused, StatusStrings.Paused }
        };

        private Color GetStatusColor()
        {
            return StatusColors.TryGetValue(Status, out var color) ? color : Color.Default;
        }

        private string GetStatusIcon()
        {
            return StatusIcons.TryGetValue(Status, out var icon) ? icon : Icons.Material.Filled.Circle;
        }

        private string GetStatusText()
        {
            return StatusTexts.TryGetValue(Status, out var text) ? text : StatusStrings.Idle;
        }
    }
}