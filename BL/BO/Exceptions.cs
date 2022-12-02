using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

[Serializable]
public class BlNullPropertyException : Exception
{
    public BlNullPropertyException(string message) : base(message) { }
}
[Serializable]
public class BlWrongCategoryException : Exception
{
    public BlWrongCategoryException(string message) : base(message) { }
}
[Serializable]
public class BlInCorrectDatesException : Exception
{
    public BlInCorrectDatesException(string message) : base(message) { }
}
[Serializable]
public class BlAlreadyExistEntityException : Exception
{
    public BlAlreadyExistEntityException(string message,Exception innerException) : base(message, innerException) { }
    public BlAlreadyExistEntityException(string message)
        : base(message) { }

    public override string ToString()=>
        base.ToString()+$"Entity already exists.";
   
}
[Serializable]
public class BlMissingEntityException : Exception
{
    public BlMissingEntityException(string message, Exception innerException) : base(message, innerException) { }

    public override string ToString() =>
        base.ToString() + $"Missing Entity.";

}
[Serializable]
public class BlDetailInvalidException : Exception
{
    public string DetailName;
    public BlDetailInvalidException(string name)
        : base() { DetailName = name; }
    public BlDetailInvalidException(string name, string massage)
        : base(massage) { DetailName = name; }
    public BlDetailInvalidException(string name, string massage, Exception innerException)
        : base(massage, innerException) { DetailName = name; }
    public override string ToString() =>
        $"The field {DetailName} is invalid";
}