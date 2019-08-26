using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    public abstract class Person 
    {
        #region Constructor
        public Person()
        {
            //this.moduleName = this.GetType().ToString();
        }
    #endregion Constructor
        #region Private field variables
        private string firstName;
        private string lastName;
        private string gender;
        private DateTime dateOfbirth;
        private string streetAddress;
        private string suburb;
        private string state;
        private string postcode;
        private string homephone;
        private string mobilephone;
        private string moduleName;

        #endregion Private field variables


        // This is a 'manually' created property. We can also use "Auto-Implemented" properties.
        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                if (value != string.Empty)
                {
                    firstName = value;
                }
                else
                {
                    firstName = "No name given";
                }
            }
        }

        // This is an example of an "Auto-implemented" property. NOTE: Auto-     Implemented properties //assign the value of the property to a "HIDDEN" field variable which you (as the developer)can NOT manipulate (i.e change the value of) without going through the public exposed    auto-implemented property.
        //public int MyProperty { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth
        {
            get
            {
                try
                {
                    return dateOfbirth;
                }
                catch
                {
                    //Return a specific date which we can test for
                    return new DateTime(1900, 1, 1);
                }
            }
            set
            {
                try
                {
                    dateOfbirth = value;
                    this.dateOfbirth = value;
                }
                catch (Exception)
                {

                    dateOfbirth = new DateTime(1900, 1, 1);
                }
            }
        }
        public string Gender { get; set; }
        public string StreetAddress { get; set; }
        public string Suburb { get; set; }
        public string State { get; set; }
        public string PostCode { get; set; }
        public string Homephone { get; set; }

        public string Mobilephone { get; set; }
    }
}
