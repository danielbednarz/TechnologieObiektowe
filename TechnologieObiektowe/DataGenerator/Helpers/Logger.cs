namespace DataGenerator
{
    public class Logger
    {
        public static void WriteTextLog(string message)
        {
            FileInfo logFileInfo;

            string logFilePath = "C:\\temp\\";
            logFilePath = logFilePath + "Log-" + System.DateTime.Today.ToString("MM-dd-yyyy") + "." + "txt";
            logFileInfo = new FileInfo(logFilePath);

            DirectoryInfo logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);
            if (!logDirInfo.Exists)
            {
                logDirInfo.Create();
            }
            
            FileStream fileStream;
            if (!logFileInfo.Exists)
            {
                fileStream = logFileInfo.Create();
            }
            else
            {
                fileStream = new FileStream(logFilePath, FileMode.Append);
            }

            StreamWriter log = new(fileStream);

            Console.WriteLine(message);
            log.WriteLine(DateTime.Now + ": " + message);
            log.Close();
        }

        public static void WriteCsvLog(OrmType orm, TableType table, OperationType operation, TimeSpan elapsedTime)
        {
            FileInfo logFileInfo;

            string logFilePath = "C:\\temp\\";
            logFilePath = logFilePath + "Log-" + System.DateTime.Today.ToString("MM-dd-yyyy") + "." + "csv";
            logFileInfo = new FileInfo(logFilePath);

            DirectoryInfo logDirInfo = new(logFileInfo.DirectoryName);
            if (!logDirInfo.Exists)
            {
                logDirInfo.Create();
            }

            FileStream fileStream;
            bool isNewFile = false;

            if (!logFileInfo.Exists)
            {
                fileStream = logFileInfo.Create();
                isNewFile = true;
            }
            else
            {
                fileStream = new FileStream(logFilePath, FileMode.Append);
            }

            StreamWriter log = new(fileStream);

            if (isNewFile)
            {
                log.WriteLine("Sep=;");
                log.WriteLine("ORM;Table;Operation;ElapsedTime;");
            }

            log.WriteLine($"{orm};{table};{operation};{elapsedTime};");
            log.Close();
        }
    }
}
