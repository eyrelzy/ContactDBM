using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactDBM
{
    class DataClean
    {
        //函数作用：清除100、400开头的业务电话
        public static List<Contact> clean(List<Contact> item)
        {
            List<int> index = new List<int>();
            for (int i = 0; i < item.Count; i++)
            {
                if (item[i].you.info.StartsWith("400") || item[i].you.info.StartsWith("100"))
                {
                    index.Add(i);
                }
            }
            for (int i = index.Count - 1; i >= 0; i--)
            {
                item.RemoveAt(index[i]);
            }
            return item;
        }
    }
}