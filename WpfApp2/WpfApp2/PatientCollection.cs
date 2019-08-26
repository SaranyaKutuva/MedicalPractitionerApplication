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
    class PatientCollection : List <Patient>
    {

        public PatientCollection(string fieldName, string searchString)
        {
            
            SqlHelper dal = new SqlHelper();
            SqlParameter[] parameters =
            {
                new SqlParameter("@FieldName",fieldName),
                new SqlParameter("@SearchString",searchString)
            };
            string SPName = "usp_GetAllPatients";
            DataTable dtPatients = dal.ExecuteStoredProc(SPName, parameters);
            
            //Iterate through each pilot 'row' of the DataTable of Patients...
            foreach (DataRow drPatient in dtPatients.Rows)
            {
                //Create a new instance of the current Patient row
                Patient newPatient = new Patient(drPatient);
                //Add the Patient object to this Collections' internal list
                this.Add(newPatient);
              //  System.Windows.MessageBox.Show(newPatient + "");
            }
        }
    }
}
