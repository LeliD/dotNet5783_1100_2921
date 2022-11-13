
namespace DO;
/// <summary>
/// Structure for order in the store 
/// </summary>
public struct Order
{
    /// <summary>
    /// Id of order
    /// </summary>
    public int ID { get; set; } 
    /// <summary>
    /// Name of customer
    /// </summary>
    public string? CustomerName { get; set; }
    /// <summary>
    /// Email of customer
    /// </summary>
    public string? CustomerEmail { get; set; }
    /// <summary>
    /// Adress of customer
    /// </summary>
    public string? CustomerAdress { get; set; }
    /// <summary>
    /// Order date
    /// </summary>
    public DateTime? OrderDate { get; set; }
    /// <summary>
    /// Ship date
    /// </summary>
    public DateTime? ShipDate { get; set; }
    /// <summary>
    /// Delivery date
    /// </summary>
    public DateTime? DeliveryDate { get; set; }
    /// <summary>
    /// Order's details
    /// </summary>
    /// <returns> returns String of order representation </returns>
    public override string ToString() => $@"
ID             =   {ID}
CustomerName   =   {CustomerName}
CustomerEmail  =   {CustomerEmail}
CustomerAdress =   {CustomerAdress}
OrderDate      =   {OrderDate}
ShipDate       =   {ShipDate}
DeliveryDate   =   {DeliveryDate}
";
}
