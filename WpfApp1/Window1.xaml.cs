using Newtonsoft.Json;
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
using System.Windows.Shapes;
using WpfApp1.DTO;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        List<PatientVisit> patientVisitList = new List<PatientVisit>();

        public Window1()
        {
            InitializeComponent();
        }

        public void LoadScreen(string userName)
        {
            lbl_LoggedInUser.Content = userName;

            LoadContent();
            this.Show();
        }

        private void LoadContent()
        {
            var filePath = ".\\PatientVisit.json";
            var exisitngContent = new List<PatientVisit>();
            if (File.Exists(filePath))
            {
                exisitngContent = JsonConvert.DeserializeObject<List<PatientVisit>>(File.ReadAllText(filePath));
            }
            patientVisitList = exisitngContent;
            _dg.ItemsSource = exisitngContent;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var test = new AddPatientVisit();
            test.Show();
            LoadContent();
        }

        private void MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedPatient = patientVisitList[(sender as DataGrid).SelectedIndex];
            var window = new AddPatientVisit(selectedPatient);
            window.ShowDialog();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var text = ((TextBox)e.Source).Text;
            var updatedList = patientVisitList.Where(x => x.Name.Contains(text, StringComparison.InvariantCultureIgnoreCase) || x.ReasonForVisit.Contains(text, StringComparison.InvariantCultureIgnoreCase)).ToList();
            _dg.ItemsSource = updatedList;
        }
    }
}
