using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpFrontAI
{
    public class GeminiAI
    {
        public string Model { get; set; }
        public string APIKey { get; set; }
        public string FixedPrompt { get; set; }
        public GeminiAI()
        {
            var secretsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Microsoft", "UserSecrets", "afdd8d1e-7552-4d8e-b199-619e2adab86f", "secrets.json");
            var secrets = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(secretsPath));
            APIKey = secrets["GEMINI_API"];
        }
    }
}
