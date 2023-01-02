using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO;
[Serializable]
public class DalMissingIdException : Exception
{
    public int EntityID;
    public string EntityName;
    public DalMissingIdException(int id, string name)
    : base() { EntityID = id; EntityName = name; }
    public DalMissingIdException(int id, string name, string message)
        : base(message) { EntityID = id; EntityName = name; }
    public DalMissingIdException(int id, string name, string message, Exception innerException)
        : base(message, innerException) { EntityID = id; EntityName = name; }
    public override string ToString()
    {
        if (EntityID == -1)
            return "ID of product doesn't exist in the order";
        return $"Id: {EntityID} of type {EntityName}, doesn't exist.";
    }
}
[Serializable]
public class DalAlreadyExistIdException : Exception
{
    public int EntityID;
    public string EntityName;
    public DalAlreadyExistIdException(int id, string name) 
        : base() { EntityID = id; EntityName = name; }
    public DalAlreadyExistIdException(int id, string name, string message)
         : base(message) { EntityID = id; EntityName = name; }
    public DalAlreadyExistIdException(int id, string name, string message, Exception innerException)
        : base(message, innerException) { EntityID = id; EntityName = name; }
    public override string ToString() => 
        $"Id: {EntityID} of type {EntityName}, already exists.";
}
[Serializable]
public class DalAlreadyExistUserNameException : Exception
{
    public string EntityName;
    public DalAlreadyExistUserNameException(string name, string message)
     : base(message) { EntityName = name; }
    public override string ToString() =>
       $"{EntityName} already exists.";
}
[Serializable]
public class DalMissingUserNameException : Exception
{
    public string EntityName;
    public DalMissingUserNameException(string name, string message)
     : base(message) { EntityName = name; }
    public override string ToString() =>
       $"{EntityName} dousn't exist.";
}
[Serializable]
public class DalConfigException : Exception
{
    public DalConfigException(string msg) : base(msg) { }
    public DalConfigException(string msg, Exception ex) : base(msg, ex) { }
}
