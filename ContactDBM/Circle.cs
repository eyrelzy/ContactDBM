using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactDBM
{
  public   class Circle
    {
        /// <summary>
        /// list arrows
        /// </summary>
        private List<Arrow> arrows = new List<Arrow>();
        public  GNode getMost()
        {
            double max = 0;
            GNode node=null;
            foreach(Arrow ar in arrows)
            {
                if (max < ar.getWeight())
                {
                    max = ar.getWeight();
                    node=ar.getTarget();
                }
            }
            return node;

        }
        /// <summary>
        /// center node
        /// </summary>
        private GNode center;
        /// <summary>
        /// set radias
        /// </summary>
        public  int r;
        public Circle(string telephone,int r)
        {
            center = new GNode(telephone);
            this.r = r;
        }
        public Circle(string telephone)
        {
            center = new GNode(telephone);
            //this.r = r;
        }
        /// <summary>
        /// 添加一条箭头
        /// </summary>
        /// <param name="target_tel">telephone</param>
        /// <param name="weight">weight for relationship</param>
        public void addArrow(string target_tel,double weight){

            arrows.Add(new Arrow(target_tel,weight));
         //   arrows.Sort();
       }
        /// <summary>
        /// 获取circle中所有箭头
        /// </summary>
        /// <returns></returns>
        public List<Arrow> getArrows()
        {
            return arrows;
 
        }
        /// <summary>
        /// 圆周上的结点列表是否存在
        /// </summary>
        public GNode ifContains(string telephone)
        {
            foreach(Arrow ar in arrows)
            {
                if (ar.getTarget().getInfo().Equals(telephone))
                    return ar.getTarget();
            }
            return null;
        }
        /// <summary>
        /// 获取中心结点
        /// </summary>
        /// <returns></returns>
        public GNode getCenter()
        {
            return this.center;

        }
        /// <summary>
        /// 设置中心点
        /// </summary>
        /// <param name="telephone">type :teleohone</param>
        public void setCenter(string telephone)
        {
            this.center = new GNode(telephone);
        }
        /// <summary>
        /// set center
        /// </summary>
        /// <param name="center">type :node</param>
        public void setCenter(GNode center)
        {
            this.center = center;
        }
        /// <summary>
        /// 设置绘图坐标
        /// </summary>
        public void setPositionList(List<Arrow> arrows)
        {
            int count=arrows.Count;
            int  radians =Convert.ToInt32( (Math.PI /180) *(360/count)); //弧度
	         for(int i = 0; i < count; i++)
             {
                int  x = center.position.x +Convert.ToInt32(Math.Round(r*Math.Sin(radians*i)));
                int y = center.position.y + Convert.ToInt32(Math.Round(r * Math.Cos(radians * i)));
                arrows[i].getTarget().position.x = x;
                arrows[i].getTarget().position.y= y;
             }
        }
        
    }
}
