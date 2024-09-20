using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SpiralApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            InitializeComponent();
        }
        private void DrawSpiral_Click(object sender,RoutedEventArgs e)
        {
            // smaže předchozí spiralu
            DrawingCanvas.Children.Clear();

            
            if (double.TryParse(SpiralLengthTextBox.Text, out double length) &&
                double.TryParse(GapTextBox.Text, out double gap))
            {
                //zde si vyberme jakou matodu použít jestli chceme použít Rekurzivní necháte to jak to je jestli
                Point startPoint = new Point(DrawingCanvas.ActualWidth / 2, DrawingCanvas.ActualHeight / 2);

                // rekurzivní příklad
                DrawSpiralRecursive(startPoint, 0, length, gap);

                //Pro while loop odkomentujte
                // DrawSpiralWhile(startPoint, length, gap);
            }
            else
            {
                MessageBox.Show("Zadejte normální číslici!!.");
            }
        }

        //rekurzivni metoda 
        private void DrawSpiralRecursive(Point startPoint, double angle, double length, double gap)
        {
            if (length <= 0) return;

            //Vypočítá konečné body a kdy se má spirála natočit 
            double endX = startPoint.X+Math.Cos(angle) * length;
            double endY = startPoint.Y +Math.Sin(angle) * length;
            Point endPoint = new Point(endX, endY);

            // vykresluje naší čáru
            Line line = new Line
            {
                X1 = startPoint.X,
                Y1 = startPoint.Y,
                X2 = endPoint.X,
                Y2 = endPoint.Y,
                Stroke = Brushes.Black,
                StrokeThickness = 2
            };
            DrawingCanvas.Children.Add(line);

            // Zde nastává naše rekurze
            DrawSpiralRecursive(endPoint, angle + Math.PI / 2, length - gap, gap);
        }

        // While loop Vykreslení 
        private void DrawSpiralWhile(Point startPoint, double length, double gap)
        {
            Point currentPoint = startPoint;
            double angle = 0;
                while (length > 0)
            {
                // Znovu vypočítává konečné body , kdy se má spirála otočit
                double endX = currentPoint.X + Math.Cos(angle) * length;
                double endY = currentPoint.Y + Math.Sin(angle) * length;
                Point endPoint = new Point(endX, endY);

                // Vykresluje
                Line line = new Line
                {
                    X1 = currentPoint.X,
                    Y1 = currentPoint.Y,
                    X2 = endPoint.X,
                    Y2 = endPoint.Y,
                    Stroke = Brushes.Black,
                    StrokeThickness = 2
                };
                DrawingCanvas.Children.Add(line);

                // Updatne nám kodnoty pro další cyklus
                currentPoint = endPoint;
                angle += Math.PI / 2;
                length -= gap;
            }
        }
    }
}

