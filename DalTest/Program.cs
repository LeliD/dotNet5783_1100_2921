using System.Security.Cryptography.X509Certificates;
using Dal;
using DO;
namespace DalTest;


public class Program
{
    public static void ProductsFunctions(ref DalProduct product)
    {
        char choice;
        do
        {
            Console.WriteLine(@"
a: Add product
b: Get product by ID
c: Get products' list
d: Update product by its ID
e: Delete product
f: Exit
");
            choice = (char)Console.Read();
            // bool check = int.TryParse(input, out choice);
            //if (!check)
            //    throw new Exception("Wrong input");
            //int ch=int.TryParse(input, out choice)? choice : throw new Exception("Wrong input");
            int ID;
            string? name;
            double price;
            Category c;
            int inStock;
            string input;
            bool check;
            Product p;
            try
            {
                switch (choice)
                {
                    case 'a':
                        Console.WriteLine("Enter ID of product");
                        string s=Console.ReadLine();
                        input = Console.ReadLine();
                        check = int.TryParse(input, out ID);
                        if (!check)
                            throw new Exception("Wrong input");

                        Console.WriteLine("Enter name of product");
                        name = Console.ReadLine();

                        Console.WriteLine("Enter price of product");
                        input = Console.ReadLine();
                        check = double.TryParse(input, out price);
                        if (!check)
                            throw new Exception("Wrong input");

                        Console.WriteLine("Enter caterory of product");
                        input = Console.ReadLine();
                        check = Category.TryParse(input, out c);
                        if (!check)
                            throw new Exception("Wrong input");

                        Console.WriteLine("Enter stock of product");
                        input = Console.ReadLine();
                        check = int.TryParse(input, out inStock);
                        if (!check)
                            throw new Exception("Wrong input");

                        p = new Product() { ID = ID, Name = name, Price = price, Category = c, InStock = inStock };
                        product.Add(p);

                        break;
                    case 'b':
                        Console.WriteLine("Enter ID of product");
                        string s2 = Console.ReadLine(); 
                        input = Console.ReadLine();
                        check = int.TryParse(input, out ID);
                        if (!check)
                            throw new Exception("Wrong input");
                        Console.WriteLine(product.GetById(ID));
                        break;
                    case 'c':
                        IEnumerable<Product?> ProductsList = product.GetAll();
                        foreach (Product? x in ProductsList)
                        {
                            Console.WriteLine(x);
                        }
                        break;
                    case 'd':
                        Console.WriteLine("Enter ID of product to update");
                        input = Console.ReadLine();
                        check = int.TryParse(input, out ID);
                        if (!check)
                            throw new Exception("Wrong input");

                        Console.WriteLine("Enter name of product");
                        name = Console.ReadLine();

                        Console.WriteLine("Enter price of product");
                        input = Console.ReadLine();
                        check = double.TryParse(input, out price);
                        if (!check)
                            throw new Exception("Wrong input");

                        Console.WriteLine("Enter caterory of product");
                        input = Console.ReadLine();
                        check = Category.TryParse(input, out c);
                        if (!check)
                            throw new Exception("Wrong input");

                        Console.WriteLine("Enter stock of product");
                        input = Console.ReadLine();
                        check = int.TryParse(input, out inStock);
                        if (!check)
                            throw new Exception("Wrong input");

                        p = new Product() { ID = ID, Name = name, Price = price, Category = c, InStock = inStock };
                        product.update(p);
                        break;
                    case 'e':
                        Console.WriteLine("Enter ID of product to delete");
                        input = Console.ReadLine();
                        check = int.TryParse(input, out ID);
                        if (!check)
                            throw new Exception("Wrong input");
                        product.delete(ID);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        while (choice != 'f');

    }
    public static void OrdersFunctions(ref DalOrder order)
    {
        char choice;
        do
        {
            Console.WriteLine(@"
a: Add order
b: Get order by ID
c: Get orders' list
d: Update order by its ID
e: Delete order
f: Exit
");
            choice = (char)Console.Read();
            // bool check = int.TryParse(input, out choice);
            //if (!check)
            //    throw new Exception("Wrong input");
            //int ch=int.TryParse(input, out choice)? choice : throw new Exception("Wrong input");
            int ID;
            string customerName;
            string customerEmail;
            string customerAdress;
            DateTime orderDate;
            DateTime shipDate;
            DateTime deliveryDate;
            DateTime? d;
            string input;
            bool check;
            Order o;
            try
            {
                switch (choice)
                {
                    case 'a':
                        
                        Console.WriteLine("Enter name of customer");
                        customerName = Console.ReadLine();

                        Console.WriteLine("Enter email of customer");
                        customerEmail = Console.ReadLine();

                        Console.WriteLine("Enter adress of customer");
                        customerAdress = Console.ReadLine();

                      
                        o = new Order() { CustomerName = customerName, CustomerAdress=customerAdress,CustomerEmail= customerEmail ,OrderDate=DateTime.Now, ShipDate=null, DeliveryDate=null };
                        order.Add(o);
                        break;
                    case 'b':
                        Console.WriteLine("Enter ID of order");
                        input = Console.ReadLine();
                        check = int.TryParse(input, out ID);
                        if (!check)
                            throw new Exception("Wrong input");
                        Console.WriteLine(order.GetById(ID));
                        break;
                    case 'c':
                        IEnumerable<Order?> OrdersList = order.GetAll();
                        foreach (Order? x in OrdersList)
                        {
                            Console.WriteLine(x);
                        }
                        break;
                    case 'd':
                        Console.WriteLine("Enter ID of order to update");
                        input = Console.ReadLine();
                        check = int.TryParse(input, out ID);
                        if (!check)
                            throw new Exception("Wrong input");

                        Console.WriteLine("Enter name of customer");
                        customerName = Console.ReadLine();

                        Console.WriteLine("Enter email of customer");
                        customerEmail = Console.ReadLine();

                        Console.WriteLine("Enter adress of customer");
                        customerAdress = Console.ReadLine();

                        Console.WriteLine("Enter order date of customer");
                        input = Console.ReadLine();
                        check = DateTime.TryParse(input, out orderDate);
                        if (!check)
                            throw new Exception("Wrong input");

                        Console.WriteLine("Enter ship date of customer");
                        input = Console.ReadLine();
                        check = DateTime.TryParse(input, out shipDate);
                        if (!check)
                            throw new Exception("Wrong input");

                        Console.WriteLine("Enter delivery date of customer");
                        input = Console.ReadLine();
                        check = DateTime.TryParse(input, out deliveryDate);

                        if (!check)
                            d = null;
                        else
                            d = deliveryDate;

                        o = new Order() { ID = ID, CustomerName = customerName, CustomerAdress = customerAdress, CustomerEmail = customerEmail, OrderDate = DateTime.Now, ShipDate = null, DeliveryDate = d };
                        order.update(o);
                        break;
                    case 'e':
                        Console.WriteLine("Enter ID of order to delete");
                        input = Console.ReadLine();
                        check = int.TryParse(input, out ID);
                        if (!check)
                            throw new Exception("Wrong input");
                        order.delete(ID);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        while (choice != 'f');

    }
    public static void OrderItemsFunctions( ref DalOrderItem orderItem)
    {
        char choice;
        do
        {
            Console.WriteLine(@"
a: Add order item
b: Get order item by ID
c: Get order items' list
d: Update order item by its ID
e: Delete order item
f: Get order item by 2 identifiers
g: Get items in order by its ID
h: Exit
");
            choice = (char)Console.Read();
            // bool check = int.TryParse(input, out choice);
            //if (!check)
            //    throw new Exception("Wrong input");
            //int ch=int.TryParse(input, out choice)? choice : throw new Exception("Wrong input");
            int ID;
            int orderID;
            int productID;
            double price;
            int amount;
            string input;
            bool check;
            OrderItem o;
            IEnumerable<OrderItem?> OrderItemsList;
            try
            {
                switch (choice)
                {
                    case 'a':
                        Console.WriteLine("Enter ID of order");
                        input = Console.ReadLine();
                        check = int.TryParse(input, out orderID);
                        if (!check)
                            throw new Exception("Wrong input");

                        Console.WriteLine("Enter ID of product");
                        input = Console.ReadLine();
                        check = int.TryParse(input, out productID);
                        if (!check)
                            throw new Exception("Wrong input");

                        Console.WriteLine("Enter price of order item");
                        input = Console.ReadLine();
                        check = double.TryParse(input, out price);
                        if (!check)
                            throw new Exception("Wrong input");

                        Console.WriteLine("Enter amount of products in the order item");
                        input = Console.ReadLine();
                        check = int.TryParse(input, out amount);
                        if (!check)
                            throw new Exception("Wrong input");
                        o = new OrderItem() {OrderID= orderID, ProductID= productID, Price= price, Amount=amount };
                        orderItem.Add(o);
                        break;
                    case 'b':
                        Console.WriteLine("Enter ID of order item");
                        input = Console.ReadLine();
                        check = int.TryParse(input, out ID);
                        if (!check)
                            throw new Exception("Wrong input");
                        Console.WriteLine(orderItem.GetById(ID));
                        break;
                    case 'c':
                        OrderItemsList = orderItem.GetAll();
                        foreach (OrderItem? x in OrderItemsList)
                        {
                            Console.WriteLine(x);
                        }
                        break;
                    case 'd':
                        Console.WriteLine("Enter ID of order item to update");
                        input = Console.ReadLine();
                        check = int.TryParse(input, out ID);
                        if (!check)
                            throw new Exception("Wrong input");

                        Console.WriteLine("Enter ID of order");
                        input = Console.ReadLine();
                        check = int.TryParse(input, out orderID);
                        if (!check)
                            throw new Exception("Wrong input");

                        Console.WriteLine("Enter ID of product");
                        input = Console.ReadLine();
                        check = int.TryParse(input, out productID);
                        if (!check)
                            throw new Exception("Wrong input");

                        Console.WriteLine("Enter price of order item");
                        input = Console.ReadLine();
                        check = double.TryParse(input, out price);
                        if (!check)
                            throw new Exception("Wrong input");

                        Console.WriteLine("Enter amount of products in the order item");
                        input = Console.ReadLine();
                        check = int.TryParse(input, out amount);
                        if (!check)
                            throw new Exception("Wrong input");

                        o = new OrderItem() { ID=ID, OrderID = orderID, ProductID = productID, Price = price, Amount = amount };
                        orderItem.update(o);
                        
                        break;
                    case 'e':
                        Console.WriteLine("Enter ID of order item to delete");
                        input = Console.ReadLine();
                        check = int.TryParse(input, out ID);
                        if (!check)
                            throw new Exception("Wrong input");
                        orderItem.delete(ID);
                        break;
                    case 'f':
                        Console.WriteLine("Enter ID of order");
                        input = Console.ReadLine();
                        check = int.TryParse(input, out orderID);
                        if (!check)
                            throw new Exception("Wrong input");

                        Console.WriteLine("Enter ID of product");
                        input = Console.ReadLine();
                        check = int.TryParse(input, out productID);
                        if (!check)
                            throw new Exception("Wrong input");
                        orderItem.GetBy2Identifiers(productID, orderID);
                        break;
                    case 'g':
                        Console.WriteLine("Enter ID of order");
                        input = Console.ReadLine();
                        check = int.TryParse(input, out orderID);
                        if (!check)
                            throw new Exception("Wrong input");

                        OrderItemsList = orderItem.GetItemsInOrder(orderID);
                        foreach (OrderItem? x in OrderItemsList)
                        {
                            Console.WriteLine(x);
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        while (choice != 'h');

    }
    static void Main(string[] args)
    {
        DalOrder order = new DalOrder();
        DalOrderItem orderItem = new DalOrderItem();
        DalProduct product = new DalProduct();
        string input;
        int choice;
      
        do
        {
            Console.WriteLine("Press 1 for product, 2 for order, 3 for orderItem, 4 for exit");
            input = Console.ReadLine();
          
            bool check = int.TryParse(input, out choice);
            if (!check)
                throw new Exception("Wrong input");
            //int ch=int.TryParse(input, out choice)? choice : throw new Exception("Wrong input");
            switch (choice)
            {
                case 1:
                    ProductsFunctions(ref product);
                    break;
                case 2:
                    OrdersFunctions(ref order);
                    break;
                case 3:
                    OrderItemsFunctions(ref orderItem);
                    break;
                default:
                    break;
            }

        }
        while (choice != 4);
        
    }
   
}   

    
