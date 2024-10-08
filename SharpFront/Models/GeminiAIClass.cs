﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace SharpFront.Models
{
    public class GeminiAIClass
    {
        public string Model { get; set; }
        public string APIKey { get; set; }
        public string FixedPrompt { get; set; }
        public string SystemPrompt { get; set; } // Added system prompt

        public GeminiAIClass()
        {
            var secretsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Microsoft", "UserSecrets", "afdd8d1e-7552-4d8e-b199-619e2adab86f", "secrets.json");
            var secrets = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(secretsPath));
            APIKey = secrets["GEMINI_API"];
        }

        /// <summary>
        /// Use Code method to add a new section to the website.
        /// This method takes a Prompt object as prompt to describe the section to Gemini.
        /// Gemini will then generate the code for the section.
        /// </summary>
        /// <param name="prompt">Prompt object to describe the section to Gemini.</param>
        /// <param name="AI">GeminiAI object to use API key and model name in defining the endpoint.</param>
    
        public async Task<string> CodeAsync(Prompt prompt)
        {
            string url = $"https://generativelanguage.googleapis.com/v1beta/models/{Model}:generateContent?key={APIKey}";
            string json = $@"{{
                ""contents"": [{{
                    ""parts"": [{{
                        ""text"": ""{prompt.Content}""
                    }}]
                }}]
            }}";
            //Added system prompt part

            using (HttpClient client = new HttpClient())
            {
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(url, content);

                //bu noktada bekleme oluyor (await fonksiyonu olduğu için olabilir)
                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    JObject obj = JObject.Parse(responseString);
                    string code = (string)obj["candidates"][0]["content"]["parts"][0]["text"];
                    code = code.Replace("`", "");
                    return code;
                    // Write the code to the file.
                }
                else
                {
                    Console.WriteLine(url);
                    Console.WriteLine($"Failed to generate code for the section. Error code: {response.StatusCode}");
                    return null;
                }
            }
        }
        public static string MakePromtp(Dictionary<string, string> prompts)
        {

            string mainPrompt = $"You are a website builder. Generate a website code according to the descriptions of the entered parts," +
                $"use bootstrap and make the design beautiful ,write 'only' code, no descriptions or titles etc and just send the code : ";
            foreach (var key in prompts.Keys)
            {
                mainPrompt = mainPrompt + $" for the {key} part: {prompts[key]}";
            }
            return mainPrompt;
        }
       
    }
}
