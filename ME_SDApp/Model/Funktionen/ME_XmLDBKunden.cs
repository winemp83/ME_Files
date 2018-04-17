using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;

namespace ME_Model.Funktionen
{
    public class ME_XmLDBKunden
    {
        private string _FileName;
        public ME_XmLDBKunden()
        {
            _FileName = "Kunden.xml";
            if (!File.Exists(_FileName))
                CreateXmL(_FileName);
        }

        public void Write(Model.ME_MKunde value) {
            XmlDocument xd = new XmlDocument();
            FileStream lfile = new FileStream(_FileName, FileMode.Open);
            xd.Load(lfile);
            XmlElement cl = xd.CreateElement("Kunde");
            cl.SetAttribute("KD", value.KundenNummer);
            cl.SetAttribute("Name", value.Name);
            xd.DocumentElement.AppendChild(cl);
            lfile.Close();
            xd.Save(_FileName);
        }

        public List<Model.ME_MKunde> Read(string which = "all", string searchTherm = "") {
            List<Model.ME_MKunde> result = new List<Model.ME_MKunde>();
            XmlDocument xdoc = new XmlDocument();
            FileStream rfile = new FileStream(_FileName, FileMode.Open);
            xdoc.Load(rfile);
            XmlNodeList list = xdoc.GetElementsByTagName("Kunde");
            for (int i = 0; i < list.Count; i++)
            {
                XmlElement cl = (XmlElement)xdoc.GetElementsByTagName("Kunde")[i];
                if (which.Equals("all"))
                    result.Add(new Model.ME_MKunde(cl.GetAttribute("KD"), cl.GetAttribute("Name")));
                else if ((cl.GetAttribute(which)) == searchTherm)
                {
                    result.Add(new Model.ME_MKunde(cl.GetAttribute("KD"), cl.GetAttribute("Name")));
                    break;
                }
            }
            rfile.Close();
            return result;
        }

        public void Update(Model.ME_MKunde value){
            XmlDocument xdoc = new XmlDocument();
            FileStream up = new FileStream(_FileName, FileMode.Open);
            xdoc.Load(up);
            XmlNodeList list = xdoc.GetElementsByTagName("Kunde");
            for (int i = 0; i < list.Count; i++)
            {
                XmlElement cu = (XmlElement)xdoc.GetElementsByTagName("Kunde")[i];
                if (cu.GetAttribute("KD") == value.KundenNummer)
                {
                    cu.SetAttribute("Name", value.Name);
                    break;
                }
            }
            up.Close();
            xdoc.Save(_FileName);
        }

        public void Delete(Model.ME_MKunde value){
            FileStream rfile = new FileStream(_FileName, FileMode.Open);
            XmlDocument tdoc = new XmlDocument();
            tdoc.Load(rfile);
            XmlNodeList list = tdoc.GetElementsByTagName("Kunde");
            for (int i = 0; i < list.Count; i++)
            {
                XmlElement cl = (XmlElement)tdoc.GetElementsByTagName("Kunde")[i];
                if (cl.GetAttribute("KD") == value.KundenNummer)
                {
                    tdoc.DocumentElement.RemoveChild(cl);
                }
            }
            rfile.Close();
            tdoc.Save(_FileName);
        }

        private void CreateXmL(string FileName) {
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