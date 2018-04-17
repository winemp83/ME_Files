using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;

namespace ME_Model.Funktionen
{
    public class ME_XmLDBNotizen
    {
        private string _FileName;
        public ME_XmLDBNotizen()
        {
            _FileName = "Notizen.xml";
            if (!File.Exists(_FileName))
                CreateXmL(_FileName);
        }

        public void Write(Model.ME_MNotiz value)
        {
            XmlDocument xd = new XmlDocument();
            FileStream lfile = new FileStream(_FileName, FileMode.Open);
            xd.Load(lfile);
            XmlElement cl = xd.CreateElement("Notiz");
            XmlElement na = xd.CreateElement("Text");
            XmlText natext = xd.CreateTextNode(value.Notiz);
            cl.SetAttribute("ID", value.ID);
            cl.SetAttribute("SD", value.SD);
            cl.SetAttribute("KD", value.Kunde);
            cl.SetAttribute("Datum", value.Date);
            na.AppendChild(natext);
            cl.AppendChild(na);
            xd.DocumentElement.AppendChild(cl);
            lfile.Close();
            xd.Save(_FileName);
        }

        public List<Model.ME_MNotiz> Read(string which = "all", string searchTherm = "")
        {
            List<Model.ME_MNotiz> result = new List<Model.ME_MNotiz>();
            XmlDocument xdoc = new XmlDocument();
            FileStream rfile = new FileStream(_FileName, FileMode.Open);
            xdoc.Load(rfile);
            XmlNodeList list = xdoc.GetElementsByTagName("Notiz");
            for (int i = 0; i < list.Count; i++)
            {
                XmlElement cl = (XmlElement)xdoc.GetElementsByTagName("Notiz")[i];
                XmlElement add = (XmlElement)xdoc.GetElementsByTagName("Text")[i];
                if (which.Equals("all"))
                    result.Add(new Model.ME_MNotiz(cl.GetAttribute("ID"),cl.GetAttribute("SD"), cl.GetAttribute("KD"), cl.GetAttribute("Datum"), add.InnerText));
                else if ((cl.GetAttribute(which)) == searchTherm && (which != "Datum" || which != "Text"))
                {
                    result.Add(new Model.ME_MNotiz(cl.GetAttribute("ID"), cl.GetAttribute("SD"), cl.GetAttribute("KD"), cl.GetAttribute("Datum"), add.InnerText));
                }
            }
            rfile.Close();
            return result;
        }

        public void Update(Model.ME_MNotiz value)
        {
            XmlDocument xdoc = new XmlDocument();
            FileStream up = new FileStream(_FileName, FileMode.Open);
            xdoc.Load(up);
            XmlNodeList list = xdoc.GetElementsByTagName("Notiz");
            for (int i = 0; i < list.Count; i++)
            {
                XmlElement cu = (XmlElement)xdoc.GetElementsByTagName("Notiz")[i];
                XmlElement add = (XmlElement)xdoc.GetElementsByTagName("Text")[i];
                if (cu.GetAttribute("ID") == value.ID)
                {
                    cu.SetAttribute("ID", value.ID);
                    cu.SetAttribute("SD", value.SD);
                    cu.SetAttribute("KD", value.Kunde);
                    cu.SetAttribute("Datum", value.Date);
                    add.InnerText = value.Notiz;
                    break;
                }
            }
            up.Close();
            xdoc.Save(_FileName);
        }

        public void Delete(Model.ME_MNotiz value)
        {
            FileStream rfile = new FileStream(_FileName, FileMode.Open);
            XmlDocument tdoc = new XmlDocument();
            tdoc.Load(rfile);
            XmlNodeList list = tdoc.GetElementsByTagName("Notiz");
            for (int i = 0; i < list.Count; i++)
            {
                XmlElement cl = (XmlElement)tdoc.GetElementsByTagName("Notiz")[i];
                if (cl.GetAttribute("ID") == value.ID)
                {
                    tdoc.DocumentElement.RemoveChild(cl);
                }
            }
            rfile.Close();
            tdoc.Save(_FileName);
        }

        private void CreateXmL(string FileName)
        {
            XmlTextWriter xtw;
            String[] _tmp = FileName.Split('.');
            xtw = new XmlTextWriter(FileName, Encoding.UTF8);
            xtw.WriteStartDocument();
            xtw.WriteStartElement(_tmp[0]);
            xtw.WriteEndElement();
            xtw.Close();
        }
    }
}