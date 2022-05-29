namespace EntityFrameworkProject
{
    public class Logger
    {
        public static void WriteLog(string message)
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
    }
}
