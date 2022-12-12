using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using BlImplementation;
namespace BlApi;

public static class Factory
{
    public static IBl Get()
    {
        return new Bl();
    }
}
