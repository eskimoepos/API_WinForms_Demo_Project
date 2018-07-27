using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Win_Forms_Client
{

    class test { 
        public static bool TryCast<T>(ref T t, object o)
        {
            if (!(o is T))
            {
                return false;
            }

            t = (T)o;
            return true;
        }
    }
}
