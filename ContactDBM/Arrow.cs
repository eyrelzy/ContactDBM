using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactDBM
{
   public  class Arrow
    {

        /// <summary>
        /// 目标节点
        /// </summary>
        GNode target;//箭头被指的结点
        /// <summary>
        /// 权重
        /// </summary>
        double weight = 0.0;//weight
        /// <summary>
        /// Arrow constructor
        /// </summary>
        /// <param name="target">箭头被指的结点</param>
        /// <param name="weight">权值</param>
        public Arrow(GNode target, double weight)
        {
            this.target = target;
            this.weight = weight;
        }
        public Arrow(string targetTelephone, double weight)
        {
            this.target = new GNode(targetTelephone);
            this.weight = weight;
        }
        /// <summary>
        /// 获取目标节点
        /// </summary>
        public GNode getTarget()
        {
            return this.target;
        }
        /// <summary>
        /// 获取权重
        /// </summary>
        public  double getWeight()
        {
            return this.weight;
        }
        /// <summary>
        /// 设置目标节点
        /// </summary>
        public void setTarget(string target_telephone)
        {
            this.target = new GNode(target_telephone);
        }
        /// <summary>
        /// 设置目标节点
        /// </summary>
            public void setTarget(GNode target)
        {
            this.target = target;
        }

            /// <summary>
            /// 设置权重
            /// </summary>
        public void  setWeight(double weight)
        {
           this.weight=weight;
        }
       

    }
}
