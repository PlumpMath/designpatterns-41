using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DesignPatterns.FlyweightPool
{
    interface IFlyweightPoolObject
    {
        bool IsInUse { get; set; }
    }

    interface IShapeFlyweight : IFlyweightPoolObject, ICloneable
    {
        Shape Shape { get; }
        Shape Transform(int top, int left, Brush brush, double opacity);
    }
}
