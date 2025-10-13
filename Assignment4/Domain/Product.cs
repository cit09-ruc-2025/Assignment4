
namespace Assignment4;

public class Product
{
  public int Id { get; set; }
  public string Name { get; set; }
  public int UnitPrice { get; set; }
  public int CategoryId { get; set; }
  public string QuantityPerUnit { get; set; }
  public int UnitsInStock { get; set; }
  public Category Category { get; set; }
}

public class ProductWithCategoryName : Product
{
  public string CategoryName { get; set; }
}