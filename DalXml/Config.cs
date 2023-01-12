using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal;

internal static class Config
{
    static string s_config = "configuration";

    public static int GetNextOrderID()
    {
        return XMLTools.LoadListFromXMLElement(s_config).ToIntNullable("NextOrderId")?? throw new DO.DalNullPropertyException("id");
    }
    public static int GetNextOrderItemID()
    {
        return XMLTools.LoadListFromXMLElement(s_config).ToIntNullable("NextOrderItemId") ?? throw new DO.DalNullPropertyException("id");
    }
    public static void SaveNextOrderItemID(int id)
    {
        XElement? root = XMLTools.LoadListFromXMLElement(s_config);
        root.Element("NextOrderItemId")!.SetValue(id.ToString());
        XMLTools.SaveListToXMLElement(root, s_config);
    }
    public static void SaveNextOrderID(int id)
    {
        XElement? root = XMLTools.LoadListFromXMLElement(s_config);
        root.Element("NextOrderId")!.SetValue(id.ToString());
        XMLTools.SaveListToXMLElement(root, s_config);
    }

}
