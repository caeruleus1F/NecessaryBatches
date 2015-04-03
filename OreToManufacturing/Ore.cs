using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OreToManufacturing
{
    class Ore : Contents
    {
        List<Mineral> contents = new List<Mineral>();

        public Ore (string name, int quantity = 0, float volume = 0.01F)
            : base(name, quantity, volume)
        {

        }

        public void Add(Mineral mineral)
        {
            contents.Add(mineral);
        }

        public Mineral MineralByName(string mineral)
        {
            Mineral m = null;
            for (int i = 0; i < contents.Count; ++i)
            {
                if (contents[i].Name == mineral)
                {
                    m = contents[i];
                }
            }

            return m;
        }
        
        public List<Mineral> Contents
        {
            get { return contents; }
        }
    }
}
