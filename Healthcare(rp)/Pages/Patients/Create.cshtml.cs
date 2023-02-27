using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Net.Security;

namespace Healthcare_rp_.Pages.Patients
{
    public class CreateModel : PageModel
    {
        public PatientInfo patientInfo = new PatientInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {

        }
        public void OnPost()
        {
            patientInfo.FirstName = Request.Form["FirstName"];
			patientInfo.LastName = Request.Form["Lastname"];
			patientInfo.Address = Request.Form["Address"];
			patientInfo.City = Request.Form["City"];
            patientInfo.State = Request.Form["State"];
            patientInfo.Country = Request.Form["Country"];
            patientInfo.PinCode = Request.Form["PinCode"];

            if(patientInfo.FirstName.Length == 0 || patientInfo.LastName.Length == 0 || patientInfo.Address.Length == 0|| patientInfo.City.Length == 0 ||
                patientInfo.State.Length == 0 || patientInfo.Country.Length == 0 || patientInfo.PinCode.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;

            }
            //save the client to the database
            try
            {

                string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=Adminhealthcare;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO PatientDetails" +
                        "(FirstName, LastName , Address, City,State,Country,Pincode) VALUES  (@FirstName,@LastName,@Address,@City,@State,@Country,@Pincode)";
                    using(SqlCommand command = new SqlCommand(sql,connection)) {


                        command.Parameters.AddWithValue("FirstName", patientInfo.FirstName);
                        command.Parameters.AddWithValue("LastName",patientInfo.LastName);
                        command.Parameters.AddWithValue("Address", patientInfo.Address);
                        command.Parameters.AddWithValue("City", patientInfo.City);
                        command.Parameters.AddWithValue("State", patientInfo.State);
                        command.Parameters.AddWithValue("Country", patientInfo.Country);
                        command.Parameters.AddWithValue("Pincode", patientInfo.PinCode);
                        command.ExecuteNonQuery();
                    
                    
                    
                    
                    
                    }

                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            patientInfo.FirstName = ""; patientInfo.LastName = ""; patientInfo.Address = ""; patientInfo.State = ""; patientInfo.Country = "";
            patientInfo.PinCode = "";
            successMessage = "New Patient Added Successfully";
            Response.Redirect("/Patients/Index");

		}
    }
}

