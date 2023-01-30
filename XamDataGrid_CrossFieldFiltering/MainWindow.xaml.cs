using Infragistics.Windows.Controls;
using Infragistics.Windows.DataPresenter;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace XamDataGrid_CrossFieldFiltering
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<TestData> testData = null;
        public MainWindow()
        {
            InitializeComponent();

            testData = new ObservableCollection<TestData>();

            for (int i = 0; i < 100; i++)
            {
                testData.Add(new TestData { Id = i, Test1 = i % 3, Test2 = "Item" + (i % 5) });
            }

            this.DataContext = testData;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //([Id]が='0' OR [Test1])が='1') AND [Test2]が='Item0'でフィルタリングする
            CrossFieldRecordFilterGroup fGroup1 = new CrossFieldRecordFilterGroup();
            fGroup1.LogicalOperator = LogicalOperator.Or;
            fGroup1.Filters.Add(
             new CrossFieldRecordFilter()
             {
                 FieldName = "Id",
                 Operator = ComparisonOperator.Equals,
                 Operand = "0"
             });
            fGroup1.Filters.Add(
              new CrossFieldRecordFilter()
              {
                  FieldName = "Test1",
                  Operator = ComparisonOperator.Equals,
                  Operand = "1"
              });
            CrossFieldRecordFilterGroup fGroup2 = new CrossFieldRecordFilterGroup();
            fGroup2.LogicalOperator = LogicalOperator.And;
            fGroup2.Filters.Add(fGroup1);
            fGroup2.Filters.Add(
              new CrossFieldRecordFilter()
              {
                  FieldName = "Test2",
                  Operator = ComparisonOperator.StartsWith,
                  Operand = "Item0"
              });
            xamDataGrid1.FieldLayouts[0].CrossFieldRecordFilters = fGroup2;
        }
    }

    public class TestData : INotifyPropertyChanged
    {
        private int m_id;
        public int Id
        {
            get { return m_id; }
            set
            {
                m_id = value;
                NotifyPropertyChanged("Id");
            }
        }

        private double m_test1;
        public double Test1
        {
            get { return m_test1; }
            set
            {
                m_test1 = value;
                NotifyPropertyChanged("Test1");
            }
        }

        private String m_test2;
        public String Test2
        {
            get { return m_test2; }
            set
            {
                m_test2 = value;
                NotifyPropertyChanged("Test2");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
