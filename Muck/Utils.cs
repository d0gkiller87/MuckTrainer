using System.IO;

namespace bruh {
    static class Utils {
        public static void Log(string message) {
            try {
                File.AppendAllText(@"%tmp%\muck.log", message);
            } catch (IOException) {}
        }
    }
}
