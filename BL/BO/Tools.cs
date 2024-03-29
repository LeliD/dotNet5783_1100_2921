﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BO;

static class Tools
{
    public static string ToStringProperty<T>(this T t)
    {
        string str = "";
        foreach (PropertyInfo item in t.GetType().GetProperties())
        {
            str += item.Name + ": ";
            if (item.GetValue(t, null) is IEnumerable<object>)
            {
                str += "\n";
                str += String.Join("", (IEnumerable<object>)item.GetValue(t, null));
            }
            else
                str += item.GetValue(t, null) + "\n";
        }
        return str;
    }
}
