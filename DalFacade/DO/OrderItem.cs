

using System.Xml.Linq;

namespace DO;
/// <summary>
/// Structure for OrderItem which links the product and the order 
/// </summary>
public struct OrderItem
{
    /// <summary>
    /// Id of item in order
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// Id of order
    /// </summary>
    public int OrderID { get; set; }
    /// <summary>
    /// Id of product
    /// </summary>
    public int ProductID { get; set; }
    /// <summary>
    /// Price of item in order
    /// </summary>
    public double Price { get; set; }
    /// <summary>
    /// Amount of item in order
    /// </summary>
    public int Amount { get; set; }
    /// <summary>
    /// Order item's details
    /// </summary>
    /// <returns>returns String of order item representation</returns>
    public override string ToString() => $@"
ID              =   {ID}
OrderID         =   {OrderID} 
ProductID       =   {ProductID}
Price           =   {Price}
Amount          =   {Amount}
";
 
}
