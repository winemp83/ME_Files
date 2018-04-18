using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;

namespace ME_ViewModel.Funktionen
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

        public void Write(ViewModel.CustomerItemViewModel value) {
            XmlDocument xd = new XmlDocument();
            FileStream lfile = new FileStream(_FileName, FileMode.Open);
            xd.Load(lfile);
            XmlElement cl = xd.CreateElement("Kunde");
            cl.SetAttribute("KD", value.KundenNummer);
            cl.SetAttribute("Name", value.KundenName);
            xd.DocumentElement.AppendChild(cl);
            lfile.Close();
            xd.Save(_FileName);
        }

        public List<ViewModel.CustomerItemViewModel> Read(string which = "all", string searchTherm = "") {
            List<ViewModel.CustomerItemViewModel> result = new List<ViewModel.CustomerItemViewModel>();
            XmlDocument xdoc = new XmlDocument();
            FileStream rfile = new FileStream(_FileName, FileMode.Open);
            xdoc.Load(rfile);
            XmlNodeList list = xdoc.GetElementsByTagName("Kunde");
            for (int i = 0; i < list.Count; i++)
            {
                XmlElement cl = (XmlElement)xdoc.GetElementsByTagName("Kunde")[i];
                if (which.Equals("all"))
                    result.Add(new ViewModel.CustomerItemViewModel(cl.GetAttribute("KD"), cl.GetAttribute("Name")));
                else if ((cl.GetAttribute(which)) == searchTherm)
                {
                    result.Add(new ViewModel.CustomerItemViewModel(cl.GetAttribute("KD"), cl.GetAttribute("Name")));
                    break;
                }
            }
            rfile.Close();
            return result;
        }

        public void Update(ViewModel.CustomerItemViewModel value){
            XmlDocument xdoc = new XmlDocument();
            FileStream up = new FileStream(_FileName, FileMode.Open);
            xdoc.Load(up);
            XmlNodeList list = xdoc.GetElementsByTagName("Kunde");
            for (int i = 0; i < list.Count; i++)
            {
                XmlElement cu = (XmlElement)xdoc.GetElementsByTagName("Kunde")[i];
                if (cu.GetAttribute("KD") == value.KundenNummer)
                {
                    cu.SetAttribute("Name", value.KundenName);
                    break;
                }
            }
            up.Close();
            xdoc.Save(_FileName);
        }

        public void Delete(ViewModel.CustomerItemViewModel value){
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