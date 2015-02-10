using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContactDBM
{
    public partial class Form2 : Form
    {
        DataProvider dp= new DataProvider();
        private static int SECOND_R = 100;
        public Form2()
        {
            InitializeComponent();
            List<string> nameList = dp.readStringListFromFile("name.txt");
            comboBox1.DataSource = nameList;
            DataMining();
          
        }
     
        Dictionary<string, Circle> circles=new Dictionary<string,Circle>();
        private void pictureBox1_Click(object sender, EventArgs e)
        {
           

         
        }
        private Circle findClickPosCircle(double x,double y)
        {
            Circle tc = null;
          double  minDistance = 1000000000000;
            
            if (circles.Count > 0)
            {
                foreach (Circle c in circles.Values)
                {
                    double temp=c.getCenter().position.getDistance((int)x, (int)y);

                    if (temp < minDistance)
                    {
                        minDistance = temp;
                        tc = c;
                    }


                }
                return tc;
            }
            return null;


        }
      
        public Dictionary<string, GNode> alreadyNodeList=new Dictionary<string,GNode>();
        public void  DrawCircle(Circle c,Pen pen)
        {
            Graphics g = pictureBox1.CreateGraphics();
            int r = c.r;
            int x = c.getCenter().position.x;
            int y = c.getCenter().position.y;
            g.DrawEllipse(pen,x-r, y-r, 2*r,2*r);
            
          
            Font myFont = new Font("Itatic", 10, FontStyle.Bold);

            Brush bush = new SolidBrush(Color.Red);//填充的颜色
            g.DrawString(c.getCenter().getName(), myFont, bush,x,y);
            setPositionList(c,Color.AliceBlue);

        }
        public void DrawNode(GNode node,Color color)
        {
            Graphics g = pictureBox1.CreateGraphics();
            Pen pen = new Pen(color);
            int x = node.position.x;
            int y = node.position.y;
           // g.DrawEllipse(pen, x, y - r, 2 * r, 2 * r);
            Brush brush = new SolidBrush(Color.Yellow);
            g.FillEllipse(brush, x, y, 10, 10);
            Font myFont = new Font("Itatic", 10, FontStyle.Bold);
            Brush bush = new SolidBrush(Color.Red);//填充的颜色
            if (!node.getName().Contains("1")&&!node.getName().Contains("0")&&!node.getName().Contains("8")&&!node.getName().Contains("4")&&!node.getName().Contains("5"))
            {
                g.DrawString(node.getName(), myFont, bush, x, y);
            }


           
        }

        /// <summary>
        /// 绘制圆上点的圆
        /// </summary>
        /// <param name="c">中心圆</param>
        /// <param name="color">颜色</param>
        public void DrawNextCircle(Circle c, Color color)
        {
            Graphics g = pictureBox1.CreateGraphics();
            Pen pen = new Pen(color);

            foreach (Arrow newStartArrow in c.getArrows())
            {
                GNode node = newStartArrow.getTarget();
              
                
                int x =node.position.x;
                int y =node.position.y;
                try
                {
                    Circle coreCircle = circles[node.getName()];
                    //第二层圆的半径
                    coreCircle.r = SECOND_R;
                    //获取坐标
                    coreCircle.getCenter().position.x = node.position.x;
                    coreCircle.getCenter().position.y = node.position.y;
                    //画圆上的带你
                    DrawCircle(coreCircle, pen);

                    int r = 100;
                    g.DrawEllipse(pen, x - r, y - r, 2 * r, 2 * r);
                }
                catch (Exception)
                {
                    ;
                }
            }
          
        }
        /// <summary>
        /// 绘制箭头
        /// </summary>
        /// <param name="a">起点a</param>
        /// <param name="b">箭头b</param>
        /// <param name="color">颜色</param>
    
        public void drawArrow(GNode a,Arrow b,Color color)
        {
            Graphics g = pictureBox1.CreateGraphics();
            Pen p = new Pen(color, (float)b.getWeight()*10);
           
            Point bpoint = new Point(b.getTarget().position.x, b.getTarget().position.y);
            Point apoint = new Point(a.position.x, a.position.y);
            //Pen p = new Pen(Color.FromArgb(144, 160, 205), 5);
            
             p.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;//恢复实线  
             p.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;//定义线尾的样式为箭头 
             g.DrawLine(p, apoint, bpoint);
         
             
            p.Dispose();
            g.Dispose();
        }

  

       /// <summary>
       /// 设置圆周结点的位置
       /// </summary>
       /// <param name="c">圆</param>
       /// <param name="color">箭头的颜色</param>
        public void setPositionList(Circle c,Color color)
        {
          
            int centerx=c.getCenter().position.x;
            int centery = c.getCenter().position.y;
            int r=c.r;
             List<Arrow> targets=predeal(c, color);
             int count = targets.Count;
            double radians =(Math.PI / 180) * (360 / count); //弧度

          
            for (int i = 0; i < count; i++)
            {
                int x = centerx + Convert.ToInt32(Math.Round(r * Math.Sin(radians * i)));
                int y = centery + Convert.ToInt32(Math.Round(r * Math.Cos(radians * i)));
               
               // Point p=new Point(x, y);
                targets[i].getTarget().position.x=x;
                targets[i].getTarget().position.y=y;
                drawArrow(c.getCenter(),targets[i],color);
                alreadyNodeList.Add(targets[i].getTarget().getName(),targets[i].getTarget());
                DrawNode(targets[i].getTarget(), Color.Yellow);
                DrawNode(c.getCenter(), Color.Red);          
            }
        }
        
        
        /// <summary>
        /// 对圆周上的结点获取其未绘制的目标点
        /// </summary>
        /// <param name="c">中心圆</param>
        /// <param name="color">箭头颜色</param>
        /// <returns></returns>
        public List<Arrow> predeal(Circle c, Color color)
        {
            List<Arrow> targets = c.getArrows();
            List<Arrow> newtargets = new List<Arrow>();
            
            foreach (Arrow tnode in targets)
            {
                string tname=tnode.getTarget().getName();
                //是否目标结点已绘制？

                if (alreadyNodeList.ContainsKey(tnode.getTarget().getName()))
                {
                    //是，直接找到目标结点的位置
                    tnode.getTarget().position.x = alreadyNodeList[tname].position.x;
                    tnode.getTarget().position.y = alreadyNodeList[tname].position.y;
                    //绘制箭头
                    drawArrow(c.getCenter(), tnode, color);
                    DrawNode(tnode.getTarget(), Color.Yellow);
                }
                else
                {
                    //添加到该点对应的新的目标结点列表
                    newtargets.Add(tnode);

                }
 
            }
            return newtargets;

        }

        private  void DataMining()
        {

            /************第一次统计数据信息***************/
            List<Item> callItem = ScoreAnalysis.readFile(@"File\\call.csv", ',');
            List<Item> becalledITem = ScoreAnalysis.readFile(@"File\\becalled.csv", ',');
            List<Contact> callAns = ScoreAnalysis.getContact(callItem, 0);
            ScoreAnalysis.getContact(becalledITem, 1);

            /*************线性回归操作****************/
            List<MatrixItem> callScoreItem = LinearRegression.readFile("File\\callScore.csv", ',');
            List<double> callIndex = LinearRegression.NormalEquation(callScoreItem, 2);
            //List<MatrixItem> becallScoreItem = LinearRegression.readFile("File\\becallScore.csv", ',');
            //List<double> becallIndex = LinearRegression.NormalEquation(becallScoreItem, 2);

            //callAns = DataClean.clean(callAns);
            /****************亲密值根据权重更新******************/
            callAns = ScoreAnalysis.getContactAgain(callAns, 0, callIndex); //.getContact(callItem, 0,callIndex);
            //ScoreAnalysis.getContact(becalledITem, 1,becallIndex);

            /****************聚类操作***********************/
            Console.WriteLine();
            callScoreItem = LinearRegression.readFile("File\\callScore.csv", ',');
            List<LZYPoint> point = Cluster.readFile("File\\callScore.csv", ',');
            point = Cluster.clean(point);
            point = Rule.deletePhone(point);
            List<Middle> model = Cluster.getMiddle(point, 2);
            int errornum = Cluster.K(point, model);
            Console.WriteLine("总数：" + callAns.Count + " 总错误率：" + (double)errornum / callAns.Count);
            Console.Read();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string studentname = comboBox1.SelectedValue.ToString().Trim();
               // MessageBox.Show(studentname);
                circles = dp.readCircleListFromFile("file.csv");
               // MessageBox.Show(circles.Count+"");
                if (circles.Keys.Contains(studentname))
                {
                    Graphics g = pictureBox1.CreateGraphics();
                    g.Clear(Color.Gray);
                    //找到中心同学的圆的信息
                    Circle coreCircle = circles[studentname];
                    coreCircle.r = 250;//中心半径
                    //设置中心点的位置，屏幕居中
                    coreCircle.getCenter().position.x = pictureBox1.Width / 2;
                    coreCircle.getCenter().position.y = pictureBox1.Height / 2;
                   //MessageBox.Show(coreCircle.getCenter().position.x+","+coreCircle.getCenter().position.y);
                    alreadyNodeList = new Dictionary<string, GNode>();//初始画过的点的列表

                    alreadyNodeList.Add(coreCircle.getCenter().getName(), coreCircle.getCenter());//添加点
                    Pen pen = new Pen(Color.Red);
                    DrawCircle(coreCircle, pen);//画中心圆
                    DrawNextCircle(coreCircle, Color.LemonChiffon);//圆上的点基础上再画圆
                    foreach (GNode newnode in alreadyNodeList.Values)
                    {
                        DrawNode(newnode, Color.Red);//填写点的信息
                    }
                }
                else
                {
                    MessageBox.Show("sorry! not found any infomation about the one");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("error happen! check the input name  exisitence");
            }

        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            //
            
            double x = e.X;
            double y = e.Y;
            //MessageBox.Show(x + "," + y);
           
           
                GNode node = findNode((int)x, (int)y);
                if (node != null)
                {
                     Circle c = findClickPosCircle(x, y);
                     if (c.getCenter().getName().Equals(node.getName()))
                     {
                         GNode g = c.getMost();
                         MessageBox.Show(c.getCenter().getName() + ":最亲密的人：" + g.getName());
                     }
                     else
                     {
                         
                        // MessageBox.Show(node.getName());
                         Circle next=drawNext(node, Color.Green);
                         string info = "";
                         if (next != null)
                         {
                              info = ":最亲密的人：" + next.getMost().getName();
                         }
                         MessageBox.Show(node.getName()+info);
                     }
                   
                }


            
        }
        private GNode findNode(int x,int y)
        {
            double mindistance=100000000000;
            GNode node=null;
            if(alreadyNodeList.Count>0)
            {
             foreach(GNode gn in alreadyNodeList.Values)
            {
                 double temp=gn.position.getDistance(x,y);
                 if(temp<mindistance)
                 {
                     mindistance=temp;
                     node=gn;
                    
                 }

            }
            return node;
            }
            else
                return null;

        }
     
        private Circle drawNext(GNode gn,Color color)
        {
            GNode node = gn;


            int x = node.position.x;
            int y = node.position.y;
            try
            {
                Graphics g = pictureBox1.CreateGraphics();
                Circle coreCircle = circles[node.getName()];
                //第二层圆的半径
              
                coreCircle.r = SECOND_R;
             
                //获取坐标
                coreCircle.getCenter().position.x = node.position.x;
                coreCircle.getCenter().position.y = node.position.y;
                //画圆上的
                Pen pen = new Pen(color);
                DrawCircle(coreCircle, pen);

                int r = 100;
                g.DrawEllipse(pen, x - r, y - r, 2 * r, 2 * r);
                return coreCircle;
            }
            catch (Exception)
            {
                ;
            }
            return null;
        }
      
    }
}
