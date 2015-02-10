using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactDBM
{
    class Contact
    {
        public Node me = new Node();
        public Node you = new Node();
        public float cnt;//cnt/CNT
        public float len;//dur/DUR
        public int tcnt;
        public int tlen;
        public float averlen;
        public float score;
        public int realScore;
    }
    class Node
    {
        public String name;//如果是班级同学，为姓名，否则是电话号码
        public String info;//telephone
    }
    class Item
    {
        public Node me = new Node();
        public Node you = new Node();
        public int duration;
        public String time;
    }
    class Pair
    {
        public String person1;
        public String person2;
        public int realScore;
    }
}
