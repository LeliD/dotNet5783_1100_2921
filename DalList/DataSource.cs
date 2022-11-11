
using System.Diagnostics;
using DO;

namespace Dal;

internal static class DataSource
{

    internal static List<Product?> ProductsList { get; } = new List<Product?>();
    internal static List<Order?> OrdersList { get; } = new List<Order?>();
    internal static List<OrderItem?> OrderItemsList { get; } = new List<OrderItem?>();
    private static readonly Random s_rand = new();
    static DataSource()
    {
        s_Initialize();
    }
    internal static class Config
    {
        internal const int StartOrderNumber = 1000;
        private static int nextOrderNumber = StartOrderNumber;
        internal static int NextOrderNumber { get => nextOrderNumber++; }
        internal const int StartOrderItemNumber = 1;
        private static int nextOrderItemNumber = StartOrderItemNumber;
        internal static int NextOrderItemNumber { get => nextOrderItemNumber++; }
    }

    private static void createAndInitProducts()
    {
        string[] namesOfProducts = new string[] {"aa","bb", "cc", "dd", "ee", "ff", "gg", "hh", "ii", "jj" };
        for (int i = 0; i < 9; i++)
        {
            ProductsList.Add(new Product() { ID = 100000 + i , Category= (Category)(s_rand.Next(4)), InStock= s_rand.Next(1,50), Price= s_rand.Next(200) , Name= namesOfProducts[i] }) ;
        }
        ProductsList.Add(new Product() { ID = 100000 + 9, Category = (Category)(s_rand.Next(4)), InStock = 0, Price = s_rand.Next(200), Name = namesOfProducts[9] });
    }
    private static void createAndInitOrders()
    {
        string[] namesOfCustomers = new string[] { "Avi", "Beni", "Dan", "Ari", "Sari", "Shilat", "Leli", "Chaya", "Ester", "Gil" };
        string[] emailsOfCustomers = new string[] { "Avi@gmail.com", "Beni@gmail.com", "Dan@gmail.com", "Ari@gmail.com", "Sari@gmail.com", "Shilat@gmail.com", "Leli@gmail.com", "Chaya@gmail.com", "Ester@gmail.com", "Gil@gmail.com" };
        string[] adressesOfCustomers = new string[] { "Tel Aviv", "Bney Brack", "Ramat Gan", "Jerusalem", "Chaifa", "Ashdod", "Eilat", "Miron", "Ashkelon", "Givaat Shmuel" };
        for (int i = 0; i < 10; i++)
        {
            DateTime date = new DateTime(s_rand.Next(2021, 2023), s_rand.Next(1, 11), s_rand.Next(1, 30), s_rand.Next(0, 24), s_rand.Next(0, 60), s_rand.Next(0, 60));
            TimeSpan t1 = new TimeSpan(s_rand.Next(0, 8), s_rand.Next(0, 24), s_rand.Next(0, 60), s_rand.Next(0, 60));
            TimeSpan t2 = new TimeSpan(s_rand.Next(0, 8), s_rand.Next(0, 24), s_rand.Next(0, 60), s_rand.Next(0, 60));

            OrdersList.Add(new Order()
            {
                ID = Config.NextOrderNumber,
                CustomerName = namesOfCustomers[i],
                CustomerEmail = emailsOfCustomers[i],
                CustomerAdress = adressesOfCustomers[i],
                OrderDate = date,
                ShipDate = date + t1,
                DeliveryDate = date + t1 + t2
            }) ;
             
        }
        for (int i = 0; i < 6; i++)
        {


            DateTime date = new DateTime(s_rand.Next(2021, 2023), s_rand.Next(1, 11), s_rand.Next(1, 30), s_rand.Next(0, 24), s_rand.Next(0, 60), s_rand.Next(0, 60));
            TimeSpan t = new TimeSpan(s_rand.Next(0, 8), s_rand.Next(0, 24), s_rand.Next(0, 60), s_rand.Next(0, 60));
            OrdersList.Add(new Order()
            {
                ID = Config.NextOrderNumber,
                CustomerName = namesOfCustomers[i],
                CustomerEmail = emailsOfCustomers[i],
                CustomerAdress = adressesOfCustomers[i],
                OrderDate = date,
                ShipDate = date+t,
                DeliveryDate = null
            });


        }
        for (int i = 0; i < 4; i++)
        {

            DateTime date = new DateTime(s_rand.Next(2021, 2023), s_rand.Next(1, 11), s_rand.Next(1, 30), s_rand.Next(0, 24), s_rand.Next(0, 60), s_rand.Next(0, 60));
            OrdersList.Add(new Order()
            {
                ID = Config.NextOrderNumber,
                CustomerName = namesOfCustomers[i],
                CustomerEmail = emailsOfCustomers[i],
                CustomerAdress = adressesOfCustomers[i],
                OrderDate = date,
                ShipDate = null,
                DeliveryDate = null
            });


        }
    }
    private static void createAndInitOrderItems()
    {
       
        foreach(Order r in OrdersList)
        {
            int idOfProduct1 = s_rand.Next(100000, 100010);
            OrderItemsList.Add(new OrderItem()
            {
               ID = Config.NextOrderItemNumber,
               OrderID = r.ID,
               ProductID= idOfProduct1,
               Price= ProductsList.Find(x=>x?.ID== idOfProduct1).Value.Price,
               Amount= s_rand.Next(1,6)
            }) ;
            int idOfProduct2 = s_rand.Next(100000, 100010);
            while(idOfProduct1 == idOfProduct2)
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
    private static void s_Initialize()
    {
        createAndInitProducts();
        createAndInitOrders();
        createAndInitOrderItems();
    }
    
}
