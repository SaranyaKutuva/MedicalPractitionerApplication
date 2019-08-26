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
    class Practitioner : Person
    {
        #region Private Field Variables
        //Properties that are NOT provided for by the Person Class
        private string prac_Id;
       // private string oldRegNumber;
        private string newRegNumber;
        private string medRegNo;
        private string desc;
        private bool monday;
        private bool tuesday;
        private bool wednesday;
        private bool thursday;
        private bool friday;
        private bool saturday;
        private bool sunday;
        private DataTable dtPractitioner;
        #endregion Private Field Variables
     
        #region Public Properties
        public string Prac_Id { get { return prac_Id; } set { prac_Id = value; } }
        public string Desc { get { return desc; }  set { desc = value; } }
        public string MedRegNo { get; set; }

        public bool Monday { get; set; }

        public bool Tuesday { get; set; }

        public bool Wednesday { get; set; }

        public bool Thursday { get; set; }

        public bool Friday { get; set; }

        public bool Saturday { get; set; }

        public bool Sunday { get; set; }

        #endregion Public Properties

        #region Public Constructors
        public Practitioner() : base()
        {
            // Does nothing... except for calling its parents construcotor.

        }
        public Practitioner(string medRegNo) : base()
        {
            //Get the dal
            SqlHelper dal = new SqlHelper();

            //Set up SQL

            string sql = "Select * from Practitioner  WHERE Practitioner_MedicalRegnno = @medRegNo";

            //Set up the Parameters
            SqlParameter[] parameters = { new SqlParameter("@medRegNo", medRegNo) };

            // Get Practitioner's details from database

            this.dtPractitioner = dal.ExecuteSql(sql, parameters);

            // Populate this class's properties with the data returned from the database
            LoadPractitionerProperties();


        }



        public Practitioner(DataRow drPractitioner) : base()
        {
            MapPilotProperties(drPractitioner);
        }

      
        #endregion Public Constructors

        #region Private Methods
        private void LoadPractitionerProperties()
        {
            //Check if the data table actually has any rows.....
            if (this.dtPractitioner != null && this.dtPractitioner.Rows.Count > 0)
            {
                //Get the first (and only) row of the DataTable ...
                DataRow practitionerRow = this.dtPractitioner.Rows[0];

                MapPilotProperties(practitionerRow);
            }

        }
        private void MapPilotProperties(DataRow practitionerRow)
        {
            //Assign practitoner's details to properties
            this.Prac_Id = practitionerRow["Practitioner_Id"].ToString();
            this.MedRegNo = practitionerRow["Practitioner_MedicalRegnno"].ToString();
                 
            this.FirstName = practitionerRow["Practitioner_Fname"].ToString();
            this.LastName = practitionerRow["Practitioner_Lname"].ToString();
            this.Desc = practitionerRow["Practitioner_Desc"].ToString();
            this.Gender = practitionerRow["Practitioner_Gender"].ToString();
            this.DateOfBirth = (DateTime)practitionerRow["Practitioner_Dateofbirth"];
            this.StreetAddress = practitionerRow["Practitioner_Street_address"].ToString();
            this.Suburb = practitionerRow["Practitioner_Suburb"].ToString();
            this.State = practitionerRow["Practitioner_State"].ToString();
            this.PostCode = practitionerRow["Practitioner_Postcode"].ToString();
            this.Homephone = practitionerRow["Practitioner_HomePhone"].ToString();
            this.Mobilephone = practitionerRow["Practitioner_MobilePhone"].ToString();
            this.Monday = practitionerRow["Monday"].ToString() == "0"? false : true;
            this.Tuesday = practitionerRow["Tuesday"].ToString() == "0" ? false : true;
            this.Wednesday = practitionerRow["Wednesday"].ToString() == "0" ? false : true;
            this.Thursday = practitionerRow["Thursday"].ToString() == "0" ? false : true;
            this.Friday = practitionerRow["Friday"].ToString() == "0" ? false : true;
            this.Saturday= practitionerRow["Saturday"].ToString() == "0" ? false : true;
            this.Sunday = practitionerRow["Sunday"].ToString() == "0" ? false : true;

        }
        #endregion Private Methods

        #region Public Methods
        public int DeletePractitioner()
        {
            //MessageBox.show("@MedRegNo");
            try
            {
                //Create the SqlHelper object...
                SqlHelper dal = new SqlHelper();

                //Assign parameter from the Practitioner's properties to the SqlHelper..
                string SPName = "usp_DeletePractitioner";
                //Set up the parameter

                SqlParameter[] parameters = { new SqlParameter("@MedRegNo", this.MedRegNo)};
               // int rowsAffected = 1;

               int rowsAffected = dal.NonQueryStoredProc(SPName, parameters);
               

                if (rowsAffected == 0)
                {
                    throw new Exception("Practitioner with Registration '" + this.MedRegNo+ "' could not be deleted from the database.They have Appointments");
                }

                return rowsAffected;

            }
            catch (Exception ex)
            {
                throw new Exception("Practititoner could not be deleted. ", ex);
            }
        }

        public int UpdatePractitioner()
        {
            try
            {
                SqlHelper dal = new SqlHelper();

                string SPName = "usp_UpdatePractitioner";

                String mon, tue, wed, thu, fri, sat, sun;
                mon = "0"; tue = "0"; wed = "0"; thu = "0"; fri = "0"; sat = "0"; sun = "0";
                if (this.Monday) { mon = "1"; }
                if (this.Tuesday) { tue = "1"; }
                if (this.Wednesday) { wed = "1"; }
                if (this.Thursday) { thu = "1"; }
                if (this.Friday) { fri = "1"; }
                if (this.Saturday) { sat = "1"; }
                if (this.Sunday) { sun = "1"; }

                //Set up the parameters
                SqlParameter[] parameters =
                {
                        //new SqlParameter("@Practitioner_Id",this.Prac_Id),
                        new SqlParameter("@Practitioner_MedicalRegNo",this.MedRegNo),               
                        new SqlParameter("@Practitioner_NewMedicalRegNo",this.MedRegNo),
                        new SqlParameter("@Practitioner_Fname",this.FirstName),
                        new SqlParameter("@Practitioner_Sname",this.LastName),
                        new SqlParameter("@Practitioner_desc",this.Desc),
                        new SqlParameter("@Practitioner_Gender",this.Gender),
                        new SqlParameter("@Practitioner_Dateofbirth",this.DateOfBirth),
                        new SqlParameter("@Practitioner_Street_address",this.StreetAddress),
                        new SqlParameter("@Practitioner_Suburb",this.Suburb),
                        new SqlParameter("@Practitioner_State",this.State),
                        new SqlParameter("@Practitioner_Postcode",this.PostCode),
                        new SqlParameter("@Practitioner_HomePhone",this.Homephone),
                        new SqlParameter("@Practitioner_MobilePhone",this.Mobilephone),
                        new SqlParameter("@monday",mon),
                        new SqlParameter("@tuesday",tue),
                        new SqlParameter("@wednesday",wed),
                        new SqlParameter("@thursday",thu),
                        new SqlParameter("@friday",fri),
                        new SqlParameter("@saturday",sat),
                        new SqlParameter("@sunday",sun),
                };

                int rowsAffected = dal.NonQueryStoredProc(SPName, parameters);

                //Check if the Pilot was actually updated in the database (1 row should be affected?).
                if (rowsAffected == 0)
                {
                    throw new Exception("Practitioner with Registration No '" + this.MedRegNo + "' could not be updated in the database! ");
                }

                return rowsAffected;
            }
            catch (Exception ex)
            {
                //Throw a new exception with a custom message (with an inner exception)
                throw new Exception("Practitioner could not be updated!", ex);
            }
        }

        public int AddPractitioner()
        {
            
            try
            {
                
                SqlHelper dal = new SqlHelper();

                string SPName = "usp_InsertNewPractitioner";
                String mon, tue, wed, thu, fri, sat, sun;
                mon = "0"; tue = "0"; wed = "0"; thu = "0"; fri = "0"; sat = "0"; sun = "0";
                if (this.monday ) { mon = "1"; }
                if (this.tuesday) { tue = "1"; }
                if (this.wednesday) { wed = "1"; }
                if (this.thursday) { thu = "1"; }
                if (this.friday) { fri = "1"; }
                if (this.saturday) { sat = "1"; }
                if (this.sunday) { sun = "1"; }


                //Set up the parameters
                SqlParameter[] parameters =
                {
                     
                       // new SqlParameter("@Practitioner_Id",this.Prac_Id),
                        new SqlParameter("@Practitioner_MedicalRegNo",this.MedRegNo),
                        new SqlParameter("@Practitioner_Fname",this.FirstName),
                        new SqlParameter("@Practitioner_Sname",this.LastName),
                        new SqlParameter("@Practitioner_desc",this.Desc),
                        new SqlParameter("@Practitioner_Gender",this.Gender),
                        new SqlParameter("@Practitioner_Dateofbirth",this.DateOfBirth),
                        new SqlParameter("@Practitioner_Street_address",this.StreetAddress),
                        new SqlParameter("@Practitioner_Suburb",this.Suburb),
                        new SqlParameter("@Practitioner_State",this.State),
                        new SqlParameter("@Practitioner_Postcode",this.PostCode),
                        new SqlParameter("@Practitioner_HomePhone",this.Homephone),
                        new SqlParameter("@Practitioner_MobilePhone",this.Mobilephone),
                        new SqlParameter("@monday",mon),
                        new SqlParameter("@tuesday",tue),
                        new SqlParameter("@wednesday",wed),
                        new SqlParameter("@thursday",thu),
                        new SqlParameter("@friday",fri),
                        new SqlParameter("@saturday",sat),
                        new SqlParameter("@sunday",sun)
                      /*  new SqlParameter("@monday",this.Monday),
                        new SqlParameter("@tuesday",this.Tuesday),
                        new SqlParameter("@wednesday",this.Wednesday),
                        new SqlParameter("@thursday",this.Thursday),
                        new SqlParameter("@friday",this.Friday),
                        new SqlParameter("@saturday",this.Saturday),
                        new SqlParameter("@sunday",this.Sunday)*/
                };

                int rowsAffected = dal.NonQueryStoredProc(SPName, parameters);

                

                //Check if the Pilot was actually updated in the database (1 row should be affected?).
                if (rowsAffected == 0)
                {
                    throw new Exception("Practitioner with Registration no '" + this.MedRegNo + "' could not be Added in the database! ");
                }

                return rowsAffected;
            }
            catch (Exception ex)
            {

                throw new Exception("Practitioner with Registration no could not be added! ", ex);
            }
        }




        #endregion Public Methods
    }
}
