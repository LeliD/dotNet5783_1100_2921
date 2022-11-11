

namespace DO;
/// <summary>
/// Structure for product in the store 
/// </summary>
public struct Product
{
    public int ID { get; set; }
    public string? Name { get; set; }
    public double Price { get; set; }
    public Category Category { get; set; }
    public int InStock { get; set; }
    public override string ToString() => $@"
ID              =   {ID}
Name            =   {Name} 
Category        =   {Category}
Price           =   {Price}
Amount in stock =   {InStock}
";

}
