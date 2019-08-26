using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Add the following references
using System.Data.SqlClient;

namespace WpfApp2
{
    class Appointment : MainWindow
    {
        #region Private Field Variables
        private int appointment_Id;
        private int patient_IdRef;
        private int newpatient_IdRef;
        private string practitioner_MedRegnoRef;
        private DateTime appointmentDate ;
        private string apptTime;
        private DataTable dtAppointment;
        private int count = 0;
        #endregion Private Field Variables

        #region Public Properties
        public int Count { get; set; }
        public int Appointment_Id { get; set; }
        public int Patient_IdRef { get; set; }

        public string Practitioner_MedRegnoRef { get; set; }
        public DateTime AppointmentDate { get; set; }

        public string ApptTime { get; set; }

        #endregion Public Properties

        #region Constructors
        public Appointment()
        {

        }

        public Appointment(DataRow drAppointment)
        {
            MapAppointmentProperties(drAppointment);
        }
        #endregion Constructors

        public Appointment(int patient_Id) : base()
        {
            //Get the dal
            SqlHelper dal = new SqlHelper();

            //Set up SQL
            string sql = "SELECT * FROM Appointment WHERE Patient_Id = @patient_Id";

            //Set up parameters
            SqlParameter[] parameters = { new SqlParameter("@patient_Id", patient_Id) };

            //Get Pilot's details from database
            this.dtAppointment = dal.ExecuteSql(sql, parameters);

            //Populate this class's properties with the data returned from the database
            LoadAppointmentProperties();
        }

        private void LoadAppointmentProperties()
        {
            //Check if the data table actually has any rows...
            if (this.dtAppointment != null && this.dtAppointment.Rows.Count > 0)
            {
                //Get the first (and only) row of the DataTable ...
                DataRow pilotRow = this.dtAppointment.Rows[0];

                MapAppointmentProperties(pilotRow);
            }
        }

        private void MapAppointmentProperties(DataRow AppointmentRow)
        {
            //Assign Appointment's details to properties
            this.Appointment_Id = (int)AppointmentRow["appointment_Id"];
            this.Patient_IdRef = (int) AppointmentRow["patient_Id"];
            this.Practitioner_MedRegnoRef = AppointmentRow["practitioner_MedicalRegnno"].ToString();
            this.AppointmentDate = (DateTime)AppointmentRow["appointment_Date"];
            this.ApptTime = AppointmentRow["appointment_Time"].ToString();
    
           
        }
        #region Public Methods

