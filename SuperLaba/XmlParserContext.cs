using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperLaba
{
    public class XmlParserContext
    {
        private IXmlParser _parser;

        public XmlParserContext(IXmlParser parser)
        {
            _parser = parser;
        }

      
        public void SetParser(IXmlParser parser)
        {
            _parser = parser;
        }

        
        public List<string> ExecuteParse(string filePath, List<string> attributes, List<string> keywords)
        {
            return _parser.Parse(filePath, attributes, keywords);
        }
    }

}
