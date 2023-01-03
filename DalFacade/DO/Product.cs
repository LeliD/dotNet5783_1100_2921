

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
    /// Product's details
    /// </summary>
    /// <returns> returns String of Product representation</returns>
    public string? ImageRelativeName { get; set; }
    
    public override string ToString()
    {
        return this.ToStringProperty();
    }

}
