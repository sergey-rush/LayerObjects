using System;
using System.Configuration;
using System.Web.Configuration;
using System.Xml;

namespace LOB.Crypto
{
    public class Settings : IConfigurationSectionHandler
    {
        public string DecryptionKey { get; private set; }
        public string ValidationKey { get; private set; }


        public static Settings Current
        {
            get
            {
                //return ((Settings) ConfigurationManager.GetSection("system.web/machineKey"));
                return new Settings();
            }
        }

        private Settings()
        {
            MachineKeySection machineKeySection =
                (MachineKeySection)ConfigurationManager.GetSection("system.web/machineKey");


            if (machineKeySection != null)
            {
                try
                {
                    DecryptionKey = machineKeySection.DecryptionKey;
                    ValidationKey = machineKeySection.ValidationKey;
                }
                catch (Exception ex)
                {
                    throw new ConfigurationErrorsException("Configuration.Error: " + ex.Message);
                }
            }
        }

        private Settings(XmlElement xmlData)
        {
            
        }

        public object Create(object parent, object configContext, XmlNode section)
        {
            XmlElement root = (XmlElement) section;
            return new Settings(root);
        }
    }
}
