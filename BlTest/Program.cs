﻿using System.Diagnostics;
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
    /// <param name="product">The function gets DalProduct variable which represents the list of products  </param>
    /// <exception cref="Exception">Throw exception if there is a wrong input</exception>
    public static void ProductsFunctions(IBl bl)
    {
        char choice;// the user's choice among the options a,b,c,d,e,f
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
                       Console.WriteLine(bl.Product.ProductDetailsForCustomer(ID));
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
    /// Enables the user to use functions of orderss  
    /// </summary>
    /// <param name="order">The function gets DalOrder variable which represents the list of orders </param>
    /// <exception cref="Exception">Throw exception if there is a wrong input</exception>
    public static void OrdersFunctions(IBl bl)
    {
//        char choice;// the user's choice among the options a,b,c,d,e,f
//        do
//        {
//            Console.WriteLine(@"a: Add order
//b: Get order by ID
//c: Get orders' list
//d: Update order by its ID
//e: Delete order
//f: Exit");
//            int ID;
//            string customerName;
//            string customerEmail;
//            string customerAddress;
//            DateTime orderDate;
//            DateTime shipDate;
//            DateTime deliveryDate;
//            DateTime? d;
//            string input;//user's input
//            bool check;  //check if the input is correct
//            Order o;
//            input = Console.ReadLine();
//            check = Char.TryParse(input, out choice);
//            if (!check)
//                throw new Exception("Wrong input");
//            try
//            {
//                switch (choice)
//                {
//                    case 'a':

//                        Console.WriteLine("Enter name of customer");
//                        customerName = Console.ReadLine();

//                        Console.WriteLine("Enter email of customer");
//                        customerEmail = Console.ReadLine();

//                        Console.WriteLine("Enter adress of customer");
//                        customerAddress = Console.ReadLine();


//                        o = new Order() { CustomerName = customerName, CustomerAddress = customerAddress, CustomerEmail = customerEmail, OrderDate = DateTime.Now, ShipDate = null, DeliveryDate = null };
//                        dal.Order.Add(o);
//                        break;
//                    case 'b':
//                        Console.WriteLine("Enter ID of order");
//                        input = Console.ReadLine();
//                        check = int.TryParse(input, out ID);
//                        if (!check)
//                            throw new Exception("Wrong input");
//                        Console.WriteLine(dal.Order.GetById(ID));
//                        break;
//                    case 'c':
//                        IEnumerable<Order?> OrdersList = dal.Order.GetAll();
//                        foreach (Order? x in OrdersList)
//                        {
//                            Console.WriteLine(x);
//                        }
//                        break;
//                    case 'd':
//                        Console.WriteLine("Enter ID of order to update");
//                        input = Console.ReadLine();
//                        check = int.TryParse(input, out ID);
//                        if (!check)
//                            throw new Exception("Wrong input");

//                        Console.WriteLine("Enter name of customer");
//                        customerName = Console.ReadLine();

//                        Console.WriteLine("Enter email of customer");
//                        customerEmail = Console.ReadLine();

//                        Console.WriteLine("Enter adress of customer");
//                        customerAddress = Console.ReadLine();

//                        Console.WriteLine("Enter ship date of customer");
//                        input = Console.ReadLine();
//                        check = DateTime.TryParse(input, out shipDate);
//                        if (!check)
//                            throw new Exception("Wrong input");

//                        Console.WriteLine("Enter delivery date of customer");
//                        input = Console.ReadLine();
//                        check = DateTime.TryParse(input, out deliveryDate);
//                        if (!check)
//                            throw new Exception("Wrong input");

//                        o = new Order() { ID = ID, CustomerName = customerName, CustomerAddress = customerAddress, CustomerEmail = customerEmail, OrderDate = dal.Order.GetById(ID).OrderDate, ShipDate = shipDate, DeliveryDate = deliveryDate };
//                        dal.Order.Update(o);
//                        break;
//                    case 'e':
//                        Console.WriteLine("Enter ID of order to delete");
//                        input = Console.ReadLine();
//                        check = int.TryParse(input, out ID);
//                        if (!check)
//                            throw new Exception("Wrong input");
//                        dal.Order.Delete(ID);
//                        break;
//                    default:
//                        break;
//                }
//            }
//            catch (Exception e)
//            {
//                Console.WriteLine(e);
//            }
//        }
//        while (choice != 'f');

    }
    /// <summary>
    /// Enables the user to use functions of orderItems 
    /// </summary>
    /// <param name="orderItem">The function gets DalOrderItem variable which represents the list of orderItems</param>
    /// <exception cref="Exception">Throw exception if there is a wrong input</exception>
    public static void CartFunctions(IBl bl)
    {
//        char choice;// the user's choice among the options a,b,c,d,e,f,g,h
//        do
//        {
//            Console.WriteLine(@"a: Add order item
//b: Get order item by ID
//c: Get order items' list
//d: Update order item by its ID
//e: Delete order item
//f: Get order item by 2 identifiers
//g: Get items in order by its ID
//h: Exit");
//            int ID;
//            int orderID;
//            int productID;
//            double price;
//            int amount;
//            string input;//user's input
//            bool check;  //check if the input is correct
//            OrderItem o;
//            IEnumerable<OrderItem?> OrderItemsList;
//            input = Console.ReadLine();
//            check = Char.TryParse(input, out choice);
//            if (!check)
//                throw new Exception("Wrong input");
//            try
//            {
//                switch (choice)
//                {
//                    case 'a':
//                        Console.WriteLine("Enter ID of order");
//                        input = Console.ReadLine();
//                        check = int.TryParse(input, out orderID);
//                        if (!check)
//                            throw new Exception("Wrong input");

//                        Console.WriteLine("Enter ID of product");
//                        input = Console.ReadLine();
//                        check = int.TryParse(input, out productID);
//                        if (!check)
//                            throw new Exception("Wrong input");

//                        Console.WriteLine("Enter price of order item");
//                        input = Console.ReadLine();
//                        check = double.TryParse(input, out price);
//                        if (!check)
//                            throw new Exception("Wrong input");

//                        Console.WriteLine("Enter amount of products in the order item");
//                        input = Console.ReadLine();
//                        check = int.TryParse(input, out amount);
//                        if (!check)
//                            throw new Exception("Wrong input");
//                        o = new OrderItem() { OrderID = orderID, ProductID = productID, Price = price, Amount = amount };
//                        dal.OrderItem.Add(o);
//                        break;
//                    case 'b':
//                        Console.WriteLine("Enter ID of order item");
//                        input = Console.ReadLine();
//                        check = int.TryParse(input, out ID);
//                        if (!check)
//                            throw new Exception("Wrong input");
//                        Console.WriteLine(dal.OrderItem.GetById(ID));
//                        break;
//                    case 'c':
//                        OrderItemsList = dal.OrderItem.GetAll();
//                        foreach (OrderItem? x in OrderItemsList)
//                        {
//                            Console.WriteLine(x);
//                        }
//                        break;
//                    case 'd':
//                        Console.WriteLine("Enter ID of order item to update");
//                        input = Console.ReadLine();
//                        check = int.TryParse(input, out ID);
//                        if (!check)
//                            throw new Exception("Wrong input");

//                        Console.WriteLine("Enter ID of order");
//                        input = Console.ReadLine();
//                        check = int.TryParse(input, out orderID);
//                        if (!check)
//                            throw new Exception("Wrong input");

//                        Console.WriteLine("Enter ID of product");
//                        input = Console.ReadLine();
//                        check = int.TryParse(input, out productID);
//                        if (!check)
//                            throw new Exception("Wrong input");

//                        Console.WriteLine("Enter price of order item");
//                        input = Console.ReadLine();
//                        check = double.TryParse(input, out price);
//                        if (!check)
//                            throw new Exception("Wrong input");

//                        Console.WriteLine("Enter amount of products in the order item");
//                        input = Console.ReadLine();
//                        check = int.TryParse(input, out amount);
//                        if (!check)
//                            throw new Exception("Wrong input");

//                        o = new OrderItem() { ID = ID, OrderID = orderID, ProductID = productID, Price = price, Amount = amount };
//                        dal.OrderItem.Update(o);

//                        break;
//                    case 'e':
//                        Console.WriteLine("Enter ID of order item to delete");
//                        input = Console.ReadLine();
//                        check = int.TryParse(input, out ID);
//                        if (!check)
//                            throw new Exception("Wrong input");
//                        dal.OrderItem.Delete(ID);
//                        break;
//                    case 'f':
//                        Console.WriteLine("Enter ID of order");
//                        input = Console.ReadLine();
//                        check = int.TryParse(input, out orderID);
//                        if (!check)
//                            throw new Exception("Wrong input");

//                        Console.WriteLine("Enter ID of product");
//                        input = Console.ReadLine();
//                        check = int.TryParse(input, out productID);
//                        if (!check)
//                            throw new Exception("Wrong input");
//                        Console.WriteLine(dal.OrderItem.GetBy2Identifiers(productID, orderID));
//                        break;
//                    case 'g':
//                        Console.WriteLine("Enter ID of order");
//                        input = Console.ReadLine();
//                        check = int.TryParse(input, out orderID);
//                        if (!check)
//                            throw new Exception("Wrong input");

//                        OrderItemsList = dal.OrderItem.GetItemsInOrder(orderID);
//                        foreach (OrderItem? x in OrderItemsList)
//                        {
//                            Console.WriteLine(x);
//                        }
//                        break;
//                    default:
//                        break;
//                }
//            }
//            catch (Exception e)
//            {
//                Console.WriteLine(e);
//            }
//        }
//        while (choice != 'h');

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
                        CartFunctions(bl);
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

