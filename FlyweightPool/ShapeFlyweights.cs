using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DesignPatterns.FlyweightPool
{
    enum ShapeType
	{
        Square,
        Circle,
        Triangle
	}

    abstract class ShapeFlyweight : IShapeFlyweight
    {
        private Shape _internalShape = null;
        private Random _randomizer = new Random();

        public Shape Shape
        {
            get
            {
                if (_internalShape == null)
                    _internalShape = Draw(10.0f);

                return _internalShape;
            }
        }

        public bool IsInUse { get; set; }


        protected abstract Shape Draw(float scaleFactor);

        public object Clone()
        {
            return this.MemberwiseClone();
        }


        public Shape Transform(int top, int left, Brush brush, double opacity)
        {
            Canvas.SetTop(this.Shape, top);
            Canvas.SetLeft(this.Shape, left);

            this.Shape.Stroke = this.Shape.Fill = brush;
            this.Shape.Opacity = opacity;

            return this.Shape;
        }
    }

    class Square : ShapeFlyweight
    {
        protected override System.Windows.Shapes.Shape Draw(float scaleFactor)
        {
            var squareShape = new System.Windows.Shapes.Rectangle();
            squareShape.Height = 10 * scaleFactor;
            squareShape.Width = 10 * scaleFactor;

            return squareShape;
        }
    }

    class Circle : ShapeFlyweight
    {
        protected override System.Windows.Shapes.Shape Draw(float scaleFactor)
        {
            var shape = new System.Windows.Shapes.Ellipse();
            shape.Height = 10 * scaleFactor;
            shape.Width = 10 * scaleFactor;

            return shape;
        }
    }

    class Triangle : ShapeFlyweight
    {
        protected override System.Windows.Shapes.Shape Draw(float scaleFactor)
        {
            var shape = new System.Windows.Shapes.Polygon();

            shape.Points = new PointCollection {
                new Point { X=0, Y=0 },
                new Point { X=0, Y=100 },
                new Point { X=50, Y=50 },
            };

            shape.Height = 10 * scaleFactor;
            shape.Width = 10 * scaleFactor;

            return shape;
        }
    }

    static class ShapeFactory
    {
        public static List<IShapeFlyweight> Create(ShapeType type, int count)
        {
            IShapeFlyweight shape = null;

            switch (type)
            {
                case ShapeType.Square:
                    shape = new Square();
                    break;
                case ShapeType.Circle:
                    shape = new Circle();
                    break;
                case ShapeType.Triangle:
                    shape = new Triangle();
                    break;
                default:
                    throw new InvalidOperationException();
            }

            var shapes = new List<IShapeFlyweight>();
            shapes.Add(shape);

            for (int i = 1; i < count; i++)
            {
                shapes.Add(shape.Clone() as IShapeFlyweight);
            }

            return shapes;
        }
    }

}
