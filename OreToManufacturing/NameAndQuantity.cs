using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OreToManufacturing
{
    class NameAndQuantity
    {
        string name = null;
        int quantity = 0;

        public NameAndQuantity(string name, int quantity)
        {
            this.name = name;
            this.quantity = quantity;
        }

        public string Name
        {
            get { return name; }
        }

        public int Quantity
        {
            get { return quantity; }
        }
    }
}
