using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OreToManufacturing
{
    abstract class Contents
    {
        protected string name = "";
        protected int quantity = 0;
        protected float volume = 0F;

        public Contents(string name, int quantity, float volume)
        {
            this.name = name;
            this.quantity = quantity;
            this.volume = volume;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public virtual int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        protected float Volume
        {
            get { return volume; }
            set { volume = value; }
        }
    }
}
