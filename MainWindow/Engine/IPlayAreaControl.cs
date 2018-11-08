using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace twoDTDS.Engine
{
    public interface IPlayAreaControl
    {
        void SetTransfrom(Transform t);
        void SetTransfromOrigin(Point pt);
    } 
}
