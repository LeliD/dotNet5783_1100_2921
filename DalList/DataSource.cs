
using System.Diagnostics;
using DO;

namespace Dal;

internal static class DataSource
{
    /// <summary>
    /// ProductsList includes all the products 
    /// </summary>
    internal static List<Product?> ProductsList { get; } = new List<Product?>();
    /// <summary>
    /// OrdersList includes all the orders
    /// </summary>
    internal static List<Order?> OrdersList { get; } = new List<Order?>();
    /// <summary>
    /// OrderItemsList includes all the OrderItems of existing orders
    /// </summary>
    internal static List<OrderItem?> OrderItemsList { get; } = new List<OrderItem?>();
    /// <summary>
    /// s_rand for random values
    /// </summary>
    private static readonly Random s_rand = new();
    /// <summary>
    /// A static constructor of DataSource which calls s_Initialize function for initializing its 3 properties. 
    /// </summary>
    static DataSource()
    {
        s_Initialize();
    }
    /// <summary>
    /// Static class Config for automatic running numbers
    /// </summary>
    internal static class Config
    {
        /// <summary>
        /// StartOrderNumber determines the starting point of orders' automatic running number 
        /// </summary>
        internal const int StartOrderNumber = 1000;
        /// <summary>
        /// A property of the next available order's running number 
        /// </summary>
        private static int nextOrderNumber = StartOrderNumber;
        internal static int NextOrderNumber { get => nextOrderNumber++; }
        /// <summary>
        /// StartOrderItemNumber determines the starting point of OrderItems' automatic running number 
        /// </summary>
        internal const int StartOrderItemNumber = 1;
        /// <summary>
        /// A property of the next available orderItem's running number 
        /// </summary>
        private static int nextOrderItemNumber = StartOrderItemNumber;
        internal static int NextOrderItemNumber { get => nextOrderItemNumber++; }
    }
    /// <summary>
    /// Initializing DataSource's 3 properties. 
    /// </summary>
    private static void s_Initialize()
    {
        createAndInitProducts();
        createAndInitOrders();
        createAndInitOrderItems();
    }
    /// <summary>
    /// Initializing ProductsList in 10 products
    /// </summary>
    private static void createAndInitProducts()
    {
        
        string[] namesOfProducts = new string[] {"aa","bb", "cc", "dd", "ee", "ff", "gg", "hh", "ii", "jj" };//An array of products' names
      
        for (int i = 0; i < 9; i++)
        {
            ProductsList.Add(new Product() { ID = 100000 + i , Category= (Category)(s_rand.Next(4)), InStock= s_rand.Next(1,50), Price= s_rand.Next(200) , Name= namesOfProducts[i] }) ;
        }
        ProductsList.Add(new Product() { ID = 100000 + 9, Category = (Category)(s_rand.Next(4)), InStock = 0, Price = s_rand.Next(200), Name = namesOfProducts[9] });//The tenth product is initialized in 0 in "InStock" property
    }
    /// <summary>
    /// Initializing OrdersList in 20 orders
    /// </summary>
    private static void createAndInitOrders()
    {
        string[] namesOfCustomers = new string[] { "Avi", "Beni", "Dan", "Ari", "Sari", "Shilat", "Leli", "Chaya", "Ester", "Gil" };//An array of namesOfCustomers
        string[] emailsOfCustomers = new string[] { "Avi@gmail.com", "Beni@gmail.com", "Dan@gmail.com", "Ari@gmail.com", "Sari@gmail.com", "Shilat@gmail.com", "Leli@gmail.com", "Chaya@gmail.com", "Ester@gmail.com", "Gil@gmail.com" };//An array of emailsOfCustomers
        string[] addressesOfCustomers = new string[] { "Tel Aviv", "Bney Brack", "Ramat Gan", "Jerusalem", "Chaifa", "Ashdod", "Eilat", "Miron", "Ashkelon", "Givaat Shmuel" };//An array of adressesOfCustomers
        for (int i = 0; i < 10; i++)// Initializing 10 orders with ShipDate and DeliveryDate
        {
            DateTime date = new DateTime(s_rand.Next(2021, 2023), s_rand.Next(1, 11), s_rand.Next(1, 30), s_rand.Next(0, 24), s_rand.Next(0, 60), s_rand.Next(0, 60));//Random calendar date from the last two years
            TimeSpan t1 = new TimeSpan(s_rand.Next(0, 8), s_rand.Next(0, 24), s_rand.Next(0, 60), s_rand.Next(0, 60));//Random date between 0-8 days
            TimeSpan t2 = new TimeSpan(s_rand.Next(0, 8), s_rand.Next(0, 24), s_rand.Next(0, 60), s_rand.Next(0, 60));//Random date between 0-8 days

            OrdersList.Add(new Order()
            {
                ID = Config.NextOrderNumber,
                CustomerName = namesOfCustomers[i],
                CustomerEmail = emailsOfCustomers[i],
                CustomerAddress = addressesOfCustomers[i],
                OrderDate = date,
                ShipDate = date + t1,
                DeliveryDate = date + t1 + t2
            }) ;
             
        }
        for (int i = 0; i < 6; i++)// Initializing 6 orders with ShipDate but not in DeliveryDate
        {
            DateTime date = new DateTime(s_rand.Next(2021, 2023), s_rand.Next(1, 11), s_rand.Next(1, 30), s_rand.Next(0, 24), s_rand.Next(0, 60), s_rand.Next(0, 60));//Random calendar date from the last two years
            TimeSpan t = new TimeSpan(s_rand.Next(0, 8), s_rand.Next(0, 24), s_rand.Next(0, 60), s_rand.Next(0, 60));//Random date between 0-8 days
            OrdersList.Add(new Order()
            {
                ID = Config.NextOrderNumber,
                CustomerName = namesOfCustomers[i],
                CustomerEmail = emailsOfCustomers[i],
                CustomerAddress = addressesOfCustomers[i],
                OrderDate = date,
                ShipDate = date+t,
                DeliveryDate = null
            });


        }
        for (int i = 0; i < 4; i++)// Initializing 4 orders without ShipDate and DeliveryDate
        {
            DateTime date = new DateTime(s_rand.Next(2021, 2023), s_rand.Next(1, 11), s_rand.Next(1, 30), s_rand.Next(0, 24), s_rand.Next(0, 60), s_rand.Next(0, 60));//Random calendar date from the last two years
            OrdersList.Add(new Order()
            {
                ID = Config.NextOrderNumber,
                CustomerName = namesOfCustomers[i],
                CustomerEmail = emailsOfCustomers[i],
                CustomerAddress = addressesOfCustomers[i],
                OrderDate = date,
                ShipDate = null,
                DeliveryDate = null
            });
        }
    }
    private static void createAndInitOrderItems()
    {
       foreach(Order r in OrdersList)//Initializing 2 orderItems for each existing order
        {
            int idOfProduct1 = s_rand.Next(100000, 100010);//First random product's id
            OrderItemsList.Add(new OrderItem()
            {
               ID = Config.NextOrderItemNumber,
               OrderID = r.ID,
               ProductID= idOfProduct1,
               Price=ProductsList.Find(x=>x?.ID== idOfProduct1).Value.Price,
               Amount= s_rand.Next(1,6)
            }) ;
            int idOfProduct2 = s_rand.Next(100000, 100010);//Second random product's id
            while (idOfProduct1 == idOfProduct2)//To make sure idOfProduct2 and idOfProduct1 are different
            {
                idOfProduct2 = s_rand.Next(100000, 100010);
            }
            OrderItemsList.Add(new OrderItem()
            {
                ID = Config.NextOrderItemNumber,
                OrderID = r.ID,
                ProductID = idOfProduct2,
                Price = ProductsList.Find(x => x?.ID == idOfProduct2).Value.Price,
                Amount = s_rand.Next(1, 6)
            });

        }
    }
  
    
}
