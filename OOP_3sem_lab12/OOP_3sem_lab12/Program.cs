using System;
using System.IO;
using System.Linq;

public class XXXLog
{
    private const string logFilePath = "C:\\Users\\user\\source\\repos\\OOP_3sem_lab12\\OOP_3sem_lab12\\xxxlogfile.txt";

    // Метод для логирования действия
    public void LogAction(string action, string fileName = "xxxlogfile.txt", string filePath = "C:\\Users\\user\\source\\repos\\OOP_3sem_lab12\\OOP_3sem_lab12\\xxxlogfile.txt")
    {
        string logEntry = $"[Действие: {action}] [Файл: {fileName}] [Путь: {filePath}] [Дата/Время: {DateTime.Now}]";

        using (StreamWriter writer = new StreamWriter(filePath, true))
        {
            writer.WriteLine(logEntry);
        }
    }

    // Метод для чтения журнала
    public string ReadLog()
    {
        if (File.Exists(logFilePath))
        {
            return File.ReadAllText(logFilePath);
        }
        return "Файл журнала не найден.";
    }

    // Метод для поиска по ключевому слову
    public string SearchLog(string keyword)
    {
        if (File.Exists(logFilePath))
        {
            var result = File.ReadAllLines(logFilePath)
                             .Where(line => line.Contains(keyword))
                             .ToList();

            return result.Count > 0 ? string.Join("\n", result) : "Совпадений не найдено.";
        }
        return "Файл журнала не найден.";
    }

    // Метод для поиска по дате или диапазону дат
    public string SearchLogByDate(DateTime startDate, DateTime? endDate = null)
    {
        if (File.Exists(logFilePath))
        {
            var logEntries = File.ReadAllLines(logFilePath)
                                 .Where(line => line.Contains("Дата/Время"))
                                 .ToList();

            var filteredEntries = logEntries.Where(entry =>
            {
                // Извлекаем дату/время из записи
                string dateString = entry.Split(new[] { "[Дата/Время: " }, StringSplitOptions.None)[1].Split(']')[0];
                DateTime logDate = DateTime.Parse(dateString);

                // Фильтрация по диапазону дат
                return endDate == null ? logDate.Date == startDate.Date : logDate >= startDate && logDate <= endDate;
            }).ToList();

            return filteredEntries.Count > 0 ? string.Join("\n", filteredEntries) : "Записи за указанный период не найдены.";
        }
        return "Файл журнала не найден.";
    }

    // Подсчет общего количества записей в логе
    public int CountLogEntries()
    {
        if (File.Exists(logFilePath))
        {
            return File.ReadAllLines(logFilePath).Count(line => line.Contains("Действие:"));
        }
        return 0;
    }
}
public static class XXXDiskInfo
{
    static DriveInfo[] allDrives = DriveInfo.GetDrives();
 
    public static void DiskInfo()
    {
        using (StreamWriter writer = new StreamWriter("C:\\Users\\user\\source\\repos\\OOP_3sem_lab12\\OOP_3sem_lab12\\xxxlogfile.txt", true))
        {
            foreach (DriveInfo drive in allDrives)
            {
                writer.WriteLine($"Диск: {drive.Name}");

                if (drive.IsReady)
                {
                    writer.WriteLine($"  Тип диска: {drive.DriveType}");
                    writer.WriteLine($"  Файловая система: {drive.DriveFormat}");
                    writer.WriteLine($"  Объем диска: {drive.TotalSize / (1024 * 1024 * 1024)} GB");
                    writer.WriteLine($"  Свободно места: {drive.AvailableFreeSpace / (1024 * 1024 * 1024)} GB");
                    writer.WriteLine($"  Метка тома: {drive.VolumeLabel}");
                }
                else
                {
                    writer.WriteLine("Диск не готов.");
                }

                writer.WriteLine();
            }
        }
    }
}

public static class XXXFileInfo
{
    static FileInfo fileInfo = new FileInfo("C:\\Users\\user\\source\\repos\\OOP_3sem_lab12\\OOP_3sem_lab12\\xxxlogfile.txt");

    public static void FileInfo() 
    {
        using (StreamWriter writer = new StreamWriter("C:\\Users\\user\\source\\repos\\OOP_3sem_lab12\\OOP_3sem_lab12\\xxxlogfile.txt", true))
        {
            if (fileInfo.Exists)
            {
                writer.WriteLine($"Имя файла: {fileInfo.Name}");
                writer.WriteLine($"Путь: {fileInfo.FullName}");
                writer.WriteLine($"Размер: {fileInfo.Length} байт");
                writer.WriteLine($"Расширение: {fileInfo.Extension}");
                writer.WriteLine($"Дата создания: {fileInfo.CreationTime}");
                writer.WriteLine($"Последнее изменение: {fileInfo.LastWriteTime}");
                writer.WriteLine($"Атрибуты: {fileInfo.Attributes}");
            }
            else
            {
                writer.WriteLine("Файл не найден.");
            }
        }
    }
}

