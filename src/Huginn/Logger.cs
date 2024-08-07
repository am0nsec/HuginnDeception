

using System.Diagnostics;

namespace Huginn {
    internal class Logger {

        /// <summary>
        /// Whether the logger is enabeled.
        /// </summary>
        public static bool IsVerbose = false;

        /// <summary>
        /// Log a verbose message.
        /// </summary>
        public static void Verbose(
            string Message,
            bool   NewLine = true
        ) { 
            if (!Logger.IsVerbose) {
                return;
            }
            Write(Message, NewLine);
        }

        /// <summary>
        /// Log an info message.
        /// </summary>
        public static void Info(
            string Message,
            bool   NewLine = true
        ) {
            Console.ForegroundColor = ConsoleColor.Blue;
            Write("[*] ", false);
            Console.ResetColor();
            Write(Message, NewLine);
        }

        /// <summary>
        /// Log a success message.
        /// </summary>
        public static void Success(
            string Message,
            bool   NewLine = true
        ) {
            Console.ForegroundColor = ConsoleColor.Green;
            Write("[+] ", false);
            Console.ResetColor();
            Write(Message, NewLine);
        }

        /// <summary>
        /// Log an error message.
        /// </summary>
        public static void Error(
            string Message,
            bool   NewLine = true
        ) {
            Console.ForegroundColor = ConsoleColor.Red;
            Write("[-] ", false);
            Console.ResetColor();
            Write(Message, NewLine);
        }

        /// <summary>
        /// Log a message to stdout.
        /// </summary>
        public static void Write(
            string Message,
            bool   NewLine = true
        ) {
            Message += NewLine ? Environment.NewLine : "";
            Console.Write(Message);
        }
    }
}
