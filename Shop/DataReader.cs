using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Shop
{
    public class DataReader : IDisposable
    {
        private readonly XmlReader baseReader;
        private readonly XmlReader reader;

        public DataReader(String stream)
        {
            baseReader = XmlReader.Create(stream, new XmlReaderSettings() { DtdProcessing = DtdProcessing.Ignore});
            baseReader.MoveToContent();
            reader = baseReader.ReadSubtree();
        }

        public IEnumerable<XElement> GetXmlItems(string name)
        {
            for (var exists = MoveToElement(name); exists; exists = reader.ReadToNextSibling(name))
            {
                yield return (XElement)(XNode.ReadFrom(reader));
            }
        }

        public bool MoveToElement(string name)
        {
            while (reader.Read())
            {
                if ((reader.NodeType == XmlNodeType.Element) && reader.LocalName.Equals(name))
                    return true;
            }
            return false;
        }
        
        public void Dispose()
        {
            reader.Close();
            baseReader.Close();
            //reader.Dispose();
            //baseReader.Dispose();
        }
    }
}
