using FTN.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
using System.Xml;
using TelventDMS.Services.NetworkModelService.TestClient.Tests;

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TestGda test = new TestGda();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GetByGID_Click(object sender, RoutedEventArgs e)
        { 
            test.GetValues(Int64.Parse(((GID.Text[1] == 'x') ? GID.Text.Substring(2) : GID.Text), System.Globalization.NumberStyles.HexNumber));
            StreamReader reader = new StreamReader(Config.Instance.ResultDirecotry + "\\GetValues_Results.xml");
            string result = reader.ReadToEnd();
            Results.Text = result;
        }

        private void GetAllByModelCode_Click(object sender, RoutedEventArgs e)
        {
            test.GetExtentValues((ModelCode)Int64.Parse(((ModelCode.Text[1] == 'x') ? ModelCode.Text.Substring(2) : ModelCode.Text), System.Globalization.NumberStyles.HexNumber));
            StreamReader reader = new StreamReader(Config.Instance.ResultDirecotry + "\\GetExtentValues_Results.xml");
            string result = reader.ReadToEnd();
            Results.Text = result;
        }

        private void GetBiggestSynchControls_Click(object sender, RoutedEventArgs e)
        {
            test.GetExtentValues((ModelCode)Int64.Parse(((ModelCode.Text[1] == 'x') ? ModelCode.Text.Substring(2) : ModelCode.Text), System.Globalization.NumberStyles.HexNumber));
            StreamReader reader = new StreamReader(Config.Instance.ResultDirecotry + "\\GetExtentValues_Results.xml");
            string result = reader.ReadToEnd();
            Results.Text = result;
        }

        private void GetBiggestSynchControls_Click_1(object sender, RoutedEventArgs e)
        {
            long max = test.GetExtentValues(FTN.Common.ModelCode.SYNCHRONOUSMACHINE).Max();
            test.GetRelatedValues(max, new Association(FTN.Common.ModelCode.REGULATINGCONDEQ_CONTROLS));
            StreamReader reader = new StreamReader(Config.Instance.ResultDirecotry + "\\GetRelatedValues_Results.xml");
            string result = reader.ReadToEnd();
            Results.Text = result;
        }
    }
}
