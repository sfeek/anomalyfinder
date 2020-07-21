using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnomalieFinder
{
    public partial class AF : Form
    {
        public AF()
        {
            InitializeComponent();
        }

        // Calculate median
        private double Median(double[] buffer, int size)
        {
            double[] srt = (double[])buffer.Clone();

            Array.Sort(srt);
            return srt[size / 2];
        }

        // Calculate Robust Z-Score
        private double[] ZRobust(double[] buffer, int size)
        {
            double[] output = new double[size];
            double[] medians = new double[size];

            double median1 = Median(buffer, size);

            for (int i =0; i<size; i++)
            {
                medians[i] = Math.Abs(buffer[i] - median1);
            }

            double median2 = Median(medians, size);

            for (int i = 0; i < size; i++)
            {
                output[i] = (0.6745 * (buffer[i] - median1)) / median2;
            }

            return output;
        }

        // Get CSV as string and split into array of numbers, return NULL if anything goes wrong
        double[] CSVSplit(string inp)
        {
            string[] fields;
            double[] values;
            int c = 0;

            try
            {
                fields = inp.Split(new[] { "\r\n", "\r", "\n", "," }, StringSplitOptions.RemoveEmptyEntries);
                values = new double[fields.Length];

                foreach (string s in fields)
                {
                    values[c++] = Convert.ToDouble(s);
                }
            }
            catch
            {
                return null;
            }

            return values;
        }

        // Search for anomalys
        private void btnSearch_Click(object sender, EventArgs e)
        {
            double pd;
            bool results = false;

            try
            {
                pd = Convert.ToDouble(txtSensitivity.Text);
            }
            catch {
                txtResults.AppendText("Z Threshold Error!");
                return; 
            }

            if (pd < 0.0)
            {
                txtResults.AppendText("Z Threshold Error!");
                return;
            }

            txtResults.Text = String.Empty;

            double[] values = CSVSplit(txtInput.Text);
            if (values == null)
            {
                txtResults.AppendText("Input Data Error!");
                return;
            }

            double[] zvalues = ZRobust(values, values.Length);

            for (int i = 0; i < values.Length; i++)
            {
                double zv = zvalues[i];
                if (Math.Abs(zv) >= pd)
                {
                    results = true;
                    if (zv > 0)
                        txtResults.AppendText("#" + (i + 1).ToString() + " (" + String.Format("{0:.###}", values[i]) + ")  Z = " + String.Format("{0:.0}", zv) + "   Spike" + Environment.NewLine);
                    else
                        txtResults.AppendText("#" + (i + 1).ToString() + " (" + String.Format("{0:.###}", values[i]) + ")  Z = " + String.Format("{0:.0}", zv) + "   Dip" + Environment.NewLine);
                }
            }

            if (!results) txtResults.AppendText("No Anomalies Found");
        }
    }
}
