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
        void Settransform(Transform t);
        void SettransformOrigin(Point pt);
    } 
}
