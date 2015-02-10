using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactDBM
{
    class Rule
    {
        public class AreaNum
        {
            public String me;
            public String you;
            public String areanum;
            public AreaNum() { }
            public AreaNum(String me, String you, String areanum)
            {
                this.me = me;
                this.you = you;
                this.areanum = areanum;
            }
        }
        //判断是否是固定电话
        public static bool ifPhone(string str)
        {


            if (!ifNotNumber(str))
            {
                if (!(str.StartsWith("1")))
                    return true;//固定电话
                if ((str.StartsWith("10")))
                    return true;
            }
            return false;
        }
        public static bool ifNotNumber(string str)
        {
            if (str.StartsWith("0") || str.StartsWith("1") || str.StartsWith("2") ||
             str.StartsWith("3") || str.StartsWith("4") || str.StartsWith("5") || str.StartsWith("6") || str.StartsWith("7") || str.StartsWith("8") || str.StartsWith("9"))
            {
                return false;
            }
            return true;
        }

        //判断是否是我家的固定电话
        public static bool ifHomePhone(String name, String phone, List<AreaNum> area)
        {
            for (int i = 0; i < area.Count; i++)
            {
                if (area[i].me.Equals(name))
                {
                    if (phone.StartsWith(area[i].areanum))
                    {
                        return true;//是我的家
                    }
                }
            }
            return false;
        }
        //启发式规则：家乡1，非家乡固定电话制成-1
        public static List<LZYPoint> deletePhone(List<LZYPoint> item)
        {
            List<AreaNum> area = new List<AreaNum>();
            using (StreamReader sr = File.OpenText("File//contact.csv"))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    if (s != null && !s.Equals(""))
                    {
                        String[] strs = s.Split(',');
                        AreaNum a = new AreaNum(strs[0], strs[1], strs[2]);
                        area.Add(a);
                    }
                }
            }
            List<int> index = new List<int>();
            List<int> negindex = new List<int>();
            for (int i = 0; i < item.Count; i++)
            {
                String yourphone = item[i].you.name;
                if (i == 25)
                {
                    int a = 0;
                }
                bool isphone = ifPhone(yourphone);
                if (isphone == true)//固定电话
                {
                    bool res = ifHomePhone(item[i].me.name, yourphone, area);
                    if (res == true)//且是我的家
                        index.Add(i);
                }
            }
            item = Cluster.writeFile(item, index, 1, 0);
            for (int i = 0; i < item.Count; i++)
            {
                String yourphone = item[i].you.name;
                bool isphone = ifPhone(yourphone);
                if (isphone == true)//固定电话
                {
                    bool res = ifHomePhone(item[i].me.name, yourphone, area);
                    if (res == false)
                        negindex.Add(i);
                }
            }
            item = Cluster.writeFile(item, negindex, -1, 0);
            return item;
        }
    }
}
