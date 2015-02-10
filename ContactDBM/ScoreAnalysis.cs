using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactDBM
{
    class ScoreAnalysis
    {
        #region 求亲密值的过程函数
        private static List<Node> getNodelist(String path, char op)
        {
            List<Node> nlist = new List<Node>();
            using (StreamReader sr = File.OpenText(path))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    if (s != null && !s.Equals(""))
                    {
                        String[] strs = s.Split(op);
                        Node n = new Node();
                        n.name = strs[0];
                        n.info = strs[1];
                        nlist.Add(n);
                    }
                }

            }
            return nlist;
        }
        public static List<Pair> getPairList(String path, char op)
        {
            List<Pair> nlist = new List<Pair>();
            using (StreamReader sr = File.OpenText(path))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    if (s != null && !s.Equals(""))
                    {
                        String[] strs = s.Split(op);
                        Pair n = new Pair();
                        n.person1 = strs[0];
                        n.person2 = strs[1];
                        n.realScore = Convert.ToInt32(strs[2]);
                        nlist.Add(n);
                    }
                }

            }
            return nlist;
        }
        private static Node findNode(List<Node> nodelist, String tel)
        {
            foreach (Node n in nodelist)
            {
                if (n.info.Equals(tel))
                {
                    return n;
                }
            }
            return null;
        }
        public static List<Item> readFile(String path, char op)
        {
            List<Item> itemlist = new List<Item>();
            List<Node> knownNode = new List<Node>();
            knownNode = getNodelist(@"File\\contact.txt", ',');//班级同学信息
            using (StreamReader sr = File.OpenText(path))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    if (s != null && !s.Equals(""))
                    {
                        Item i = new Item();
                        String[] strs = s.Split(op);
                        String mytele = strs[0];
                        String youtele = strs[1];
                        //主叫是不是同学
                        Node iret = findNode(knownNode, mytele);
                        if (iret == null)
                        {
                            i.me.info = mytele;
                            i.me.name = mytele;
                        }
                        else
                        {
                            i.me = iret;
                        }
                        Node yret = findNode(knownNode, youtele);
                        if (yret == null)
                        {
                            i.you.info = youtele;
                            i.you.name = youtele;
                        }
                        else i.you = yret;
                        int duration = Convert.ToInt32(strs[2]);
                        i.duration = duration;
                        i.time = strs[3];
                        itemlist.Add(i);
                    }
                }
            }
            return itemlist;
        }
        public static Contact findYM(String youtel, String metel, List<Contact> clist)
        {
            for (int i = 0; i < clist.Count; i++)
            {
                Contact c = clist[i];
                if (c.me.info.Equals(metel) && c.you.info.Equals(youtel))
                    return c;
            }
            return null;
        }
        public static int findSum(String metel, List<Item> itemlist)
        {
            int sum = 0;
            for (int i = 0; i < itemlist.Count; i++)
            {
                Item c = itemlist[i];
                if (c.me.info.Equals(metel))
                    sum++;
            }
            return sum;
        }
        public static int findDur(String metel, List<Item> itemlist)
        {
            int d = 0;
            for (int i = 0; i < itemlist.Count; i++)
            {
                Item c = itemlist[i];
                if (c.me.info.Equals(metel))
                    d = d + c.duration;
            }
            return d;
        }
        public static int getRealScore(Contact c, List<Pair> pair)
        {
            for (int i = 0; i < pair.Count; i++)
            {
                if (pair[i].person1.Equals(c.me.name) && pair[i].person2.Equals(c.you.name))
                    return pair[i].realScore;
            }
            return 0;
        }
        //函数作用：根据权重更新亲密值文件
        public static List<Contact> getContactAgain(List<Contact> clist, int type, List<double> index)
        {
            for (int i = 0; i < clist.Count; i++)
            {
                clist[i].score = getScore(clist[i], index);
            }
            writeFile(clist, type);
            return clist;
        }
        public static List<Contact> getContact(List<Item> itemlist, int type, List<double> index = null)//0-call 1-becalled
        {
            List<Contact> clist = new List<Contact>();
            List<Pair> pair = getPairList("File\\realScore.csv", ',');
            Node nodeme = itemlist[0].me;
            Node nodeyou = itemlist[0].you;
            int d = itemlist[0].duration;
            for (int i = 0; i < itemlist.Count; i++)
            {
                //如果存在我和你的记录就增加计数，如果不存在就新建                    
                Contact fc = findYM(itemlist[i].you.info, itemlist[i].me.info, clist);
                if (fc == null)
                {
                    Contact c = new Contact();
                    c.me = itemlist[i].me;
                    c.you = itemlist[i].you;
                    c.tcnt = 1;
                    c.tlen = itemlist[i].duration;
                    c.cnt = (1 / (float)findSum(itemlist[i].me.info, itemlist));
                    c.len = itemlist[i].duration / (float)findDur(itemlist[i].me.info, itemlist);
                    c.score = getScore(c, index);
                    c.realScore = getRealScore(c, pair);
                    clist.Add(c);
                }
                else
                {
                    fc.tcnt = fc.tcnt + 1;
                    fc.tlen = fc.tlen + itemlist[i].duration;
                    fc.cnt = fc.cnt + (1 / (float)findSum(itemlist[i].me.info, itemlist));
                    fc.len = fc.len + itemlist[i].duration / (float)findDur(itemlist[i].me.info, itemlist);
                    fc.score = getScore(fc, index);
                    fc.realScore = getRealScore(fc, pair);
                }
            }
            for (int i = 0; i < clist.Count; i++)
            {
                Contact c = clist[i];
                c.averlen = c.tlen / (float)c.tcnt;
            }
            writeFile(clist, type);
            return clist;
        }
        #endregion
        //亲密值计算
        public static float getScore(Contact c, List<double> index)
        {
            if (index != null && index.Count >= 3)
                return (float)(index[1] * c.cnt + index[2] * c.len + index[0] + 1);
            return 0;
        }
        public static void writeFile(List<Contact> clist, int type)
        {
            FileStream fs, fs1;
            fs1 = new FileStream("file.txt", FileMode.Create);
            if (type == 0)
                fs = new FileStream("File\\callScore.csv", FileMode.Create);
            else
                fs = new FileStream("File\\becallScore.csv", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
            StreamWriter sw1 = new StreamWriter(fs1, Encoding.UTF8);
            {
                for (int i = 0; i < clist.Count; i++)
                {
                    String s = clist[i].me.name + "," + clist[i].you.name + "," + clist[i].cnt + "," + clist[i].len + "," + clist[i].tcnt + "," + clist[i].tlen + "," + clist[i].score + "," + clist[i].realScore + "\n";
                    sw.Write(s);
                    sw.Flush();
                    sw1.Write(clist[i].me.name + "," + clist[i].you.name + "," + clist[i].score + "\n");
                    sw1.Flush();
                }
            }
            sw.Close();
            fs.Close();
            sw1.Close();
            fs1.Close();
        }
    }
}