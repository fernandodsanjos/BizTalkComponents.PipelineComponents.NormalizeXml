using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.BizTalk.Streaming;
using System.Xml;
using System.IO;

namespace BizTalkComponents.PipelineComponents.NormalizeXml
{
    public class NormalizeXmlStream : XmlTranslatorStream
    {
        
        public NormalizeXmlStream(XmlReader reader) : base(reader)
        {

        }

        public NormalizeXmlStream(XmlReader reader, Encoding encoding) : base(reader, encoding)
        {
        }

        protected override void TranslateStartElement(string prefix, string localName, string nsURI)
        {

          

            if (base.m_reader.Depth == 2)
            {
                
                base.m_writer.WriteStartElement(localName);
               

                WriteAttributes(base.m_reader.ReadSubtree());
                base.m_writer.WriteEndElement();

            }
            else
            {

                base.TranslateStartElement(prefix, localName, nsURI);
            }


        }

        private void WriteAttributes(XmlReader reader)
        {
            reader.MoveToContent();

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {

                    string localName = reader.LocalName;

                    if (reader.IsEmptyElement == false)
                    {
                        while (reader.NodeType != XmlNodeType.Text && reader.NodeType != XmlNodeType.EndElement && reader.Read()) ;

                        if (reader.NodeType == XmlNodeType.EndElement)
                            continue;

                        string text = reader.Value;

                        base.m_writer.WriteAttributeString(localName, text.Trim() );
                    }
                }
            }

        }
    }
}
