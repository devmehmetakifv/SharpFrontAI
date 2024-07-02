namespace SharpFront.Models
{
    public class Prompt
    {
        public string Content { get; set; }
        public Prompt(string _content)
        {
            Content = _content;
        }
    }
    public class PromptResult
    {
        public string Area { get; set; }
        public string Prompt { get; set; }
        public string Result { get; set; }
    }

}
