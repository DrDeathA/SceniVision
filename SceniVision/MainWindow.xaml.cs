using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace SceniVision
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string outPut = "0";
        string outPutNum = "0";
        // operator is a reserved keyword
        string displayOpratpor = "";
        string legacyOpratpor = "";
        string memory = "0";
        double temp = 0;
        bool preserve = false;
        StringBuilder historyEntry = new StringBuilder();
        List<string> history = new List<string>();
        public MainWindow()
        {
            InitializeComponent();
            txbOutPut.Text = outPut;
        }

        private void NumBtn_Click(object sender, RoutedEventArgs e)
        {
            string value = ((Button)sender).Content.ToString();

            if (outPut == "0")
            {
                outPut = "";
                outPutNum = "";
            }

            switch (value)
            {
                case "0":
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                    AppendOutPut(value);
                    outPutNum += value;
                    break;
                case ",":
                    if (outPut == "")
                    {
                        AppendOutPut("0,");
                        outPutNum = "0,";
                    }
                    else if (outPutNum.Contains(","))
                    {
                        break;
                    }
                    else
                    {
                        AppendOutPut(value);
                        outPutNum += value;
                    }
                    break;
                default:
                    break;
            }

        }

        private void OpperatorBtn_Click(object sender, RoutedEventArgs e)
        {
            string value = ((Button)sender).Content.ToString();
            OpperatorHandler(value);
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            outPut = "0";
            outPutNum = "0";
            temp = 0;
            memory = "0";
            legacyOpratpor = "";
            displayOpratpor = "";
            txbOprator.Text = displayOpratpor;
            txbTemp.Text = "";
            txbOutPut.Text = outPut;
            txbMemory.Text = "";
            history = new List<string>();
            historyEntry.Clear();
        }

        private void btnEquals_Click(object sender, RoutedEventArgs e)
        {
            if (outPut == "0")
            {
                return;
            }
            temp = Calculate();
            historyEntry.Append("= " + temp.ToString() + "\t");
            history.Add(historyEntry.ToString());
            txbTemp.Text = "";
            outPutNum = temp.ToString();
            outPut = temp.ToString();
            txbOutPut.Text = outPut;
            legacyOpratpor = "";
            displayOpratpor = "";
            preserve = true;
            historyEntry.Clear();
            txbOprator.Text = displayOpratpor;
        }

        private void btnBackSpace_Click(object sender, RoutedEventArgs e)
        {
            if (outPut.Length > 1)
            {
                outPut = outPut.Remove(GetOutPutLastIndex());
            }
            else
            {
                outPut = "0";
            }

            outPutNum = outPut;
            txbOutPut.Text = outPut;
        }

        private void btnMemAdd_Click(object sender, RoutedEventArgs e)
        {
            temp += (double.Parse(outPutNum) + double.Parse(memory));
            txbTemp.Text = temp.ToString();
            outPut = "0";
            outPutNum = "0";
            txbOutPut.Text = outPut;
        }

        private void btnMemSub_Click(object sender, RoutedEventArgs e)
        {
            temp -= (double.Parse(outPutNum) - double.Parse(memory));
            txbTemp.Text = temp.ToString();
            outPut = "0";
            outPutNum = "0";
            txbOutPut.Text = outPut;
        }

        private void btnMemAssign_Click(object sender, RoutedEventArgs e)
        {
            if (memory == "0")
            {
                memory = outPut;
                outPut = "0";
                outPutNum = "0";
                txbOutPut.Text = outPut;
                txbMemory.Text = "M";
            }
            else
            {
                outPut = memory;
                outPutNum = memory;
                txbOutPut.Text = outPut;
            }
            
        }

        private void btnSquare_Click(object sender, RoutedEventArgs e)
        {
            if (legacyOpratpor == "")
            {
                legacyOpratpor = "+";
            }
            historyEntry.Append($" {temp.ToString()} {legacyOpratpor} {outPutNum}² (");
            outPutNum = (double.Parse(outPutNum) * double.Parse(outPutNum)).ToString();
            historyEntry.Append($"{outPutNum})");
            temp = Calculate(false);
            historyEntry.Append($"= {temp.ToString()}");
            history.Add(historyEntry.ToString());
            historyEntry.Clear();
            txbTemp.Text = temp.ToString();
            outPut = "0";
            outPutNum = "0";
            txbOutPut.Text = outPut;
        }

        private void btnSquareRoot_Click(object sender, RoutedEventArgs e)
        {
            if (legacyOpratpor == "")
            {
                legacyOpratpor = "+";
            }
            historyEntry.Append($" {temp.ToString()} {legacyOpratpor} √{outPutNum} (");
            outPutNum = Math.Sqrt(double.Parse(outPutNum)).ToString();
            historyEntry.Append($"{outPutNum})");
            temp = Calculate(false);
            historyEntry.Append($"= {temp.ToString()}");
            history.Add(historyEntry.ToString());
            historyEntry.Clear();
            txbTemp.Text = temp.ToString();
            outPut = "0";
            outPutNum = "0";
            txbOutPut.Text = outPut;
        }

        private void btnFraction_Click(object sender, RoutedEventArgs e)
        {
            if (legacyOpratpor == "")
            {
                legacyOpratpor = "+";
            }
            historyEntry.Append($" {temp.ToString()} {legacyOpratpor} 1/{outPutNum} (");
            outPutNum = (1/double.Parse(outPutNum)).ToString();
            historyEntry.Append($"{outPutNum})");
            temp = Calculate(false);
            historyEntry.Append($"= {temp.ToString()}");
            history.Add(historyEntry.ToString());
            txbTemp.Text = temp.ToString();
            outPut = "0";
            outPutNum = "0";
            txbOutPut.Text = outPut;
        }

        private void btnPercentage_Click(object sender, RoutedEventArgs e)
        {
            if (legacyOpratpor == "")
            {
                legacyOpratpor = "+";
            }
            historyEntry.Append($" {temp.ToString()} {legacyOpratpor} {outPutNum}% (");
            outPutNum = (temp * ( double.Parse(outPutNum)/ 100)).ToString();
            historyEntry.Append($"{outPutNum})");
            temp = Calculate(false);
            historyEntry.Append($"= {temp.ToString()}");
            history.Add(historyEntry.ToString());
            txbTemp.Text = temp.ToString();
            outPut = "0";
            outPutNum = "0";
            txbOutPut.Text = outPut;
        }

        private void btnHistory_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder message = new StringBuilder();

            foreach (var item in history)
            {
                message.Append(item + "\n\n");
            }

            MessageBox.Show(message.ToString());
        }

        private void AppendOutPut(string value)
        {
            if (outPut.Length < 14)
            {
                outPut += value;
                txbOutPut.Text = outPut;
            }
            else
            {
                MessageBox.Show("Max length Calculator can display reached");
                return;
            }
        }

        private void OpperatorHandler(string oprator)
        {
            if (legacyOpratpor == "") 
            { 
                legacyOpratpor = oprator;
            }

            if (temp != 0 && outPut == "0")
            {
                legacyOpratpor = oprator;
                displayOpratpor = oprator;
                txbOprator.Text = displayOpratpor;
                return;
            }

            displayOpratpor = oprator;
            txbOprator.Text = displayOpratpor;

            if (preserve)
            {
                temp = double.Parse(outPutNum);
                txbTemp.Text = temp.ToString();
                outPut = "0";
                outPutNum = "0";
                txbOutPut.Text = outPut;
                preserve = false;
                return;
            }

            if (temp == 0)
            {
                legacyOpratpor = oprator;
                temp = double.Parse(outPutNum);
                txbTemp.Text = temp.ToString();
                outPut = "0";
                outPutNum = "0";
                txbOutPut.Text = outPut;
                return;
            }

            if (temp == 0 && legacyOpratpor == "")
            {
                temp = double.Parse(outPutNum);
            }
            else
            {
                temp = Calculate();
                outPutNum = "0";
                historyEntry.Append("= " + temp.ToString());
            }
            if (temp.ToString() == "NaN")
            {
                txbTemp.Text = "0";
                return;
            }
            txbTemp.Text = temp.ToString();
            legacyOpratpor = oprator;
            ClrearOutPut();
        }

        private int GetOutPutLastIndex()
        {
            return (outPut.Length == 0) ? 0 : (outPut.Length - 1);
        }

        private void ClrearOutPut()
        {
            outPut = "0";
            txbOutPut.Text = outPut;
        }

        private double Calculate(bool log = true)
        {
            if (legacyOpratpor == "")
            {
                legacyOpratpor = "+";
            }

            if (log)
            {
                if (historyEntry.Length == 0)
                {
                    historyEntry.Append(temp.ToString());
                }

                historyEntry.Append(" " + legacyOpratpor + " " + outPutNum + " ");
            }

            switch (legacyOpratpor)
            {
                case "+":
                    return temp + double.Parse(outPutNum);
                case "-":
                    return temp - double.Parse(outPutNum);
                case "/":
                case "÷":
                    return temp / double.Parse(outPutNum);
                case "x":
                case "*":
                    return (temp * double.Parse(outPutNum));
                default:
                    return 0;
            }
        }
    }
}
