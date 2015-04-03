using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OreToManufacturing
{
    class SkillLevel
    {
        int typeid = 0;
        int level = 0;
        string name = null;

        public SkillLevel(string name, int typeid, int level)
        {
            this.name = name;
            this.typeid = typeid;
            this.level = level;
        }

        public string Name
        {
            get { return name; }
        }

        public int TypeID
        {
            get { return typeid; }
        }

        public int Level
        {
            get { return level; }
        }


    }
}
