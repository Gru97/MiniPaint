using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniPaint
{
    interface ISerializer
    {
        string txtSerialize();
        void txtDeserialize(string s);   
    }
}