        public int DeleteAppointment()
        {
            try
            {
                //Create the SqlHelper object...
                SqlHelper dal = new SqlHelper();

                //Assign parameter from the Pilot's properties to the SqlHelper..
                string SPName = "usp_DeleteAppointment";
                //Set up the parameter
                SqlParameter[] parameters = {
                    new SqlParameter("@patient_Id", this.Patient_IdRef),
                    new SqlParameter("@practitioner_MedicalRegNo", this.Practitioner_MedRegnoRef),
                    new SqlParameter("@appointment_Date", this.AppointmentDate),
                    new SqlParameter("@appointment_Time", this.ApptTime)};

                int rowsAffected = dal.NonQueryStoredProc(SPName, parameters);

                if (rowsAffected == 0)
                {
                    throw new Exception("Appointment with Patient Id '" + this.Patient_IdRef + "' could not be deleted from the database.");
                }

                return rowsAffected;

            }
            catch (Exception ex)
            {
                throw new Exception("Appointment could  not be deleted. ", ex);
            }
        }
        public int validateAppointment()
        {
            try
            {
                SqlHelper dal = new SqlHelper();

                //Set up SQL
                string sql = "SELECT * FROM  Practitioner WHERE Practitioner_MedicalRegnno = @pratitioner_Ref and (" +
                    "(Monday =1 and @appointmentDay=1) or " +
                    "(Tuesday =1 and @appointmentDay=2) or " +
                    "(Wednesday =1 and @appointmentDay=3) or " +
                    "(Thursday =1 and @appointmentDay=4) or " +
                    "(Friday =1 and @appointmentDay=5) or " +
                    "(Saturday =1 and @appointmentDay=6) or " +
                    "(Sunday =1 and @appointmentDay=0)  " + ")";

                //Set up parameters
                SqlParameter[] parameters = { new SqlParameter("@pratitioner_Ref", this.Practitioner_MedRegnoRef),
                          new SqlParameter("@appointmentDay", this.AppointmentDate.DayOfWeek.ToString("d"))
            };
              

                //  System.Windows.MessageBox.Show("Karthik 2 " + this.AppointmentDate.DayOfWeek.ToString("d"));
                //SqlParameter[] parameters = { new SqlParameter("@appointmentDate", patient_Id) };

                //Get Pilot's details from database
                this.dtAppointment = dal.ExecuteSql(sql, parameters);

                // System.Windows.MessageBox.Show("Karthik 3 " + this.dtAppointment.Rows.Count);

                return this.dtAppointment.Rows.Count;
            }
            catch
            {

                return 0;
            }

        }
        public string validateAppointmentRequiredFields()
        {
            /*System.Windows.MessageBox.Show(
             " patient id " +this.Patient_IdRef + ' ' +
              "  medno " + this.Practitioner_MedRegnoRef + ' ' +
               "  AppointmentDate " + this.AppointmentDate + ' ' +
               " time " + this.ApptTime + ' ' +
              "  ID " + this.Appointment_Id + ' ');*/

            if (this.Patient_IdRef ==0) { return "Validation Error: Patient can't be empty"; }

            if (this.Practitioner_MedRegnoRef.Equals("0")) { return "Validation Error:  Pratitioner can't be empty"; }
            if (this.ApptTime.Contains("Select")) { return "Validation Error: Time  can't be empty"; }


           if( this.AppointmentDate == DateTime.ParseExact("31/12/4712", "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
            {
                return "Validation Error: Date can't be empty";
            }

            return "Success";
        }



        public int UpdateAppointment()
        {
            int validateResult = -1;
            string validateMsg = "";
            try
            {
               // System.Windows.MessageBox.Show(validateAppointmentRequiredFields());
                validateMsg = validateAppointmentRequiredFields();

                if (validateMsg.Contains("Error")){
                    throw new Exception( validateMsg);
                }
                if (this.Appointment_Id.ToString().Equals(null)) { throw new Exception("Validation Error: Choose existing record for update!"); }



                validateResult = validateAppointment();

                if (validateResult == 0)
                {
                    throw new Exception("Appoitnment is not booked as Doctor is not availiable on the choosen date!");
                }
                SqlHelper dal = new SqlHelper();

                string SPName = "usp_UpdateAppointment";

                //Set up the parameters
                SqlParameter[] parameters =
                {
                    new SqlParameter("@patient_Id", this.Patient_IdRef),                  
                    new SqlParameter("@practitioner_MedRegNo", this.Practitioner_MedRegnoRef),
                    new SqlParameter("@appointment_Date", this.AppointmentDate),
                    new SqlParameter("@appointment_Time", this.ApptTime),
                      new SqlParameter("@appointment_Id", this.Appointment_Id),
                };

                int rowsAffected = dal.NonQueryStoredProc(SPName, parameters);

                //Check if the Pilot was actually updated in the database (1 row should be affected?).
                if (rowsAffected == 0)
                {
                    throw new Exception("Patient with Id '" + this.Patient_IdRef + "' could not be updated in the database! ");
                }

                return rowsAffected;
            }
            catch (Exception ex)
            {
                if (validateMsg.Contains("Error"))
                {
                    throw new Exception( validateMsg);

                }
                if (validateResult == 0)
                {
                    throw new Exception("Validation Error: Appoitnment is not booked as Doctor is not availiable on the choosen date!");
                }
                //Throw a new exception with a custom message (with an inner exception)
                throw new Exception("Patient Appointment could not be updated!", ex);
            }
        }

       

        public int AddAppointment()
        {
            int validateResult =-1;
            string validateMsg = "";
            try
            {

                validateMsg = validateAppointmentRequiredFields();

                if (validateMsg.Contains("Error"))
                {
                    throw new Exception(validateMsg);

                }
                //System.Windows.MessageBox.Show("Karthik " + this.AppointmentDate.DayOfWeek.ToString("d"));

                validateResult = validateAppointment();

                if (validateResult == 0)
                {
                    throw new Exception("Appoitnment is not booked as Doctor is not availiable on the choosen date!");
                }
                SqlHelper dal = new SqlHelper();

                string SPName = "usp_InsertNewAppointment";

                //Set up the parameters
                SqlParameter[] parameters =
                {
                  // new SqlParameter("@Appointment_Id",this.Appointment_Id),
                    new SqlParameter("@patient_Id", this.Patient_IdRef),
                    new SqlParameter("@practitioner_MedicalRegNo", this.Practitioner_MedRegnoRef),
                    new SqlParameter("@appointment_Date", this.AppointmentDate),
                    new SqlParameter("@appointment_Time", this.ApptTime)
                };

                
                  /* SqlParameter[] parameters =
                  {

                      new SqlParameter("@patient_Id", 1000),
                      new SqlParameter("@practitioner_MedicalRegNo", "365243546352435"),
                      new SqlParameter("@appointment_Date", "01/01/2018"),
                      new SqlParameter("@appointment_Time", "09:30")
                  };*/


                 int rowsAffected = dal.NonQueryStoredProc(SPName, parameters);
               // int rowsAffected = 1;

                //Check if the Pilot was actually updated in the database (1 row should be affected?).
                if (rowsAffected == 0)
                {
                    throw new Exception("Appointment with Patient_Id '" + this.Patient_IdRef + "' could not be Added in the database! ");
                }

               


                return rowsAffected;
            }
            catch (Exception ex)
            {
                if (validateMsg.Contains("Error"))
                {
                    throw new Exception(validateMsg);

                }
                if (validateResult == 0)
                    {
                        throw new Exception("Validation Error: Appoitnment is not booked as Doctor is not availiable on the choosen date!");
                    }
                  throw new Exception("Appointment could not be added! ", ex);
            }
        }



        #endregion Public Methods

        #region Private Methods

        
        private void AssignPropertiesToAppt(Appointment appt)
        {
            this.Appointment_Id = int.Parse(txtApptId.Text);
            this.Patient_IdRef = (int)cboPatientRef.SelectedValue;
            this.Practitioner_MedRegnoRef = cboPractitionerRef.SelectedValue.ToString();
            this.AppointmentDate= DPApptDate.SelectedDate.Value;
            this.ApptTime = cboApptTime.SelectedValue.ToString();
          //  System.Windows.MessageBox.Show(this.Appointment_Id.ToString());
           

        }
        
            
            
            
            
            

        

        #endregion Private Methods



    }
}
