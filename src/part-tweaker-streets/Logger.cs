using MelonLoader;
using System.Drawing;
using System.Reflection;

namespace part_tweaker_streets
{
    public static class Logger
    {
        static MelonLogger.Instance loggerInstance;
        public enum LogLevel { Info, Error, Warning, Debug }
        
        public static void Log(string msg, LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Info:
                    loggerInstance = new MelonLogger.Instance(Assembly.GetExecutingAssembly().GetName().Name, Color.BlueViolet);
                    break;
                case LogLevel.Error:
                    loggerInstance = new MelonLogger.Instance(Assembly.GetExecutingAssembly().GetName().Name, Color.Red);
                    break;
                case LogLevel.Warning:
                    loggerInstance = new MelonLogger.Instance(Assembly.GetExecutingAssembly().GetName().Name, Color.Yellow);
                    break;
                case LogLevel.Debug:
                    loggerInstance = new MelonLogger.Instance(Assembly.GetExecutingAssembly().GetName().Name, Color.CornflowerBlue);
                    break;
                default:
                    throw new Exception("Not a valid log level!");
            }

            loggerInstance.Msg(msg);
        }
    }
}
