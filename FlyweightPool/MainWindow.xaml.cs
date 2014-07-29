using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DesignPatterns.FlyweightPool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ShapeFlyweightPool _shapePool;
        private Random _randomizer;

        public MainWindow()
        {
            InitializeComponent();

            _shapePool = new ShapeFlyweightPool();
            _randomizer = new Random();
        }

        private Brush PickBrush()
        {
            Brush result = Brushes.Transparent;

            Type brushesType = typeof(Brushes);
            PropertyInfo[] properties = brushesType.GetProperties();

            int random = _randomizer.Next(properties.Length);
            result = (Brush)properties[random].GetValue(null, null);

            return result;
        }

        private void RunButton_Click(object sender, RoutedEventArgs e)
        {
            Clear();

            while (true)
            {
                var shapeFlyweight = _shapePool.GetAShapeFlyweight();
                if (shapeFlyweight == null) return;

                var shape = shapeFlyweight.Transform(top: _randomizer.Next(500), 
                                                     left: _randomizer.Next(500),
                                                     brush: PickBrush(), 
                                                     opacity: 0.9);

                canvas.Children.Add(shape);
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            Clear();
        }

        private void Clear()
        {
            canvas.Children.Clear();
            _shapePool.Reset();
        }
    }
}
