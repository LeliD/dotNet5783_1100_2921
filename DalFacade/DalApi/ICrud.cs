using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
namespace DalApi;

public interface ICrud<T> where T : struct
{

    int Add (T item);
    T GetById(int id);
    void Update (T item); 
    void Delete (int id);
    IEnumerable<T?> GetAll();


}
