using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactDBM
{
    class Cluster
    {
        //函数作用：读预测文件，统计总数及错误数
        public static int readPredictFile(String path, int errortype, char op)
        {
            int totalnum = 0, errornum = 0;
            using (StreamReader sr = File.OpenText(path))
            {
                String s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    if (s != null && !s.Equals(""))
                    {
                        totalnum++;
                        String[] strs = s.Split(op);
                        if ((strs[strs.Length - 1]).Equals(errortype + ""))
                        {
                            errornum++;
                        }
                    }
                }
                sr.Close();
                if (errortype == 1)
                    Console.WriteLine("-1类总数：" + totalnum + "   -1类错误数：" + errornum + " 错误率：" + (double)errornum / totalnum);
                else
                    Console.WriteLine("1类总数：" + totalnum + "   1类错误数：" + errornum + " 错误率：" + (double)errornum / totalnum);
            }
            return errornum;
        }
        //函数作用：清除100、400开头的业务电话
        public static List<LZYPoint> clean(List<LZYPoint> item)
        {
            List<int> index = new List<int>();
            for (int i = 0; i < item.Count; i++)
            {
                if (item[i].you.name.StartsWith("400") || item[i].you.name.StartsWith("100") || item[i].you.name.StartsWith("411"))
                {
                    index.Add(i);
                }
            }
            item = writeFile(item, index, -1, 1);
            return item;
        }
        //启发式规则选中的先写入到文件中
        public static List<LZYPoint> writeFile(List<LZYPoint> point, List<int> index, int filetype, int opentype)
        {
            FileStream fs = null;
            if (filetype == 1)
            {
                if (opentype == 1)
                {
                    fs = new FileStream("File\\callClass1.csv", FileMode.Create);
                    FileStream fs0 = new FileStream("File\\callClass-1.csv", FileMode.Create);
                    fs0.Close();
                }
                else
                    fs = new FileStream("File\\callClass1.csv", FileMode.Append);
            }
            else
            {
                if (opentype == 1)
                {
                    fs = new FileStream("File\\callClass-1.csv", FileMode.Create);
                    FileStream fs0 = new FileStream("File\\callClass1.csv", FileMode.Create);
                    fs0.Close();
                }
                else
                    fs = new FileStream("File\\callClass-1.csv", FileMode.Append);
            }
            StreamWriter sw = new StreamWriter(fs, Encoding.Default);
            for (int i = 0; i < index.Count; i++)
            {
                int j = index[i];
                String s = point[j].index + "," + point[j].me.name + "," + point[j].you.name + ",";
                for (int k = 0; k < point[j].z.Count; k++)
                {
                    s += point[j].z[k] + ",";
                }
                s += point[j].distance + "," + point[j].realScore + "\n";
                sw.Write(s);
                sw.Flush();
            }
            for (int i = index.Count - 1; i >= 0; i--)
            {
                point.RemoveAt(index[i]);
            }
            sw.Close();
            fs.Close();
            return point;
        }
        //函数作用：读文件
        public static List<LZYPoint> readFile(String path, char op)
        {
            List<LZYPoint> l = new List<LZYPoint>();
            using (StreamReader sr = File.OpenText(path))
            {
                string s = "";
                int index = 0;
                while ((s = sr.ReadLine()) != null)
                {
                    if (s != null && !s.Equals(""))
                    {
                        String[] strs = s.Split(op);
                        LZYPoint u = new LZYPoint();
                        List<double> zlist = new List<double>();
                        //只有name，info为null
                        u.me.name = strs[0];
                        u.you.name = strs[1];
                        for (int i = 2; i < 7; i++)
                        {
                            zlist.Add(Convert.ToDouble(strs[i]));
                        }
                        u.realScore = Convert.ToInt32(strs[7]);
                        u.z = zlist;
                        u.index = index;
                        u.distance = 99999999;
                        //Point u = new Point(index, Convert.ToDouble(strs[2]), Convert.ToDouble(strs[3]), new Middle(), 9999);
                        l.Add(u);
                        index++;
                    }
                }
                sr.Close();
            }

            return l;
        }
        //指定k获得k个中心点
        public static List<Middle> getMiddle(List<LZYPoint> point, int k)
        {
            List<Middle> middle = new List<Middle>();
            int duration = point.Count / k;
            for (int i = 0; i < k; i++)
            {
                int index = i * duration;
                List<double> z = new List<double>();
                for (int j = 0; j < point[index].z.Count; j++)
                {
                    z.Add(point[index].z[j]);
                }
                middle.Add(new Middle(z));
            }
            //List<double> ml = new List<double>();
            //ml.Add(0.05263158);
            //ml.Add(0.05638853);
            //ml.Add(5);
            //ml.Add(692);
            //middle.Add(new Middle(ml));
            //ml.Clear();
            //ml.Add(0.006097561);
            //ml.Add(0.000357594);
            //ml.Add(2);
            //ml.Add(47);
            //middle.Add(new Middle(ml));
            return middle;
        }
        //函数作用：kmeans算法实现
        public static void Kmeans(List<LZYPoint> point, List<Middle> middle)
        {
            int changed = 1;
            while (changed == 1)
            {
                changed = 0;
                for (int i = 0; i < point.Count; i++)
                {
                    Middle upmiddle = getMiddle(point, middle, i);
                    if (!upmiddle.Equals(point[i].cluster))
                    {
                        changed = 1;
                        point[i].cluster = upmiddle;
                        double dis = 0;
                        for (int j = 0; j < point[i].z.Count; j++)
                        {
                            dis += Math.Pow(point[i].z[j] - upmiddle.z[j], 2);
                        }
                        point[i].distance = Math.Sqrt(dis);//Math.Sqrt(Math.Pow((point[i].x - upmiddle.x), 2) + Math.Pow(point[i].y - upmiddle.y,2));
                    }
                }
            }
            //show(point,middle);
        }
        public static double getCost(List<LZYPoint> point)
        {
            double cost = 0;
            for (int i = 0; i < point.Count; i++)
                cost += point[i].distance;
            return cost;
        }
        public static int K(List<LZYPoint> point, List<Middle> middle, int detail = 0)
        {
            double precost = 9999;
            while (true)
            {
                Kmeans(point, middle);
                double cost = getCost(point);
                if (precost == cost)
                    break;
                List<double> midz = new List<double>();
                for (int r = 0; r < point[0].z.Count; r++)
                {
                    midz.Add(0);
                }

                double x = 0, y = 0, num = 0;
                for (int j = 0; j < middle.Count; j++)
                {
                    for (int k = 0; k < midz.Count; k++)
                    {
                        midz[k] = 0;
                    }
                    x = y = num = 0;
                    for (int i = 0; i < point.Count; i++)
                    {
                        if (point[i].cluster.Equals(middle[j]))
                        {
                            for (int t = 0; t < point[i].z.Count; t++)
                            {
                                midz[t] += point[i].z[t];
                            }
                            num++;
                        }
                    }
                    for (int t = 0; t < midz.Count; t++)
                    {
                        middle[j].z[t] = midz[t] / num;
                    }
                }
                precost = cost;
            }
            //show(point, middle, detail);
            toFile(point, middle, detail);
            int num1 = readPredictFile("File//callClass1.csv", -1, ',');
            int num2 = readPredictFile("File//callClass-1.csv", 1, ',');
            return num1 + num2;
        }
        //打印结果
        public static void show(List<LZYPoint> point, List<Middle> middle, int detail)
        {
            for (int i = 0; i < middle.Count; i++)
            {
                //Console.Write("middle point (" + middle[i].x + "," + middle[i].y + "):  ");
                int num = 0;
                for (int j = 0; j < point.Count; j++)
                {
                    if (point[j].cluster.Equals(middle[i]))
                    {
                        num++;
                        //if (detail == 1)
                        // Console.Write(point[j].index + "(" + point[j].x + "," + point[j].y + ")" + "\t");
                    }
                }
                //if(i==0)
                //    Console.WriteLine("1类总数:"+num);
                //else
                //    Console.WriteLine("-1类总数:" + num);
            }
        }
        //取真正的评分
        public static int getRealScore(LZYPoint c, List<Pair> pair)
        {
            for (int i = 0; i < pair.Count; i++)
            {
                if (pair[i].person1.Equals(c.me.name) && pair[i].person2.Equals(c.you.name))
                    //if (pair[i].person1.Equals(c.me.name) && pair[i].person2.Equals(c.you.name))
                    return pair[i].realScore;
            }
            return 0;
        }
        //打印file结果
        public static void toFile(List<LZYPoint> point, List<Middle> middle, int detail)
        {
            //List<Pair> pair = ScoreAnalysis.getPairList("File\\realScore.csv", ',');

            for (int i = 0; i < middle.Count; i++)
            {
                FileStream fs = null;
                if (i == 1)
                    fs = new FileStream("File\\callClass-1.csv", FileMode.Append);
                else
                    fs = new FileStream("File\\callClass1.csv", FileMode.Append);
                StreamWriter sw = new StreamWriter(fs, Encoding.Default);
                int num = 0;
                for (int j = 0; j < point.Count; j++)
                {
                    if (point[j].cluster.Equals(middle[i]))
                    {
                        //int realScore = getRealScore(point[j], pair);
                        if (i == 0 && point[j].realScore != 1)
                        {
                            num++;
                        }
                        if (i == 1 && point[j].realScore != -1)
                        {
                            num++;
                        }
                        //sw.Write(point[j].index + "," + point[j].x + "," + point[j].y + "," +point[j].distance+","+item[point[j].index].y+ "\n");
                        String s = point[j].index + "," + point[j].me.name + "," + point[j].you.name + ",";
                        for (int k = 0; k < point[j].z.Count; k++)
                        {
                            s += point[j].z[k] + ",";
                        }
                        s += point[j].distance + "," + point[j].realScore + "\n";
                        sw.Write(s);
                        sw.Flush();
                    }

                }
                //if(i==1)
                //    Console.Write( "-1类错误数:" + num + "\n");
                //else
                //    Console.Write("1类错误数：" + num + "\n");
                sw.Close();
                fs.Close();
            }
        }
        //函数作用：计算点的中心点
        public static Middle getMiddle(List<LZYPoint> point, List<Middle> middle, int unknown)
        {
            double min = point[unknown].distance;
            LZYPoint up = point[unknown];
            Middle res = point[unknown].cluster;
            for (int i = 0; i < middle.Count; i++)
            {
                Middle mp = middle[i];
                //
                double dis = 0;
                for (int j = 0; j < up.z.Count; j++)
                {
                    dis += Math.Pow(up.z[j] - mp.z[j], 2);
                }
                double temp = Math.Sqrt(dis);
                //
                //double temp = Math.Sqrt(Math.Pow(up.x - mp.x, 2) + Math.Pow(up.y - mp.y, 2));
                if (temp < min)
                {
                    min = temp;
                    res = mp;
                }
            }
            return res;
        }
    }
}