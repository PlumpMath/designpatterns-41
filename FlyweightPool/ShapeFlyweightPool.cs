using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Shapes;

namespace DesignPatterns.FlyweightPool
{
    class ShapeFlyweightPool
    {
        private const int MAX_COUNT = 10;
        List<IShapeFlyweight> _shapeFlyweights;

        public ShapeFlyweightPool()
        {
            _shapeFlyweights = ShapeFactory.Create(ShapeType.Circle, MAX_COUNT);
            _shapeFlyweights.AddRange(ShapeFactory.Create(ShapeType.Square, MAX_COUNT));
            _shapeFlyweights.AddRange(ShapeFactory.Create(ShapeType.Triangle, MAX_COUNT));

            Reset();
        }

        public void Reset()
        {
            _shapeFlyweights.ForEach(s => s.IsInUse = false);
        }

        public IShapeFlyweight GetAShapeFlyweight()
        {
            var nextShapeFlyweight = _shapeFlyweights.FirstOrDefault(s => !s.IsInUse);
            if (nextShapeFlyweight == null) return null;

            nextShapeFlyweight.IsInUse = true;
            return nextShapeFlyweight;
        }
    }
}
