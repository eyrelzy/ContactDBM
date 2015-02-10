using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContactDBM
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
           // string arr="05秒";
           // MessageBox.Show(ConvertToNumber(arr)+"");

        }
        string filePath = "";
        List<string> Datas = new List<string>();
        private void 预处理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            predeal();

        }

        private void predeal()
        {
            openFileDialog1.Reset();

            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;
            openFileDialog1.Multiselect = false;
            openFileDialog1.Title = "打开文件";

            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                //文件路径 和文件名字  
                filePath = openFileDialog1.FileName;
                List<string> list = new List<string>();
                try
                {
                    string strline;

                    StreamReader mysr = new StreamReader(filePath, System.Text.Encoding.Default);
                    int i = 0;
                    while ((strline = mysr.ReadLine()) != null)
                    {
                        // MessageBox.Show(strline);

                        string[] strs = strline.Split(',');
                        if (strs.Length >= 4)
                        {
                            int temp = ConvertToNumber(strs[2]);
                            strs[2] = temp.ToString();
                            if (strs[3].Trim().Equals("主叫"))
                            {
                                strs[3] = 0 + "";
                            }
                            else
                            {
                                strs[3] = 1 + "";
                            }



                        }
                        string newstr = "";
                        foreach (string str in strs)
                        {
                            newstr += str + ",";
                        }

                        newstr = newstr.TrimEnd(',');
                        list.Add(newstr);
                    }


                    mysr.Close();
                    //return ul;

                }
                catch (Exception eee)
                {
                    //throw (Stack.GetErrorStack(strpath + "读取CSV文件中的数据出错." + e.Message, "OpenCSVFile("));
                    // return null;

                }
                SaveCSVFromList(list, filePath);
            }
        }
      
        private int ConvertToNumber(string str)
    {
        int num=0;
        int total = 0;
        if (str.Contains("小时"))
        {
            string strs = str.Replace("小时", ",");
            string[] newarr = strs.Split(',');
            num = Convert.ToInt32(newarr[0]);
            total += num * 60 * 60;
            if (newarr.Length >= 2)
            {
                total +=getMinute(newarr[1]);
            }

        }
        else
        {
            total +=getMinute(str);
        }
       
        return total;
    }
        private int getMinute(string arr)
        {
            
            if (arr.Contains("分"))
            {
                string strs = arr.Replace("分", ",");
                string[] newarr = strs.Split(',');
                 int min=Convert.ToInt32(newarr[0])*60;
                 if (newarr.Length >= 2)
                  {
                      min += getSecond(newarr[1]);
 
                  }
                 return min;
            }
            else
            {

                return  getSecond(arr);
            }


        }
        private int getSecond(string arr)
        {

            if (arr.Contains("秒"))
            {
                string strs = arr.Replace("秒", "");
               return  Convert.ToInt32(strs);

            }
            else
            {
                return 0;
            }


        }
        public void SaveCSVFromList(List<string> dt, string fileName)
        {
            FileStream fs = new FileStream(fileName, System.IO.FileMode.Create, System.IO.FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Default);
            string data = "";

            //写出各行数据
            for (int i = 0; i < dt.Count; i++)
            {

                data = dt[i];

                sw.WriteLine(data);
            }

            sw.Close();
            fs.Close();
            MessageBox.Show("CSV文件保存成功！");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void 可视化关系图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Form2().Show();
        }

        private void 可视化关系图ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new Form2().Show();
        }

        private void 时间预处理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            predeal();
        }


    }

}
