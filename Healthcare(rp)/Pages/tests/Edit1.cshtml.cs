using Healthcare_rp_.Pages.Patients;
using Healthcare_rp_.tests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Healthcare_rp_.Pages.tests
{
    public class Edit1Model : PageModel
    {
		public TestInfo Testinfo = new TestInfo();
		public String errorMessage = "";
		public String successMessage = "";

		public object TestId { get; private set; }

		public void OnGet()
        {
			string Patientid = Request.Query["TestId"];

			try
			{
				String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=Adminhealthcare;Integrated Security=True";

				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					String sql = "SELECT * FROM TestsDetails WHERE TestId=@TestId";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@TestId", TestId);
						using (SqlDataReader reader = command.ExecuteReader())
						{
							if (reader.Read())
							{
								Testinfo.TestId = "" + reader.GetInt32(0);
								Testinfo.TestName = reader.GetString(1);
							
							}
						}
					}
				}

			}
			catch (Exception ex)
			{


				errorMessage = ex.Message;
			}
		}
		public void Onpost()
		{
	           Testinfo.TestId = Request.Form["TestId"];
			    Testinfo.TestName = Request.Form["TestName"];
		

			if (Testinfo.TestId.Length == 0 || Testinfo.TestName.Length == 0 )
			{
				errorMessage = "All the fields are required";
				return;

			}
			try
			{
				String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=Adminhealthcare;Integrated Security=True";

				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					String sql = "UPDATE TestsDetails" +
						"SET TestName=@TestName)";

					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@TestName", Testinfo.TestName);
					
						command.ExecuteNonQuery();




					}
				}
			}


			catch (Exception ex)
			{



				errorMessage = ex.Message;
				return;
			}
			Response.Redirect("/tests/Edit1");
		}
	}
}

        
    
