using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AT3
{
    public class Game
    {
        protected String name;
        protected String platform;
        protected String genre;

        public String getName()
        {
            return name;
        }

        public String getPlatform()
        {
            return platform;
        }

        public String getGenre()
        {
            return genre;
        }

        public void setName(string n)
        {
            name = n;
        }

        public void setPlatform(string p)
        {
            platform = p;
        }

        public void setGenre(string g)
        {
            genre = g;
        }

        public Game(string n, string p, string g)
        {
            name = n;
            platform = p;
            genre = g;
        }

        public Game() { }

        public override string ToString()
        {
            return getName() + "," + getPlatform() + "," + getGenre();
        }
    }
}
