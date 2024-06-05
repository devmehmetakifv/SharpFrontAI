using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpFrontAI
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
