using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactDBM
{
    class LZYPoint
    {
        public int index;
        public List<double> z;
        public Node me = new Node();
        public Node you = new Node();
        public double x;//
        public double y;//
        public Middle cluster = new Middle();
        public double distance;
        public int realScore;
        public int ifuse;
        public LZYPoint() { }

        public LZYPoint(int index, double x, double y, Middle cluster, double distance)
        {
            this.index = index;
            this.x = x;
            this.y = y;
            this.cluster = cluster;
            this.distance = distance;
            ifuse = 1;
        }
    }
    class Middle
    {
        //public Middle();
        public List<double> z;
        public double x;//
        public double y;//
        public Middle(List<double> z)
        {
            this.z = z;
        }
        public Middle(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
        public Middle()
        {
            this.x = -1;
            this.y = -1;
        }
    }
}