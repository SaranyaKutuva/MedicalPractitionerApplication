using System;
using System.Collections.Generic;
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
using System.Text.RegularExpressions;
//Add the following references
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private Int32 homeno,  mobileno,  mno;
        //private Int32 mrno,homeno1, mobileno1;
        private string selectedPatientNo;
        private int listViewSelectedItemIndex;
        private DataTable dtAppointment;
        private string pracMedicalReg;


        public MainWindow()
        {
            InitializeComponent();
        }


        #region Patient Methods


        #region Private Patient Methods

        private void AssignPropertiesToPatient(Patient newPatient)
        {

           // MessageBox.Show("assign");
            try
            {
                if (!Regex.IsMatch(Mobileno.Text, @"^\d+$"))
                {
                    MessageBox.Show("Please enter a valid Medicare Number!");
                    Mno.Clear();
                    Mno.Focus();
                    return;
                }
                else
                {
                    newPatient.Medicare_No = Mno.Text;
                }
                Regex r = new Regex("^[a-zA-Z\\s\\,]*$");
              
                if (String.IsNullOrWhiteSpace(fname.Text))
                {


                    MessageBox.Show("Patient First Name Can't be Empty", "Patient", MessageBoxButton.OK
                        , MessageBoxImage.Stop);
                    fname.Clear();
                    fname.Focus();
                    return;
                }
                else if (r.IsMatch(fname.Text))
                {
                    newPatient.FirstName = fname.Text;

                }
           
                
                else
                {
                    MessageBox.Show("Please enter valid First Name", "Patient", MessageBoxButton.OK
                        , MessageBoxImage.Stop);
                    fname.Clear();
                    fname.Focus();
                    return;
                }

                if (String.IsNullOrEmpty(lname.Text))
                {
                    MessageBox.Show("Patient Last Name Can't be Empty", "Patient", MessageBoxButton.OK
                        , MessageBoxImage.Stop);
                    lname.Clear();
                    lname.Focus();
                    return;
                }
                else if (r.IsMatch(lname.Text))
                {
                    newPatient.LastName = lname.Text;
                }
                else
                {
                    MessageBox.Show("Please enter valid Last Name", "Patient", MessageBoxButton.OK
                        , MessageBoxImage.Stop);
                    lname.Clear();
                    lname.Focus();
                    return;
                }
                
                if (String.IsNullOrEmpty(Homeno.Text) && String.IsNullOrEmpty(Mobileno.Text))
                {
                    MessageBox.Show("Please Enter One of the Phone Numbers", "Patient", MessageBoxButton.OK
                        , MessageBoxImage.Stop);
                    return;
                }
                else if (!Regex.IsMatch(Homeno.Text, @"^\d+$"))
                {
                    MessageBox.Show("Please enter  a valid Home Number!", "Patient", MessageBoxButton.OK
                        , MessageBoxImage.Stop);
                    Homeno1.Clear();
                    Homeno1.Focus();
                    return;
                }
                else if (!Regex.IsMatch(Mobileno.Text, @"^\d+$"))
                {
                    MessageBox.Show("Please enter  a valid Mobile Number!", "Patient", MessageBoxButton.OK
                        , MessageBoxImage.Stop);
                    Mobileno1.Clear();
                    Mobileno1.Focus();
                    return;
                }
                else
                {
                    newPatient.Homephone = Homeno.Text;
                    newPatient.Mobilephone = Mobileno.Text;
                }
            }


            catch (FormatException frmtExc)
            {
                Homeno.Text = "Please Enter only Numbers:" + frmtExc.Message;
                Mobileno.Text = "Please Enter only Numbers:" + frmtExc.Message;
                Mno.Text = "Please Enter only Numbers:" + frmtExc.Message;
                Homeno.Clear();
                Mobileno.Clear();
                Mno.Clear();
                fname.Text = "Please Enter only Letters" + frmtExc.Message;
                lname.Text = "Please Enter only Letters" + frmtExc.Message;
                fname.Clear();
                lname.Clear();
            }

            finally
            {
                // This section will execute regardless of an exception occuring or not!
                // This is a good place to clean up(usually close or dispose of) objects that have been left
                // open or hanging around in memory that should be disposed of.
            }
            if (txtPatient_id.Text.Length != 0)
            {
                newPatient.Patient_Id = int.Parse(txtPatient_id.Text);
            }
            //this.Patient_Id
            newPatient.FirstName = fname.Text;
            newPatient.LastName = lname.Text;
            //newPatient.Medicare_No = Mno.Text;
            newPatient.Gender = patient_gender.Text;
            newPatient.DateOfBirth = DateTime.ParseExact("31/12/4712", "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            if (newPatient.DateOfBirth != null)
            {
                newPatient.DateOfBirth = patient_date.SelectedDate.Value;
            }
            //newPatient.DateOfBirth = patient_date.SelectedDate.Value;
            newPatient.StreetAddress = patient_address.Text;
            newPatient.Suburb = patient_suburb.Text;
            newPatient.State = patient_state.Text;
            newPatient.PostCode = patient_postcode.Text;
            newPatient.Homephone = Homeno.Text;
            newPatient.Mobilephone = Mobileno.Text;
            newPatient.Notes = notes.Text;
            update.IsEnabled = true;
            delete.IsEnabled = true;
            Add.Content = "CREATE";


            //String msg =
            //                "Patient_Fname" + newPatient.FirstName +
            //                "Patient_Sname" + newPatient.LastName +
            //                "Patient_MedicareNo" + newPatient.Medicare_No +
            //                "Patient_Gender" + "Female" +
            //                "Patient_DateOfBirth" + newPatient.DateOfBirth +
            //                "Patient_Street_Address" + newPatient.StreetAddress +
            //                "Patient_Suburb" + newPatient.Suburb +
            //                "Patient_State" + newPatient.State +
            //                "Patient_PostCode" + newPatient.PostCode +
            //                "Patient_HomePhone" + newPatient.Homephone +
            //                "Patient_MobilePhone" + newPatient.Mobilephone +
            //                "Patient_Notes" + newPatient.Notes;
            //MessageBox.Show(msg);
        }
        private void LoadFieldNameCombo()
        {
            try
            {
                SqlHelper dal = new SqlHelper();
                //We want to call the stored proc that retrieves the names of the columns in the 
                //Pilot table. i.e. "usp_GetTableFieldName".  It expects one parameter "@TableName" which
                //will have the value "tblPilot".
                string SPName = "usp_GetTableFieldNames";
                SqlParameter[] parameters = { new SqlParameter("@TableName", "Patient") };

                DataTable dtFieldNames = dal.ExecuteStoredProc(SPName, parameters);

                //Map the database table field names (columns) to more human FRIENDLY names.
                //Define a new column object with the column name "FRIENDLY_NAMES"
                DataColumn colFriendlyNames = new DataColumn("FRIENDLY_NAMES", System.Type.GetType("System.String"));
                //Add our FRIENDLY_NAMES column to our DataTable.
                dtFieldNames.Columns.Add(colFriendlyNames);

                //  MessageBox.Show("Show data successful " + //"Patient");
                DataRow dtFieldNames1;
                dtFieldNames1 = dtFieldNames.NewRow();

                dtFieldNames1["FRIENDLY_NAMES"] = "--Select Field--";
                dtFieldNames1["COLUMN_NAME"] = "0";
                dtFieldNames.Rows.InsertAt(dtFieldNames1, 0);


                foreach (DataRow currentRow in dtFieldNames.Rows)
                {
                    switch (currentRow[0].ToString())
                    {
                        case "Patient_Id":
                            currentRow[1] = "Patient_Id";
                            break;
                        case "Patient_Fname":
                            currentRow[1] = "First Name";
                            break;
                        case "Patient_Lname":
                            currentRow[1] = "Surname";
                            break;
                        case "Patient_MedicareNo":
                            currentRow[1] = "Medicare No";
                            break;
                        case "Patient_Gender":
                            currentRow[1] = "Gender";
                            break;
                        case "Patient_DateOfBirth":
                            currentRow[1] = "Date Of Birth";
                            break;
                        case "Patient_Street_Address":
                            currentRow[1] = "Street";
                            break;
                        case "Patient_Suburb":
                            currentRow[1] = "Suburb";
                            break;
                        case "Patient_State":
                            currentRow[1] = "State";
                            break;
                        case "Patient_PostCode":
                            currentRow[1] = "Post Code";
                            break;
                        case "Patient_HomePhone":
                            currentRow[1] = "HomePhone";
                            break;
                        case "Patient_MobilePhone":
                            currentRow[1] = "MobilePhone";
                            break;
                        case "Patient_Notes":
                            currentRow[1] = "Patient_Notes";
                            break;
                        default:
                            break;
                    }
                }

                cboFieldNames.SelectedIndex = 0;
                cboFieldNames.SelectedValuePath = "COLUMN_NAME";
                cboFieldNames.DisplayMemberPath = "FRIENDLY_NAMES";
                cboFieldNames.ItemsSource = dtFieldNames.DefaultView;

            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured loading the search field names. " + ex.Message);

            }
        }
        private void ShowPatientData()
        {
            PatientCollection myPatients;

            try
            {
                if (cboFieldNames.SelectedValue != null && txtSearchString.Text.Length > 0)
                {

                    try
                    {
                        if (cboFieldNames.SelectedValue.ToString().Equals("Patient_DateOfBirth"))
                        {
                            DateTime ld;

                            ld = DateTime.ParseExact(txtSearchString.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        }
                    }
                    catch (Exception ex)
                    {
                        string ls = ex.Message;
                        MessageBox.Show("Date format is incorrect! Expected format is dd/mm/yyyy. ");
                        return;
                    }

                    if (cboFieldNames.SelectedValue.ToString().Equals("Patient_DateOfBirth") && txtSearchString.Text.Split('/').Count() > 2)
                    {
                        MessageBox.Show(cboFieldNames.SelectedValue.ToString());
                        String[] dob = txtSearchString.Text.Split('/');
                        String dobString = dob[2] + '-' + dob[1].PadLeft(2).Replace(' ', '0') + '-' + dob[0].PadLeft(2).Replace(' ', '0');
                        MessageBox.Show(dobString);
                        myPatients = new PatientCollection(cboFieldNames.SelectedValue.ToString(), dobString);
                        lvPatients.DataContext = myPatients;
                    }
                    else
                    {
                        //Search criteria HAVE been specified.  i.e. user has selected a field to search and provided a search string
                        myPatients = new PatientCollection(cboFieldNames.SelectedValue.ToString(), txtSearchString.Text);
                        lvPatients.DataContext = myPatients;
                    }
                }
                else
                {
                    //First time the window loads (or the user clicks the search button WITHOUT specifying search criteria).
                    myPatients = new PatientCollection(null, null);
                    lvPatients.DataContext = myPatients;
                }
                lvPatients.SelectedIndex = 0;
                lvPatients.ScrollIntoView(lvPatients.SelectedItem);
                lvPatients.Focus();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion Private Patient Methods

        #region Public Patient Methods
        private void Add_Click_1(object sender, RoutedEventArgs e)
        {

            if (Add.Content.ToString().Contains("CREATE"))
            {
                ClearPatientFields();
                Add.Content = "ADD";
                update.IsEnabled = false;
                delete.IsEnabled = false;
                MessageBox.Show("Click Add button after entering the new patient.","Patient",MessageBoxButton.OK,MessageBoxImage.Warning);

            }
            else
            {

                try
                {

                    //Create a new Pilot object
                    Patient newPatient = new Patient();
                    //Get the values from the user interface (textboxes etc.) and assign them to Patient object.
                    AssignPropertiesToPatient(newPatient);
                    //Call the AddPatient() of the Patient class.
                    newPatient.AddPatient();

                    //Clear out the form..
                    ClearPatientFields();
                    //Refresh the list of Patients
                    ShowPatientData();

                    //Select the new Pilot in the Refreshed list.
                    lvPatients.SelectedIndex = lvPatients.Items.Count - 1;
                    lvPatients.ScrollIntoView(lvPatients.SelectedItem);

                    //Give some hope to the user.
                    MessageBox.Show("Thank you!  The new Patient's details have been added to the Database.","Patient",MessageBoxButton.OK,MessageBoxImage.Information);
                    Add.Content = "CREATE";
                    update.IsEnabled = true;
                    delete.IsEnabled = true;

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Something went wrong, but we don't know what!  Patient was NOT added to the database. " + ex.Message);
                    //fname.Text = "Something went wrong!" + ex.Message;
                    //lname.Text = "Something went wrong!" + ex.Message;
                    //Mno.Text = "Something went wrong!" + ex.Message;

                }

                //catch (FormatException frmtExc)
                //{
                //    Homeno1.Text = "Please Enter only Numbers:" + frmtExc.Message;
                //    Mobileno1.Text = "Please Enter only Numbers:" + frmtExc.Message;
                //    MRNo.Text = "Please Enter only Numbers:" + frmtExc.Message;
                //    Homeno1.Clear();
                //    Mobileno1.Clear();
                //    MRNo.Clear();
                //    fname1.Text = "Please Enter only Letters" + frmtExc.Message;
                //    lname1.Text = "Please Enter only Letters" + frmtExc.Message;
                //    fname1.Clear();
                //    lname1.Clear();
                //}



                finally
                {
                    // This section will execute regardless of an exception occuring or not!
                    // This is a good place to clean up(usually close or dispose of) objects that have been left
                    // open or hanging around in memory that should be disposed of.
                }

                Patient myPatient = new Patient();
                //myPatient.Patient_Id = 1002;
                myPatient.FirstName = fname.Text;
                myPatient.LastName = lname.Text;
                myPatient.Medicare_No = Mno.Text;
                myPatient.Gender = patient_gender.SelectedValue.ToString();
                //myPatient.DateOfBirth = DateTime.ParseExact("31/12/4712", "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                //if (myPatient.DateOfBirth != null)
                //{
                myPatient.DateOfBirth = DateTime.Parse(patient_date.Text);
                //}

                //myPatient.DateOfBirth = patient_date.SelectedDate.Value;
                myPatient.StreetAddress = patient_address.Text;
                myPatient.Suburb = patient_suburb.Text;
                myPatient.State = patient_state.Text;
                myPatient.PostCode = patient_postcode.Text;
                myPatient.Homephone = Homeno.Text;
                myPatient.Mobilephone = Mobileno.Text;
                myPatient.Notes = notes.Text;
                //if (myPatient.AddPatient() == 1)
                //{
                //    MessageBox.Show("Yipeee!! New Patientt was inserted.");
                //}
                //else
                //{
                //    MessageBox.Show("DOH!!!  Something went wrong!");
                //}
            }

        }

        private void ClearPatientFields()
        {
            txtPatient_id.Clear();
            Mno.Clear();
            fname.Clear();
            lname.Clear();
            patient_gender.SelectedIndex = 0;
            patient_date.SelectedDate = null;
            patient_address.Clear();
            patient_suburb.Clear();
            patient_state.Clear();
            patient_postcode.Clear();
            Homeno.Clear();
            Mobileno.Clear();
            notes.Clear();
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            txtPatient_id.Clear();
            Mno.Clear();
            fname.Clear();
            lname.Clear();
            patient_gender.SelectedIndex = -1;
            patient_date.SelectedDate = null;
            patient_address.Clear();
            patient_suburb.Clear();
            patient_state.Clear();
            patient_postcode.Clear();
            Homeno.Clear();
            Mobileno.Clear();
            notes.Clear();
        }

        private void update_Click(object sender, RoutedEventArgs e)
        {
            //Get the index of the currently selected item in the ListView
            //so that we can go back to that row after the update.
            listViewSelectedItemIndex = lvPatients.SelectedIndex;

            //Instantiate the currently selected Pilot from the ListView
            Patient selectedPatient;
            selectedPatient = (Patient)lvPatients.SelectedItem;

            //Assign the updated values from the UI to the selected Pilot
            AssignPropertiesToPatient(selectedPatient);
            //MessageBox.Show("Update " + selectedPatient.FirstName.ToString());

            try
            {
                selectedPatient.UpdatePatient();
                ShowPatientData();
                lvPatients.SelectedIndex = listViewSelectedItemIndex;
                lvPatients.ScrollIntoView(lvPatients.SelectedItem);
                lvPatients.Focus();

                MessageBox.Show("The Patient's details have been update in the Database.","Update Patient?",MessageBoxButton.OK,MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error has occured. The Patient's details were NOT updated.  Please contact your system administrator! ", "Update Patient?" + ex.Message);
            }
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            Patient selectedPatient = (Patient)lvPatients.SelectedItem;

            //MessageBox.Show(selectedPatient.Patient_Id.ToString());

            try
            {
                if (MessageBox.Show("Are you sure you want to PERMANENTLY delete the Patient's details?", "Delete Patient?", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    selectedPatient.DeletePatient();
                    ShowPatientData();
                    MessageBox.Show("Thank you!  The Patient's details have been deleted!","Delete Patient?",MessageBoxButton.OK,MessageBoxImage.Information);
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error has occured!  The Patient's details have NOT been deleted. ","Delete Patient?" + ex.Message);
            }
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            PatientCollection myPatients = new PatientCollection(null, null);
            lvPatients.DataContext = myPatients;

            //Patient myPatient = new Patient();
            ////myPatient.Patient_id = "1002";
            //myPatient.FirstName = "Saranya";
            //myPatient.LastName = "Kutuva";
            //myPatient.Medicare_No = "344434343434";
            //myPatient.Gender = "Female";
            //myPatient.DateOfBirth = DateTime.Parse("23/01/1962");
            //myPatient.StreetAddress = "23 Balmain Street";
            //myPatient.Suburb = "Manly";
            //myPatient.State = "NSW";
            //myPatient.PostCode = "2095";
            //myPatient.Homephone = "9887746666";
            //myPatient.Mobilephone = "04523434344";
            //myPatient.Notes = "Fracture in Right Leg";
            //if (myPatient.AddPatient() == 1)
            //{
            //    MessageBox.Show("Yipeee!! New Patientt was inserted.");
            //}
            //else
            //{
            //    MessageBox.Show("DOH!!!  Something went wrong!");
            //}

            ShowPatientData();




        }



        private void lvPatient_Loaded(object sender, RoutedEventArgs e)
        {
           
            LoadFieldNameCombo();       //Loads the different fields (columns)
            ShowPatientData();    //Get the data from the database and display them in the UI.
        }

        #endregion Public Patient Methods

        #endregion Patient Methods

        #region Practitioner Methods


        #region Private Practitioner Methods

        private void ShowPractitionerData()
        {
            PractitionerCollection myPractitioners;

            try
            {

                //if ((cboFieldNamesAppt.SelectedValue.ToString().Length > 0 && txtSearchStringAppt.Text.ToString().Length > 0))
                /* if ((cboFieldNamesAppt.SelectedValue.ToString() != null && txtSearchStringAppt.Text.ToString().Length > 0))
                 {
                     // MessageBox.Show("Inside");
                     //Search criteria HAVE been specified.  i.e. user has selected a field to search and provided a search string
                     //MessageBox.Show(txtSearchStringAppt.Text.Split('/').Count().ToString());
                     try
                     {
                         if (cboFieldNamesAppt.SelectedValue.ToString().Equals("Appointment_Date"))
                         {
                             DateTime ld;

                             ld = DateTime.ParseExact(txtSearchStringAppt.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                         }
                     }
                     catch (Exception ex)
                     {
                         string ls = ex.Message;
                         MessageBox.Show("Date format is incorrect! Expected format is dd/mm/yyyy. ");
                         return;
                     }

                     if (cboFieldNamesAppt.SelectedValue.ToString().Equals("Appointment_Date") && txtSearchStringAppt.Text.Split('/').Count() > 2)
                     {

                         String[] dob = txtSearchStringAppt.Text.Split('/');
                         String dobString = dob[2] + '-' + dob[1].PadLeft(2).Replace(' ', '0') + '-' + dob[0].PadLeft(2).Replace(' ', '0');
                         MessageBox.Show(dobString);
                         myAppointments = new AppointmentCollection(cboFieldNamesAppt.SelectedValue.ToString(), dobString);
                         lvAppointments.DataContext = myAppointments;
                     }
                     else
                     {*/

                if (cboFieldNames_Practitioner.SelectedValue != null && txtSearchString_Practitioner.Text.ToString().Length > 0)
                {

                    try
                    {
                        if (cboFieldNames_Practitioner.SelectedValue.ToString().Equals("Practitioner_Dateofbirth"))
                        {
                            DateTime ld;

                            ld = DateTime.ParseExact(txtSearchString_Practitioner.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        }
                    }
                    catch (Exception ex)
                    {
                        string ls = ex.Message;
                        MessageBox.Show("Date format is incorrect! Expected format is dd/mm/yyyy. ");
                        return;
                    }
                    //Search criteria HAVE been specified.  i.e. user has selected a field to search and provided a search string
                    if (cboFieldNames_Practitioner.SelectedValue.ToString().Equals("Practitioner_Dateofbirth"))
                    {
                        String[] dob = txtSearchString_Practitioner.Text.Split('/');
                        String dobString = dob[2] + '-' + dob[1].PadLeft(2).Replace(' ', '0') + '-' + dob[0].PadLeft(2).Replace(' ', '0');
                        // MessageBox.Show(dobString);
                        myPractitioners = new PractitionerCollection(cboFieldNames_Practitioner.SelectedValue.ToString(), dobString);
                        lvPractitioners.DataContext = myPractitioners;
                    }
                    else
                    {

                        myPractitioners = new PractitionerCollection(cboFieldNames_Practitioner.SelectedValue.ToString(), txtSearchString_Practitioner.Text);
                        lvPractitioners.DataContext = myPractitioners;
                    }
                }
                else
                {
                    //First time the window loads (or the user clicks the search button WITHOUT specifying search criteria).
                    myPractitioners = new PractitionerCollection(null, null);
                    lvPractitioners.DataContext = myPractitioners;
                }
                lvPractitioners.SelectedIndex = 0;
                lvPractitioners.ScrollIntoView(lvPractitioners.SelectedItem);
                lvPractitioners.Focus();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ShowPractitionerData2()
        {
            PractitionerCollection myPractitioners;
           // MessageBox.Show("Changed  " + this.cboPractitionerRef.SelectedValue.ToString());
            try
            {
            
                    myPractitioners = new PractitionerCollection("Practitioner_MedicalRegnno", pracMedicalReg);
                               
                    lvPractitioners1.DataContext = myPractitioners;
                

            }
            catch (Exception ex)
            {
               MessageBox.Show(ex.Message);
            }
        }
        private void AssignPropertiesToPractitioner(Practitioner newPractitioner)
        {

            try
            {
                if (String.IsNullOrWhiteSpace(MRNo.Text))
                {
                    MessageBox.Show("Please Enter Medicare Registration Number", "Practitioner", MessageBoxButton.OK, MessageBoxImage.Stop);
                    MRNo.Clear();
                    MRNo.Focus();
                    //return;
                }
                else if (!Regex.IsMatch(MRNo.Text, @"^\d+$"))
                {
                    MessageBox.Show("Please Enter Valid Medicare Registration Number", "Practitioner", MessageBoxButton.OK, MessageBoxImage.Stop);
                    MRNo.Clear();
                    MRNo.Focus();
                    // return;
                }
                else
                {
                    newPractitioner.MedRegNo = MRNo.Text;
                }
                Regex r = new Regex("^[a-zA-Z\\s\\,]*$");
                //validated first name for numeric 
                if (String.IsNullOrWhiteSpace(fname1.Text))
                {
                    MessageBox.Show("Practitioner First Name Can't be Empty", "Practitioner", MessageBoxButton.OK
                        , MessageBoxImage.Stop);
                    fname1.Clear();
                    fname1.Focus();
                    return;
                }
                else if (r.IsMatch(fname1.Text))
                {
                    newPractitioner.FirstName = fname1.Text;
                }
                else
                {
                    MessageBox.Show("Please enter valid First Name", "Practitioner", MessageBoxButton.OK, MessageBoxImage.Stop);
                    fname1.Clear();
                    fname1.Focus();
                    return;
                }
               

                if (String.IsNullOrEmpty(lname1.Text))
                {
                    MessageBox.Show("Practitioner Last Name Can't be Empty", "Practitioner", MessageBoxButton.OK
                        , MessageBoxImage.Stop);
                    lname1.Clear();
                    lname1.Focus();
                    return;
                }
                else if (r.IsMatch(lname1.Text))
                {

                    newPractitioner.LastName = lname1.Text;
                }
            
                else
                {
                    MessageBox.Show("Please enter valid Last Name", "Practitioner",MessageBoxButton.OK,MessageBoxImage.Stop);
                    lname1.Clear();
                    lname1.Focus();
                    //return;
                }

                if (String.IsNullOrEmpty(Homeno1.Text) || String.IsNullOrEmpty(Mobileno1.Text))
                {
                    MessageBox.Show("Please Enter Both the Phone Numbers", "Practitioner", MessageBoxButton.OK, MessageBoxImage.Stop);
                    Homeno1.Clear();
                    Homeno1.Focus();
                    Mobileno1.Clear();
                    Mobileno1.Focus();
                    //return;
                }
                else if (!Regex.IsMatch(Homeno1.Text, @"^\d+$"))
                {
                    MessageBox.Show("Please Enter Valid Home Number", "Practitioner", MessageBoxButton.OK, MessageBoxImage.Stop);
                    Homeno1.Clear();
                    Homeno1.Focus();
                   
                 //   return;
                }
                else if (!Regex.IsMatch(Mobileno1.Text, @"^\d+$"))
                {
                    MessageBox.Show("Please Enter Valid Mobile Number", "Practitioner", MessageBoxButton.OK, MessageBoxImage.Stop);
                   
                    Mobileno1.Clear();
                    Mobileno1.Focus();
                }
                else
                {
                    newPractitioner.Homephone = Homeno1.Text;
                    newPractitioner.Mobilephone = Mobileno1.Text;
                }


                newPractitioner.Prac_Id = prac_ID.Text;
                // newPractitioner.MedRegNo = MRNo.Text;
                // newPractitioner.FirstName = fname1.Text;
                // newPractitioner.LastName = lname1.Text;
                newPractitioner.Desc = cboDescription.Text;
                newPractitioner.Gender = prac_gender.Text;
                //newPractitioner.DateOfBirth = DateTime.ParseExact("31/12/4712", "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                //if (newPractitioner.DateOfBirth != null)
                //{
                    newPractitioner.DateOfBirth = DateTime.Parse(prac_date.Text);
                //}
                //newPractitioner.DateOfBirth = prac_date.SelectedDate.Value;
                newPractitioner.StreetAddress = prac_address.Text;
                newPractitioner.Suburb = prac_suburb.Text;
                newPractitioner.State = prac_state.Text;
                newPractitioner.PostCode = prac_postcode.Text;
                //newPractitioner.Homephone = Homeno1.Text;
                //newPractitioner.Mobilephone = Mobileno1.Text;
                newPractitioner.Monday = (bool)Mondaychk.IsChecked;
                newPractitioner.Tuesday = (bool)Tuesdaychk.IsChecked;
                newPractitioner.Wednesday = (bool)Wednesdaychk.IsChecked;
                newPractitioner.Thursday = (bool)ThursdayChk.IsChecked;
                newPractitioner.Friday = (bool)Fridaychk.IsChecked;
                newPractitioner.Saturday = (bool)Saturdaychk.IsChecked;
                newPractitioner.Sunday = (bool)Sundaychk.IsChecked;
                Update1.IsEnabled = true;
                Delete1.IsEnabled = true;
                Add1.Content = "CREATE";

            }

            catch (FormatException frmtExc)
            {
                Homeno1.Text = "Please Enter only Numbers:" + frmtExc.Message;
                Mobileno1.Text = "Please Enter only Numbers:" + frmtExc.Message;
                MRNo.Text = "Please Enter only Numbers:" + frmtExc.Message;
                Homeno1.Clear();
                Mobileno1.Clear();
                MRNo.Clear();
                fname1.Text = "Please Enter only Letters" + frmtExc.Message;
                lname1.Text = "Please Enter only Letters" + frmtExc.Message;
                fname1.Clear();
                lname1.Clear();
            }


            catch (Exception ex)
            {
                fname1.Text = "Something went wrong!" + ex.Message;
                lname1.Text = "Something went wrong!" + ex.Message;
                MRNo.Text = "Something went wrong!" + ex.Message;
            }
            finally
            {
                // This section will execute regardless of an exception occuring or not!
                // This is a good place to clean up(usually close or dispose of) objects that have been left
                // open or hanging around in memory that should be disposed of.
            }


           
            //          String msg1 = " Prac_Id   " + newPractitioner.Prac_Id +
            //" MedRegNo" + newPractitioner.MedRegNo +
            //" FirstName " + newPractitioner.FirstName +
            //" LastName" + newPractitioner.LastName +
            //" Desc" + newPractitioner.Desc +
            // "Gender " + newPractitioner.Gender +
            //" DateOfBirth" + newPractitioner.DateOfBirth +
            //" StreetAddress " + newPractitioner.StreetAddress +
            //" Suburb " + newPractitioner.Suburb +
            //" State " + newPractitioner.State +
            //" PostCode " + newPractitioner.PostCode +
            //" Homephone " + newPractitioner.Homephone +
            //" Mobilephone " + newPractitioner.Mobilephone +
            //" Monday " + newPractitioner.Monday +
            //" Tuesday " + newPractitioner.Tuesday +
            //" Wednesday " + newPractitioner.Wednesday +
            //" Thursday " + newPractitioner.Thursday +
            //" Friday " + newPractitioner.Friday +
            //" Saturday " + newPractitioner.Saturday +
            //" Sunday " + newPractitioner.Sunday;

            //          MessageBox.Show(msg1);

        }

        private void LoadFieldNameCombo1()
        {
            try
            {
                SqlHelper dal = new SqlHelper();
                //We want to call the stored proc that retrieves the names of the columns in the 
                //Pilot table. i.e. "usp_GetTableFieldName".  It expects one parameter "@TableName" which
                //will have the value "tblPilot".
                string SPName = "usp_GetTableFieldNames";
                SqlParameter[] parameters = { new SqlParameter("@TableName", "Practitioner") };

                DataTable dtFieldNames = dal.ExecuteStoredProc(SPName, parameters);

                //Map the database table field names (columns) to more human FRIENDLY names.
                //Define a new column object with the column name "FRIENDLY_NAMES"
                DataColumn colFriendlyNames1 = new DataColumn("FRIENDLY_NAMES", System.Type.GetType("System.String"));
                //Add our FRIENDLY_NAMES column to our DataTable.
                dtFieldNames.Columns.Add(colFriendlyNames1);

                // MessageBox.Show("Show data successful " + "Practitioner");
                DataRow dtFieldNames1;
                dtFieldNames1 = dtFieldNames.NewRow();

                dtFieldNames1["FRIENDLY_NAMES"] = "--Select a FieldName--";
                dtFieldNames1["COLUMN_NAME"] = "0";
                dtFieldNames.Rows.InsertAt(dtFieldNames1, 0);


                foreach (DataRow currentRow in dtFieldNames.Rows)
                {
                    switch (currentRow[0].ToString())
                    {
                        case "Practitioner_Id":
                            currentRow[1] = "Practitioner_Id";
                            break;
                        case "Practitioner_MedicalRegnno":
                            currentRow[1] = "Medical Registration No";
                            break;
                        case "Practitioner_Fname":
                            currentRow[1] = "First Name";
                            break;
                        case "Practitioner_Lname":
                            currentRow[1] = "Surname";
                            break;
                        case "Practitioner_Desc":
                            currentRow[1] = "Description";
                            break;
                        case "Practitioner_Gender":
                            currentRow[1] = "Gender";
                            break;
                        case "Practitioner_Dateofbirth":
                            currentRow[1] = "Date Of Birth";
                            break;
                        case "Practitioner_Street_address":
                            currentRow[1] = "Street";
                            break;
                        case "Practitioner_Suburb":
                            currentRow[1] = "Suburb";
                            break;
                        case "Practitioner_State":
                            currentRow[1] = "State";
                            break;
                        case "Practitioner_Postcode":
                            currentRow[1] = "Post Code";
                            break;
                        case "Practitioner_HomePhone":
                            currentRow[1] = "HomePhone";
                            break;
                        case "Practitioner_MobilePhone":
                            currentRow[1] = "MobilePhone";
                            break;
                        case "Monday":
                            currentRow[1] = "Monday";
                            break;
                        case "Tuesday":
                            currentRow[1] = "Tuesday";
                            break;
                        case "Wednesday":
                            currentRow[1] = "Wednesday";
                            break;
                        case "Thursday":
                            currentRow[1] = "Thursday";
                            break;
                        case "Friday":
                            currentRow[1] = "Friday";
                            break;
                        case "Saturday":
                            currentRow[1] = "Saturday";
                            break;
                        case "Sunday":
                            currentRow[1] = "Sunday";
                            break;
                        default:
                            break;
                    }
                }

                cboFieldNames_Practitioner.SelectedIndex = 0;
                cboFieldNames_Practitioner.SelectedValuePath = "COLUMN_NAME";
                cboFieldNames_Practitioner.DisplayMemberPath = "FRIENDLY_NAMES";
                cboFieldNames_Practitioner.ItemsSource = dtFieldNames.DefaultView;

            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured loading the search field names. " + ex.Message);

            }
        }



        #endregion Private Practitioner Methods

        #region Public Practitioner Methods
        private void Delete1_Click(object sender, RoutedEventArgs e)
        {
            Practitioner selectedPractitioner = (Practitioner)lvPractitioners.SelectedItem;
            try
            {
                if (MessageBox.Show("Are you sure you want to PERMANENTLY delete the Practitioner's details?", "Delete Practitioner?", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {

                    selectedPractitioner.DeletePractitioner();
                    ShowPractitionerData();
                    MessageBox.Show("Thank you!  The Practitioner's details have been deleted!","Delete Practitioner",MessageBoxButton.OK,MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Error");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error has occured!  The Practitioner's details have NOT been deleted. Practitioner's might have Appointments with Patients.  "+ ex.Message);
            }
        }
        private void Add1_Click_1(object sender, RoutedEventArgs e)
        {
            if (Add1.Content.ToString().Contains("CREATE"))
            {
                ClearPractitionerFields();
                Add1.Content = "ADD";
                Update1.IsEnabled = false;
                Delete1.IsEnabled = false;
                //prac_ID.IsReadOnly = false;
                MessageBox.Show("Click Add button after entering the new Practitioner.","Add Practitioner",MessageBoxButton.OK,MessageBoxImage.Asterisk);

            }
            else
            {
                try
                {
                   
                    //Create a new Practitioner object
                    Practitioner newPractitioner = new Practitioner();
                    //Get the values from the user interface (textboxes etc.) and assign them to Pilot object.
                    AssignPropertiesToPractitioner(newPractitioner);

                    int rows = newPractitioner.AddPractitioner();

                    //MessageBox.Show("Rows Affected" + rows);

                    //Clear out the form..
                    //ClearFields();

                    //Refresh the list of Practitioners
                    ShowPractitionerData();

                    //Select the new Practitioner in the Refreshed list.
                    lvPractitioners.SelectedIndex = lvPractitioners.Items.Count - 1;
                    lvPractitioners.ScrollIntoView(lvPractitioners.SelectedItem);
                    
                    //Give some hope to the user.
                    MessageBox.Show("Thank you!  The new Practitioner's details have been added to the Database.","Add Practitioner", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                  
                    Add1.Content = "CREATE";
                   
                    Update1.IsEnabled = true;
                    Delete1.IsEnabled = true;

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Something went wrong, but we don't know what!  Practitioner was NOT added to the database. "+ ex.Message);
                }
            }
        }

        private void ClearPractitionerFields()
        {
            prac_ID.Clear();
            MRNo.Clear();
            fname1.Clear();
            lname1.Clear();
            cboDescription.SelectedIndex = -1;
            prac_address.Clear();
            prac_state.Clear();
            prac_suburb.Clear();
            prac_postcode.Clear();
            prac_gender.SelectedIndex = -1;
            prac_date.SelectedDate = null;
            Homeno1.Clear();
            Mobileno1.Clear();
            Mondaychk.IsChecked = false;
            Tuesdaychk.IsChecked = false;
            Wednesdaychk.IsChecked = false;
            ThursdayChk.IsChecked = false;
            Fridaychk.IsChecked = false;
            Saturdaychk.IsChecked = false;
            Sundaychk.IsChecked = false;
        }

        private void Search1_Click(object sender, RoutedEventArgs e)
        {
            // Practitioner myPractitioner = new Practitioner();
            ShowPractitionerData();

        }

        //private void MRNo_MouseLeave(object sender, MouseEventArgs e)
        //{
        //    listViewSelectedItemIndex = lvPractitioners.SelectedIndex;

        //    MessageBox.Show(" Selected Index" + listViewSelectedItemIndex);

        //}

        private void Update1_Click(object sender, RoutedEventArgs e)
        {
            //Get the index of the currently selected item in the ListView
            //so that we can go back to that row after the update.
            listViewSelectedItemIndex = lvPractitioners.SelectedIndex;

            //MessageBox.Show(" Index" + listViewSelectedItemIndex);

            //Instantiate the currently selected Pilot from the ListView
            Practitioner selectedPractitioner;
            selectedPractitioner = (Practitioner)lvPractitioners.SelectedItem;

            //Assign the updated values from the UI to the selected Pilot
            AssignPropertiesToPractitioner(selectedPractitioner);

            try
            {
                selectedPractitioner.UpdatePractitioner();
                ShowPractitionerData();
                lvPractitioners.SelectedIndex = listViewSelectedItemIndex;
                lvPractitioners.ScrollIntoView(lvPractitioners.SelectedItem);
                lvPractitioners.Focus();

                MessageBox.Show("The Practiitioner's details have been update in the Database.","Update Practitioner ",MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error has occured. The Practitioner's details were NOT updated.  Please contact your system administrator! ", "Update Practitioner" + ex.Message);
                   
            }
        }

        private void Clear1_Click(object sender, RoutedEventArgs e)
        {
            prac_ID.Clear();
            MRNo.Clear();
            fname1.Clear();
            lname1.Clear();
            cboDescription.SelectedIndex = -1;
            prac_address.Clear();
            prac_state.Clear();
            prac_suburb.Clear();
            prac_postcode.Clear();
            prac_gender.SelectedIndex = -1;
            prac_date.SelectedDate = null;
            Homeno1.Clear();
            Mobileno1.Clear();
            Mondaychk.IsChecked = false;
            Tuesdaychk.IsChecked = false;
            Wednesdaychk.IsChecked = false;
            ThursdayChk.IsChecked = false;
            Fridaychk.IsChecked = false;
            Saturdaychk.IsChecked = false;
            Sundaychk.IsChecked = false;
        }

        private void lvPractitioner_Loaded(object sender, RoutedEventArgs e)
        {
            LoadFieldNameCombo1();// Loads the combo box for searching particular Practitioners by different fields(columns)
        
            ShowPractitionerData(); // Get the Practitioners from the database and display them in the UI.
        }
        #endregion Public Practitioner Methods


        #endregion Practitioner Methods

        #region Appointment Methods


        #region Public Appointment Methods



        private void tabAppointment_Loaded(object sender, RoutedEventArgs e)
        {
            LoadApptFieldNameCombo();
            LoadPatientCombo();
            LoadPractitionerCombo();
            LoadApptTimeCombo();
            ShowAppointmentData();
            ShowPractitionerData2();
            DPApptDate.DisplayDateStart = DateTime.Now;
        }
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            //LoadApptFieldNameCombo();
            LoadPatientCombo();
            LoadPractitionerCombo();
            DPApptDate.SelectedDate = null;
            LoadApptTimeCombo();
            ShowAppointmentData();
        }


        private void Deleteappt_Click(object sender, RoutedEventArgs e)
        {
            
            Appointment selectedAppointment = (Appointment)lvAppointments.SelectedItem;

            // MessageBox.Show(selectedAppointment.Patient_IdRef.ToString());

            try
            {
                if (MessageBox.Show("Are you sure you want to PERMANENTLY delete the Appointment's details?", "Delete Appointment?", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    selectedAppointment.DeleteAppointment();
                    ShowAppointmentData();
                    MessageBox.Show("Thank you!  The Appointment's details have been deleted!", "Delete Appointment", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error has occured!  The Appointment's details have NOT been deleted. " + ex.Message);
            }
        }

        private void Addappt_Click(object sender, RoutedEventArgs e)
        {

            if (Addappt.Content.ToString().Contains("Create"))
            {

                PractitionerCollection myPractitioners;
                // MessageBox.Show("Changed  " + this.cboPractitionerRef.SelectedValue.ToString());
                try
                {
                    myPractitioners = new PractitionerCollection(null, null);
                    lvPractitioners1.DataContext = myPractitioners;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                ClearFields();
                Addappt.Content = "Add";
                ShowPractitionerData2();
                UpdateAppt.IsEnabled = false;
                Deleteappt.IsEnabled = false;

                MessageBox.Show("Click Add button after entering new appointment.","Add Appointment",MessageBoxButton.OK,MessageBoxImage.Information);

            }
            else
            {
                try
                {
                    Appointment newAppt = new Appointment();
                    AssignPropertiesToAppointment(newAppt);

                    newAppt.AddAppointment();
                    //ClearFields();

                    lvAppointments.SelectedIndex = lvAppointments.Items.Count;
                    lvAppointments.ScrollIntoView(lvAppointments.SelectedItem);
                    MessageBox.Show("Thank You! The new Appointment's  details have been added to the Database.", "Add Appointment", MessageBoxButton.OK, MessageBoxImage.Information);
                    AppointmentCollection myAppointments;
                    myAppointments = new AppointmentCollection(null, null);
                    lvAppointments.DataContext = myAppointments;


                   lvAppointments.SelectedIndex = 0;
                    lvAppointments.ScrollIntoView(lvAppointments.SelectedItem);
                    lvAppointments.Focus();
                    // DPApptDate.= DPApptDate.Value.Date;

                   
                    Addappt.Content = "Create";
                    UpdateAppt.IsEnabled = true;
                    Deleteappt.IsEnabled = true;


                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("Validation Error: "))
                    {
                        MessageBox.Show(ex.Message);
                        Addappt.Content = "Add";
                    }
                    else
                    {
                        MessageBox.Show("Something Went Wrong, but we don't know that! Appointment was NOT added to the database. " + ex.Message);
                        Addappt.Content = "Add";
                    }


                }
            }
        }

        private void ClearFields()
        {
            cboPatientRef.SelectedIndex = 0;
            cboPractitionerRef.SelectedIndex = 0;
            cboApptTime.SelectedIndex = 0;
            cboFieldNamesAppt.SelectedIndex = 0;
            DPApptDate.SelectedDate = null;
            txtSearchStringAppt.Clear();
            txtApptId.Clear();
            //  LoadPractitionerCombo();
            // LoadApptTimeCombo();
            // LoadPatientCombo();

            //Go to the top row and scroll the list up to the top
            //    lvAppointments.SelectedIndex = 0;
            //  lvAppointments.Focus();
            //   lvAppointments.ScrollIntoView(lvAppointments.SelectedItem);
            //Deselect any Pilot in the list.
            //  lvAppointments.SelectedIndex = -1;
            //We might want refresh our view of the Data from the Database (the source) depending on what the user has done.
            //  ShowAppointmentData();
        }

        private void AssignPropertiesToAppointment(Appointment newAppt)
        {
            newAppt.Patient_IdRef = (int)cboPatientRef.SelectedValue;
            newAppt.Practitioner_MedRegnoRef = cboPractitionerRef.SelectedValue.ToString();
            //  newAppt.AppointmentDate =DateTime.Parse("31/12/4712","d/mm/yyyy");
            newAppt.AppointmentDate = DateTime.ParseExact("31/12/4712", "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            if (DPApptDate.SelectedDate != null)
            {
                newAppt.AppointmentDate = DPApptDate.SelectedDate.Value;
            }

            newAppt.ApptTime = cboApptTime.Text;
            // MessageBox.Show(newAppt.ApptTime);

        }

        private void ClearAppt_Click(object sender, RoutedEventArgs e)
        {
            ClearFields();
        }

        private void UpdateAppt_Click(object sender, RoutedEventArgs e)
        {
            //DPApptDate.DisplayDateStart = DateTime.Now;
            //Get the index of the currently selected item in the ListView
            //so that we can go back to that row after the update.
            listViewSelectedItemIndex = lvAppointments.SelectedIndex;

            //Instantiate the currently selected Pilot from the ListView
            Appointment selectedAppointment;
            selectedAppointment = (Appointment)lvAppointments.SelectedItem;

            //Assign the updated values from the UI to the selected Pilot
            AssignPropertiesToAppointment(selectedAppointment);
            //MessageBox.Show("Update " + selectedPatient.FirstName.ToString());

            try
            {
                selectedAppointment.UpdateAppointment();
                ShowAppointmentData();
            
                lvAppointments.SelectedIndex = listViewSelectedItemIndex;
                lvAppointments.ScrollIntoView(lvAppointments.SelectedItem);
                lvAppointments.Focus();

                MessageBox.Show("The Appointment's details have been update in the Database.","Update Appointment",MessageBoxButton.OK,MessageBoxImage.Information);
            }
            catch (Exception ex)
            {

                if (ex.Message.Contains("Validation Error:"))
                {
                    MessageBox.Show(ex.Message);
                }
                else
                {
                    MessageBox.Show("An error has occured. The appointment's details were NOT updated.  Please contact your system administrator! " + ex.Message);
                }


            }
        }


        #endregion Public Appointment Methods

        #region Private Appointment Methods

        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }




        private void cboPractitionerRef_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            try
            {
                SqlHelper dal = new SqlHelper();
                string SPName = "usp_GetAllPractitionerNamesAndIDs";

                DataTable dtPractitioners = dal.ExecuteStoredProc(SPName);

                //Add a message to the comboBox for the user to select an Airline
                DataRow drSelectMessage;
                drSelectMessage = dtPractitioners.NewRow();

                drSelectMessage["Practitioner_Name"] = "--Select a Doctor --";
                drSelectMessage["Practitioner_MedicalRegnno"] = "0";

                dtPractitioners.Rows.InsertAt(drSelectMessage, 0);

                //MessageBox.Show(" kjk " + dtPractitioners.Rows[cboPractitionerRef.SelectedIndex]["Practitioner_MedicalRegnno"].ToString());\
                if (cboPractitionerRef.SelectedIndex >0) { 
                pracMedicalReg = dtPractitioners.Rows[cboPractitionerRef.SelectedIndex]["Practitioner_MedicalRegnno"].ToString();
                workingDays.Content = dtPractitioners.Rows[cboPractitionerRef.SelectedIndex]["Practitioner_Name"].ToString() ;
                ShowPractitionerData2();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
            

        

       
        

        private void lvPractitioners1_Loaded(object sender, RoutedEventArgs e)
        {

            //LoadApptFieldNameCombo();
            //LoadPatientCombo();
            //LoadPractitionerCombo();
            //LoadApptTimeCombo();
               ShowPractitionerData2();
        }

      

        private void ShowAppointmentData()
        {
            AppointmentCollection myAppointments;

            try
            {
                //if ((cboFieldNamesAppt.SelectedValue.ToString().Length > 0 && txtSearchStringAppt.Text.ToString().Length > 0))
                if ((cboFieldNamesAppt.SelectedValue.ToString() != null && txtSearchStringAppt.Text.ToString().Length > 0))
                {
                    // MessageBox.Show("Inside");
                    //Search criteria HAVE been specified.  i.e. user has selected a field to search and provided a search string
                    //MessageBox.Show(txtSearchStringAppt.Text.Split('/').Count().ToString());
                    try
                    {
                        if (cboFieldNamesAppt.SelectedValue.ToString().Equals("Appointment_Date"))
                        {
                            DateTime ld;

                            ld = DateTime.ParseExact(txtSearchStringAppt.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        }
                    }
                    catch (Exception ex)
                    {
                        string ls = ex.Message;
                        MessageBox.Show("Date format is incorrect! Expected format is dd/mm/yyyy. ");
                        return;
                    }

                    if (cboFieldNamesAppt.SelectedValue.ToString().Equals("Appointment_Date") && txtSearchStringAppt.Text.Split('/').Count() > 2)
                    {

                        String[] dob = txtSearchStringAppt.Text.Split('/');
                        String dobString = dob[2] + '-' + dob[1].PadLeft(2).Replace(' ', '0') + '-' + dob[0].PadLeft(2).Replace(' ', '0');
                        //MessageBox.Show(dobString);
                        myAppointments = new AppointmentCollection(cboFieldNamesAppt.SelectedValue.ToString(), dobString);
                        lvAppointments.DataContext = myAppointments;
                    }
                    else
                    {

                        myAppointments = new AppointmentCollection(cboFieldNamesAppt.SelectedValue.ToString(), txtSearchStringAppt.Text);
                        lvAppointments.DataContext = myAppointments;
                    }
                }/*else if (((cboFieldNamesAppt.SelectedValue.ToString()=="0" && txtSearchStringAppt.Text.ToString().Length > 0)))
                {
                    //MessageBox.Show("Search for entire row");
                    myAppointments = new AppointmentCollection(null, txtSearchStringAppt.Text.ToString());
                    lvAppointments.DataContext = myAppointments;

                }*/
                else
                {
                    //MessageBox.Show("Show All Data");
                    //First time the window loads (or the user clicks the search button WITHOUT specifying search criteria).
                    myAppointments = new AppointmentCollection(null, null);
                    lvAppointments.DataContext = myAppointments;

                }
                //MessageBox.Show(cboFieldNamesAppt.SelectedValue.ToString()); 
                lvAppointments.SelectedIndex = lvAppointments.Items.Count;
                lvAppointments.ScrollIntoView(lvAppointments.SelectedItem);


               // lvAppointments.SelectedIndex = 0;
                //lvAppointments.ScrollIntoView(lvAppointments.SelectedItem);
               lvAppointments.Focus();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadApptTimeCombo()
        {
            try
            {
                SqlHelper dal = new SqlHelper();
                string SPName = "usp_GetAllAppointmentsTimes";

                DataTable dtTimeSlot = dal.ExecuteStoredProc(SPName);

                //Add a message to the comboBox for the user to select an Airline
                DataRow drSelectMessage;
                drSelectMessage = dtTimeSlot.NewRow();

                drSelectMessage["Appointment_Time"] = "--Select a Time--";
                // drSelectMessage["Appointment_Time"] = "0";

                dtTimeSlot.Rows.InsertAt(drSelectMessage, 0);

                //Now bind the DataTable to the combobox.
                //cboApptTime.SelectedValuePath = "Appointment_Time";
                cboApptTime.DisplayMemberPath = "Appointment_Time";
                // cboApptTime.SelectedValue.ToString();
                cboApptTime.ItemsSource = dtTimeSlot.DefaultView;

                cboApptTime.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadPractitionerCombo()
        {
            try
            {
                SqlHelper dal = new SqlHelper();
                string SPName = "usp_GetAllPractitionerNamesAndIDs";

                DataTable dtPractitioners = dal.ExecuteStoredProc(SPName);

                //Add a message to the comboBox for the user to select an Airline
                DataRow drSelectMessage;
                drSelectMessage = dtPractitioners.NewRow();

                drSelectMessage["Practitioner_Name"] = "--Select a Doctor --";
                drSelectMessage["Practitioner_MedicalRegnno"] = "0";

                dtPractitioners.Rows.InsertAt(drSelectMessage, 0);

                //Now bind the DataTable to the combobox.
                cboPractitionerRef.SelectedValuePath = "Practitioner_MedicalRegnno";
                cboPractitionerRef.DisplayMemberPath = "Practitioner_Name";
                cboPractitionerRef.ItemsSource = dtPractitioners.DefaultView;

                cboPractitionerRef.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadPatientCombo()
        {
            try
            {
                SqlHelper dal = new SqlHelper();
                string SPName = "usp_GetAllPatientNamesAndIDs";

                DataTable dtPatients = dal.ExecuteStoredProc(SPName);

                //Add a message to the comboBox for the user to select an Airline
                DataRow drSelectMessage;
                drSelectMessage = dtPatients.NewRow();

                drSelectMessage["Patient_Name"] = "--Select a Patient--";
                drSelectMessage["Patient_Id"] = "0";

                dtPatients.Rows.InsertAt(drSelectMessage, 0);

                //Now bind the DataTable to the combobox.
                cboPatientRef.SelectedValuePath = "Patient_Id";
                cboPatientRef.DisplayMemberPath = "Patient_Name";
                cboPatientRef.ItemsSource = dtPatients.DefaultView;

                cboPatientRef.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void LoadApptFieldNameCombo()
        {

            try
            {
                SqlHelper dal = new SqlHelper();
                //We want to call the stored proc that retrieves the names of the columns in the 
                //Pilot table. i.e. "usp_GetTableFieldName".  It expects one parameter "@TableName" which
                //will have the value "tblPilot".
                string SPName = "usp_GetTableFieldNames";
                SqlParameter[] parameters = { new SqlParameter("@TableName", "Appointment") };

                DataTable dtFieldNames = dal.ExecuteStoredProc(SPName, parameters);

                //Map the database table field names (columns) to more human FRIENDLY names.
                //Define a new column object with the column name "FRIENDLY_NAMES"
                DataColumn colFriendlyNames = new DataColumn("FRIENDLY_NAMES", System.Type.GetType("System.String"));



                //Add our FRIENDLY_NAMES column to our DataTable.
                dtFieldNames.Columns.Add(colFriendlyNames);
                DataRow dtFieldNames1;
                dtFieldNames1 = dtFieldNames.NewRow();

                dtFieldNames1["FRIENDLY_NAMES"] = "--Select a FieldName--";
                dtFieldNames1["COLUMN_NAME"] = "0";
                dtFieldNames.Rows.InsertAt(dtFieldNames1, 0);
               // dtFieldNames.Rows.RemoveAt(1);
               

                foreach (DataRow currentRow in dtFieldNames.Rows)
                {
                    //TODO 1 - Fix the friendly names here.
                    switch (currentRow[0].ToString())
                    {
                        case "Patient_Id":
                            currentRow[1] = "Patient Id";
                            break;
                        case "Appointment_Id":
                            currentRow[1] = "Appointment Id";
                            break;
                        case "Practitioner_MedicalRegnno":
                            currentRow[1] = "Prctitioner Medical Registration no";
                            break;
                        case "Appointment_Date":
                            currentRow[1] = "Appointment Date";
                            break;
                        case "Appointment_Time":
                            currentRow[1] = "Appointment Time";
                            break;
                        default:
                            break;
                    }
                }

                cboFieldNamesAppt.SelectedIndex = 0;
                cboFieldNamesAppt.SelectedValuePath = "COLUMN_NAME";
                cboFieldNamesAppt.DisplayMemberPath = "FRIENDLY_NAMES";
                cboFieldNamesAppt.ItemsSource = dtFieldNames.DefaultView;

            }
            catch (Exception)
            {
                // MessageBox.Show("An error occured loading the search field names. " + ex.Message);

            }
        }















        #endregion Private Appointment Methods

        #endregion Appointment Methods

        private void DPApptDate_CalendarOpened(object sender, RoutedEventArgs e)
        {
            //DateTime now = new DateTime(DateTime.Today.Year, DateTime.Today.Month,
            // MessageBox.Show(now.ToString());
            DPApptDate.SelectedDate = System.DateTime.Today;
            DPApptDate.DisplayDateStart = System.DateTime.Now;
            //MessageBox.Show(DPApptDate.DisplayDateStart.);
        }

        private void prac_date_CalendarOpened(object sender, RoutedEventArgs e)
        {
            prac_date.SelectedDate = System.DateTime.Today;
            prac_date.DisplayDateEnd = DateTime.Now;
        }

        private void patient_date_CalendarOpened(object sender, RoutedEventArgs e)
        {
            patient_date.SelectedDate = System.DateTime.Today;
            patient_date.DisplayDateEnd = System.DateTime.Now;
        }
    }
    
}

 
