using System;
using System.Collections.Generic;
using System.Linq;
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
            double[] islice = new double[w];
            double[] oslice;
            int s;

            for (s = 0; s < size - w; s++)
            {
                // Get a slice
                for (int i = s; i < s + w; i++)
                {
                    islice[i - s] = buffer[i];
                }

                // Calculate Z Scores for the slice
                oslice = ZRobust(islice, w);

                // Keep only largest in the output
                for (int i = s; i < s + w; i++)
                {
                    if (Math.Abs(oslice[i-s]) > Math.Abs(output[i]))  output[i] = oslice[i-s];
                }
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
            int pd;
            int zsize;
            bool results = false;
            double[] zvalues;
            string stype, dtype;
            string z;


            try
            {
                pd = Convert.ToInt32(txtSensitivity.Text);
            }
            catch
            {
                txtResults.AppendText("Top # Error");
                return;
            }

            if (pd < 1)
            {
                txtResults.AppendText("Top # Error");
                return;
            }

            txtResults.Text = String.Empty;

            double[] values = CSVSplit(txtInput.Text);
            if (values == null)
            {
                txtResults.AppendText("Input Data Error!");
                return;
            }

            try
            {
                zsize = Convert.ToInt32(txtWindowSize.Text);
            }
            catch
            {
                txtResults.AppendText("Moving Z Size Error!");
                return;
            }

            if (values.Length < zsize)
            {
                txtResults.AppendText("Too Few Entries Error!");
                return;
            }

            if (zsize < 3)
            {
                txtResults.AppendText("Moving Z Size Error!");
                return;
            }

            stype = "  spike";
            dtype = "  dip";

            zvalues = ZMoving(values, values.Length, zsize);

            var zdictionary = new Dictionary<int, double>();

            for (int i = 1; i < zvalues.Length; i++) zdictionary.Add(i,zvalues[i - 1]);

            var zitems = from pair in zdictionary
                    orderby pair.Value descending
                    select pair;

            // Show "Spikes"
            int c = 0;
            foreach (KeyValuePair<int, double> pair in zitems)
            {
                if (c == pd) break;
                double zv = zdictionary[pair.Key];

                z = ")  Z = ";
                if (zv < -100.0) { zv = -100.0; z = ")  Z < "; }
                if (zv > 100.0) { zv = 100.0; z = ")  Z > "; }

                if (Math.Abs(zv) < 100.0)
                {
                    results = true;
                    
                    if (zv > 0)
                        txtResults.AppendText("#" + (pair.Key).ToString() + " (" + String.Format("{0:.###}", values[pair.Key - 1]) + z + String.Format("{0:.0}", zv) + stype + Environment.NewLine);
                    else
                        txtResults.AppendText("#" + (pair.Key).ToString() + " (" + String.Format("{0:.###}", values[pair.Key - 1]) + z + String.Format("{0:.0}", zv) + dtype + Environment.NewLine);
                    //txtResults.AppendText(String.Format("{0:.0}", zv) + Environment.NewLine);
                }
                c++;
            }

            txtResults.AppendText(Environment.NewLine);

            // Show "Dips"
            c = 0;
            foreach (KeyValuePair<int, double> pair in zitems.Reverse())
            {
                if (c == pd) break;
                double zv = zdictionary[pair.Key];

                z = ")  Z = ";
                if (zv < -100.0) { zv = -100.0; z = ")  Z < "; }
                if (zv > 100.0) { zv = 100.0; z = ")  Z > "; }

                if (Math.Abs(zv) < 100.0)
                {
                    results = true;

                    if (zv > 0)
                        txtResults.AppendText("#" + (pair.Key).ToString() + " (" + String.Format("{0:.###}", values[pair.Key - 1]) + z + String.Format("{0:.0}", zv) + stype + Environment.NewLine);
                    else
                        txtResults.AppendText("#" + (pair.Key).ToString() + " (" + String.Format("{0:.###}", values[pair.Key - 1]) + z + String.Format("{0:.0}", zv) + dtype + Environment.NewLine);
                    //txtResults.AppendText(String.Format("{0:.0}", zv) + Environment.NewLine);
                }

                c++;
            }

            if (!results) txtResults.AppendText("No Anomalies Found");
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtInput.Text = String.Empty;
            txtResults.Text = String.Empty;
        }
    }
}
