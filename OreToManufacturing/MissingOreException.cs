using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OreToManufacturing
{
    class MissingOreException : Exception
    {

        string _ore_name = null;

        public MissingOreException(string ore_name)
        {
            _ore_name = ore_name;            
        }

        public override string ToString()
        {
            return _ore_name;
        }
    }
}
