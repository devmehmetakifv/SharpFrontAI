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
}
