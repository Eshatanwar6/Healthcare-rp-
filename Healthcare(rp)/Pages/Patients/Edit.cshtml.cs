using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;

namespace Healthcare_rp_.Pages.Patients
{
	public class EditModel : PageModel
	{
		public PatientInfo patientInfo = new PatientInfo();
		public String errorMessage = "";
		public String successMessage = "";
		public void OnGet()
		{
			string Patientid = Request.Query["Patientid"];

			try
			{
				String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=Adminhealthcare;Integrated Security=True";

				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					String sql = "SELECT * FROM PatientDetails WHERE PatientId=@Patientid";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@Patientid", Patientid);
						using (SqlDataReader reader = command.ExecuteReader())
						{
							if (reader.Read())
							{
								patientInfo.Patientid = "" + reader.GetInt32(0);
								patientInfo.FirstName = reader.GetString(1);
								patientInfo.LastName = reader.GetString(2);
								patientInfo.Address = reader.GetString(3);
								patientInfo.City = reader.GetString(4);
								patientInfo.State = reader.GetString(5);
								patientInfo.Country = reader.GetString(6);
								patientInfo.PinCode = reader.GetString(7);
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
			patientInfo.Patientid = Request.Form["patientid"];
			patientInfo.FirstName = Request.Form["FirstName"];
			patientInfo.LastName = Request.Form["Lastname"];
			patientInfo.Address = Request.Form["Address"];
			patientInfo.City = Request.Form["City"];
			patientInfo.State = Request.Form["State"];
			patientInfo.Country = Request.Form["Country"];
			patientInfo.PinCode = Request.Form["PinCode"];

			if (patientInfo.Patientid.Length == 0||patientInfo.FirstName.Length == 0 || patientInfo.LastName.Length == 0 || patientInfo.Address.Length == 0 || patientInfo.City.Length == 0 ||
				patientInfo.State.Length == 0 || patientInfo.Country.Length == 0 || patientInfo.PinCode.Length == 0)
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
					String sql = "UPDATE PatientDetails" +
						"SET FirstName=@FirstName, LastName=@LastName, Address=@Address ,City=@City,State=@State,@Country=@Country,@Pincode=@Pincode" + "WHERE PatientId=@Patientid";

					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@FirstName", patientInfo.FirstName);
						command.Parameters.AddWithValue("@LastName", patientInfo.LastName);
						command.Parameters.AddWithValue("@Address",patientInfo.Address);
						command.Parameters.AddWithValue("@City", patientInfo.City);
						command.Parameters.AddWithValue("@State", patientInfo.State);
						command.Parameters.AddWithValue("@Country", patientInfo.Country);
						command.Parameters.AddWithValue("@Pincode", patientInfo.PinCode);
						command.ExecuteNonQuery();




					}
				}
			}


			catch (Exception ex)
			{



				errorMessage = ex.Message;
				return;
			}
			Response.Redirect("/Patients/Index");
		}
	}
	}


					

				
				
				
    

    