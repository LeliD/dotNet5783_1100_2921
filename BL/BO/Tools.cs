using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BO;

static class Tools
{
    public static string ToStringProperty<T>(this T t)
    {
        string str = "";
        foreach (PropertyInfo item in t.GetType().GetProperties())
        {
            if (item.PropertyType == typeof(IEnumerable<>))
            {

            }
                //foreach(var v in item.GetValue(t,null))
                //{

                //}
            else

                        str += item.Name + ": " + item.GetValue(t, null) + "\n";
        }
          
        return str;
    }
}
