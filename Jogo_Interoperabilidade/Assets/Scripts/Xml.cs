using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Text;

public class Xml
{
    


    public void EscreveXML(int turno, string vencedor)
    {
        
        XmlTextWriter writer = new XmlTextWriter(@"D:\Documentos\Ifrj\Sexto periodo\Interoperabilidade\Projeto\BuildXML\Xml.xml",null );

        writer.WriteStartDocument();
        writer.Formatting = Formatting.Indented;

        writer.WriteStartElement("Jogo");
        writer.WriteStartElement("Level1");
        writer.WriteElementString("turno",turno.ToString());
        writer.WriteElementString("vencedor", vencedor);
       
        writer.WriteEndElement();
        writer.WriteFullEndElement();
        writer.Close();


    }

    public string LendoXML()
    {

        return File.ReadAllText(@"D:\Documentos\Ifrj\Sexto periodo\Interoperabilidade\Projeto\BuildXML\Xml.xml");
    }

    public JogoXml converterXmlParaObjeto(string xml)
    {
        byte[] byteArray = Encoding.UTF8.GetBytes(xml);
        MemoryStream stream = new MemoryStream(byteArray);
        
        XmlTextReader xtr = new XmlTextReader(stream);

        
        JogoXml dadosXml = new JogoXml();

        while(xtr.Read())
        {

            if(xtr.NodeType == XmlNodeType.Element && xtr.Name == "turno")
            {

                dadosXml.turno = xtr.ReadElementContentAsInt();


            }
            if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "vencedor")
            {

                dadosXml.vencedor = xtr.ReadElementContentAsString();
                
                

            }

        }

        return dadosXml;

    }



}
