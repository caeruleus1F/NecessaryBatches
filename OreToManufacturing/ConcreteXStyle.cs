using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OreToManufacturing
{
    class ConcreteXStyle : AbstractParseStyle
    {
        public override List<NameAndQuantity> Parse(string text)
        {
            List<string> lines = text.Split('\n').ToList();
            List<NameAndQuantity> items = new List<NameAndQuantity>();

            for (int i = lines.Count - 1; i >= 0; --i)
            {
                if (lines[i] == "")
                {
                    lines.RemoveAt(i);
                }
            }

            foreach (string line in lines)
            {
                string mineral_name = line.Split(' ')[2];
                string raw_quantity = line.Split(' ')[0];
                raw_quantity = raw_quantity.Replace(",", "");
                int quantity = Convert.ToInt32(raw_quantity);
                items.Add(new NameAndQuantity(mineral_name, quantity));
            }

            return items;
        }
    }
}
