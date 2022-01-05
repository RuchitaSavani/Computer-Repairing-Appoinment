using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml.Serialization;
using FinalProject;

namespace FinalWpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    enum DeviceIssues{
        Hardware_Issue,
        Software_Issue,
        Screen_Issue,
        Other_Issue
    }
    public partial class MainWindow : Window
    {
      
        // For available appoinments list
        private AppoinmentList appList = null;

        /// Name of saved XML file
        public string fileName = "AppoinmentList.xml";
        private string sampleString = null;
       
        public string SampleString { get => sampleString; set => sampleString = value; }

        public MainWindow()
        {
            InitializeComponent();
            SetTimeSlots();
            InitDeviceIssue();
            appList = new AppoinmentList();
            DataContext = this;
            
        }
       

        /// Set available time slots in time combobox.
        public void SetTimeSlots()
        {
         timeSlotBookingComboBox.Items.Clear();
            for (int i = 10; i < 20; i++)
            {
                timeSlotBookingComboBox.Items.Add(TimeInHourFormat(i));
            }
            timeSlotBookingComboBox.SelectedIndex = 0;

        }

        /// Set customer issue in Issue Combobox
        void InitDeviceIssue()
        {
            var vehicles = Enum.GetValues(typeof(DeviceIssues));
            foreach (var val in vehicles)
            {
                issueComboBox.Items.Add(val);
            }
        }

        /// return time in AM/PM format
        public string TimeInHourFormat(int hours)
        {
            var tFormat = "AM";
            if (hours == 0)
            {
                hours = 12;
            }
           
            else if (hours > 12)
            {
                hours -= 12;
                tFormat = "PM";
            }
            else if (hours == 12)
            {
                tFormat = "PM";
            }
            var timeIn24 = string.Format("{0}:{1:00} {2}", hours, 00, tFormat);
            return timeIn24;
        }

        /// <summary>
        /// For Book Appointmet
        /// </summary>
        
        private void bookAppoinmentOnClick(object sender, RoutedEventArgs e)
        {
            Laptop laptop = null;
            if (CheckValidation())
            {
                string warrenty = null;
                string selectedTimeSlot = timeSlotBookingComboBox.SelectedItem.ToString();
                string selectedIssue = issueComboBox.SelectedItem.ToString();
                string fullName = nameTextBox.Text;
                string modelNumber = modelNumberTextBox.Text;
                string itemDescription = briefTextBlock.Text;
                string phoneNumber = phoneTextBox.Text;
                if (rbYes.IsChecked == true)
                {
                     warrenty = rbYes.Content.ToString();
                }
                else if(rbNo.IsChecked == true)
                {
                    warrenty = rbNo.Content.ToString();
                }
                     
                Appointment appoinment = new Appointment();
                appoinment.Time = selectedTimeSlot;
                appoinment.Warrenty = warrenty;
                switch (issueComboBox.SelectedValue)
                {
                    case DeviceIssues.Hardware_Issue:
                        laptop = new HardwareIssue(fullName, modelNumber, itemDescription,phoneNumber, "Issue in hardware");
                        
                        break;

                    case DeviceIssues.Software_Issue:
                        laptop = new SoftwareIssue(fullName, modelNumber, itemDescription, phoneNumber, "Issue in software");
                         break;

                    case DeviceIssues.Screen_Issue:
                        laptop = new ScreenIssue(fullName, modelNumber, itemDescription, phoneNumber, "Issue in screen");
                        break;

                    case DeviceIssues.Other_Issue:
                        laptop = new OtherIssue(fullName, modelNumber, itemDescription, phoneNumber , "Other Issues");
                        break;

                }
                appoinment.Laptop = laptop; 
                appList.Add(appoinment);


                timeSlotBookingComboBox.Items.Remove(timeSlotBookingComboBox.SelectedItem);
                if (timeSlotBookingComboBox.Items.Count == 0)
                {

                    MessageBox.Show("There are no slots available!");
                }
                else
                {
                    timeSlotBookingComboBox.SelectedIndex = 0;
                }
                StoreDataInGrid(appList);
                ResetForm();
            }
        
        }

        /// <summary>
        /// Store user inputed data in xml along with data grid
        /// </summary>
        private void StoreDataInGrid(AppoinmentList appList)
        {
            XmlSerializer serializer = null;
            TextWriter writer = null;
            try
            {
                
                serializer = new XmlSerializer(typeof(AppoinmentList));
                writer = new StreamWriter(fileName);
                serializer.Serialize(writer, appList);
                writer.Close();
                 customerGrid.ItemsSource = appList.List;
                MessageBox.Show("Data saved successfully");
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
                if (writer != null)
                {
                    writer.Close();
                }
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }
        }

        /// <summary>
        /// reset appointment form after appointment booked successfully
        /// </summary>
        private void ResetForm()
        {
            nameTextBox.Text = string.Empty;
            modelNumberTextBox.Text = string.Empty;
            briefTextBlock.Text = string.Empty;
            issueComboBox.SelectedIndex = -1;
            
            phoneTextBox.Text = string.Empty;
            MessageShow.Content = string.Empty;
        }

        /// <summary>
        /// check validation for all user input fields
        /// </summary>
         
        public bool CheckValidation()
        {
            bool check = true;
            // Time Booking Validation
            if (timeSlotBookingComboBox.Text != "")
            {
                TimeSlotError.Visibility = Visibility.Hidden;
                TimeSlotError.Foreground = Brushes.White;
            }
            else
            {
                check = false;
                TimeSlotError.Visibility = Visibility.Visible;
                TimeSlotError.Foreground = Brushes.Red;
            }
            // Issue Selection Valiation
            if (issueComboBox.Text != "")
            {
                selectReason.Visibility = Visibility.Hidden;
                selectReason.Foreground = Brushes.White;
            }
            else
            {
                check = false;
                selectReason.Visibility = Visibility.Visible;
                selectReason.Foreground = Brushes.Red;
            }

            //Customer Name Validation
            if (nameTextBox.Text == "" || !ValidString(nameTextBox.Text))
            {
                               check = false;
                enterName.Foreground = Brushes.Red;
                enterName.Visibility = Visibility.Visible;

            }
            else
            {
                        
                enterName.Visibility = Visibility.Hidden;
                enterName.Foreground = Brushes.White;
            }

            //mobile number Validation
            
            if (phoneTextBox.Text == "" || phoneTextBox.Text.Length != 10 )
            {

                check = false;
                phoneNumber.Visibility = Visibility.Visible;
                phoneNumber.Foreground = Brushes.Red;
            }
            else
            {
                phoneNumber.Visibility = Visibility.Hidden;
                phoneNumber.Foreground = Brushes.White;
            }

            //model number validation


            if (modelNumberTextBox.Text == "" || !ValidModelNo(modelNumberTextBox.Text))
            {
                check = false;
                computerModel.Visibility = Visibility.Visible;
                computerModel.Foreground = Brushes.Red;
            }
            else
            {
                computerModel.Visibility = Visibility.Hidden;
                computerModel.Foreground = Brushes.White;
            }

            // Message Box Validation
            if (briefTextBlock.Text == "" || !ValidDescrpition(briefTextBlock.Text))
            {
                check = false;
                message.Visibility = Visibility.Visible;
                message.Foreground = Brushes.Red;

            }
            else
            {
                message.Visibility = Visibility.Hidden;
                message.Foreground = Brushes.White;
            }

            return check;
        }

        /// <summary>
        /// show all booked appoinmets with given fields
        /// </summary>
         
        private void showAppointmentOnClick(object sender, RoutedEventArgs e)
        {

            GetAppointmentData();

        }

        /// <summary>
        /// get data from XML
        /// </summary>
        public void GetAppointmentData()
        {
            XmlSerializer serializer = null;
            StreamReader reader = null;


            try
            {
                if (File.Exists(fileName))
                {

                    serializer = new XmlSerializer(typeof(AppoinmentList));
                    reader = new StreamReader(fileName);
                    appList = (AppoinmentList)serializer.Deserialize(reader);
                    customerGrid.ItemsSource = appList.List;

                    if (appList.Count <= 0)
                    {
                        MessageBox.Show("No data available");
                    }
                    for (int i = 0; i < appList.Count; i++)
                    {
                        timeSlotBookingComboBox.Items.Remove(appList[i].Time);
                        if (timeSlotBookingComboBox.Items.Count == 0)
                        {
                            appointmentButton.IsEnabled = false;
                        }
                        else
                        {
                            timeSlotBookingComboBox.SelectedIndex = 0;
                        }
                    }
                    MessageShow.Content = string.Empty;
                }
                else
                {
                    MessageBox.Show("No data available");
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }

            }
        }

        // validation for only character in customer name
        bool ValidString(String userString)
        {
            StringBuilder uString = new StringBuilder(userString);


            bool isValid = true;
            
            for (int i = 0; i < uString.Length; i++)
            {
                if (!char.IsLetter(uString[i]))
                {
                    isValid = false;
                }
            }


            return isValid;
        }


        /// <summary>
        /// It will search the data from the list by name of the customer.
        /// </summary>
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
             

                string modelText = searchBox.Text;
                if (modelText.Length > 0)
                {

                   var searchQuery = from appoinment in appList.List
                                     where appoinment.Laptop.FullName.ToString().ToLower().Contains(modelText.ToLower())
                                     select appoinment;
                    customerGrid.ItemsSource = searchQuery;
                }
                else
                {
                GetAppointmentData();
            }
            
           
        }

        // text change event for name of customer
        private void nameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (nameTextBox.Text == "" ||!ValidString(nameTextBox.Text) )
            {
                MessageShow.Content = string.Format(" Please Enter  First Name ( no space)");
                    
                nameTextBox.Foreground = Brushes.Red;
            }
            else
            {
                nameTextBox.Foreground = Brushes.Black;
                MessageShow.Content = string.Empty;
            }

        }

        // text change event for model number
        private void modelNumberTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (modelNumberTextBox.Text == "" || !ValidModelNo(modelNumberTextBox.Text))
            {
                MessageShow.Content = string.Format(" model no AA 123456 ");
                modelNumberTextBox.Foreground = Brushes.Red;
            }
            else
            {
                modelNumberTextBox.Foreground = Brushes.Black;
                MessageShow.Content = string.Empty;

            }

        }

        // text change event for issue description
        private void briefTextBlock_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (briefTextBlock.Text == "" || !ValidDescrpition(briefTextBlock.Text) )
            {
                MessageShow.Content = string.Format("Text descrption between 10 - 50 and char only");
                briefTextBlock.Foreground = Brushes.Red;
            }
            else
            {
                briefTextBlock.Foreground = Brushes.Black;
                MessageShow.Content = string.Empty;

            }
        }

        /// mobile number validation 
        public static bool IsVaidNumber(string number)
        {
            // return Regex.Match(number, @"^(\+[0-9]{9})$").Success;
            StringBuilder MobileNumber = new StringBuilder(number);
            if (number.Length != 10)
            {
                return false;
            }

            bool isValid = true;

            for (int i = 0; i < MobileNumber.Length ; i++)
            {
                if (!char.IsDigit(MobileNumber[i]))
                {
                    isValid = false;
                }
            }
            return isValid;
        }

        // validation for laptop model number
        bool ValidModelNo(string PCString)
        {
            StringBuilder PCModelNumber = new StringBuilder(PCString);
            if (PCString.Length != 9)
            {
                return false;
            }

            bool isValid = true;

            for (int i = 0; i < PCModelNumber.Length - 7; i++)
            {
                if (!char.IsUpper(PCModelNumber[i]))
                {
                    isValid = false;
                }
            }

            if (!char.IsWhiteSpace(PCModelNumber[2]))
            {
                isValid = false;
            }
            for (int i = 3; i < PCModelNumber.Length ; i++)
            {
                if (!char.IsDigit(PCModelNumber[i]))
                {
                    isValid = false;
                }
            }
            

            return isValid;
        }

        // validation for user issue in minimum and maximum range
        bool ValidDescrpition(string PCString)
        {
            StringBuilder PCModelNumber = new StringBuilder(PCString);
            if (PCString.Length < 10 ||  PCString.Length > 50)
            {
                return false;
            }

            bool isValid = true;
            

            for (int i = 0; i < PCModelNumber.Length ; i++)
            {
                if (!(char.IsLetter(PCModelNumber[i]) ||(char.IsWhiteSpace(PCModelNumber[i]))))
                {
                    isValid = false;
                }
            }

           
                
            

            return isValid;
        }

        // text change event for mobile number
        private void phoneTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (phoneTextBox.Text == "" || !IsVaidNumber(phoneTextBox.Text))
            {
                MessageShow.Content = string.Format(" Enter 10 digit number");
                phoneTextBox.Foreground = Brushes.Red;
            }
            else
            {
                phoneTextBox.Foreground = Brushes.Black;
                MessageShow.Content = string.Empty;

            }
        }
        void InitializeTime(bool data = true)
        {
            timeSlotBookingComboBox.Items.Clear();
            if (data)
            {
                for (int i = 10; i < 20; i++)
                {
                    timeSlotBookingComboBox.Items.Add(TimeInHourFormat(i));
                }
                timeSlotBookingComboBox.SelectedIndex = 0;
            }
            
        }
        

       
    }
   
}
