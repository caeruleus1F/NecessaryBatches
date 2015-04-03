using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OreToManufacturing
{
    abstract class AbstractParseStyle
    {
        public abstract List<NameAndQuantity> Parse(string text);
    }
}
