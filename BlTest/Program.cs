using System.Diagnostics;
using System.Xml;
using System.Xml.Linq;
using BlApi;
using BlImplementation;
using BO;
using Dal;
using DalApi;

namespace BlTest;

public class Program
{
    /// <summary>
    /// Enables the user to use functions of products
    /// </summary>
    /// <param name="bl">The function gets bl variable</param>
    /// <exception cref="Exception">Throw exception if there is a wrong input</exception>

    public static void ProductsFunctions(IBl bl)
    {
        char choice;// the user's choice among the options a,b,c,d,e,f,g,h
        do
        {
            Console.WriteLine(@"a: Get list of products for manager
b: Get list of products for customer
c: Get product's details for manager by its ID
d: Get product's details for customer by its ID
e: Add product
f: Update product
g: Delete product
h: Exit");
            int ID;
            string? name;
            double price;
            Category c;
            int inStock;
            string input; //user's input
            bool check;  //check if the input is correct
            Product p;
            Cart cart = new Cart() { CustomerName = "", CustomerEmail = "", CustomerAddress = "", TotalPrice = 0, Items = new List<OrderItem>() };

            input = Console.ReadLine();
            check = Char.TryParse(input, out choice);
            if (!check)
                throw new Exception("Wrong input");
            try
            {
                switch (choice)
                {

                    case 'a':
                       
                        foreach (var item in bl.Product.GetListedProductsForManager())
                        {
                            Console.WriteLine(item);
                        }

                        break;
                    case 'b':
                        foreach (var item in bl.Product.GetListedProductsForCustomer())
                        {
                            Console.WriteLine(item);
                        }
                        break;
                    case 'c':
                        Console.WriteLine("Enter ID of product to get its details (for manager)");
                        input = Console.ReadLine();
                        check = int.TryParse(input, out ID);
                        if (!check)
                            throw new Exception("Wrong input");
                        Console.WriteLine(bl.Product.ProductDetailsForManager(ID));
                        break;
                    case 'd':
                        Console.WriteLine("Enter ID of product to get its details (for customer)");
                        input = Console.ReadLine();
                        check = int.TryParse(input, out ID);
                        if (!check)
                            throw new Exception("Wrong input");
                       Console.WriteLine(bl.Product.ProductDetailsForCustomer(ID,cart));
                        break;
                    case 'e':
                        Console.WriteLine("Enter ID of product");
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
                        bl.Product.AddProduct(p);

                        break;
                    case 'f':
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
                        bl.Product.UpdateProduct(p);
                        break;
                    case 'g':
                        Console.WriteLine("Enter ID of product to get its details (for manager)");
                        input = Console.ReadLine();
                        check = int.TryParse(input, out ID);
                        if (!check)
                            throw new Exception("Wrong input");
                        bl.Product.DeleteProduct(ID);
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
    /// <summary>
    /// Enables the user to use functions of orders
    /// </summary>
    /// <param name="bl">The function gets bl variable</param>
    /// <exception cref="Exception">Throw exception if there is a wrong input</exception>
    public static void OrdersFunctions(IBl bl)
    {
        char choice;// the user's choice among the options a,b,c,d,e,f,g
        do
        {
            Console.WriteLine(@"a: Get list of orders for manager
b: Get orders' details by its ID
c: Update ship date of order
d: Update delivery date of order
e: Order tracking for manager
f: Update order for manager
g: Exit");
            int ID;
            string input;//user's input
            bool check;  //check if the input is correct
            input = Console.ReadLine();
            check = Char.TryParse(input, out choice);
            if (!check)
                throw new Exception("Wrong input");
            try
            {
                switch (choice)
                {
                    case 'a':

                        foreach (var item in bl.Order.GetOrdersForManager())
                        {
                            Console.WriteLine(item);
                        }

                        break;
                    case 'b':
                        Console.WriteLine("Enter ID of order to get its details");
                        input = Console.ReadLine();
                        check = int.TryParse(input, out ID);
                        if (!check)
                            throw new Exception("Wrong input");
                        Console.WriteLine(bl.Order.GetOrderByID(ID));
                        break;
                    case 'c':
                        Console.WriteLine("Enter ID of order to update its ship date");
                        input = Console.ReadLine();
                        check = int.TryParse(input, out ID);
                        if (!check)
                            throw new Exception("Wrong input");
                        Console.WriteLine(bl.Order.UpdateShipDate(ID));
                        break;
                    case 'd':
                        Console.WriteLine("Enter ID of order to update its delivery date");
                        input = Console.ReadLine();
                        check = int.TryParse(input, out ID);
                        if (!check)
                            throw new Exception("Wrong input");
                        Console.WriteLine(bl.Order.UpdateDeliveryDate(ID));
                        break;
                    case 'e':
                        Console.WriteLine("Enter ID of order to track");
                        input = Console.ReadLine();
                        check = int.TryParse(input, out ID);
                        if (!check)
                            throw new Exception("Wrong input");
                        Console.WriteLine(bl.Order.OrderTrack(ID));
                        break;
                    case 'f':
                        Console.WriteLine("Enter ID of order to update");
                        input = Console.ReadLine();
                        check = int.TryParse(input, out ID);
                        if (!check)
                            throw new Exception("Wrong input");
                        bl.Order.UpdateOrder(ID);//to print? void..
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
        while (choice != 'g');

    }


    /// <summary>
    /// Enables the user to use functions of cart
    /// </summary>
    /// <param name="orderItem">The function gets bl variable </param>
    /// <exception cref="Exception">Throw exception if there is a wrong input</exception>
    public static void CartFunctions(IBl bl, Cart cart)
    {
        char choice;// the user's choice among the options a,b,c,d
        do
        {
            Console.WriteLine(@"a: Add product to cart
b: Update amount of product in cart
c: Make order
d: Exit");
            int ID;
            int amount;
            string input;//user's input
            bool check;  //check if the input is correct
            
            input = Console.ReadLine();
            check = Char.TryParse(input, out choice);
            if (!check)
                throw new Exception("Wrong input");
            try
            {
                switch (choice)
                {
                    case 'a':
                        Console.WriteLine("Enter ID of product to add to cart");
                        input = Console.ReadLine();
                        check = int.TryParse(input, out ID);
                        if (!check)
                            throw new Exception("Wrong input");

                        Console.WriteLine(bl.Cart.AddProductToCart(cart,ID));

                        break;
                    case 'b':
                        Console.WriteLine("Enter ID of product to update its amount");
                        input = Console.ReadLine();
                        check = int.TryParse(input, out ID);
                        if (!check)
                            throw new Exception("Wrong input");

                        Console.WriteLine("Enter amount of product to update in cart");
                        input = Console.ReadLine();
                        check = int.TryParse(input, out amount);
                        if (!check)
                            throw new Exception("Wrong input");

                        Console.WriteLine(bl.Cart.UpdateAmountOfProductInCart(cart,ID,amount));
                        break;
                    case 'c':
                        bl.Cart.MakeOrder(cart);
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
        while (choice != 'd');

    }
    /// <summary>
    /// Enables the user to use functions of products, orders and orderItems 
    /// </summary>
    /// <param name="args"></param>
    /// <exception cref="Exception"> Throw exception when the choice isn't one of the followings: 1,2,3,4 </exception>
    static void Main(string[] args)
    {
        IBl bl = new Bl();
        string input;
        int choice = 0;
        Cart cart = new Cart() { CustomerName = "Tovi", CustomerEmail = "Tovi@gmail.com", CustomerAddress = "Chaifa", TotalPrice = 0, Items = new List<OrderItem>() };
        do
        {
            try
            {
                Console.WriteLine("Press 1 for product, 2 for order, 3 for cart, 4 for exit");
                input = Console.ReadLine();
                bool check = int.TryParse(input, out choice);
                if (!check)
                    throw new Exception("Wrong input");
                switch (choice)
                {
                    case 1:
                        ProductsFunctions(bl);
                        break;
                    case 2:
                        OrdersFunctions(bl);
                        break;
                    case 3:
                        CartFunctions(bl,cart);
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
        while (choice != 4);
    }
}

