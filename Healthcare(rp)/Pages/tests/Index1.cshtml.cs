using Healthcare_rp_.Pages.Patients;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Healthcare_rp_.tests
{
    public class IndexModel : PageModel
    {
		public List<TestInfo> TestList = new List<TestInfo>();
		public void OnGet()
        {
			try
			{
				String connectionString = "Data Source =.\\sqlexpress; Initial Catalog = Adminhealthcare; Integrated Security = True";
				using (SqlConnection connection = new SqlConnection(connectionString))
				{

					connection.Open();
					string sql = "SELECT * FROM  TestsDetails";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						using (SqlDataReader reader = command.ExecuteReader())
						{
							while (reader.Read())

							{
								TestInfo testinfo = new TestInfo();
								testinfo.TestId = "" + reader.GetInt32(0);
								testinfo.TestName = reader.GetString(1);

								TestList.Add(testinfo);
							}
						}
					}
				}
			}

			catch (Exception ex)
			{

				Console.WriteLine("Exception: " + ex.ToString());
			}

        }

        public void OnPost()
        {

        }
    }
    public class TestInfo
    {
        public string TestId;
        public string TestName;


    }
}
