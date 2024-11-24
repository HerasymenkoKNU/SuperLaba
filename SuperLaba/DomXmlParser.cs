using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace SuperLaba
{
    public class DomXmlParser : IXmlParser
    {
        public List<string> Parse(string filePath, List<string> attributes, List<string> keywords)
        {
            var results = new List<string>();
            var doc = new XmlDocument();
            doc.Load(filePath);

            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                if (node.Attributes != null)
                {
                    foreach (var attribute in attributes)
                    {
                        var value = node.Attributes[attribute]?.Value;
                        if (value != null && keywords.Any(keyword => value.Contains(keyword)))
                        {
                            var elementXml = node.OuterXml;
                            var formattedXml = FormatXml(elementXml);
                            results.Add($"Element:\n{formattedXml}\n");
                        }
                    }
                }
            }

            return results;
        }

        private string FormatXml(string xml)
        {
            var doc = XDocument.Parse(xml);
            return doc.ToString();
        }
    }




}
