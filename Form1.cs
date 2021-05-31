using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Analizator_Algorytmow_Bak_Teska
{
    public partial class Analizator_algorytmow : Form
    {
        int Margines = 20;
        int ReaserchSample = 100;
        int MaxArraySize = 60;
        double LowerValueLimit = 10.00;
        double UpperValueLimit = 20000.00;
        double[] Table;
        double[] Table_Before;
        float[] MeasurementResults;
        double[] AnalyticalResults;
        float[] MemoryResult;
        int[] LODTable;
        int[] MCTable;
        public Analizator_algorytmow()
        {
            InitializeComponent();
            this.Width = (int)(Screen.PrimaryScreen.Bounds.Width * 0.9f);
            this.Height = (int)(Screen.PrimaryScreen.Bounds.Height * 0.93f);
            this.Location = new Point(20, 20);
            this.StartPosition = FormStartPosition.Manual;
            textBoxMin.Text = ReaserchSample.ToString();
            textBoxMax.Text = MaxArraySize.ToString();
            textBoxBottom.Text = LowerValueLimit.ToString();
            textBoxTop.Text = UpperValueLimit.ToString();
            this.SetAutoSizeMode(AutoSizeMode.GrowAndShrink);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.AutoScroll = true;
        }
        private void buttonExit_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Czy na pewno chcesz zamknac program?", "Uwaga!", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

   

        private void buttonReset_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void buttonAccept_Click(object sender, EventArgs e)
        {
            Random Rnd = new Random();
            buttonAfterSort.Enabled = true;
            buttonAccept.Enabled = false;
            Table_Before = new double[MaxArraySize];
            MeasurementResults = new float[MaxArraySize];
            AnalyticalResults = new double[MaxArraySize];
            MemoryResult = new float[MaxArraySize];
            LODTable = new int[ReaserchSample];
            MCTable = new int[ReaserchSample];
            textBoxMin.Enabled = false;
            textBoxMax.Enabled = false;
            textBoxBottom.Enabled = false;
            textBoxTop.Enabled = false;
            groupBoxChooseAlgorithm.Enabled = false;
        }

        private void buttonChart_Click(object sender, EventArgs e)
        {
            groupBoxGraphics.Enabled = true;
            groupBoxLineThickness.Enabled = true;
            chartVisualization.Visible = true;
            dataGridViewTable.Visible = false;
            dataGridViewVisualization.Visible = false;
            pictureBoxDemo.Visible = false;
            chartVisualization.BackColor = textBoxLineColor.BackColor;
            switch (CheckChoosenAlgorithm())
            {
                case 0:
                    {
                        chartVisualization.Titles.Add("Algorytm sortowania bąbelkowego");
                        break;
                    }
                case 1:
                    {
                        chartVisualization.Titles.Add("Algorytm sortowania bibliotecznego");
                        break;
                    }
                case 2:
                    {
                        chartVisualization.Titles.Add("Algorytm sortowania gnome sort");
                        break;
                    }
                case 3:
                    {
                        chartVisualization.Titles.Add("Algorytm sortowania insertion sort");
                        break;
                    }
                case 4:
                    {
                        chartVisualization.Titles.Add("Algorytm sortowania quick sort");
                        break;
                    }
                default:
                    {
                        return;
                    }
            }
            chartVisualization.Legends["Legend1"].Docking =
                System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            int[] TableSize = new int[MaxArraySize];
            for (int i = 0; i < MaxArraySize; i++)
            {
                TableSize[i] = i;
                chartVisualization.Series[0].Name = "Koszt czasowy z pomiaru";
                chartVisualization.Series[1].Name = "Koszt czasowy wedlug wzoru";
                chartVisualization.Series[2].Name = "Koszt pamieci";
                chartVisualization.Series[0].ChartType =
                    System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                chartVisualization.Series[1].ChartType =
                    System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                chartVisualization.Series[2].ChartType =
                    System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                chartVisualization.Series[0].Color = Color.Pink;
                chartVisualization.Series[1].Color = Color.Black;
                chartVisualization.Series[2].Color = Color.Yellow;
                chartVisualization.Series[0].BorderDashStyle =
                    System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
                chartVisualization.Series[0].BorderDashStyle =
                    System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.DashDot;
                chartVisualization.Series[0].BorderDashStyle =
                    System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
                chartVisualization.Series[0].BorderWidth = 2;
                chartVisualization.Series[1].BorderWidth = 2;
                chartVisualization.Series[2].BorderWidth = 2;
                chartVisualization.Series[0].Points.DataBindXY(TableSize, MeasurementResults);
                chartVisualization.Series[1].Points.DataBindXY(TableSize, AnalyticalResults);
                chartVisualization.Series[2].Points.DataBindXY(TableSize, MemoryResult);
            }

        }

        private void buttonAfterSort_Click(object sender, EventArgs e)
        {
            int ODCounter;
            int MemoryCounter;
            int[] Counters = new int[2];
            float ODSum, ODAverage;
            float MemorySum, MemoryAverage;
            Random Rnd = new Random();
            Sort Algorithm = new Sort();
            for (int i = 0; i < MaxArraySize; i++)
            {
                Table = new double[i + 1];
                for (int j = 0; j < ReaserchSample; j++)
                {
                    for (int k = 0; k < Table.Length; k++)
                        Table[k] = Rnd.NextDouble() * (UpperValueLimit - LowerValueLimit) + LowerValueLimit;

                    switch (CheckChoosenAlgorithm())
                    {
                        case 0:
                            {
                                Counters = Algorithm.BubbleSort(ref Table);
                                ODCounter = Counters[0];
                                MemoryCounter = Counters[1];
                                break;
                            }
                        case 1:
                            {
                                Counters = Algorithm.LibrarySort(ref Table);
                                ODCounter = Counters[0];
                                MemoryCounter = Counters[1];
                                break;
                            }
                        case 2:
                            {
                                Counters = Algorithm.GnomeSort(ref Table);
                                ODCounter = Counters[0];
                                MemoryCounter = Counters[1];
                                break;
                            }
                        case 3:
                            {
                                Counters = Algorithm.InsertionSort(ref Table);
                                ODCounter = Counters[0];
                                MemoryCounter = Counters[1];
                                break;
                            }
                        case 4:
                            {
                                Counters = Algorithm.QuickSort<double>(ref Table, 0, i);
                                ODCounter = Counters[0];
                                MemoryCounter = Counters[1];
                                break;
                            }
                        default:
                            {
                                return;
                            }
                    }
                    LODTable[j] = ODCounter;
                    MCTable[j] = MemoryCounter;
                }
                ODSum = 0.0F;
                MemorySum = 0.0F;
                for (int l = 0; l < ReaserchSample; l++)
                {
                    ODSum = ODSum + LODTable[l];
                    MemorySum = MemorySum + MCTable[l];
                }
                ODAverage = ODSum / ReaserchSample;
                MeasurementResults[i] = ODAverage;
                MemoryAverage = MemorySum / ReaserchSample;
                MemoryResult[i] = MemoryAverage;

                switch (CheckChoosenAlgorithm())
                {
                    case 0:
                        {
                            AnalyticalResults[i] = Math.Pow(i - 1, 2);
                            break;
                        }
                    case 1:
                        {
                            if (i == 0)
                                AnalyticalResults[0] = 1;
                            else
                                AnalyticalResults[i] = i * (Math.Log(i, 2) + 1);
                            break;
                        }
                    case 2:
                        {
                            AnalyticalResults[i] = Math.Pow(i - 1, 2);
                            break;
                        }
                    case 3:
                        {
                            AnalyticalResults[i] = i * (i - 1) / 2;
                            break;
                        }
                    case 4:
                        {
                            if (i == 0)
                                AnalyticalResults[0] = 0;
                            else
                                AnalyticalResults[i] = i * (Math.Log(i, 2) + 1);
                            break;
                        }
                    default:
                        {
                            return;
                        }
                }
            }
            for (int i = 0; i < MaxArraySize; i++)
            {
                dataGridViewTable.Rows.Add();
                dataGridViewTable.Rows[i].Cells[0].Value = i + 1;
                dataGridViewTable.Rows[i].Cells[1].Value = String.Format("{0, 8:F3}", Table[i]);
                dataGridViewTable.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewTable.Rows[i].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                if ((i % 2) == 0)
                {
                    dataGridViewTable.Rows[i].Cells[0].Style.BackColor = Color.LightGray;
                    dataGridViewTable.Rows[i].Cells[1].Style.BackColor = Color.LightGray;
                }
                else
                {
                    dataGridViewTable.Rows[i].Cells[0].Style.BackColor = Color.White;
                    dataGridViewTable.Rows[i].Cells[1].Style.BackColor = Color.White;
                }
            }
            dataGridViewTable.Visible = true;
            buttonTable.Enabled = true;
            buttonChart.Enabled = true;
            dataGridViewVisualization.Visible = false;
            chartVisualization.Visible = false;
            buttonDemo.Enabled = true;
            pictureBoxDemo.Visible = false;
        }

        private int CheckChoosenAlgorithm()
        {
            var ChoosenAlgorithm = groupBoxChooseAlgorithm.Controls.OfType<RadioButton>().FirstOrDefault(n => n.Checked);
            switch (ChoosenAlgorithm.Name)
            {
                case "radioButtonBubbleSort":
                    {
                        return 0;
                    }
                case "radioButtonLibrarySort":
                    {
                        return 1;
                    }
                case "radioButtonGnomeSort":
                    {
                        return 2;
                    }
                case "radioButtonInstertionSort":
                    {
                        return 3;
                    }
                case "radioButtonQuickSort":
                    {
                        return 4;
                    }
                default:
                    {
                        return 0;
                    }
            }
        }

        private void textBoxMin_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(textBoxMin.Text, out ReaserchSample))
                errorProvider1.SetError(textBoxMin, "ERROR! Wystąpił niedozwolony znak, podaj wartosc ponownie");
            else
                errorProvider1.Dispose();
        }

        private void textBoxMax_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(textBoxMax.Text, out MaxArraySize))
                errorProvider1.SetError(textBoxMax, "ERROR! Wystąpił niedozwolony znak, podaj wartosc ponownie");
            else
                errorProvider1.Dispose();
        }

        private void textBoxBottom_TextChanged(object sender, EventArgs e)
        {
            if (!double.TryParse(textBoxBottom.Text, out LowerValueLimit))
                errorProvider1.SetError(textBoxBottom, "ERROR! Wystąpił niedozwolony znak, podaj wartosc ponownie");
            else
                errorProvider1.Dispose();
        }

        private void textBoxTop_TextChanged(object sender, EventArgs e)
        {
            if (!double.TryParse(textBoxTop.Text, out UpperValueLimit))
                errorProvider1.SetError(textBoxTop, "ERROR! Wystąpił niedozwolony znak, podaj wartosc ponownie");
            else
                errorProvider1.Dispose();
        }

        private void buttonTable_Click(object sender, EventArgs e)
        {
            dataGridViewVisualization.Visible = true;
            dataGridViewTable.Visible = false;
            pictureBoxDemo.Visible = false;
            groupBoxLineThickness.Enabled = false;
            groupBoxGraphics.Enabled = false;
            chartVisualization.Visible = false;
            for (int i = 0; i < MaxArraySize; i++)
            {
                dataGridViewVisualization.Rows.Add();
                dataGridViewVisualization.Rows[i].Cells[0].Value = i + 1;
                dataGridViewVisualization.Rows[i].Cells[1].Value = String.Format("{0:F2}", MeasurementResults[i]);
                dataGridViewVisualization.Rows[i].Cells[2].Value = String.Format("{0:F2}", AnalyticalResults[i]);
                dataGridViewVisualization.Rows[i].Cells[3].Value = MemoryResult[i];
                dataGridViewVisualization.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewVisualization.Rows[i].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewVisualization.Rows[i].Cells[2].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewVisualization.Rows[i].Cells[3].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                if ((i % 2) == 0)
                {
                    dataGridViewVisualization.Rows[i].Cells[0].Style.BackColor = Color.LightGray;
                    dataGridViewVisualization.Rows[i].Cells[1].Style.BackColor = Color.LightGray;
                    dataGridViewVisualization.Rows[i].Cells[2].Style.BackColor = Color.LightGray;
                    dataGridViewVisualization.Rows[i].Cells[3].Style.BackColor = Color.LightGray;
                }
                else
                {
                    dataGridViewVisualization.Rows[i].Cells[0].Style.BackColor = Color.White;
                    dataGridViewVisualization.Rows[i].Cells[1].Style.BackColor = Color.White;
                    dataGridViewVisualization.Rows[i].Cells[2].Style.BackColor = Color.White;
                    dataGridViewVisualization.Rows[i].Cells[3].Style.BackColor = Color.White;
                }

            }
        }

        private void buttonLined_Click(object sender, EventArgs e)
        {
            chartVisualization.Series[0].BorderDashStyle =
                    System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartVisualization.Series[1].BorderDashStyle =
                   System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartVisualization.Series[2].BorderDashStyle =
                   System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
        }

        private void buttonDotted_Click(object sender, EventArgs e)
        {
            chartVisualization.Series[0].BorderDashStyle =
                   System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartVisualization.Series[1].BorderDashStyle =
                   System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartVisualization.Series[2].BorderDashStyle =
                   System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
        }

        private void uttonLineColor_Click(object sender, EventArgs e)
        {
            ColorDialog ColorPalette = new ColorDialog();
            ColorPalette.Color = chartVisualization.Series[0].Color;
            if (ColorPalette.ShowDialog() == DialogResult.OK)
            {
                chartVisualization.Series[0].Color = ColorPalette.Color;
                chartVisualization.Series[1].Color = ColorPalette.Color;
                chartVisualization.Series[2].Color = ColorPalette.Color;

            }
            textBoxChartColor.BackColor = chartVisualization.Series[0].Color;
        }

        private void buttonChartColor_Click(object sender, EventArgs e)
        {
            ColorDialog ColorPalette = new ColorDialog();
            ColorPalette.Color = chartVisualization.BackColor;
            if (ColorPalette.ShowDialog() == DialogResult.OK)
                chartVisualization.BackColor = ColorPalette.Color;
            textBoxLineColor.BackColor = chartVisualization.BackColor;
        }

        private void trackBarLineThickness_Scroll(object sender, EventArgs e)
        {
            chartVisualization.Series[0].BorderWidth = trackBarLineThickness.Value;
            chartVisualization.Series[1].BorderWidth = trackBarLineThickness.Value;
            chartVisualization.Series[2].BorderWidth = trackBarLineThickness.Value;
            comboboxLineThickness.SelectedIndex = trackBarLineThickness.Value;
        }

        private void comboboxLineThickness_SelectedIndexChanged(object sender, EventArgs e)
        {
            chartVisualization.Series[0].BorderWidth = comboboxLineThickness.SelectedIndex + 1;
            chartVisualization.Series[1].BorderWidth = comboboxLineThickness.SelectedIndex + 1;
            chartVisualization.Series[2].BorderWidth = comboboxLineThickness.SelectedIndex + 1;
        }

        private void buttonDemo_Click(object sender, EventArgs e)
        {
            pictureBoxDemo.Visible = true;
            chartVisualization.Visible = false;
            dataGridViewTable.Visible = false;
            dataGridViewVisualization.Visible = false;
            switch (CheckChoosenAlgorithm())
            {
                case 0:
                    {
                        pictureBoxDemo.Image = Image.FromFile(@"Gifs\Bubble.gif");
                        break;
                    }
                case 1:
                    {
                        pictureBoxDemo.Image = Image.FromFile(@"Gifs\Library.gif");
                        break;
                    }
                case 2:
                    {
                        pictureBoxDemo.Image = Image.FromFile(@"Gifs\Gnome.gif");
                        break;
                    }
                case 3:
                    {
                        pictureBoxDemo.Image = Image.FromFile(@"Gifs\Insertion.gif");
                        break;
                    }
                case 4:
                    {
                        pictureBoxDemo.Image = Image.FromFile(@"Gifs\Quick.gif");
                        break;
                    }
                default:
                    {
                        return;
                    }
            }
        }

        private void Analizator_algorytmow_Load(object sender, EventArgs e)
        {
            panelTitle.Location = new Point(this.Left, this.Top);
            pictureBoxIcon.Location = new Point(panelTitle.Location.X + Margines / 2, panelTitle.Top + Margines / 3);
            labelTitle.Location = new Point(pictureBoxIcon.Location.X + pictureBoxIcon.Width + Margines,
                pictureBoxIcon.Location.Y + Margines / 2);
            buttonExit.Location = new Point(this.Right - buttonExit.Width - Margines, panelTitle.Location.Y);
            buttonReset.Location = new Point(buttonExit.Location.X - buttonReset.Width * 2, labelTitle.Location.Y);
            groupBoxChooseAlgorithm.Location = new Point(panelTitle.Location.X + Margines,
                panelTitle.Location.Y + panelTitle.Height + Margines / 2);
            labelMin.Location = new Point(groupBoxChooseAlgorithm.Location.X, groupBoxChooseAlgorithm.Location.Y +
                groupBoxChooseAlgorithm.Height + Margines / 2);
            textBoxMin.Location = new Point(labelMin.Location.X, labelMin.Location.Y + labelMin.Height + Margines / 2);
            labelMax.Location = new Point(textBoxMin.Location.X, textBoxMin.Location.Y + textBoxMin.Height + Margines);
            textBoxMax.Location = new Point(labelMax.Location.X, labelMax.Location.Y + labelMax.Height + Margines / 2);
            labelBottom.Location = new Point(textBoxMax.Location.X, textBoxMax.Location.Y + textBoxMax.Height + Margines);
            textBoxBottom.Location = new Point(labelBottom.Location.X, labelBottom.Location.Y + labelBottom.Height + Margines / 2);
            labelTop.Location = new Point(textBoxBottom.Location.X, textBoxBottom.Location.Y + textBoxBottom.Height + Margines);
            textBoxTop.Location = new Point(labelTop.Location.X, labelTop.Location.Y + labelTop.Height + Margines / 2);
            groupBoxGraphics.Location = new Point(buttonReset.Location.X, panelTitle.Location.Y + panelTitle.Height + Margines / 2);
            groupBoxLineThickness.Location = new Point(groupBoxChooseAlgorithm.Location.X + groupBoxChooseAlgorithm.Width + Margines,
                groupBoxChooseAlgorithm.Location.Y);
            chartVisualization.Location = new Point(groupBoxLineThickness.Location.X,
                groupBoxLineThickness.Location.Y + groupBoxLineThickness.Height + Margines / 2);
            dataGridViewTable.Location = new Point(chartVisualization.Location.X, chartVisualization.Location.Y);
            buttonTable.Location = new Point(dataGridViewTable.Location.X, dataGridViewTable.Location.Y + dataGridViewTable.Height + Margines);
            buttonChart.Location = new Point(buttonTable.Location.X + buttonTable.Width + Margines / 2, buttonTable.Location.Y);
            buttonAfterSort.Location = new Point(buttonChart.Location.X + buttonChart.Width + Margines / 2, buttonChart.Location.Y);
            buttonAccept.Location = new Point(textBoxTop.Location.X, textBoxTop.Location.Y + textBoxTop.Height + Margines / 2);
            dataGridViewVisualization.Location = new Point(dataGridViewTable.Location.X, dataGridViewTable.Location.Y);
            buttonDemo.Location = new Point(buttonAfterSort.Location.X + buttonAfterSort.Width + Margines / 2, buttonAfterSort.Location.Y);
            pictureBoxDemo.Location = new Point(chartVisualization.Location.X + dataGridViewTable.Width / 2,
                chartVisualization.Location.Y + Margines);
        }

       
    }
}
