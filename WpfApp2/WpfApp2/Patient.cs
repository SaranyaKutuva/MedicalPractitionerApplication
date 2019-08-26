using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Add the following references
using System.Data;
using System.Data.SqlClient;


namespace WpfApp2 
{
    class Patient : Person
    {
        private string patient_Id;
        private string newpatient_Id;
        private string medicare_No;
        private string notes1;
        private DataTable dtPatient;
        public Patient(DataRow drPatient) : base()
        {
            MapPatientProperties(drPatient);
        }

        public Patient() :base()
        {

        }
        public Patient(string patient_Id) : base()
        {
            //Get the dal
            SqlHelper dal = new SqlHelper();

            //Set up SQL
            string sql = "SELECT * FROM Patient WHERE Patient_Id = @patient_Id";

            //Set up parameters
            SqlParameter[] parameters = { new SqlParameter("@Patient_Id", patient_Id) };


            // Get Pilot's details from database
            this.dtPatient = dal.ExecuteSql(sql, parameters);

            //Populate this class's properties with the data returned from the database
            LoadPatientProperties();

        }


        private void LoadPatientProperties()
        {            //// Check if the data table actually has any rows.....
        if (this.dtPatient != null && this.dtPatient.Rows.Count > 0)
        {
            // Get the first( and only ) row of the Datatable....
            DataRow patientRow = this.dtPatient.Rows[0];
            MapPatientProperties(patientRow);
        }
       }
    private void MapPatientProperties(DataRow patientRow)
    {
            //// Check if the data table actually has any rows.....
            //if(this.dtpilot != null && this.dtpilot.Rows.Count > 0)
            //{
            ////Get the first(and only) row of the data table - just to make your job easier......
            //DataRow pilotRow = this.dtpilot.Rows[0];
            //Assign pilot's details to properties
            this.Patient_Id = (int)patientRow["Patient_Id"];
            this.FirstName = patientRow["Patient_Fname"].ToString();
            this.LastName = patientRow["Patient_Lname"].ToString();
            this.Medicare_No = patientRow["Patient_MedicareNo"].ToString();
            this.Gender = patientRow["Patient_Gender"].ToString();
            this.DateOfBirth = (DateTime)patientRow["Patient_DateOfBirth"];
            this.StreetAddress = patientRow["Patient_Street_Address"].ToString();
            this.Suburb = patientRow["Patient_Suburb"].ToString();
            this.State = patientRow["Patient_State"].ToString();
            this.PostCode = patientRow["Patient_PostCode"].ToString();
            this.Homephone = patientRow["Patient_HomePhone"].ToString();
            this.Mobilephone = patientRow["Patient_MobilePhone"].ToString();
            this.Notes = patientRow["Patient_Notes"].ToString();

      

    }
        #region Public Properties
        public string Newpatient_Id { get; set; }

        public string Medicare_No { get; set; }

        //public string GetPatient_Id()
        //{
        //    return this.patient_Id;
        //}

        //public void SetPatient_Id(string value)
        //{
        //    this.patient_Id = value;
        //}
        public int Patient_Id { get; set; }

        public string Notes { get; set; }
        #endregion Public Properties
        public int AddPatient()
        {
           
            try
            {
                
               
                SqlHelper dal = new SqlHelper();
                string SPName = "usp_InsertNewPatient";
                
                Patient patient = new Patient();
                //Set up the parameters
                SqlParameter[] parameters =
                {
                   // new SqlParameter("@OldPatient_Id",this.Patient_Id),
                    new SqlParameter("@Patient_Fname",this.FirstName),
                    new SqlParameter("@Patient_Sname",this.LastName),
                    new SqlParameter("@Patient_MedicareNo",this.Medicare_No),
                    new SqlParameter("@Patient_Gender",this.Gender),
                    new SqlParameter("@Patient_DateOfBirth",this.DateOfBirth),
                    new SqlParameter("@Patient_Street_Address",this.StreetAddress),
                    new SqlParameter("@Patient_Suburb",this.Suburb),
                    new SqlParameter("@Patient_State",this.State),
                    new SqlParameter("@Patient_PostCode",this.PostCode),
                    new SqlParameter("@Patient_HomePhone",this.Homephone),
                    new SqlParameter("@Patient_MobilePhone",this.Mobilephone),
                    new SqlParameter("@Patient_Notes",this.Notes)

                };
                int rowsAffected = dal.NonQueryStoredProc(SPName, parameters);

                //int rowsAffected = 0;
                //Check if the Pilot was acutally updated in the database (1 row should be affected?).
                if (rowsAffected == 0)
                {
                    throw new Exception("Patient with Patient '" + this.FirstName + "' could not be Added in the database! ");
                }
                return rowsAffected;
            }
            catch (Exception ex)
            {
                // Throw a new exception with a custom message(with an inner exception)
                throw new Exception("Patient could not be Added!", ex);
            }
        }
        public int UpdatePatient()
    {
        try
        {
            SqlHelper dal = new SqlHelper();
            string SPName = "usp_UpdatePatient";
          Patient patient = new Patient();

             
            //Set up the parameters
            SqlParameter[] parameters =
            {
                    new SqlParameter("@OldPatient_Id",this.Patient_Id),
                    //new SqlParameter("@Patient_Id",this.Newpatient_Id),                   
                    new SqlParameter("@Patient_Fname",this.FirstName),
                    new SqlParameter("@Patient_Sname",this.LastName),
                     new SqlParameter("@Patient_MedicareNo",this.Medicare_No),
                    new SqlParameter("@Patient_Gender",this.Gender),
                    new SqlParameter("@Patient_DateOfBirth",this.DateOfBirth),
                    new SqlParameter("@Patient_Street_Address",this.StreetAddress),
                    new SqlParameter("@Patient_Suburb",this.Suburb),
                    new SqlParameter("@Patient_State",this.State),
                    new SqlParameter("@Patient_PostCode",this.PostCode),
                    new SqlParameter("@Patient_HomePhone",this.Homephone),
                    new SqlParameter("@Patient_MobilePhone",this.Mobilephone),
                    new SqlParameter("@Patient_Notes",this.Notes)
                                     
                };
                 int rowsAffected = dal.NonQueryStoredProc(SPName, parameters);
                //int rowsAffected =1;
            //Check if the Pilot was acutally updated in the database (1 row should be affected?).
            if (rowsAffected == 0)
            {
                throw new Exception("Patient with patient_Id '" + this.Patient_Id + "' could not be updated in the database! ");
            }
            return rowsAffected;
        }
        catch (Exception ex)
        {
            // Throw a new exception with a custom message(with an inner exception)
            throw new Exception("Patient could not be updated!", ex);
        }
    }
    public int DeletePatient()
    {// TRl+MM To collapse the region shortcut method
        try
        {
            //Create the SqlHelper object
            SqlHelper dal = new SqlHelper();
            // Assign parameter from the Pilot's properties to the SqlHelper...
            string SPName = "usp_DeletePatient";
         //   Patient patient = new Patient();
            //Set up the parameter
            SqlParameter[] parameters = {new SqlParameter("@PatientId", this.Patient_Id)};
              //  int rowsAffected = 1;
            int rowsAffected = dal.NonQueryStoredProc(SPName, parameters);
            if (rowsAffected == 0)
            {
                throw new Exception("Patient with patient_Id '" + this.Patient_Id + "' could not be deleted from the database.");
            }
            return rowsAffected;

        }
        catch (Exception ex)
        {

            throw new Exception("Patient could not be deleted", ex);
        }
    }

    }

}
