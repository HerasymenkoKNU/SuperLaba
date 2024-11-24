using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SuperLaba
{


    public class LinqToXmlParser : IXmlParser
    {
        public List<string> Parse(string filePath, List<string> attributes, List<string> keywords)
        {
            var results = new List<string>();
            var doc = XDocument.Load(filePath);

            foreach (var element in doc.Descendants())
            {
                foreach (var attribute in attributes)
                {
                    var value = element.Attribute(attribute)?.Value;
                    if (value != null && keywords.Any(keyword => value.Contains(keyword)))
                    {
                        results.Add(element.ToString());
                    }
                }
            }

            return results;
        }
    }


}
