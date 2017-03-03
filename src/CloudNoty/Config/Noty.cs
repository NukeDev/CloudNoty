using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudNoty.Config
{
    public class Noty
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int Autor { get; set; }
        public string Content { get; set; }
        public string EncrAlg { get; set; }
        public DateTime LastEdit { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
