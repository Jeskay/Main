using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mainWpf
{
    public class ChartController
    {
        public void ReadData(string Path)
        {
            StreamReader sr = new StreamReader(Path);
            
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                

            }
        }
    }
}
