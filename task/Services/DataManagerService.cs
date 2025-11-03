using System.Net.Http.Json;
using task.Models;

namespace task.Services
{
    /// <summary>
    /// Сервис для управления данными случайных чисел и подсчёта статистики
    /// </summary>
    public class DataManagerService : IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl = "http://193.124.204.27:5022"; 

        private Timer? _timer;
        private bool _isRunning;

        // Данные для расчётов
        private readonly List<int> _last10 = new();
        private double _sum10 = 0;

        private readonly List<int> _last60 = new();
        private double _sum60 = 0;

        private double _avgAllTime = 0;
        private int _countAllTime = 0;

        /// <summary>
        /// Текущее число
        /// </summary>
        public int? CurrentNumber { get; private set; }

        /// <summary>
        /// Статистика
        /// </summary>
        public Statistics Statistics { get; private set; } = new();

        /// <summary>
        /// Статус подключения
        /// </summary>
        public ConnectionStatus ConnectionStatus { get; private set; } = ConnectionStatus.Idle;

        /// <summary>
        /// Последние 10 чисел для визуализации
        /// </summary>
        public List<int> Last10Numbers { get; private set; } = new();

        /// <summary>
        /// Событие обновления данных
        /// </summary>
        public event Action? OnDataUpdated;

        public DataManagerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Запустить опрос API
        /// </summary>
        public void Start()
        {
            if (_isRunning) return;

            _isRunning = true;
            ConnectionStatus = ConnectionStatus.Connecting;
            OnDataUpdated?.Invoke();

            _timer = new Timer(async _ => await FetchNumber(), null, 0, 1000);
        }

        /// <summary>
        /// Приостановить опрос API
        /// </summary>
        public void Pause()
        {
            _isRunning = false;
            _timer?.Dispose();
            _timer = null;
            ConnectionStatus = ConnectionStatus.Paused;
            OnDataUpdated?.Invoke();
        }

        /// <summary>
        /// Сбросить все данные
        /// </summary>
        public void Reset()
        {
            Pause();

            _last10.Clear();
            _sum10 = 0;

            _last60.Clear();
            _sum60 = 0;

            _avgAllTime = 0;
            _countAllTime = 0;

            CurrentNumber = null;
            Last10Numbers.Clear();

            UpdateStatistics();
        }

        private async Task FetchNumber()
        {
            if (!_isRunning) return;

            try
            {
                ConnectionStatus = ConnectionStatus.Connecting;

                var response = await _httpClient.GetFromJsonAsync<RandomNumberResponse>($"{_apiUrl}/random/");

                if (response != null)
                {
                    ProcessNewNumber(response.RandomNumber);
                    ConnectionStatus = ConnectionStatus.Connected;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"API Error: {ex.Message}");
                ConnectionStatus = ConnectionStatus.Error;
            }

            OnDataUpdated?.Invoke();
        }

        private void ProcessNewNumber(int number)
        {
            CurrentNumber = number;

            // Среднее за 10 секунд
            if (_last10.Count >= 10)
            {
                var removed = _last10[0];
                _last10.RemoveAt(0);
                _sum10 -= removed;
            }
            _last10.Add(number);
            _sum10 += number;

            // Среднее за 60 секунд 
            if (_last60.Count >= 60)
            {
                var removed = _last60[0];
                _last60.RemoveAt(0);
                _sum60 -= removed;
            }
            _last60.Add(number);
            _sum60 += number;

            // Среднее за всё время
            _countAllTime++;
            _avgAllTime = _avgAllTime + (number - _avgAllTime) / _countAllTime;

            // Обновляем последние 10 для графика
            Last10Numbers = new List<int>(_last10);

            UpdateStatistics();
        }

        private void UpdateStatistics()
        {
            var avg10s = _last10.Count > 0 ? _sum10 / _last10.Count : (double?)null;
            var avg60s = _last60.Count > 0 ? _sum60 / _last60.Count : (double?)null;

            Statistics = new Statistics
            {
                Current = CurrentNumber,
                Avg10Seconds = avg10s,
                Avg1Minute = avg60s,
                AvgAllTime = _avgAllTime,
                TotalCount = _countAllTime
            };
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}