using Dal;
using DO;
namespace DalTest;


public class Program
{
    static void Main(string[] args)
    {
        DalOrder order = new DalOrder();
        DalOrderItem orderItem = new DalOrderItem();
        DalProduct product = new DalProduct();
        string choice;
        do
        { 
        Console.WriteLine("Press a for product, b for order, c for orderItem, e for exit");
        choice= Console.ReadLine();

        //switch (choice)
          //  {

           // }

        }
            while (choice != "e") ;
                 
          
        
    }
}   

    
