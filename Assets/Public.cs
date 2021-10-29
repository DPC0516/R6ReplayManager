using System.IO;

public static class Paths
{
    public static string R6ReplayPath;
    public static string R6ReplaySavePath;
}

public static class Public
{
    public static LogManager logManager;

    public static bool CheckPathSetting()
    {
        return CheckPathSetting(Paths.R6ReplayPath, Paths.R6ReplaySavePath);
    }

    public static bool CheckPathSetting(string _R6ReplayPath, string _R6ReplaySavePath)
    {
        try
        {
            if (!new DirectoryInfo(_R6ReplayPath).Exists)
            {
                logManager.AddLog("R6 Replay Path setting is weird");
                return false;
            }
        }
        catch
        {
            logManager.AddLog("R6 Replay Path setting is weird");
            return false;
        }
        try
        {
            if (!new DirectoryInfo(_R6ReplaySavePath).Exists)
            {
                logManager.AddLog("R6 Replay Save Path setting is weird");
                return false;
            }
        }
        catch
        {
            logManager.AddLog("R6 Replay Save Path setting is weird");
            return false;
        }
        return true;

    }
}