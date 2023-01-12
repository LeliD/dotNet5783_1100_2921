using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal;

internal static class Config
{
    /// <summary>
    /// name of configuration xml file 
    /// </summary>
    static string s_config = "configuration"; 
    /// <summary>
    ///  Returns the next order ID
    /// </summary>
    /// <returns></returns>
    /// <exception cref="DO.DalNullPropertyException">Throws an exception if reading int from file failed</exception>
    public static int GetNextOrderID()//returns the next orderID
    {
        return XMLTools.LoadListFromXMLElement(s_config).ToIntNullable("NextOrderId")?? throw new DO.DalNullPropertyException("id");
    }
    /// <summary>
    /// Returns the next orderItem ID
    /// </summary>
    /// <returns></returns>
    /// <exception cref="DO.DalNullPropertyException">Throws an exception if reading int from file failed</exception>
    public static int GetNextOrderItemID()
    {
        return XMLTools.LoadListFromXMLElement(s_config).ToIntNullable("NextOrderItemId") ?? throw new DO.DalNullPropertyException("id");
    }
    /// <summary>
    /// Saves the next available orderItem id in config xml 
    /// </summary>
    /// <param name="id">id of orderItem to save in config xml</param>
    public static void SaveNextOrderItemID(int id)
    {
        XElement? root = XMLTools.LoadListFromXMLElement(s_config);
        root.Element("NextOrderItemId")!.SetValue(id.ToString());
        XMLTools.SaveListToXMLElement(root, s_config);
    }
    /// <summary>
    /// Saves the next available order id in config xml 
    /// </summary>
    /// <param name="id">id of an order to save in config xml</param>
    public static void SaveNextOrderID(int id)
    {
        XElement? root = XMLTools.LoadListFromXMLElement(s_config);
        root.Element("NextOrderId")!.SetValue(id.ToString());
        XMLTools.SaveListToXMLElement(root, s_config);
    }

}
