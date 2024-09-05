
using System.Diagnostics;
using System.IO;

class ProcessManager
{
    private static string logFilePath = "process_log.txt";

    public static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("1. Показать запущенные процессы");
            Console.WriteLine("2. Завершить процесс по ID");
            Console.WriteLine("3. Запустить новый процесс");
            Console.WriteLine("4. Выйти из программы");
            Console.Write("Выберите действие: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    ShowRunningProcesses();
                    break;
                case 2:
                    TerminateProcess();
                    break;
                case 3:
                    StartNewProcess();
                    break;
                case 4:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Неверный выбор");
                    break;
            }
        }
    }

    public static void ShowRunningProcesses()
    {
        Process[] processes = Process.GetProcesses();
        using (StreamWriter sw = File.AppendText(logFilePath))
        {
            foreach (Process process in processes)
            {
                Console.WriteLine($"Имя процесса: {process.ProcessName}, ID: {process.Id}, Потребляемая память: {process.WorkingSet64} байт, Состояние: {process.Responding}");
                sw.WriteLine($"Имя процесса: {process.ProcessName}, ID: {process.Id}, Потребляемая память: {process.WorkingSet64} байт, Состояние: {process.Responding}");
            }
        }
    }

    public static void TerminateProcess()
    {
        Console.Write("Введите ID процесса для завершения: ");
        int processId = int.Parse(Console.ReadLine());

        Process process = Process.GetProcessById(processId);
        process.Kill();

        using (StreamWriter sw = File.AppendText(logFilePath))
        {
            Console.WriteLine($"Процесс с ID {processId} завершен");
            sw.WriteLine($"Процесс с ID {processId} завершен");
        }
    }

    public static void StartNewProcess()
    {
        Console.Write("Введите путь к исполняемому файлу для запуска: ");
        string path = Console.ReadLine();

        Process.Start(path);

        using (StreamWriter sw = File.AppendText(logFilePath))
        {
            Console.WriteLine("Новый процесс запущен");
            sw.WriteLine("Новый процесс запущен");
        }
    }
}