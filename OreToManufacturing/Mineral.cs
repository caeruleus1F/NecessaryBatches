using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OreToManufacturing
{
    class Mineral : Contents
    {
        int needed = 0;

        public Mineral (string name, int quantity = 0, float volume = 0.01F)
            : base(name, quantity, volume)
        {

        }

        public override int Quantity
        {
            get { return base.Quantity; }
            set { base.Quantity = value; }
        }

        public int QuantityNeeded
        {
            get { return needed; }
            set { needed = value; }
        }

        public void ClearAll()
        {
            needed = 0;
            quantity = 0;
        }

    }
}
