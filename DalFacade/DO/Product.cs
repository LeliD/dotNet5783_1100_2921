

namespace DO;
/// <summary>
/// Structure for product in the store 
/// </summary>
public struct Product
{
    /// <summary>
    /// Id of product
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// Name of product
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// Price of product
    /// </summary>
    public double Price { get; set; }
    /// <summary>
    /// Category of product
    /// </summary>
    public Category Category { get; set; }
    /// <summary>
    /// The amount of product in stock
    /// </summary>
    public int InStock { get; set; }
    /// <summary>
    /// 
    /// </summary>
    /// <returns> returns String of Product representation</returns>
    public override string ToString() => $@"
ID              =   {ID}
Name            =   {Name} 
Category        =   {Category}
Price           =   {Price}
Amount in stock =   {InStock}
";

}
