using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpFrontAI
{
    public static class Server
    {
        public static string Directory { get; set; }
        public static void InitializeServer(int port)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "python",
                Arguments = $"-m http.server {port}",
                WorkingDirectory = Directory,
                CreateNoWindow = true,
                UseShellExecute = false
            };

            Process process = new Process { StartInfo = startInfo };
            bool started = process.Start();
            if (!started)
                Console.WriteLine($"Server failed to start. Please check the port {port} is not already in use.");
            else
                Console.WriteLine($"Server started. You can view the web page at http://localhost:{port}.");
        }
    }
}
