

using System.Xml.Linq;

namespace DO;
/// <summary>
/// Structure for OrderItem which links the product and the order 
/// </summary>
public struct OrderItem
{
    public int ID { get; set; }
    public int OrderID { get; set; }
    public int ProductID { get; set; }
    public double Price { get; set; }
    public int Amount { get; set; }
    public override string ToString() => $@"
ID              =   {ID},
OrderID         =   {OrderID}, 
ProductID       =   {ProductID},
Price           =   {Price},
Amount in stock =   {Amount}
";

}
