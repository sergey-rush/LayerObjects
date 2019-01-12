using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Web.Configuration;
using System.Web.Mvc;
using LOB.Core;

namespace LOB.Web.Code
{
    public class Settings
    {
        public static void SetConnectionString(string name)
        {
            Configuration configuration = WebConfigurationManager.OpenWebConfiguration("~");

            for (int i = 0; i < configuration.ConnectionStrings.ConnectionStrings.Count; i++)
            {
                ConnectionStringSettings key = configuration.ConnectionStrings.ConnectionStrings[i];

                if (key.Name.StartsWith("LayerObjects"))
                {
                    if (key.Name == name)
                    {
                        key.Name = "LayerObjects1";
                    }
                    else
                    {
                        key.Name = "LayerObjects" + (i + 2).ToString();
                    }
                }
            }

            configuration.Save();

        }

        public static List<SelectListItem> GetConnectionStrings()
        {
            List<SelectListItem> keys = new List<SelectListItem>();
            Configuration configuration = WebConfigurationManager.OpenWebConfiguration("~");
            foreach (ConnectionStringSettings key in configuration.ConnectionStrings.ConnectionStrings)
            {
                if (key.Name.StartsWith("LayerObjects"))
                {
                    SelectListItem selectListItem = new SelectListItem();
                    selectListItem.Text = key.ConnectionString;
                    selectListItem.Value = key.Name;

                    if (key.Name == "LayerObjects1")
                    {
                        selectListItem.Selected = true;
                    }

                    keys.Add(selectListItem);
                }
            }

            return keys;
        }
    }
}