using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace WpfApp2
{
    class AppointmentCollection : List<Appointment>
    {
        public AppointmentCollection(string fieldName, string searchString)
        {
            SqlHelper dal = new SqlHelper();
            SqlParameter[] parameters =
            {
                new SqlParameter("@FieldName",fieldName),
                new SqlParameter("@SearchString",searchString)
            };
            string SPName = "usp_GetAllAppointments";
            DataTable dtAppointments = dal.ExecuteStoredProc(SPName, parameters);
           
            //Iterate through each pilot 'row' of the DataTable of Patients...
            foreach (DataRow drAppointment in dtAppointments.Rows)
            {
               
                //Create a new instance of the current Patient row
                Appointment newAppointment = new Appointment(drAppointment);
                
                this.Add(newAppointment);
                //System.Windows.MessageBox.Show(dtPractitioners.Rows.Count.ToString() + " 3 ");

            }
        }
    }
}
