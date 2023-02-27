using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Healthcare_rp_.Pages.Patients
{
    public class IndexModel : PageModel
    {
        public List<PatientInfo> patientList = new List<PatientInfo>();

        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=Adminhealthcare;Integrated Security=True";
                  using (SqlConnection connection = new SqlConnection(connectionString)) 
                {

                    connection.Open();
                    string sql = "SELECT * FROM  PatientDetails";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                          using(SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                PatientInfo patientInfo = new PatientInfo();
								patientInfo.Patientid = "" + reader.GetInt32(0);
								patientInfo.FirstName = reader.GetString(1);
                                patientInfo.LastName = reader.GetString(2);
                                patientInfo.Address = reader.GetString(3);
                                 patientInfo.City = reader.GetString(4);
                                patientInfo.State = reader.GetString(5);
                                patientInfo.Country = reader.GetString(6);  
                                patientInfo.PinCode= reader.GetString(7);

                               patientList.Add(patientInfo);
                            }
                        }
                    }

				}
            }
            catch (Exception ex){
                Console.WriteLine("Exception: " + ex.ToString());

            }
        }
       
    }
    public class PatientInfo
    {
        public string Patientid;
        public string FirstName;

        public string LastName;
        public string Address;
        public string City;
        public string State;
        public string Country;
        public string PinCode;
    }
}
