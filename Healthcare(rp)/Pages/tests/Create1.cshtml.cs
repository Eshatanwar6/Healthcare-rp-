using Healthcare_rp_.Pages.Patients;
using Healthcare_rp_.tests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Healthcare_rp_.Pages.tests
{
    public class Create1Model : PageModel
    {
		 public TestInfo Testinfo = new TestInfo();
	
		public string errorMessage = "";
		public string successMessage = "";
		public void OnGet()
        {

        }
		public void OnPost() {
		
			Testinfo.TestName = Request.Form["TestName"];
			

			if (Testinfo.TestName.Length == 0 )
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
					String sql = "INSERT INTO TestsDetails" +
						" VALUES (@TestName);";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{


						command.Parameters.AddWithValue("TestName", Testinfo.TestName);
						
						command.ExecuteNonQuery();





					}

				}
			}
			catch (Exception ex)
			{
				errorMessage = ex.Message;
				return;
			}
			Testinfo.TestName = ""; 
			successMessage = "New Test Added Successfully";
			Response.Redirect("/tests/Index1");

		}
	}
}

