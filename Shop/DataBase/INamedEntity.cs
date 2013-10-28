using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shop.DataBase
{
    public interface INamedEntity
    {
        string Name { get; set; }
    }
}
