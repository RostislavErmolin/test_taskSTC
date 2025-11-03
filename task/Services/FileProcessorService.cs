using System.Text;
using task.Models;

namespace task.Services;

/// <summary>
/// Сервис для обработки текстовых файлов и подсчета частоты символов
/// </summary>
public class FileProcessorService
{
    private const int ChunkSize = 1024 * 1024; // 1 MB
    private readonly Dictionary<string, int> _frequencyMap = new();

    /// <summary>
    /// Событие для отслеживания прогресса обработки файла
    /// </summary>
    public event Action<ProcessingProgress>? ProgressChanged;

    /// <summary>
    /// Обработка файла с подсчетом частоты символов
    /// </summary>
    /// <param name="fileStream">Поток данных файла</param>
    /// <param name="fileSize">Размер файла в байтах</param>
    /// <param name="fileName">Имя файла</param>
    /// <returns>Результат анализа частоты символов</returns>
    public async Task<FrequencyResult> ProcessFileAsync(Stream fileStream, long fileSize, string fileName)
    {
        _frequencyMap.Clear();

        var startTime = DateTime.Now;
        long processedBytes = 0;
        var totalChunks = (int)Math.Ceiling((double)fileSize / ChunkSize);
        var currentChunk = 0;

        var decoder = Encoding.UTF8.GetDecoder();
        var buffer = new byte[ChunkSize];
        var charBuffer = new char[ChunkSize];

        // Начальный прогресс
        EmitProgress(new ProcessingProgress
        {
            BytesProcessed = 0,
            TotalBytes = fileSize,
            Percentage = 0,
            Speed = 0,
            Eta = 0,
            Status = ProcessingStatusEnum.Processing,
            CurrentChunk = 0,
            TotalChunks = totalChunks
        });

        while (processedBytes < fileSize)
        {
            // Читаем chunk
            var bytesToRead = (int)Math.Min(ChunkSize, fileSize - processedBytes);
            var bytesRead = await fileStream.ReadAsync(buffer.AsMemory(0, bytesToRead));

            if (bytesRead == 0) break;

            // Декодируем байты в символы
            var isLastChunk = processedBytes + bytesRead >= fileSize;
            var charsDecoded = decoder.GetChars(buffer, 0, bytesRead, charBuffer, 0, flush: isLastChunk);

            // Обрабатываем декодированный текст
            ProcessText(new string(charBuffer, 0, charsDecoded));

            // Обновляем прогресс
            processedBytes += bytesRead;
            currentChunk++;

            var elapsedSeconds = (DateTime.Now - startTime).TotalSeconds;
            var speed = elapsedSeconds > 0 ? processedBytes / elapsedSeconds : 0;
            var remainingBytes = fileSize - processedBytes;
            var eta = speed > 0 ? remainingBytes / speed : 0;

            EmitProgress(new ProcessingProgress
            {
                BytesProcessed = processedBytes,
                TotalBytes = fileSize,
                Percentage = (double)processedBytes / fileSize * 100,
                Speed = speed,
                Eta = eta,
                Status = ProcessingStatusEnum.Processing,
                CurrentChunk = currentChunk,
                TotalChunks = totalChunks
            });

            // Даем UI возможность обновиться
            await Task.Yield();
        }

        var processingTime = (long)(DateTime.Now - startTime).TotalMilliseconds;

        // Финальный прогресс
        EmitProgress(new ProcessingProgress
        {
            BytesProcessed = fileSize,
            TotalBytes = fileSize,
            Percentage = 100,
            Speed = 0,
            Eta = 0,
            Status = ProcessingStatusEnum.Completed,
            CurrentChunk = totalChunks,
            TotalChunks = totalChunks
        });

        return new FrequencyResult
        {
            FrequencyMap = new Dictionary<string, int>(_frequencyMap),
            TotalChars = _frequencyMap.Values.Sum(),
            UniqueChars = _frequencyMap.Count,
            ProcessingTime = processingTime,
            FileName = fileName,
            FileSize = fileSize
        };
    }

    /// <summary>
    /// Обработка текста - подсчет символов 
    /// </summary>
    /// <param name="text">Текст для анализа</param>
    private void ProcessText(string text)
    {
        var normalized = text.ToLower();

        foreach (var ch in normalized)
        {
            var key = ch.ToString();
            _frequencyMap.TryGetValue(key, out var count);
            _frequencyMap[key] = count + 1;
        }
    }

    /// <summary>
    /// Отправка события обновления прогресса
    /// </summary>
    /// <param name="progress">Объект прогресса</param>
    private void EmitProgress(ProcessingProgress progress)
    {
        ProgressChanged?.Invoke(progress);
    }
}