using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.Distributions;

namespace fluffy_waffle_core
{
    public class Branch
    {
        public Matrix<double> Weight { get; set; }
        public Matrix<double> UpdateWeight { get; set; }
        public Connector[,] Web;

        public IValuable From { get; set; }
        public IValuable To { get; set; }

        public Branch(IValuable from, IValuable to)
        {
            From = from;
            To = to;
            Web = new Connector[from.Size, to.Size];
            Weight = Matrix<double>.Build.Random(from.Size, to.Size, new Gamma(1, 2.0));
            Weight -= 1;

            ConnectWeb();
        }
        
        private void ConnectWeb()
        {
            List<Neuron> firstList = GetNeurons(From);
            List<Neuron> secondList = GetNeurons(To);

            for(int i = 0; i < firstList.Count; i++)
            {
                for(int j = 0; j < secondList.Count; j++)
                {
                    Connector connector = new Connector(
                        (Point)firstList[i].Position, 
                        (Point)secondList[j].Position, 
                        Weight[i,j]
                        );
                    Web[i, j] = connector;
                }
            }
        }

        private List<Neuron> GetNeurons(IValuable valuable)
        {
            if (valuable is Neuron)
                return new List<Neuron>() { (Neuron)valuable };
            else
                return ((NeuronGroup)valuable).Group;
        }

        public void WeightUpdate()
        {
            Weight = UpdateWeight;
            for (int i = 0; i < Weight.RowCount; i++)
            {
                for (int j = 0; j < Weight.ColumnCount; j++)
                {
                    Web[i, j].Value = Weight[i, j];
                }
            }
        }

        public void BackPropagation(Vector<double> output, Vector<double> delta)
        {
            Matrix<double> matOutput = DenseMatrix.OfColumnVectors(output);
            Matrix<double> matDelta = DenseMatrix.OfRowVectors(delta);

            UpdateWeight = Weight - matOutput * matDelta;
        }
    }
}
