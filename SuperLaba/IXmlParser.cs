using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperLaba
{




    public interface IXmlParser
    {
        List<string> Parse(string filePath, List<string> attributes, List<string> keywords);
    }
}
