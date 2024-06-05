using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SharpFrontAI;

namespace SharpFrontTest
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            Website myWebsite = new Website
            {
                Title = "Mehmet Akif VARDAR's Portfolio Website.",
                Language = "en-US",
                Category = "Personal Portfolio Website",
                Description = "A portfolio website for Mehmet Akif VARDAR, a 2nd year software engineering student specialized in Database Administration and Web Development.",
                Keywords = "Software Engineering, Database Administration, Database Development, .NET Core, Backend Development",
                ColorList = new List<RGBColor> { new RGBColor(0, 0, 0), new RGBColor(255, 255, 255) },
                Theme = "Dark & White, Elegant, Simple and Responsive. Use Bootstrap 5.2.3.",
            };
            Server.Directory = "C:\\Users\\Mehmet Akif\\Desktop\\Server" ;

            GeminiAI myAI = new GeminiAI 
            {
                Model = "gemini-1.5-flash",
                FixedPrompt = $"Here are some informations that might be useful: Website Title: {myWebsite.Title}, Website Category: {myWebsite.Category}, Website Language: {myWebsite.Language}, Website Keywords: {myWebsite.Keywords}, Website Theme: {myWebsite.Theme}, Website Description: {myWebsite.Description}. Only generate the code all the way to the end of the header tag, don't say anything else. Don't code CSS.",
            };

            WebSection headerSection = new WebSection();
            await headerSection.CodeAsync(myAI, new Prompt($"Create a header section with a logo and a navigation bar. Logo should be aligned to left and navigation bar should be aligned to right. Navigation bar should contain the followings: Home, Privacy Policy, Social Media, My Posts, Projects, Contact Me. {myAI.FixedPrompt}"));
            WebSection learnMoreSection = new WebSection();
            await learnMoreSection.CodeAsync(myAI, new Prompt($"Create a Lean More section under the header you've just generated. There must be 3 figures aligned in middle with texts underneath them. First figure text is 'Student', second figure text is 'Programmer', third figure text is 'Musician'. Under the 'Programmer' text, there is a button that has a text 'Learn More'. {myAI.FixedPrompt}"));
            //WebSection aboutMeSection = new WebSection();
            //aboutMeSection.Code(new Prompt($"Create an About Me section under 'Learn More' section. This section is like a square divided into 4 parts. Upper left and bottom right parts has image figures, rest contain some texts about me. Write lorem ipsum texts for them for now. {myAI.FixedPrompt}"));
            //WebSection footerSection = new WebSection();
            //footerSection.Code(new Prompt($"Create a footer section at the bottom of the website. This section has only one part and it's the copyright text. It says '© 2023 - This website is coded by Mehmet Akif VARDAR. All rights reserved.' and it is aligned to left. {myAI.FixedPrompt}"));

            Server.InitializeServer(8080);
        }
    }
}
