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
    /// Interaction logic for AddPatientVisit.xaml
    /// </summary>
    public partial class AddPatientVisit : Window
    {

        public AddPatientVisit(PatientVisit patientVisit)
        {
            InitializeComponent();
            UpdateDataOnUI(patientVisit);
            _btnSave.IsEnabled = false;
        }
            
        private void UpdateDataOnUI(PatientVisit patientVisit)
        {
            _age.Text = patientVisit.Age.ToString();
            _dateOfVisit.Text = patientVisit.VisitDate.ToString();
            _details.Text = patientVisit.Details;
            _gender.SelectedIndex = GetGender(patientVisit.Gender);
            _patientName.Text = patientVisit.Name;
            _reasonForVisit.Text = patientVisit.ReasonForVisit;
        }

        public AddPatientVisit()
        {
            InitializeComponent();
            _btnSave.IsEnabled = true;
            _dateOfVisit.SelectedDate = DateTime.Now;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var dateOfVisit = _dateOfVisit.SelectedDate;
            var patientName = _patientName.Text;
            if (dateOfVisit == null || string.IsNullOrEmpty(patientName))
            {
                MessageBox.Show("Details missing","Missing Data",MessageBoxButton.OK,MessageBoxImage.Error);
                return;
            }
            if (int.TryParse(_age.Text, out var age)) {
                var patientVisit = new PatientVisit() { 
                    Age = age, Gender=GetGender(_gender),Details=_details.Text,
                    Name= patientName,
                    ReasonForVisit=_reasonForVisit.Text,
                    VisitDate= Convert.ToDateTime(dateOfVisit)
                };
                UpdateFile(patientVisit);
                this.Close();
            }
        }

        private void UpdateFile(PatientVisit patientVisit)
        {
            var filePath = ".\\PatientVisit.json";
            var exisitngContent = new List<PatientVisit>();
            if (File.Exists(filePath))
            {
                exisitngContent = JsonConvert.DeserializeObject<List<PatientVisit>>(File.ReadAllText(filePath));
            }
            if (exisitngContent == null || !exisitngContent.Any())
                exisitngContent = new List<PatientVisit> { patientVisit };
            else
                exisitngContent.Add(patientVisit);


            var txtToWrite = JsonConvert.SerializeObject(exisitngContent);
            File.WriteAllText(filePath, txtToWrite);
        }

        private char GetGender(ComboBox gender)
        {
            switch (gender.Text)
            {
                case "Male":
                    return 'M';
                case "Female":
                    return 'F';
                default:
                    return 'N';
            }
        }
        private int GetGender(Char gender)
        {
            switch (gender)
            {
                case 'M':
                    return 0;
                case 'F':
                    return 1;
                default:
                    return 2;
            }
        }
    }
}
