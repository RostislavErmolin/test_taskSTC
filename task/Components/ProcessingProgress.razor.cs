using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using task.Models;
using task.Resources;

namespace task.Components
{
    public partial class ProcessingProgress
    {
        [Parameter]
        public Models.ProcessingProgress? ProgressData { get; set; }

        private Color GetStatusColor()
        {
            if (ProgressData == null) return Color.Default;
            return ProgressData.Status switch
            {
                ProcessingStatusEnum.Idle => Color.Default,
                ProcessingStatusEnum.Processing => Color.Info,
                ProcessingStatusEnum.Completed => Color.Success,
                ProcessingStatusEnum.Error => Color.Error,
                _ => Color.Default
            };
        }

        private string GetStatusText()
        {
            if (ProgressData == null) return string.Empty;
            return ProgressData.Status switch
            {
                ProcessingStatusEnum.Idle => ProcessingStrings.Idle,
                ProcessingStatusEnum.Processing => ProcessingStrings.Processing,
                ProcessingStatusEnum.Completed => ProcessingStrings.Completed,
                ProcessingStatusEnum.Error => ProcessingStrings.Error,
                _ => string.Empty
            };
        }

        private string FormatBytes(long bytes)
        {
            if (bytes == 0) return ProcessingStrings.ZeroBytes;
            
            const int k = 1024;
            string[] sizes = { 
                ProcessingStrings.Bytes, 
                ProcessingStrings.Kilobytes, 
                ProcessingStrings.Megabytes, 
                ProcessingStrings.Gigabytes 
            };
            
            int i = (int)Math.Floor(Math.Log(bytes) / Math.Log(k));
            return $"{Math.Round(bytes / Math.Pow(k, i), 2)} {sizes[i]}";
        }

        private string FormatSpeed(double bytesPerSecond)
        {
            return $"{FormatBytes((long)bytesPerSecond)}{ProcessingStrings.SpeedFormat}";
        }

        private string FormatTime(double seconds)
        {
            if (!double.IsFinite(seconds) || seconds < 0) 
                return ProcessingStrings.Unknown;
            
            if (seconds < 60)
            {
                return $"{Math.Round(seconds)} {ProcessingStrings.Seconds}";
            }
            else if (seconds < 3600)
            {
                var minutes = (int)Math.Floor(seconds / 60);
                var secs = (int)Math.Round(seconds % 60);
                return $"{minutes} {ProcessingStrings.Minutes} {secs} {ProcessingStrings.Seconds}";
            }
            else
            {
                var hours = (int)Math.Floor(seconds / 3600);
                var minutes = (int)Math.Floor((seconds % 3600) / 60);
                return $"{hours} {ProcessingStrings.Hours} {minutes} {ProcessingStrings.Minutes}";
            }
        }
    }
}