using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SuperLaba
{


    public class SaxXmlParser : IXmlParser
    {
        public List<string> Parse(string filePath, List<string> attributes, List<string> keywords)
        {
            var results = new List<string>();

            using (var reader = XmlReader.Create(filePath))
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.HasAttributes)
                    {
                        foreach (var attribute in attributes)
                        {
                            var value = reader.GetAttribute(attribute);
                            if (value != null && keywords.Any(keyword => value.Contains(keyword)))
                            {
                                results.Add($"Element: {reader.Name}\nAttribute: {attribute} = {value}\n{reader.ReadOuterXml()}\n");
                            }
                        }
                    }
                }
            }

            return results;
        }
    }
}