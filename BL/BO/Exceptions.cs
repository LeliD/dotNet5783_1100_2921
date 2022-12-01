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
public class BlInCorrectDataException : Exception
{
    public BlInCorrectDataException(string message) : base(message) { }
}
[Serializable]
public class BlAlreadyExistEntityException : Exception
{
    public BlAlreadyExistEntityException(string message,Exception innerException) : base(message, innerException) { }

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