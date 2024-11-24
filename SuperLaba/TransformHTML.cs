using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Xsl;

namespace SuperLaba
{
    class TransformHTML
    {
        public static void TransformXmlToHtml(string xmlFilePath, string htmlFilePath)
        {
            var xslt = new XslCompiledTransform();


            string xsltFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "template.xslt");

            if (!File.Exists(xsltFilePath))
            {
                throw new FileNotFoundException("XSLT не знайдено за : " + xsltFilePath);
            }

            xslt.Load(xsltFilePath);

            xslt.Transform(xmlFilePath, htmlFilePath);
        }
    }
}
