using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SharpFrontAI
{
    public class Website
    {
        public string Title { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string Keywords { get; set; }
        public string Language { get; set; }
        public string Theme { get; set; }
        public List<RGBColor> ColorList { get; set; }
    }
}
