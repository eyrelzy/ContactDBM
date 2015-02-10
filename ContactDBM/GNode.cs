using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactDBM
{
    public class GNode
    {
        string name;//如果是班级同学，为姓名，否则是电话号码
        string info;//telephone
        public Posistion position=new Posistion();
       
        /// <summary>
        ///get name
        /// </summary>
        /// <returns></returns>
        public string getName()
        {
            return this.name;
        }
        /// <summary>
        ///get  info:tepephone
        /// </summary>
        /// <returns></returns>
        public string getInfo()
        { 
            return this.info;
        }
        /// <summary>
        /// set name 
        /// </summary>
        /// <param name="name"></param>
        public void setName(string name)
        {
            this.name = name;

        }
        /// <summary>
        /// set info 
        /// </summary>
        /// <param name="info"></param>
        public void setInfo(string info)
        {
            this.info = info;

        }
       
        /// <summary>
        /// telephone
        /// </summary>
        /// <param name="telephone"></param>
        public GNode(string telephone)
        {
            this.info = telephone;//暂定为电话号码
            this.name = telephone;

        }

    }
   public  class Posistion
    {
       public  int x=0;
       public  int y=0;
       public Posistion()
       { 
       }
       public Posistion(int x,int y)
       {
           this.x = x;
           this.y = y;
       }

       public  double getDistance(int a_x,int a_y)
       {
          return  Math.Pow((a_x-x),2)+Math.Pow((a_y-y),2);

       }
    }
}
