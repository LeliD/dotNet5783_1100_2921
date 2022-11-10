using Dal;
using DO;
namespace DalTest;


public class Program
{
    public static void ProductsFunctions()
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
            switch (choice)
            {
                case 'a':
                    ProductsFunctions();
                    break;
                case 'b':
                    OrdersFunctions();
                    break;
                case 'c':
                    OrderItemsFunctions();
                    break;
                case 'd':
                    OrderItemsFunctions();
                    break;
                case 'e':
                    OrderItemsFunctions();
                    break;
                default:
                    break;
            }
        }
        while (choice != 'f');

    }
    public static void OrdersFunctions()
    {

    }
    public static void OrderItemsFunctions()
    {

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
                    ProductsFunctions();
                    break;
                case 2:
                    OrdersFunctions();
                    break;
                case 3:
                    OrderItemsFunctions();
                    break;
                default:
                    break;
            }



        }
        while (choice != 4);
                 
          
        
    }
   
}   

    
