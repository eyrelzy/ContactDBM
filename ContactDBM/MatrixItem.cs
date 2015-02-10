using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactDBM
{
    class MatrixItem
    {  
        public double x0;
        public double x1;
        public double y;
        public MatrixItem(double x0, double x1, double y)
        {
            this.x0 = x0;
            this.x1 = x1;
            this.y = y;
        }
    }
}
