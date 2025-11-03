using task.Models;

namespace task.Pages
{
    public partial class Task1 : IDisposable
    {
        protected override void OnInitialized()
        {
            DataManager.OnDataUpdated += StateHasChanged;
            DataManager.Start();
        }

        private void HandlePlayPause()
        {
            if (DataManager.ConnectionStatus == ConnectionStatus.Connected ||
                DataManager.ConnectionStatus == ConnectionStatus.Connecting)
            {
                DataManager.Pause();
            }
            else
            {
                DataManager.Start();
            }
        }

        public void Dispose()
        {
            DataManager.OnDataUpdated -= StateHasChanged;
            DataManager.Pause();
        }
    }
}