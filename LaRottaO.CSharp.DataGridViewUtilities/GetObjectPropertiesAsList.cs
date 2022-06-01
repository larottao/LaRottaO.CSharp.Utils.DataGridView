using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LaRottaO.CSharp.DataGridViewUtilities
{
    public class GetObjectPropertiesAsList
    {
        public List<String> get(Object argObject)
        {
            List<String> resultList = new List<String>();

            PropertyInfo[] properties = argObject.GetType().GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (property != null && property.Name != null)
                {
                    resultList.Add(property.Name);
                }
                else
                {
                    resultList.Add("null");
                }
            }

            return resultList;
        }
    }
}