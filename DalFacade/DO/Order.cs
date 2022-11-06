
namespace DO;
/// <summary>
/// Structure for order in the store 
/// </summary>
public struct Order
{
    public int ID { get; set; }
    public string? CustomerName { get; set; }
    public string? CustomerEmail { get; set; }
    public string? CustomerAdress { get; set; }
    public DateTime? OrderDate { get; set; }
    public DateTime? ShipDate { get; set; }
    public DateTime? DeliveryDate { get; set; }

    public override string ToString() => $@"
ID             =   {ID},
CustomerName   =   {CustomerName}, 
CustomerEmail  =   {CustomerEmail},
CustomerAdress =   {CustomerAdress},
OrderDate      =   {OrderDate},
ShipDate       =   {ShipDate},
DeliveryDate   =   {DeliveryDate}
";
}
