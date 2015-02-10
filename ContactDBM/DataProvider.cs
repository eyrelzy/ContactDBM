using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactDBM
{
    class DataProvider
    {

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
            //MessageBox.Show("CSV文件保存成功！");
        }
        public List<string> readStringListFromFile(string filepath)
        {
            string strpath = filepath; //文件的路径

            List<string> dl = new List<string>();
            try
            {   string strline;
                StreamReader mysr = new StreamReader(strpath, System.Text.Encoding.Default);
                while ((strline = mysr.ReadLine()) != null)
                {
                    dl.Add(strline);

                }
                return dl;
            }
            catch (Exception e)
            {
                //throw (Stack.GetErrorStack(strpath + "读取CSV文件中的数据出错." + e.Message, "OpenCSVFile("));
                return null;
            }
        }
        

        public   Dictionary<string, Circle> readCircleListFromFile(string filepath)
        {
            string strpath = filepath; //文件的路径

            List<string> dl = new List<string>();

            Dictionary<string, Circle> circles = new Dictionary<string, Circle>();
            try
            {
                string strline;
                StreamReader mysr = new StreamReader(strpath, System.Text.Encoding.Default);
                while ((strline = mysr.ReadLine()) != null)
                {
                    string[] strarray = strline.Split(',');
                    if (strarray.Length == 3)
                    {
                      string cname=strarray[0];
                      string tname=strarray[1];
                      double weight=Convert.ToDouble(strarray[2]);
                      if(circles.ContainsKey(cname))
                      {

                        Circle a= circles[cname];
                        a.addArrow(tname,weight);

                      }
                      else
                      {
                          Circle acircle=new Circle(cname);
                          acircle.addArrow(tname,weight);
                          circles.Add(cname,acircle);
                          
                      }
                    }
                   //dl.Add(strline);

                }
                return circles;
            }
            catch (Exception e)
            {
                //throw (Stack.GetErrorStack(strpath + "读取CSV文件中的数据出错." + e.Message, "OpenCSVFile("));
                return null;
            }
        }

    }
}
