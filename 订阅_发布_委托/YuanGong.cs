using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 订阅_发布_委托
{
    internal class YuanGong
    {
        public string Name { get; private set; }
        public int Num { get; private set; }

        public YuanGong(string name, int num)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Num = num;
        }

        public static bool sort(YuanGong yg1,YuanGong yg2) 
        {
            return yg1.Num > yg2.Num;
        }

    }
}
