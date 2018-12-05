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
