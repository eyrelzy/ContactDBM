using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactDBM
{
    class LinearRegression
    {
        //函数作用：正规方程法计算线性回归的系数
        public static List<double> NormalEquation(List<MatrixItem> items, int column)
        {
            Matrix m = getMartix(items, column + 1);
            Matrix trm = getTransposeMatrix(m);
            Matrix res = getMultiply(trm, m);
            Matrix inv = getInverseMatrix(res);
            res = getMultiply(inv, trm);
            Matrix y = getYMartix(items);
            Matrix final = getMultiply(res, y);
            final.OutputM();
            List<double> list = getMatrixList(final);
            return list;
        }
        #region 调用矩阵类的函数
        public static List<double> getMatrixList(Matrix m)
        {
            return m.GetM();
        }
        //函数作用：获得原y矩阵
        public static Matrix getYMartix(List<MatrixItem> items)
        {
            Matrix y = new Matrix(items.Count, 1);
            for (int i = 0; i < items.Count; i++)
            {
                y.SetNum(i, 0, items[i].y);
            }
            return y;
        }

        //函数作用：获得原矩阵
        public static Matrix getMartix(List<MatrixItem> items, int column)
        {
            Matrix matrix = new Matrix(items.Count, column);
            for (int i = 0; i < items.Count; i++)
            {
                matrix.SetNum(i, 0, 1);
                matrix.SetNum(i, 1, items[i].x0);
                matrix.SetNum(i, 2, items[i].x1);
            }
            return matrix;
        }

        //函数作用：获得转置矩阵
        public static Matrix getTransposeMatrix(Matrix m)
        {
            return m.Transpose();
        }

        //函数作用：获得矩阵乘积
        public static Matrix getMultiply(Matrix m1, Matrix m2)
        {
            return Matrix.Mutiply(m1, m2);
        }

        //函数作用：获得逆矩阵
        public static Matrix getInverseMatrix(Matrix m)
        {
            return Matrix.Inverse(m);
        }
        #endregion
        #region 文件操作
        public static List<MatrixItem> readFile(String path, char op)
        {
            List<MatrixItem> l = new List<MatrixItem>();
            using (StreamReader sr = File.OpenText(path))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    if (s != null && !s.Equals(""))
                    {
                        String[] strs = s.Split(op);
                        MatrixItem u = new MatrixItem(Convert.ToDouble(strs[2]), Convert.ToDouble(strs[3]), Convert.ToDouble(strs[7]));
                        l.Add(u);
                    }
                }
            }
            return l;
        }
        #endregion
    }
}
