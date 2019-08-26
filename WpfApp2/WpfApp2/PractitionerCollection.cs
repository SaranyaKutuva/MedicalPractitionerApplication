using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace WpfApp2
{
    class PractitionerCollection : List <Practitioner>
    {
        public PractitionerCollection(string fieldName, string searchString)
        {
            SqlHelper dal = new SqlHelper();
            SqlParameter[] parameters =
            {
                new SqlParameter("@FieldName",fieldName),
                new SqlParameter("@SearchString",searchString)
            };
            string SPName = "usp_GetAllPractitioners";
            DataTable dtPractitioners = dal.ExecuteStoredProc(SPName, parameters);
            //System.Windows.MessageBox.Show(dtPractitioners.Rows.Count.ToString());
            //Iterate through each pilot 'row' of the DataTable of Patients...
            foreach (DataRow drPractitioner in dtPractitioners.Rows)
            {
               // System.Windows.MessageBox.Show(dtPractitioners.Rows.Count.ToString() +" 1 ");
                //Create a new instance of the current Patient row
                Practitioner newPractitioner = new Practitioner(drPractitioner);
                //System.Windows.MessageBox.Show(dtPractitioners.Rows.Count.ToString() +" 2 ");
                // Patient newPatient = new Patient(drPatient);
                //Add the Patient object to this Collections' internal list
                this.Add(newPractitioner);
                //System.Windows.MessageBox.Show(dtPractitioners.Rows.Count.ToString() + " 3 ");

            }
        }
    }
}
