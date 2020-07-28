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

        // Calculate average
        public double Avg(double[] buffer, int size)
        {
            int i;
            double total = 0;

            for (i = 0; i < size; i++)
                total += buffer[i];

            return total / size;
        }

        // Calculate Standard Deviation of Population
        public double SDPop(double[] buffer, int size)
        {
            double mean, standardDeviation = 0.0;
            int i;
            mean = Avg(buffer, size);

            for (i = 0; i < size; ++i)
                standardDeviation += Math.Pow(buffer[i] - mean, 2);

            return Math.Sqrt(standardDeviation / size);
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

        // Calculate Moving Z score
        private double[] ZMoving(double[] buffer, int size, int w)
        {
            double[] output = new double[size];
            double[] slice = new double[w];
            int o, j, s;
            double avg, sd;

            o = 0;
            for(int i=w; i<size; i++)
            {
                output[i] = 0;
                j = i - w;
                s = 0;
                for (int x = j; x < i; x++)
                    slice[s++] = buffer[x];
                avg = Avg(slice, w);
                sd = SDPop(slice, w);
                output[i] = (buffer[i] - avg) / sd;
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
            int zsize;
            bool results = false;
            double[] zvalues;
            string stype, dtype;
            string z;


            try
            {
                pd = Convert.ToDouble(txtSensitivity.Text);
            }
            catch
            {
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

            if (values.Length < 5)
            {
                txtResults.AppendText("Too Few Entries Error!");
                return;
            }

            if (chkMovingZ.Checked == true)
            {
                try
                {
                    zsize = Convert.ToInt32(txtWindowSize.Text);
                }
                catch
                {
                    txtResults.AppendText("Moving Z Size Error!");
                    return;
                }

                if (zsize < 3 || zsize > (values.Length / 3))
                {
                    txtResults.AppendText("Moving Z Size Error!");
                    return;
                }

                stype = "  rise";
                dtype = "  fall";

                zvalues = ZMoving(values, values.Length, zsize);
            }
            else
            {
                stype = "  spike";
                dtype = "  dip";

                zvalues = ZRobust(values, values.Length);
            }

            for (int i = 0; i < zvalues.Length; i++)
            {
                double zv = zvalues[i];

                z = ")  Z = ";
                if (zv < -100.0) { zv = -100.0; z = ")  Z < "; }
                if (zv > 100.0) { zv = 100.0; z = ")  Z > "; }

                if (Math.Abs(zv) >= pd)
                {
                    results = true;
                    if (zv > 0)
                        txtResults.AppendText("#" + (i + 1).ToString() + " (" + String.Format("{0:.###}", values[i]) + z + String.Format("{0:.0}", zv) + stype + Environment.NewLine);
                    else
                        txtResults.AppendText("#" + (i + 1).ToString() + " (" + String.Format("{0:.###}", values[i]) + z + String.Format("{0:.0}", zv) + dtype + Environment.NewLine);
                }
            }

            if (!results) txtResults.AppendText("No Anomalies Found");
        }
    }
}
