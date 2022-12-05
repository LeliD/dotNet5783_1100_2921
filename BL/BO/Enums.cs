using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public enum Category
{
    KITCHEN,
    BATHROOM,
    LIVING_ROOM,
    BEDROOM,
    KIDS
}
public enum OrderStatus
{
    Initiated,
    Ordered,
    Paid,
    Shipped,
    Delivered
}