public  class XXXDirInfo
{
    private DirectoryInfo dirInfo;
    private static string logFilePath = "C:\\Users\\user\\source\\repos\\OOP_3sem_lab12\\OOP_3sem_lab12\\xxxlogfile.txt";

    public XXXDirInfo(string dirPath)
    {
        if (Directory.Exists(dirPath))
        {
            dirInfo = new DirectoryInfo(dirPath);
        }
        else
        {
            throw new DirectoryNotFoundException($"Директория {dirPath} не найдена.");
        }
    }

    public void LogFileCount()
    {
        FileInfo[] files = dirInfo.GetFiles();
        using (StreamWriter writer = new StreamWriter(logFilePath, true))
        {
            writer.WriteLine($"Количество файлов в директории {dirInfo.FullName}: {files.Length}");
        }
    }

    public void LogCreationTime()
    {
        using (StreamWriter writer = new StreamWriter(logFilePath, true))
        {
            writer.WriteLine($"Время создания директории {dirInfo.FullName}: {dirInfo.CreationTime}");
        }
    }

    public void LogSubdirectoryCount()
    {
        DirectoryInfo[] subdirs = dirInfo.GetDirectories();
        using (StreamWriter writer = new StreamWriter(logFilePath, true))
        {
            writer.WriteLine($"Количество поддиректорий в {dirInfo.FullName}: {subdirs.Length}");
        }
    }

    public void LogParentDirectories()
    {
        using (StreamWriter writer = new StreamWriter(logFilePath, true))
        {
            writer.WriteLine($"Родительские директории для {dirInfo.FullName}:");

            DirectoryInfo parentDir = dirInfo.Parent;
            while (parentDir != null)
            {
                writer.WriteLine(parentDir.FullName);
                parentDir = parentDir.Parent;
            }
        }
    }

    public static void Demo()
    {
        try
        {
            XXXDirInfo dirInfo = new XXXDirInfo("C:\\Users\\user\\source\\repos\\OOP_3sem_lab12");

            dirInfo.LogFileCount();
            dirInfo.LogCreationTime();
            dirInfo.LogSubdirectoryCount();
            dirInfo.LogParentDirectories();
        }
        catch (Exception ex)
        {
            using (StreamWriter writer = new StreamWriter(logFilePath, true))
            {
                writer.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }
}


class Program
{
    static void Main(string[] args)
    {
        XXXLog logger = new XXXLog();
        logger.LogAction("Создание файла:", "xxxlogfile.txt", @"C:\\Users\\user\\source\\repos\\OOP_3sem_lab12\\OOP_3sem_lab12\\xxxlogfile.txt");

        logger.LogAction("Чтение из файла:", "xxxlogfile.txt", @"C:\\Users\\user\\source\\repos\\OOP_3sem_lab12\\OOP_3sem_lab12\\xxxlogfile.txt");
        Console.WriteLine(logger.ReadLog());

        logger.LogAction("Поиск в файле:", "search_task1.txt", @"C:\\Users\\user\\source\\repos\\OOP_3sem_lab12\\OOP_3sem_lab12\\xxxlogfile.txt");
        Console.WriteLine(logger.SearchLog("using\\"));

        logger.LogAction("Информация о диске:", "xxxlogfile", @"C:\\Users\\user\\source\\repos\\OOP_3sem_lab12\\OOP_3sem_lab12\\xxxlogfile.txt");
        XXXDiskInfo.DiskInfo();

        logger.LogAction("Информация о файле:", "xxxlogfile", @"C:\\Users\\user\\source\\repos\\OOP_3sem_lab12\\OOP_3sem_lab12\\xxxlogfile.txt");
        XXXFileInfo.FileInfo();

        logger.LogAction("Информация о директории:", "OOP_3sem_lab12", @"C:\\Users\\user\\source\\repos\\OOP_3sem_lab12\\OOP_3sem_lab12\\xxxlogfile.txt");
        XXXDirInfo.Demo();

        DateTime searchDate = DateTime.Now.AddDays(-4); 
        Console.WriteLine($"\nЗаписи за {searchDate.ToShortDateString()}:");
        Console.WriteLine(logger.SearchLogByDate(searchDate));

        DateTime startDate = DateTime.Now.AddDays(-1);
        DateTime endDate = DateTime.Now;
        Console.WriteLine($"\nЗаписи с {startDate.ToShortDateString()} по {endDate.ToShortDateString()}:");
        Console.WriteLine(logger.SearchLogByDate(startDate, endDate));

        Console.WriteLine($"\nОбщее количество записей в журнале: {logger.CountLogEntries()}");

        File.AppendAllText(@"C:\\Users\\user\\source\\repos\\OOP_3sem_lab12\\OOP_3sem_lab12\\xxxlogfile.txt", "\n\n");
    }
}
 